using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;
using OneVK.Helpers;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления раздела фотографий.
    /// </summary>
    public class PhotosViewModel : BaseKeyedViewModel<PhotosViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="uniqueKey">Уникальный ключ модели представления.</param>
        /// <param name="ownerID">Идентификатор пользователя 
        /// или сообщества.</param>
        public PhotosViewModel(string uniqueKey, long ownerID)
            : base(uniqueKey)
        {
            _albums = new PhotoAlbumsCollection(ownerID);
            RefreshAlbums = new RelayCommand(() => Albums.Refresh());

#if DEBUG
            if (IsInDesignModeStatic)
            {
                for (int i = 0; i < 50; i++)
                {
                    Albums.Add(DesignDataHelper.GetPhotoAlbum());
                }
            }
#endif
        }
        #endregion

        #region Приватные поля
        private PhotoAlbumsCollection _albums;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция фотоальбомов.
        /// </summary>
        public PhotoAlbumsCollection Albums { get { return _albums; } }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления коллекции альбомов.
        /// </summary>
        public RelayCommand RefreshAlbums { get; private set; }
        #endregion

        #region Приватные методы
        #endregion

        #region Публичные методы
        #endregion
    }
}
