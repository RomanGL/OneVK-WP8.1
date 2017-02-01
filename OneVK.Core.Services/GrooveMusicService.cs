using OneVK.Core.Models.AppNotifications;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xbox.Music;
using Xbox.Music.Model.Filters;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы с Groove Music.
    /// </summary>
    public sealed class GrooveMusicService : IGrooveMusicService
    {
        private const string CLIENT_SECRET = null;    // TODO.
        private const string CLIENT_NAME = null;    // TODO.

        private static readonly FilterType[] Artistfilter = new FilterType[] { FilterType.Artists };
        private readonly RegionInfo Region = new RegionInfo("en-us");
        private readonly XboxMusicServiceClient client;
        private bool isInitialized;

        private IAppNotificationsService appNotificationService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GrooveMusicService"/>.
        /// </summary>
        /// <param name="appNotificationService">Сервис внутренних уведомлений.</param>
        public GrooveMusicService(IAppNotificationsService appNotificationService)
        {
            this.appNotificationService = appNotificationService;
            client = new XboxMusicServiceClient(CLIENT_NAME, CLIENT_SECRET);
        }

        /// <summary>
        /// Проверяет инициализацию клиента.
        /// </summary>
        private async Task<bool> CheckInitialized()
        {
            try
            {
                if (!isInitialized)
                {
                    if (String.IsNullOrEmpty(CLIENT_NAME) || String.IsNullOrEmpty(CLIENT_SECRET))
                    {
                        var notification = new AppNotification
                        {
                            Type = AppNotificationType.Error,
                            Title = "Невозможно инициализировать GrooveMusicService",
                            Content = "CLIENT_NAME или CLIENT_SECRET не заданы"
                        };
                        appNotificationService.SendNotification(notification);
                        isInitialized = false;
                        return false;
                    }
                    await client.InitializeAccessTokenAsync();
                }
                isInitialized = true;
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Получить изображение исполнителя.
        /// </summary>
        /// <param name="artistName">Имя исполнителя.</param>
        public async Task<string> GetArtistImageURL(string artistName)
        {
            if (!await CheckInitialized()) return null;
            string result = null;

            try
            {
                var searchResponse = await client.SearchAsync(artistName, Region, 1, Artistfilter);
                if (searchResponse != null && searchResponse.Artists != null && searchResponse.Artists.Items.Count != 0)
                {
                    var artist = searchResponse.Artists.Items[0];
                    return artist.ImageUrl;
                    //result = artist.GetCustomImageUri(artist.Id, "en-us", PictureResizeMode.Crop, width, height).ToString();
                }
            }
            catch (ArgumentNullException)
            {
                isInitialized = false;
            }
            return result;
        }

        /// <summary>
        /// Возвращает ссылку на изображение трека.
        /// </summary>
        /// <param name="trackName">Название трека.</param>
        /// <param name="artistName">Имя исполнителя.</param>
        public async Task<string> GetTrackImageUrl(string trackName, string artistName)
        {            
            if (!await CheckInitialized()) return null;
            string result = null;

            return result;
        }
    }
}
