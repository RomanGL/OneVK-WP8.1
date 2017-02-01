using GalaSoft.MvvmLight.Command;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Photo;
using OneVK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel.Settings
{
    /// <summary>
    /// Представляет модель представления страницы общих настроек.
    /// </summary>
    public class CommonSettingsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public CommonSettingsViewModel()
        {
            _service = ServiceHelper.SettingsService;

            ClearMessagesCacheCommand = new RelayCommand(async () =>
            {
                ServiceHelper.MessagesService.Conversations.Clear();
                await ServiceHelper.MessagesCacheService.ClearConversationsCache();
            });
        }
        #endregion

        #region Приватные поля
        private SettingsService _service;
        #endregion

        #region Свойства
        /// <summary>
        /// Индекс выбранного типа Emoji-символов.
        /// </summary>
        public int EmojiTypeIndex
        {
            get { return (int)_service.EmojiType; }
            set
            {
                _service.EmojiType = (EmojiType)value;
                RaisePropertyChanged(() => EmojiTypeIndex);
            }
        }

        /// <summary>
        /// Включен ли режим невидимки.
        /// </summary>
        public bool StayOfflineMode
        {
            get { return _service.StayOfflineMode; }
            set
            {
                _service.StayOfflineMode = value;
                RaisePropertyChanged(() => StayOfflineMode);
            }
        }

        /// <summary>
        /// Экномичный к трафику режим.
        /// </summary>
        public bool EconomyMode
        {
            get { return _service.MaxPhotosSize == VKPhotoSizes.Photo130; }
            set
            {
                _service.MaxPhotosSize = value ? VKPhotoSizes.Photo130 : VKPhotoSizes.Photo807;
                RaisePropertyChanged(() => EconomyMode);
            }
        }

        /// <summary>
        /// Включить кэширование аудиозаписей.
        /// </summary>
        public bool EnableMusicCache
        {
            get { return _service.EnableMusicCache; }
            set
            {
                _service.EnableMusicCache = value;
                RaisePropertyChanged(() => EnableMusicCache);
            }
        }

        /// <summary>
        /// Включить кэширование изображений исполнителей.
        /// </summary>
        public bool EnableCacheArtistsImages
        {
            get { return _service.EnableCacheArtistsImages; }
            set
            {
                _service.EnableCacheArtistsImages = value;                
                RaisePropertyChanged(() => EnableCacheArtistsImages);
                RaisePropertyChanged(() => IsCacheImagesFromWiFiToggleSwitchAvailable);

                if (!value) EnableCacheArtistsImagesOnlyFromWiFi = false;
            }
        }

        /// <summary>
        /// Включить кэширование изображений исполнителей только через Wi-Fi.
        /// </summary>
        public bool EnableCacheArtistsImagesOnlyFromWiFi
        {
            get { return _service.EnableCacheArtistsImagesOnlyFromWiFi; }
            set
            {
                _service.EnableCacheArtistsImagesOnlyFromWiFi = value;
                RaisePropertyChanged(() => EnableCacheArtistsImagesOnlyFromWiFi);
            }
        }

        /// <summary>
        /// Включить рекламные акции в приложении.
        /// </summary>
        public bool EnablePromo
        {
            get { return _service.EnablePromo; }
            set
            {
                _service.EnablePromo = value;
                RaisePropertyChanged(() => EnablePromo);
            }
        }

        /// <summary>
        /// Доступна ли возможность включить кэширование изображений только через Wi-Fi.
        /// </summary>
        public bool IsCacheImagesFromWiFiToggleSwitchAvailable
        {
            get { return EnableCacheArtistsImages; }
        }
        #endregion

        #region Команды

        /// <summary>
        /// Команда очистки кэша сообщений.
        /// </summary>
        public RelayCommand ClearMessagesCacheCommand { get; private set; }

        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
