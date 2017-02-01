using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using OneVK.Model.Photo;
using OneVK.ViewModel.Messages;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using OneVK.Request;
using OneVK.Enums.Common;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления страницы просмотра изображений.
    /// </summary>
    public class ImageViewModel : BaseKeyedViewModel<ImageViewModel>
    {
        private const string TotalNumberPropertyName = "TotalNumber";

        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр модели представления.
        /// </summary>
        /// <param name="uniqueKey">Уникальный ключ модели представления.</param>
        /// <param name="photos">Кортеж с необходимыми данными. Коллекция всех фотографий и индекс нужной.</param>
        public ImageViewModel(string uniqueKey, Tuple<List<VKPhotoExtended>, int> photos)
            : base(uniqueKey, 0)
        {
            Photos = new ObservableCollection<VKPhotoExtended>(photos.Item1);
            CurrentIndex = photos.Item2;

            ChangePhotoSize = new RelayCommand<string>(e =>
            {
                switch (e)
                {
                    case "75":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo75;
                        break;
                    case "130":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo130;
                        break;
                    case "604":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo604;
                        break;
                    case "807":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo807;
                        break;
                    case "1280":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo1280;
                        break;
                    case "2560":
                        CurrentPhoto.CurrentSourceSize = VKPhotoSizes.Photo2560;
                        break;
                }
            });

            LoadLikes();
        }
        #endregion

        #region Приватные поля
        private ObservableCollection<VKPhotoExtended> _photos;
        private int _currentIndex;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция фотографий.
        /// </summary>
        public ObservableCollection<VKPhotoExtended> Photos
        {
            get { return _photos; }
            private set { Set(() => Photos, ref _photos, value); }
        }
        /// <summary>
        /// Индекс текущей фотографии.
        /// </summary>
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                Set(() => CurrentIndex, ref _currentIndex, value);
                RaisePropertyChanged(() => CurrentPhoto);                
            }
        }
        /// <summary>
        /// Общее число фотографий.
        /// </summary>
        public int TotalNumber { get { return Photos.Count; } }
        /// <summary>
        /// Текущая фотография.
        /// </summary>
        public VKPhotoExtended CurrentPhoto { get { return Photos[CurrentIndex]; } }
        #endregion

        #region Команды
        /// <summary>
        /// Команда изменения размера текущей фотографии.
        /// </summary>
        public RelayCommand<string> ChangePhotoSize { get; private set; }
        #endregion

        #region Публичные методы
        private async void LoadLikes()
        {
            var photos = new List<string>(Photos.Count);
            foreach (var photo in Photos)
            {
                if (String.IsNullOrEmpty(photo.AccessKey))
                    photos.Add(photo.OwnerID + "_" + photo.ID);
                else
                    photos.Add(photo.OwnerID + "_" + photo.ID + "_" + photo.AccessKey);
            }

            var request = new GetPhotosByIDExtendedRequest(photos);
            bool success = false;
            byte numOfRetries = 0;

            while (!success && numOfRetries <= 3)
            {
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                {
                    success = true;
                    for (int i = 0; i < response.Response.Count; i++)
                    {
                        Photos[i].CommentsCount = response.Response[i].CommentsCount;
                        Photos[i].CanComment = response.Response[i].CanComment;
                        Photos[i].Comments = response.Response[i].Comments;
                        Photos[i].Likes = response.Response[i].Likes;
                    }
                }
                else
                {
                    numOfRetries++;
                    if (numOfRetries <= 3)
                        await Task.Delay(3000 * numOfRetries);
                }
            }            
        }
        #endregion

        #region Приватные методы
        #endregion
    }
}
