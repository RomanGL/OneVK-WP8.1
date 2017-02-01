using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Request.LFRequests;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Enums.App;
using System.Collections.ObjectModel;
using OneVK.Model.Audio;
using Windows.UI.Core;
using OneVK.Core.Player;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using OneVK.Core;
using VKSaver.Commands.SDK;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    public class PlayerViewModel : BaseViewModel
    {
        /// <summary>
        /// Происходит при изменении текущего трека.
        /// </summary>
        public event EventHandler<IAudioTrack> TrackChanged = delegate { };

        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public PlayerViewModel()
        {
#if DEBUG
            if (IsInDesignModeStatic)
            {
                CurrentTrack = new VKAudio { Title = "Poker Face", Artist = "Lady Gaga", LyricsID = 1 };
                DurationStart = TimeSpan.FromSeconds(0);
                DurationEnd = TimeSpan.FromSeconds(123);
                ArtistImage = new BitmapImage(new Uri("http://userserve-ak.last.fm/serve/500/60890169/Lady+Gaga+Gaga.png"));
                return;
            }
#endif  

            NextTrack = new RelayCommand(() =>
            {
                if (Tracks == null || Tracks.Count == 0) return;

                ServiceHelper.PlayerService.NextTrack();

                Duration = TimeSpan.Zero;
                DurationStart = TimeSpan.Zero;
                DurationEnd = TimeSpan.Zero;

                if (_currentTrackID + 1 == Tracks.Count) _currentTrackID = 0;
                else _currentTrackID++;
            });

            PreviousTrack = new RelayCommand(() =>
            {
                if (CurrentTrack == null || Tracks == null || Tracks.Count == 0) return;

                ServiceHelper.PlayerService.PreviousTrack();
                
                DurationStart = TimeSpan.Zero;
                DurationEnd = TimeSpan.Zero;

                if (_currentTrackID - 1 == -1) _currentTrackID = Tracks.Count - 1;
                else _currentTrackID--;                
            });

            PlayTrack = new RelayCommand<IAudioTrack>(a => SetNewTrack(a));
            PlayResume = new RelayCommand(() => ServiceHelper.PlayerService.ResumePause());

            DownloadTrack = new RelayCommand(async () =>
            {
                var command = new VKSaverDownloadCommand();
               //command.Downloads.Add(CoreHelper.GetDownload(CurrentTrack));
                await command.TryExecute();
            }, () => CurrentTrack != null && !String.IsNullOrEmpty(CurrentTrack.Source));

            ShowTrackLyrics = new RelayCommand(() =>
            {
                NavigationHelper.Navigate(AppViews.TrackLyricsView, CurrentTrack);
            },
            () => CurrentTrack != null && CurrentTrack.LyricsID != 0);
            
            _timer.Tick += (s, e) =>
            {
                if (ServiceHelper.PlayerService.CurrentState == PlayerState.Playing)
                    try
                    {
                        DurationStart = ServiceHelper.PlayerService.CurrentPosition;                        
                        DurationEnd = DurationStart - Duration;
                    }
                    catch (Exception) { }              
            };
            
            _timer.Start();
            Messenger.Default.Register<PlayTrackMessage>(this, OnPlayTrackMessageReceived);            
        }
        #endregion

        #region Приватные поля
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        private string _title;
        private string _artist;
        private bool _canShuffle = true;
        private bool _isOnView;
        private bool _isInitialized;
        private ImageSource _artistLoadingImage;
        private ImageSource _artistImage;
        private ImageSource _trackImage;
        private TimeSpan _durationStart;
        private TimeSpan _durationEnd;
        private TimeSpan _duration;
        private double _totalSeconds;
        private double _leftSeconds;
        private ObservableCollection<IAudioTrack> _tracks;
        private List<IAudioTrack> _normalTrackList;
        private IAudioTrack _currentTrack;
        private int _currentTrackID;
        private Random _random = new Random(Environment.TickCount);
        private bool _isPlaying;
        #endregion

        #region Свойства
        /// <summary>
        /// Полный список треков.
        /// </summary>
        public ObservableCollection<IAudioTrack> Tracks
        {
            get { return _tracks; }
            private set { Set(() => Tracks, ref _tracks, value); }
        }
        /// <summary>
        /// Текущий трек.
        /// </summary>
        public IAudioTrack CurrentTrack
        {
            get
            {
                if (_currentTrack == null)
                {
                    LoadSavedInfo();                    
                }
                return _currentTrack;
            }
            private set
            {
                Set(() => CurrentTrack, ref _currentTrack, value);
                TrackChanged(this, _currentTrack);
            }
        }
        /// <summary>
        /// Включен ли случайный режим.
        /// </summary>
        public bool IsShuffleMode
        {
            get
            {
#if DEBUG
                if (IsInDesignMode) return false;
#endif
                return ServiceHelper.SettingsService.PlayerShuffleMode;
            }
            set
            {
                ServiceHelper.SettingsService.PlayerShuffleMode = value;
                RaisePropertyChanged("IsShuffleMode");
                ShuffleTracks(value, CurrentTrack);
            }
        }
        /// <summary>
        /// Возможно ли перемешать треки.
        /// </summary>
        public bool CanShuffle
        {
            get { return _canShuffle; }
            private set { Set(() => CanShuffle, ref _canShuffle, value); }
        }
        /// <summary>
        /// Включен ли режим повтора.
        /// </summary>
        public bool IsRepeatMode
        {
            get
            {
#if DEBUG
                if (IsInDesignMode) return true;
#endif
                return ServiceHelper.SettingsService.PlayerRepeatMode;
            }
            set
            {
                ServiceHelper.SettingsService.PlayerRepeatMode = value;
                RaisePropertyChanged("IsRepeatMode");
                ServiceHelper.PlayerService.UpdateRepeatMode();    
            }
        }
        /// <summary>
        /// Работает ли в данный момент проигрыватель.
        /// </summary>
        public bool IsPlayerWork
        {
            get
            {
#if DEBUG
                if (IsInDesignMode) return true;
#endif
                return ServiceHelper.PlayerService.CurrentState != PlayerState.Closed;
            }
        }
        /// <summary>
        /// Выполняется ли воспроизведение.
        /// </summary>
        public bool IsPlaying
        {
            get { return ServiceHelper.PlayerService.CurrentState == PlayerState.Playing; }
            //private set { Set(() => IsPlaying, ref _isPlaying, value); }
        }
        /// <summary>
        /// Изображение исполнителя.
        /// </summary>
        public ImageSource ArtistImage
        {
            get { return _artistImage; }
            private set { Set(() => ArtistImage, ref _artistImage, value); }
        }
        /// <summary>
        /// Изображение трека.
        /// </summary>
        public ImageSource TrackImage
        {
            get { return _trackImage; }
            private set { Set(() => TrackImage, ref _trackImage, value); }
        }
        /// <summary>
        /// Сколько прошло времени в секундах прошло с начала композиции.
        /// </summary>
        public TimeSpan DurationStart
        {
            get { return _durationStart; }
            private set
            {
                Set(() => DurationStart, ref _durationStart, value);
                RaisePropertyChanged("LeftSeconds");
            }
        }
        /// <summary>
        /// Сколько осталось времени до конца композиции.
        /// </summary>
        public TimeSpan DurationEnd
        {
            get { return _durationEnd; }
            private set { Set(() => DurationEnd, ref _durationEnd, value); }
        }
        /// <summary>
        /// Полная длительность композиции.
        /// </summary>
        public TimeSpan Duration
        {
            get { return _duration; }
            private set
            {
                Set(() => Duration, ref _duration, value);
                if (value.TotalSeconds <= 0) LeftSeconds = 0;
                TotalSeconds = value.TotalSeconds >= 0 ? value.TotalSeconds : 0;
            }
        }

        public double TotalSeconds
        {
            get { return _totalSeconds; }
            set { Set(() => TotalSeconds, ref _totalSeconds, value); }
        }

        public double LeftSeconds
        {
            get { return DurationStart.TotalSeconds >= 0 ? DurationStart.TotalSeconds : 0; }
            set
            {
                ServiceHelper.PlayerService.CurrentPosition = TimeSpan.FromSeconds(value);
            }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Приостановить или возобновить воспроизведение.
        /// </summary>
        public RelayCommand PlayResume { get; private set; }
        /// <summary>
        /// Воспроизвести указанный трек.
        /// </summary>
        public RelayCommand<IAudioTrack> PlayTrack { get; private set; }
        /// <summary>
        /// Перейти на следующий трек.
        /// </summary>
        public RelayCommand NextTrack { get; private set; }
        /// <summary>
        /// Перейти на предыдущий трек.
        /// </summary>
        public RelayCommand PreviousTrack { get; private set; }
        /// <summary>
        /// Отобразить текст аудиозаписи.
        /// </summary>
        public RelayCommand ShowTrackLyrics { get; private set; }
        /// <summary>
        /// Загрузить указанный трек.
        /// </summary>
        public RelayCommand DownloadTrack { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            await LoadSavedInfo();            

            //_isOnView = true;
            DownloadTrack.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => IsPlayerWork);

            //if (ServiceHelper.PlayerService.CurrentState == PlayerState.Playing)
            //    _timer.Start();
        }        

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            //_isOnView = false;
            //_timer.Stop();
        }
        #endregion

        #region Приватные методы
        private async Task LoadSavedInfo()
        {
            await Initialize();

            if (Tracks != null && Tracks.Count > 0)
            {
                _currentTrackID = ServiceHelper.PlayerService.CurrentTrackID;
                if (_currentTrackID != -1)
                {
                    try
                    {
                        CurrentTrack = Tracks[_currentTrackID];
                    }
                    catch (Exception) { CurrentTrack = Tracks[0]; }
                    try
                    {
                        LoadTrackInfo(CurrentTrack);

                        DurationStart = ServiceHelper.PlayerService.CurrentPosition;
                        Duration = ServiceHelper.PlayerService.Duration;
                        DurationEnd = DurationStart - Duration;
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Вызывается при получении собщения о необходимости воспроизведения трека.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        private async void OnPlayTrackMessageReceived(PlayTrackMessage message)
        {
            _currentTrackID = 0;
            Tracks = new ObservableCollection<IAudioTrack>(message.Tracks);
            await ServiceHelper.PlayerService.SetNewPlaylist(Tracks);
            await ShuffleTracks(IsShuffleMode, message.TrackToPlay);
            SetNewTrack(message.TrackToPlay);         
        }

        /// <summary>
        /// Установить новый трек.
        /// </summary>
        /// <param name="track">Трек.</param>
        private void SetNewTrack(IAudioTrack track)
        {
            _isPopSended = false;
            _currentTrackID = Tracks.IndexOf(track);
            PlayTrackFromID(_currentTrackID);
        }

        /// <summary>
        /// Воспроизвести трек по его идентификатору в плейлисте.
        /// </summary>
        /// <param name="trackID">Идентификатор трека.</param>
        private void PlayTrackFromID(int trackID)
        {
            if (trackID == -1) return;
            CurrentTrack = Tracks[trackID];            
            ShowTrackLyrics.RaiseCanExecuteChanged();
            DownloadTrack.RaiseCanExecuteChanged();

            if (_isOnView) _timer.Stop();
            ServiceHelper.PlayerService.PlayFromID(trackID);

            DurationStart = TimeSpan.FromSeconds(0);
            
            //_timer.Start();
            LoadTrackInfo(CurrentTrack);
        }
        
        /// <summary>
        /// Загружает информацию о треке.
        /// </summary>
        /// <param name="title">Заголовок трека.</param>
        /// <param name="artist">Исполнитель трека.</param>
        /// <param name="mbid">Идентификатор трека (если имеется).</param>
        private void LoadTrackInfo(IAudioTrack track, string mbid = null)
        {
            RaisePropertyChanged(() => IsPlayerWork);
            LoadAlbumImage(track, mbid);
            LoadArtistsImage(track);            
        }  
        
        /// <summary>
        /// Загрузит обложку альбома.
        /// </summary>
        /// <param name="track">Трек.</param>
        /// <param name="mbid">Идентификатор трека (если имеется).</param>
        private async void LoadAlbumImage(IAudioTrack track, string mbid = null)
        {
            TrackImage = new BitmapImage(new Uri("ms-appx:///Assets/Logo.scale-240.png"));

            TrackGetInfoRequest request = String.IsNullOrWhiteSpace(mbid) ?
                new TrackGetInfoRequest(track.Title, track.Artist) :
                new TrackGetInfoRequest(mbid);

            var response = await request.ExecuteAsync();
            if (response.IsValid() && response.ErrorType == LFErrors.None && 
                response.Track.Album != null && track == CurrentTrack)
            {
                if (!String.IsNullOrEmpty(response.Track.Album.MaxImage.URL))
                {
                    TrackImage = new BitmapImage(new Uri(response.Track.Album.MaxImage.URL));                    
                }
            }
        }

        private bool _isPopSended;

        /// <summary>
        /// Загрузить картинку исполнителя трека.
        /// </summary>
        /// <param name="track">Трек.</param>
        private async void LoadArtistsImage(IAudioTrack track)
        {
            var pop = new PopupMessage()
            {
                Title = CurrentTrack.Artist,
                Content = CurrentTrack.Title,
                Type = PopupMessageType.Info,
                Parameter = new NavigateToPageMessage()
                {
                    Page = AppViews.PlayerView
                }
            };

            Uri artistImageUri = await ImagesHelper.GetCachedArtistImage(track.Artist);
            if (track == CurrentTrack)
            {
                if (artistImageUri != null)
                {
                    ArtistImage = new BitmapImage(artistImageUri);
                    pop.ImageUrl = artistImageUri.ToString();
                    if (!_isPopSended && !_isOnView) Messenger.Default.Send(pop);
                    _isPopSended = true;
                    return;
                }

                ArtistImage = new BitmapImage();
            }
            else
                return;

            if (!_isPopSended && !_isOnView) Messenger.Default.Send(pop);
            _isPopSended = true;
            string artistUrl = null;

            artistUrl = await ServiceHelper.XboxMusicService.GetArtistImageURL(track.Artist, 480, 800);

            if (String.IsNullOrEmpty(artistUrl))
            {
                var response = await (new ArtistGetInfoRequest(track.Artist, false)).ExecuteAsync();
                if (response.ErrorType == LFErrors.None && response.IsValid())
                {
                    artistUrl = response.Artist.MegaImage.URL;
                    if (String.IsNullOrEmpty(artistUrl)) return;
                }
                else return;
            }

            artistImageUri = await ImagesHelper.CacheAndGetArtistsImage(track.Artist, artistUrl);
            if (artistImageUri != null && track == CurrentTrack)
            {
                ArtistImage = new BitmapImage(artistImageUri);
                return;
            }
        } 
        
        /// <summary>
        /// Перемешать треки или вернуть обратно.
        /// </summary>
        /// <param name="isOn"></param>
        private async Task ShuffleTracks(bool isOn, IAudioTrack currentTrack)
        {            
            if (!isOn)
            {
                if (_normalTrackList == null) return;
                Tracks = new ObservableCollection<IAudioTrack>(_normalTrackList);
                _normalTrackList = null;
                _currentTrackID = Tracks.IndexOf(_currentTrack);
                ServiceHelper.PlayerService.UpdateShuffleMode();
                return;
            }
            else if (Tracks == null) return;

            CanShuffle = false;
            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _normalTrackList = new List<IAudioTrack>(Tracks);
                var newTracks = new List<IAudioTrack>(Tracks);

                for (int i = 0; i < newTracks.Count; i++)
                {                    
                    int id = _random.Next(0, _normalTrackList.Count);
                    if (id == i) continue;

                    var track = newTracks[i];
                    newTracks[i] = newTracks[id];
                    newTracks[id] = track;
                }

                Tracks = new ObservableCollection<IAudioTrack>(newTracks);
                newTracks = null;

                int currentID = Tracks.IndexOf(currentTrack);
                if (currentID > 0)
                {
                    var track = Tracks[0];
                    Tracks[0] = Tracks[currentID];
                    Tracks[currentID] = track;
                }
                _currentTrackID = 0;
            });
            await ServiceHelper.PlayerService.SetNewShuffledPlaylist(Tracks);
            ServiceHelper.PlayerService.UpdateShuffleMode();
            CanShuffle = true;
        } 
        
        /// <summary>
        /// Инициализирует работу проигрывателя.
        /// </summary>
        private async Task Initialize()
        {
            if (_isInitialized) return;

            ServiceHelper.PlayerService.PlayerStateChanged += PlayerService_PlayerStateChanged;
            ServiceHelper.PlayerService.TrackIDChanged += PlayerService_TrackIDChanged;

            var normalList = await ServiceHelper.PlayerService.GetPlaylist();
            var shuffledList = await ServiceHelper.PlayerService.GetShuffledPlaylist();

            if (IsShuffleMode)
            {
                if (shuffledList != null && normalList != null)
                {
                    Tracks = new ObservableCollection<IAudioTrack>(shuffledList);
                    _normalTrackList = new List<IAudioTrack>(normalList);
                }
                else if (normalList != null)
                {
                    IsShuffleMode = false;
                    Tracks = new ObservableCollection<IAudioTrack>(normalList);
                }
                else
                    IsShuffleMode = false;
            }
            else if (normalList != null)
            {
                Tracks = new ObservableCollection<IAudioTrack>(normalList);
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Вызывается при изменении идентификатора текущего трека.
        /// </summary>
        private async void PlayerService_TrackIDChanged(object sender, int e)
        {
            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _isPopSended = false;
                _currentTrackID = e;
                var track = Tracks[_currentTrackID];
                if (CurrentTrack == track)
                    return;                
                CurrentTrack = track;
                ShowTrackLyrics.RaiseCanExecuteChanged();
                DownloadTrack.RaiseCanExecuteChanged();
                LoadTrackInfo(CurrentTrack);
            });            
        }

        /// <summary>
        /// Вызывается при изменении состояния проигрывателя.
        /// </summary>
        private async void PlayerService_PlayerStateChanged(object sender, PlayerState e)
        {
            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (e == PlayerState.Playing)
                {
                    //_timer.Start();
                    Duration = ServiceHelper.PlayerService.Duration;
                    RaisePropertyChanged(() => IsPlaying);
                }
                else
                {
                    //_timer.Stop();
                    RaisePropertyChanged(() => IsPlaying);
                }

                RaisePropertyChanged(() => IsPlayerWork);
            });            
        }
        #endregion
    }
}
