using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Media.Playback;
using System.Diagnostics;
using Windows.Foundation.Collections;
using Windows.Media;
using OneVK.Helpers;

namespace OneVK.BackgroundPlayer
{
    /// <summary>
    /// Представляет задачу фонового проигрывателя.
    /// </summary>
    public sealed class AudioTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        private SystemMediaTransportControls _controls;
        private MediaPlayer _player;
        private PlaybackManager _manager;
        private AutoResetEvent _taskRunningEvent = new AutoResetEvent(false);
        private bool _isTaskRunning;

        /// <summary>
        /// Запускает задачу.
        /// </summary>
        /// <param name="taskInstance">Экземпляр фоновой задачи.</param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
#if DEBUG
            Debug.WriteLine("AudiosTask is starting...");
#endif
            _deferral = taskInstance.GetDeferral();
            _controls = SystemMediaTransportControls.GetForCurrentView();
            _player = BackgroundMediaPlayer.Current;
            _manager = new PlaybackManager(_player);

            _controls.IsEnabled = true;
            _controls.IsPlayEnabled = true;
            _controls.IsPauseEnabled = true;
            _controls.IsNextEnabled = true;
            _controls.IsPreviousEnabled = true;

            taskInstance.Canceled += TaskInstance_Canceled;
            _controls.ButtonPressed += Controls_ButtonPressed;
            _player.CurrentStateChanged += Player_CurrentStateChanged;
            _manager.TrackChanged += Manager_TrackChanged;
            BackgroundMediaPlayer.MessageReceivedFromForeground += OnMessageReceivedFromForeground;

            SettingsHelper.Set(AppConstants.PlayerTaskRunning, true);
            SendMessageToForeground(AppConstants.PlayerTaskRunning, true);

            _isTaskRunning = true;
            _taskRunningEvent.Set();            
#if DEBUG
            Debug.WriteLine("AudiosTask is successfully started.");
#endif
        }

        /// <summary>
        /// Вызывается при отмене фоновой задачи.
        /// </summary>
        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
#if DEBUG
            Debug.WriteLine("AudioTask cancel requested.");
#endif
            SendMessageToForeground(AppConstants.PlayerTaskRunning, false);
            SettingsHelper.Set(AppConstants.PlayerTaskRunning, false);
            _isTaskRunning = false;

            BackgroundMediaPlayer.MessageReceivedFromForeground -= OnMessageReceivedFromForeground;
            _player.CurrentStateChanged -= Player_CurrentStateChanged;
            _manager.TrackChanged -= Manager_TrackChanged;
            _controls.ButtonPressed -= Controls_ButtonPressed;
            _manager = null;
            BackgroundMediaPlayer.Shutdown();           

            _deferral.Complete();
        }
        
        /// <summary>
        /// Вызывается при нажатии кнопкок на панели управления аудио.
        /// </summary>
        private void Controls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            if (!_isTaskRunning)
            {
                _player = BackgroundMediaPlayer.Current;
                if (!_taskRunningEvent.WaitOne(AppConstants.AudioTaskStartingTimeout))
                {
#if DEBUG
                    Debug.WriteLine("AudioTask start timeout exceeded!");
#endif
                    return;
                }
            }
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    _manager.ResumePause();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    _manager.ResumePause();
                    break;
                case SystemMediaTransportControlsButton.Next:
                    _manager.NextTrack();
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    _manager.PreviousTrack();
                    break;
            }
        }

        /// <summary>
        /// Вызывается при изменении состояния проигрывателя.
        /// </summary>
        private void Player_CurrentStateChanged(MediaPlayer sender, object args)
        {            
            switch (_player.CurrentState)
            {
                case MediaPlayerState.Closed:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Closed;
                    break;
                case MediaPlayerState.Opening:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Changing;
                    break;
                case MediaPlayerState.Buffering:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Changing;
                    break;
                case MediaPlayerState.Playing:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Playing;
                    break;
                case MediaPlayerState.Paused:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Paused;
                    break;
                case MediaPlayerState.Stopped:
                    _controls.PlaybackStatus = MediaPlaybackStatus.Stopped;
                    break;
            }
        }

        /// <summary>
        /// Взывается при смене текущего трека в менеджере.
        /// </summary>
        private void Manager_TrackChanged(object sender, TrackChangedEventArgs e)
        {
            UpdateControlsOnNewTrack(e.Track);
            SendMessageToForeground(AppConstants.PlayerCurrentTrackID, e.TrackID);
        }

        /// <summary>
        /// Вызывается при получении сообщения от процесса переднего плана.
        /// </summary>
        private async void OnMessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            if (!_isTaskRunning)
            {
                _player = BackgroundMediaPlayer.Current;
                if (!_taskRunningEvent.WaitOne(AppConstants.AudioTaskStartingTimeout))
                {
#if DEBUG
                    Debug.WriteLine("AudioTask start timeout exceeded!");
#endif
                    return;
                }
            }
            foreach (string key in e.Data.Keys)
            {
                switch (key)
                {
                    case AppConstants.PlayerNewPlaylist:
                        await _manager.Update(true);
                        break;
                    case AppConstants.PlayerNextTrack:
                        _manager.NextTrack();
                        break;
                    case AppConstants.PlayerPreviousTrack:
                        _manager.PreviousTrack();
                        break;
                    case AppConstants.PlayerPauseResume:
                        _manager.ResumePause();
                        break;
                    case AppConstants.PlayerCurrentTrackID:
                        _manager.PlayTrackFromID((int)e.Data[key]);
                        break;
                    case AppConstants.PlayerShuffleMode:
                        await _manager.UpdateShuffleMode();
                        break;
                    case AppConstants.PlayerRepeatMode:
                        _manager.UpdateRepeatMode();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Обновить системные кнопки управления проигрывателем при старте нового трека.
        /// </summary>
        /// <param name="newTrack">Новый трек.</param>
        private void UpdateControlsOnNewTrack(AudioTrack newTrack)
        {
            _controls.DisplayUpdater.Type = MediaPlaybackType.Music;
            _controls.DisplayUpdater.MusicProperties.Title = newTrack.Title;
            _controls.DisplayUpdater.MusicProperties.Artist = newTrack.Artist;
            _controls.DisplayUpdater.Update();
        }

        #region Utils
        /// <summary>
        /// Отправляет сообщение в процесс переднего плана.
        /// </summary>
        /// <param name="message">Сообщение для отправки.</param>
        internal static void SendMessageToForeground(ValueSet message)
        {
            BackgroundMediaPlayer.SendMessageToForeground(message);
        }

        /// <summary>
        /// Отправить сообщение процессу переднего плана.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        internal static void SendMessageToForeground(string key, object value)
        {
            var message = new ValueSet();
            message.Add(key, value);
            SendMessageToForeground(message);
        }
        #endregion
    }
}
