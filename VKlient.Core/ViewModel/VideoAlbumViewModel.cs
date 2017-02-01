using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Video;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления страницы видеоальбома.
    /// </summary>
    public class VideoAlbumViewModel : BaseKeyedViewModel<VideoAlbumViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public VideoAlbumViewModel(string uniqueKey, VKVideoAlbumExtended album)
            : base(uniqueKey)
        {
            Album = album;
            _videos = new VideoAlbumViewCollection(album.OwnerID, album.ID);
            RefreshCommand = new RelayCommand(() => Videos.Refresh());
            DeleteCommand = new RelayCommand<VKVideoBase>(async video =>
            {
                var request = new Request.VideoRemoveFromAlbumRequest((ulong)video.ID, video.OwnerID, (ulong)album.ID);
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                    Videos.Remove(video);
                else
                    await ServiceHelper.DialogService.ShowMessageBox("Произошла ошибка: " + response.Error.ErrorType.ToString(),
                        "Не удалось удалить видеозапись");
            });
        }
        #endregion

        #region Приватные поля
        private VKVideoAlbumExtended _album;
        private VideoAlbumViewCollection _videos;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция видеоальбома.
        /// </summary>
        public VideoAlbumViewCollection Videos { get { return _videos; } }
        /// <summary>
        /// Видеоальбом.
        /// </summary>
        public VKVideoAlbumExtended Album
        {
            get { return _album; }
            private set { Set(() => Album, ref _album, value); }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления списка видеозаписей.
        /// </summary>
        public RelayCommand RefreshCommand { get; private set; }
        /// <summary>
        /// Команда удаления видеозаписи из видеоальбома.
        /// </summary>
        public RelayCommand<VKVideoBase> DeleteCommand { get; private set; }
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
