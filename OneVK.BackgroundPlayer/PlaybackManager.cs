using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Playback;
using OneVK.Helpers;
using Windows.Storage;
using Windows.Foundation.Collections;

namespace OneVK.BackgroundPlayer
{
    /// <summary>
    /// Представляет менеджер плейлиста и воспроизведения.
    /// </summary>
    internal class PlaybackManager
    {
        private MediaPlayer _player;
        private List<AudioTrack> _tracks;
        private bool _isShuffleMode;
        private bool _isRepeatMode;
        private int _currentTrackID;

        /// <summary>
        /// Происходит при изменении текущего трека.
        /// </summary>
        public event EventHandler<TrackChangedEventArgs> TrackChanged;

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным экземпляром
        /// проигрывателя.
        /// </summary>
        /// <param name="player">Экземпляр проигрывателя.</param>
        public PlaybackManager(MediaPlayer player)
        {
            _player = player;
            _player.AutoPlay = false;

            _player.MediaOpened += Player_MediaOpened;
            _player.MediaEnded += Player_MediaEnded;
            _player.MediaFailed += Player_MediaFailed;
            _player.BufferingStarted += Player_BufferingStarted;
            _player.BufferingEnded += Player_BufferingEnded;

            _currentTrackID = SettingsHelper.Get<int>(AppConstants.PlayerCurrentTrackID);
            _isShuffleMode = SettingsHelper.Get<bool>(AppConstants.PlayerShuffleMode);            
            _player.IsLoopingEnabled = SettingsHelper.Get<bool>(AppConstants.PlayerRepeatMode);
        }
        
        #region Свойства
        /// <summary>
        /// Текущий трек.
        /// </summary>
        public AudioTrack CurrentTrack { get; private set; }
        /// <summary>
        /// Идентификатор текущего трека в текущем плейлисте.
        /// </summary>
        public int CurrentTrackID
        {
            get { return _currentTrackID; }
            private set
            {
                _currentTrackID = value;
                SettingsHelper.Set(AppConstants.PlayerCurrentTrackID, value);
            }
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Обновляет менеджер воспроизведения.
        /// </summary>
        public async Task Update(bool reset = false)
        {
            if (reset)
            {
                _tracks = null;
                return;
            }

            StorageFile file = null;
            if (_isShuffleMode)
                file = await FileHelper.GetLocalFileFromName(AppConstants.ShuffledPlaylistFileName);
            else
                file = await FileHelper.GetLocalFileFromName(AppConstants.PlaylistFileName);

            if (file == null)
            {
#if DEBUG
                if (_isShuffleMode)
                    Debug.WriteLine("PlaybackManager cant find shuffled playlist file.");
                else
                    Debug.WriteLine("PlaybackManager cant find normal playlist file.");
#endif
                return;
            }

            string json = await FileHelper.ReadTextFromFile(file);
            if (String.IsNullOrEmpty(json))
            {
#if DEBUG
                Debug.WriteLine("Playlist file is empty.");
#endif
                return;
            }
            _tracks = JsonSerializationHelper.DeserializeFromJson<List<AudioTrack>>(json);
        }

        /// <summary>
        /// Обновить состояние режима смешанного плейлиста.
        /// </summary>
        public async Task UpdateShuffleMode()
        {
            _isShuffleMode = SettingsHelper.Get<bool>(AppConstants.PlayerShuffleMode);
            await Update();
            CurrentTrackID = _tracks.IndexOf(CurrentTrack);
        }

        /// <summary>
        /// Обновить состояние повтора текущего трека.
        /// </summary>
        public void UpdateRepeatMode()
        {
            _player.IsLoopingEnabled = SettingsHelper.Get<bool>(AppConstants.PlayerRepeatMode);
        }

        /// <summary>
        /// Продолжить или приостановить воспроизведение.
        /// </summary>
        public async void ResumePause()
        {
            if (!await HasTracks())
            {
#if DEBUG
                Debug.WriteLine("PlaybackManager has empty tracks list. Cant play/pause.");
#endif
            }
            switch (_player.CurrentState)
            {
                case MediaPlayerState.Playing:
                    _player.Pause();
                    break;
                case MediaPlayerState.Paused:
                    _player.Play();
                    break;
                case MediaPlayerState.Closed:
                    PlayTrackFromID(CurrentTrackID);
                    break;
                case MediaPlayerState.Stopped:
                    PlayTrackFromID(CurrentTrackID);
                    break;
            }
            AudioTask.SendMessageToForeground(AppConstants.PlayerPauseResume, null);
        }

        /// <summary>
        /// Переключить на следующий трек.
        /// </summary>
        public async void NextTrack()
        {
            if (! await HasTracks())
            {
#if DEBUG
                Debug.WriteLine("PlaybackManager has empty tracks list. Cant play next track.");
#endif
                return;
            }

            int id = 0;
            if (CurrentTrackID + 1 != _tracks.Count) id = CurrentTrackID + 1;
            PlayTrackFromID(id);
        }

        /// <summary>
        /// Переключить на предыдущий трек.
        /// </summary>
        public async void PreviousTrack()
        {
            if (!await HasTracks())
            {
#if DEBUG
                Debug.WriteLine("PlaybackManager has empty tracks list. Cant play previous track.");
#endif
                return;
            }

            if (_player.Position.TotalSeconds >= AppConstants.PlayerReplayAgainTriggerSeconds)
            {
                _player.Position = TimeSpan.Zero;
                return;
            }

            int id;
            if (CurrentTrackID - 1 == -1) id = _tracks.Count - 1;
            else id = CurrentTrackID - 1;

            PlayTrackFromID(id);
        }

        /// <summary>
        /// Воспроизводит трек по его идентификатору в плейлисте.
        /// </summary>
        /// <param name="trackID">Идентификатор трека.</param>
        public async void PlayTrackFromID(int trackID)
        {
            if (!await HasTracks())
            {
#if DEBUG
                Debug.WriteLine(String.Format("PlaybackManager has empty tracks list. Cant play from id \"{0}\".", trackID));
#endif
                return;
            }

            CurrentTrack = _tracks[trackID];
            CurrentTrackID = trackID;
            if (CurrentTrack.Source == null) return;
            _player.SetUriSource(new Uri(CurrentTrack.Source));

            if (TrackChanged != null)
                TrackChanged(this, new TrackChangedEventArgs(CurrentTrack, CurrentTrackID));            
        }
        #endregion

        #region Приватные методы 
        /// <summary>
        /// Имеются литреки в плейлисте.
        /// </summary>
        private async Task<bool> HasTracks()
        {
            if (_tracks == null || _tracks.Count == 0)
            {
                await Update();
                return _tracks != null && _tracks.Count != 0;
            }
            return true;
        }

        /// <summary>
        /// Вызывается при открытии мультимедиа проигрывателем.
        /// </summary>
        private void Player_MediaOpened(MediaPlayer sender, object args)
        {
            _player.Play();
            AudioTask.SendMessageToForeground(AppConstants.PlayerMediaOpened, null);
        }

        /// <summary>
        /// Вызывается при окончании воспроизведения мультимедиа проигрывателем.
        /// </summary>
        private void Player_MediaEnded(MediaPlayer sender, object args)
        {
            AudioTask.SendMessageToForeground(AppConstants.PlayerMediaEnded, null);
            if (false)
            {
                _player.Play();
                return;
            }
            NextTrack();
        }

        /// <summary>
        /// Вызывается при ошибке при воспроизведении мультимедиа проигрывателем.
        /// </summary>
        private void Player_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
        {
            AudioTask.SendMessageToForeground(AppConstants.PlayerMediaFailed, null);
        }        

        /// <summary>
        /// Вызывается в момент начала буферизации мультимедиа проигрывателем.
        /// </summary>
        private void Player_BufferingStarted(MediaPlayer sender, object args)
        {
            AudioTask.SendMessageToForeground(AppConstants.PlayerBufferingStarted, null);
        }

        /// <summary>
        /// Вызывается при окончании буферизации мультимедиа проигрывателем.
        /// </summary>
        private void Player_BufferingEnded(MediaPlayer sender, object args)
        {
            AudioTask.SendMessageToForeground(AppConstants.PlayerBufferingEnded, null);
        }
        #endregion
    }
}
