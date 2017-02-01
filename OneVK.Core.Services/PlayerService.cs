using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Core.Models;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис воспроизведения аудиозаписей.
    /// </summary>
    public sealed class PlayerService : IPlayerService
    {
        private const int RPC_S_SERVER_UNAVAILABLE = -2147023174; // 0x800706BA
                
        private ISettingsService settingsService;
        private bool isSubscribed;
        private bool? isTaskRunning;
        private readonly AutoResetEvent taskStarted = new AutoResetEvent(false);

        /// <summary>
        /// Просиходит при изменении идентификатора текущего трека.
        /// </summary>
        public event EventHandler<int> TrackIDChanged;

        /// <summary>
        /// Происходит при изменении состояния плеера.
        /// </summary>
        public event EventHandler<MediaPlayerState> PlayerStateChanged;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerService"/> с
        /// необходимыми сервисами.
        /// </summary>
        /// <param name="settingsService">Сервис настроек.</param>
        public PlayerService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Выполнить инициализацию сервиса.
        /// </summary>
        public void StartService()
        {
            settingsService.Set(PlayerConstants.APP_RUNNING, true);
            //settingsService.Remove(PlayerConstants.TASK_RUNNING);
            SubscribeToEvents();
            isTaskRunning = null;
        }

        /// <summary>
        /// Выполнить остановку сервиса.
        /// </summary>
        public void StopService()
        {
            settingsService.Set(PlayerConstants.APP_RUNNING, false);
            isTaskRunning = null;
            UnsubscribeToEvents();
        }

        /// <summary>
        /// Возвращает идентификатор текущего трека в плейлисте.
        /// </summary>
        public int CurrenTrackID
        {
            get { return settingsService.GetNoCache(PlayerConstants.CURRENT_TRACK_ID, -1); }
        }

        /// <summary>
        /// Возвращает значение, запущен ли проигрыватель.
        /// </summary>
        public bool IsTaskRunning
        {
            get
            {
                if (isTaskRunning == null)
                    isTaskRunning = settingsService.GetNoCache<bool>(PlayerConstants.TASK_RUNNING);
                return isTaskRunning.Value;
            }
        }    

        /// <summary>
        /// Возвращает текущее состояние проигрывателя.
        /// </summary>
        public MediaPlayerState CurrentState
        {
            get
            {
                if (!IsTaskRunning) return MediaPlayerState.Closed;
                return CurrentPlayer.CurrentState;
            }
        }

        /// <summary>
        /// Возвращает или задает текущую позицию.
        /// </summary>
        public TimeSpan CurrentPosition
        {
            get
            {
                if (!IsTaskRunning) return TimeSpan.Zero;
                return CurrentPlayer.Position;
            }
            set
            {
                if (IsTaskRunning) CurrentPlayer.Position = value;
            }
        }

        /// <summary>
        /// Возвращает длительность текущего трека.
        /// </summary>
        public TimeSpan CurrentDuration
        {
            get
            {
                if (!IsTaskRunning) return TimeSpan.Zero;
                return CurrentPlayer.NaturalDuration;
            }
        }

        /// <summary>
        /// Приостановить или возобновить воспротзведение.
        /// </summary>
        public void PlayPause()
        {
            if (!IsTaskRunning) StartBackgroundAudioTask();

            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.PLAYER_PLAY_PAUSE, null);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Записывает новые треки в хранилище.
        /// </summary>
        /// <param name="tracksToPlay">Список треков для воспроизведения.</param>
        public async Task SetNewTracks(IList<IVKAudioTrack> tracksToPlay)
        {
            string json = await Task.Run(() => JsonConvert.SerializeObject(tracksToPlay));
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(PlayerConstants.NORMAL_PLAYLIST_FILE, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);

            if (!IsTaskRunning) StartBackgroundAudioTask();

            SendMessageToBackground(PlayerConstants.UPDATE_PLAYLIST, null);
        }

        /// <summary>
        /// Записывает новые перемешанные треки в хранилище.
        /// </summary>
        /// <param name="tracksToPlay">Список треков для воспроизведения.</param>
        public async Task SetNewShuffledTracks(IList<IVKAudioTrack> tracksToPlay)
        {
            string json = await Task.Run(() => JsonConvert.SerializeObject(tracksToPlay));
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(PlayerConstants.SHUFFLED_PLAYLIST_FILE, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);

            if (!IsTaskRunning) StartBackgroundAudioTask();

            SendMessageToBackground(PlayerConstants.UPDATE_PLAYLIST, null);
        }

        /// <summary>
        /// Возвращает нормальный плейлист.
        /// </summary>
        public async Task<IEnumerable<IVKAudioTrack>> GetNormalTracks()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(PlayerConstants.NORMAL_PLAYLIST_FILE);
                string json = await FileIO.ReadTextAsync(file);
                var tracks = JsonConvert.DeserializeObject<List<AudioTrack>>(json);
                return tracks;
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Возвращает перемешанный плейлист.
        /// </summary>
        public async Task<IEnumerable<IVKAudioTrack>> GetShuffledTracks()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(PlayerConstants.SHUFFLED_PLAYLIST_FILE);
                string json = await FileIO.ReadTextAsync(file);
                var tracks = JsonConvert.DeserializeObject<List<AudioTrack>>(json);
                return tracks;
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Обновить состояние случайного режима.
        /// </summary>
        public void UpdateShuffleMode()
        {
            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.UPDATE_SHUFFLE_MODE, null);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Обновить состояние режима повтора.
        /// </summary>
        public void UpdateRepeatMode()
        {
            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.UPDATE_REPEAT_MODE, null);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Воспроизвести трек по его идентификатору в плейлисте.
        /// </summary>
        /// <param name="trackID">Идентификатор трека.</param>
        public void PlayFromID(int trackID)
        {
            if (!IsTaskRunning) StartBackgroundAudioTask();

            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.PLAY_TRACK_ID, trackID);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Переключить на следующий трек.
        /// </summary>
        public void SkipNext()
        {
            if (!IsTaskRunning) StartBackgroundAudioTask();

            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.PLAYER_NEXT_TRACK, null);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Переключить на предыдущий трек.
        /// </summary>
        public void SkipPrevious()
        {
            if (!IsTaskRunning) StartBackgroundAudioTask();

            var valueSet = new ValueSet();
            valueSet.Add(PlayerConstants.PLAYER_PREVIOUS_TRACK, null);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }

        /// <summary>
        /// Возвращает экземпляр текущего фонового проигрывателя.
        /// </summary>
        private MediaPlayer CurrentPlayer
        {
            get
            {
                MediaPlayer player = null;
                int retryCount = 2;

                while (player == null || --retryCount >= 0)
                {
                    try
                    {
                        player = BackgroundMediaPlayer.Current;
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == RPC_S_SERVER_UNAVAILABLE)
                        {
                            ResetAfterLostBackground();
                            StartBackgroundAudioTask();
                        }
                        //else
                        //    throw new PlayerException("Не удалось получить экземпляр фонового проигрывателя.", ex);
                    }
                }

                return player;
            }
        }

        /// <summary>
        /// Запускает фоновый процесс проигрывателя.
        /// </summary>
        private void StartBackgroundAudioTask()
        {
            SubscribeToEvents();

            if (!taskStarted.WaitOne(4000))
                throw new PlayerException("Проигрыватель не запустился за отведенное время.");
        }
                
        /// <summary>
        /// Сбрасывает фоновую задачу при потере доступа к ней (RPC_S_SERVER_UNAVAILABLE).
        /// </summary>
        private void ResetAfterLostBackground()
        {
            BackgroundMediaPlayer.Shutdown();
            settingsService.Set(PlayerConstants.TASK_RUNNING, false);
            taskStarted.Reset();

            try
            {
                BackgroundMediaPlayer.MessageReceivedFromBackground += MessageReceivedFromBackground;
                CurrentPlayer.CurrentStateChanged += Player_CurrentStateChanged;
                isSubscribed = true;
            }
            catch (Exception ex) { throw new PlayerException("Не удалось запустить проигрыватель.", ex); }
        }

        /// <summary>
        /// Подписаться на необходимые события проигрывателя.
        /// </summary>
        private void SubscribeToEvents()
        {
            if (isSubscribed) return;

            try
            {
                BackgroundMediaPlayer.MessageReceivedFromBackground += MessageReceivedFromBackground;
                CurrentPlayer.CurrentStateChanged += Player_CurrentStateChanged;
                isSubscribed = true;
            }
            catch (Exception ex)
            {
                if (ex.HResult == RPC_S_SERVER_UNAVAILABLE)
                    ResetAfterLostBackground();
                else new PlayerException("Не удалось запустить проигрыватель.", ex);
            }
        }

        /// <summary>
        /// Отписаться от событий проигрывателя.
        /// </summary>
        private void UnsubscribeToEvents()
        {
            if (!isSubscribed) return;
            
            BackgroundMediaPlayer.MessageReceivedFromBackground -= MessageReceivedFromBackground;
            CurrentPlayer.CurrentStateChanged -= Player_CurrentStateChanged;
            isSubscribed = false;
        }

        /// <summary>
        /// Вызывается при получении сообщения от фоновой задачи проигрывателя.
        /// </summary>
        private void MessageReceivedFromBackground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            foreach (var key in e.Data.Keys)
            {
                switch (key)
                {
                    case PlayerConstants.TASK_RUNNING:
                        taskStarted.Set();
                        isTaskRunning = true;
                        break;
                    case PlayerConstants.CURRENT_TRACK_ID:
                        if (TrackIDChanged != null) TrackIDChanged(this, (int)e.Data[key]);
                        break;
                }
            }
        }

        /// <summary>
        /// Вызывается при изменении состояния проигрывателя.
        /// </summary>
        private void Player_CurrentStateChanged(MediaPlayer sender, object args)
        {
            if (PlayerStateChanged != null)
                PlayerStateChanged(this, sender.CurrentState);
        }

        /// <summary>
        /// Отправляет сообщение в фоновую задачу.
        /// </summary>
        /// <typeparam name="T">Тип параметра.</typeparam>
        /// <param name="name">Имя параметра.</param>
        /// <param name="parameter">Параметр.</param>
        private void SendMessageToBackground(string name, object parameter)
        {
            var valueSet = new ValueSet();
            valueSet.Add(name, parameter);
            BackgroundMediaPlayer.SendMessageToBackground(valueSet);
        }
    }
}
