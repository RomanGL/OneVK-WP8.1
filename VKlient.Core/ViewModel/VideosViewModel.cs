using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Video;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы списка видеозаписей пользователя или сообщества.
    /// </summary>
    public class VideosViewModel : BaseKeyedViewModel<VideosViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public VideosViewModel(string uniqueKey, long ownerID)
            : base(uniqueKey)
        {
            _videos = new VideosCollection(ownerID);
            _albums = new VideoAlbumsCollection(ownerID);
            Refresh = new RelayCommand(() => Videos.Refresh());
            RefreshAlbums = new RelayCommand(() => Albums.Refresh());
            DeleteCommand = new RelayCommand<VKVideoBase>(async video =>
            {
                var request = new Request.VideoDeleteRequest(video.ID, video.OwnerID, (long)ServiceHelper.SettingsService.AccessToken.UserID);
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                    Videos.Remove(video);
                else
                    await ServiceHelper.DialogService.ShowMessageBox("Произошла ошибка: " + response.Error.ErrorType.ToString(),
                        "Не удалось удалить видеозапись");
            });
            DeleteAlbumCommand = new RelayCommand<VKVideoAlbumExtended>(async album =>
            {
                var request = new Request.VideoDeleteAlbumRequest((ulong)album.ID);
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                    Albums.Remove(album);
                else
                    await ServiceHelper.DialogService.ShowMessageBox("Произошла ошибка: " + response.Error.ErrorType.ToString(),
                        "Не удалось удалить видеоальбом");
            });
        }
        #endregion

        #region Приватные поля
        private VideosCollection _videos;
        private VideoAlbumsCollection _albums;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция списка видеозаписей.
        /// </summary>
        public VideosCollection Videos { get { return _videos; } }
        /// <summary>
        /// Коллекция списка видеоальбомов.
        /// </summary>
        public VideoAlbumsCollection Albums { get { return _albums; } }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления списка видеозаписей.
        /// </summary>
        public RelayCommand Refresh { get; private set; }
        /// <summary>
        /// Команда обновления списка альбомов.
        /// </summary>
        public RelayCommand RefreshAlbums { get; private set; }
        /// <summary>
        /// Команда удаления видеозаписи.
        /// </summary>
        public RelayCommand<VKVideoBase> DeleteCommand { get; private set; }
        /// <summary>
        /// Команда удаления видеоальбома.
        /// </summary>
        public RelayCommand<VKVideoAlbumExtended> DeleteAlbumCommand { get; private set; }
        #endregion

        #region Приватные методы
        #endregion

        #region Публичные методы
        #endregion
    }
}
