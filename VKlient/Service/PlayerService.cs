using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Foundation.Collections;
using OneVK.Helpers;
using System.Threading;
using OneVK.Enums.App;
using OneVK.Core.Player;

namespace OneVK.Service
{
    /// <summary>
    /// Провайдер данных для проигрывателя.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private AutoResetEvent _taskStartedEvent = new AutoResetEvent(false);
        private bool _isInitialized;
        private bool? _isTaskRunning;
        private int? _currentTrackID; 

        /// <summary>
        /// Выполняется ли в данный момент фоновая задача.
        /// </summary>
        private bool IsTaskRunning
        {
            get
            {
                if (_isTaskRunning == null)
                    _isTaskRunning = ServiceHelper.SettingsService.IsPlayerTaskRunning;

                if (_isTaskRunning.Value)
                    return true;                

                if(_isTaskRunning.Value == false)
                {
                    var player = BackgroundMediaPlayer.Current;
                    if (_taskStartedEvent.WaitOne(AppConstants.AudioTaskStartingTimeout))
                        _isTaskRunning = true;
                }

                return _isTaskRunning.Value;
            }
            set { _isTaskRunning = value; }
        }

        /// <summary>
        /// Происходит при изменении идентификатора текущего трека.
        /// </summary>
        public event EventHandler<int> TrackIDChanged = delegate { };
        /// <summary>
        /// Происходит при изменении состояния проигрывателя.
        /// </summary>
        public event EventHandler<PlayerState> PlayerStateChanged = delegate { };

        /// <summary>
        /// Текущее состояние проигрывателя.
        /// </summary>
        public PlayerState CurrentState
        {
            get { return IsTaskRunning ? (PlayerState)((byte)BackgroundMediaPlayer.Current.CurrentState) : PlayerState.Closed; }
        }

        /// <summary>
        /// Возвращает или задает идентификатор текущего трека.
        /// </summary>
        public int CurrentTrackID
        {
            get
            {
                if (_currentTrackID != null)
                    return _currentTrackID.Value;
                _currentTrackID = ServiceHelper.SettingsService.CurrentTrackID;

                return _currentTrackID.Value;
            }
            set { _currentTrackID = value; }
        }

        /// <summary>
        /// Возвращает или задает текущую позицию в треке.
        /// </summary>
        public TimeSpan CurrentPosition
        {
            get
            {
                if (IsTaskRunning)
                    return BackgroundMediaPlayer.Current.Position;
                return TimeSpan.Zero;
            }
            set
            {
                if (IsTaskRunning)
                    BackgroundMediaPlayer.Current.Position = value;
            }
        }

        /// <summary>
        /// Возвращает длительность текущего трека.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                if (IsTaskRunning)
                    return BackgroundMediaPlayer.Current.NaturalDuration;
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Предыдущий трек.
        /// </summary>
        public void PreviousTrack()
        {
            SendMessageToBackground(AppConstants.PlayerPreviousTrack, null);
        }

        /// <summary>
        /// Следующий трек.
        /// </summary>
        public void NextTrack()
        {
            SendMessageToBackground(AppConstants.PlayerNextTrack, null);
        }

        /// <summary>
        /// Продолжить или приостановить воспроизведение.
        /// </summary>
        public void ResumePause()
        {
            SendMessageToBackground(AppConstants.PlayerPauseResume, null);
        }

        /// <summary>
        /// Указывает проигрывателю на необходимость обновления плейлиста.
        /// </summary>
        public void UpdateNewPlaylist()
        {
            SendMessageToBackground(AppConstants.PlayerNewPlaylist, null);
        }

        /// <summary>
        /// Воспроизводит трек из текущего плейлиста по его идентификатору.
        /// </summary>
        /// <param name="trackId">Идентификатор трека.</param>
        public void PlayFromID(int trackID)
        {
            SendMessageToBackground(AppConstants.PlayerCurrentTrackID, trackID);
        }

        /// <summary>
        /// Указывает проигрывателю переключиться на смешанный плейлсит, если необходимо.
        /// </summary>
        public void UpdateShuffleMode()
        {
            SendMessageToBackground(AppConstants.PlayerShuffleMode, null);
        }

        /// <summary>
        /// Указывает проигрывателю на необходимость обновления параметра повтора.
        /// </summary>
        public void UpdateRepeatMode()
        {
            SendMessageToBackground(AppConstants.PlayerRepeatMode, null);
        }

        /// <summary>
        /// Инициализирует провайдер.
        /// </summary>
        public void Initialize()
        {
            if (!_isInitialized)
            {
                BackgroundMediaPlayer.MessageReceivedFromBackground += OnNewMessageReceived;
                BackgroundMediaPlayer.Current.CurrentStateChanged += Current_CurrentStateChanged;
                _isInitialized = true;
            }
        }

        private void Current_CurrentStateChanged(MediaPlayer sender, object args)
        {
            PlayerStateChanged(this, (PlayerState)((byte)sender.CurrentState));
        }

        /// <summary>
        /// Деинициализирует провайдер.
        /// </summary>
        public void Deinitialize()
        {
            if (_isInitialized)
            {
                BackgroundMediaPlayer.MessageReceivedFromBackground -= OnNewMessageReceived;
                BackgroundMediaPlayer.Current.CurrentStateChanged -= Current_CurrentStateChanged;
                _isTaskRunning = null;
                _currentTrackID = null;
                _isInitialized = false;
            }
        }        

        /// <summary>
        /// Вызывается при получении нового сообщения от фонового процесса проигрывателя.
        /// </summary>
        private void OnNewMessageReceived(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            foreach (string key in e.Data.Keys)
            {
                switch (key)
                {
                    case AppConstants.PlayerTaskRunning:
                        IsTaskRunning = (bool)e.Data[key];
                        _taskStartedEvent.Set();
                        break;
                    case AppConstants.PlayerCurrentTrackID:
                        CurrentTrackID = (int)e.Data[key];
                        TrackIDChanged(this, CurrentTrackID);
                        break;
                    case AppConstants.PlayerBufferingStarted:
                        //PlayerStateChanged(this, CurrentState);
                        break;
                    case AppConstants.PlayerBufferingEnded:
                        //PlayerStateChanged(this, CurrentState);
                        break;
                    case AppConstants.PlayerMediaOpened:
                        //PlayerStateChanged(this, CurrentState);
                        break;
                    case AppConstants.PlayerMediaEnded:
                        //PlayerStateChanged(this, CurrentState);
                        break;
                    case AppConstants.PlayerMediaFailed:
                        break;
                    case AppConstants.PlayerPauseResume:
                        //PlayerStateChanged(this, CurrentState);
                        break;            
                }
            }
        }

        /// <summary>
        /// Отправляет сообщение фоновому процессу проигрывателя.
        /// </summary>
        /// <param name="message">Сообщение для отправки.</param>
        private void SendMessageToBackground(ValueSet message)
        {
            if (IsTaskRunning)
                BackgroundMediaPlayer.SendMessageToBackground(message);
        }

        /// <summary>
        /// Отправляет сообщение фоновому процессу проигрывателя.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        private void SendMessageToBackground(string key, object value)
        {
            var message = new ValueSet();
            message.Add(key, value);
            SendMessageToBackground(message);
        }

        /// <summary>
        /// Устанавливает новый плейлист.
        /// </summary>
        /// <param name="tracks">Список треков.</param>
        public async Task SetNewPlaylist(IEnumerable<IAudioTrack> tracks)
        {
            var list = tracks.Select(t => new AudioTrack
            {
                Title = t.Title,
                Artist = t.Artist,
                Source = t.Source,
                LyricsID = t.LyricsID
            });
            bool result = await FileHelper.WriteTextToFile(
                await FileHelper.CreateLocalFile(AppConstants.PlaylistFileName),
                JsonSerializationHelper.SerializeToJson(list));
            UpdateNewPlaylist();
        }

        /// <summary>
        /// Устанавливает новый перемешанный плейлист.
        /// </summary>
        /// <param name="tracks">Список треков.</param>
        public async Task SetNewShuffledPlaylist(IEnumerable<IAudioTrack> tracks)
        {
            var list = tracks.Select(t => new AudioTrack
            {
                Title = t.Title,
                Artist = t.Artist,
                Source = t.Source,
                LyricsID = t.LyricsID
            });
            bool result = await FileHelper.WriteTextToFile(
                await FileHelper.CreateLocalFile(AppConstants.ShuffledPlaylistFileName),
                JsonSerializationHelper.SerializeToJson(list));
        }

        /// <summary>
        /// Возвращает сохраненный плейлист.
        /// </summary>
        public async Task<IEnumerable<IAudioTrack>> GetPlaylist()
        {
            var file = await FileHelper.GetLocalFileFromName(AppConstants.PlaylistFileName);
            if (file == null) return null;

            string json = await FileHelper.ReadTextFromFile(file);
            if (String.IsNullOrEmpty(json)) return null;
            return JsonSerializationHelper.DeserializeFromJson<List<AudioTrack>>(json);
        }

        /// <summary>
        /// Возвращает сохраненный перемешанный плейлист.
        /// </summary>
        public async Task<IEnumerable<IAudioTrack>> GetShuffledPlaylist()
        {
            var file = await FileHelper.GetLocalFileFromName(AppConstants.ShuffledPlaylistFileName);
            if (file == null) return null;

            string json = await FileHelper.ReadTextFromFile(file);
            if (String.IsNullOrEmpty(json)) return null;
            return JsonSerializationHelper.DeserializeFromJson<List<AudioTrack>>(json);
        }
    }
}
