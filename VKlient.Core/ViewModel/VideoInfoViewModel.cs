using GalaSoft.MvvmLight.Command;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Video;
using OneVK.Request;
using System.Collections.Generic;
using VKSaver.Commands.SDK;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы сведений о видеозаписи.
    /// </summary>
    public class VideoInfoViewModel : BaseKeyedViewModel<VideoInfoViewModel>, IContentViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public VideoInfoViewModel(string uniqueKey, VKVideoBase video)
            : base(uniqueKey, 1)
        {
            _video = video;
            OpenVideoCommand = new RelayCommand(async () =>
            {
                var command = new VKSaverOpenVKVideoCommand()
                {
                    VideoID = _video.ID,
                    OwnerID = _video.OwnerID,
                    AccessKey = _video.AccessKey,
                    AccessToken = ServiceHelper.SettingsService.AccessToken.AccessToken
                };
                await command.TryExecute();
            });
        }
        #endregion

        #region Приватные поля
        private VKVideoBase _video;
        private ContentState _videoInfoState;
        #endregion

        #region Свойства
        /// <summary>
        /// Загружены ли данные в модель представления.
        /// </summary>
        public bool IsLoaded
        {
            get { return GetIsLoaded(VideoInfoState); }
        }
        /// <summary>
        /// Выполняется ли в данный момент загрузка данных.
        /// </summary>
        public bool IsLoading
        {
            get { return VideoInfoState == ContentState.Loading; }
        }
        /// <summary>
        /// Состояние загрузки информации о сообществе.
        /// </summary>
        public ContentState VideoInfoState
        {
            get { return _videoInfoState; }
            private set { Set(() => VideoInfoState, ref _videoInfoState, value); }
        }
        /// <summary>
        /// Видеозапись, сведения о которой отображаются в данный момент.
        /// </summary>
        public VKVideoBase Video
        {
            get { return _video; }
            private set { Set(() => Video, ref _video, value); }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда открытия видеозаписи.
        /// </summary>
        public RelayCommand OpenVideoCommand { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            LoadData();
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Загружает информацию о видеозаписи.
        /// </summary>
        private async void LoadData()
        {
            if (IsLoaded || IsLoading) return;

            Dictionary<long, ulong> videos = new Dictionary<long, ulong>();
            videos.Add(_video.OwnerID, (ulong)_video.ID);

            VideoInfoState = ContentState.Loading;
            var response = await new VideoGetExtendedRequest(videos).ExecuteAsync();
            if (response.Error.ErrorType == VKErrors.None)
            {
                if (response.Response.Count != 0)
                {
                    VideoInfoState = ContentState.Normal;
                }
                else
                    VideoInfoState = ContentState.NoData;
            }
            else
                VideoInfoState = ContentState.Error;
        }
        #endregion
    }
}
