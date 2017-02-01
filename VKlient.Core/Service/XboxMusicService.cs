using System;
using System.Threading.Tasks;
using Xbox.Music;
using Xbox.Music.Model.Filters;
using System.Globalization;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис для работы с Xbox Music.
    /// </summary>
    public class XboxMusicService
    {
        private const string ClientSecret = "SMiic6+O8YKa13ak4Jpd0+ncQaCTakYZWc7mQtJolFg=";
        private const string ClientName = "vksaver";

        private static readonly FilterType[] Artistfilter = new FilterType[] { FilterType.Artists };
        private readonly RegionInfo Region;
        private bool _isInitialized;
        private XboxMusicServiceClient _client;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public XboxMusicService()
        {
            _client = new XboxMusicServiceClient(ClientName, ClientSecret);
            Region = new RegionInfo("en-us");
        }

        /// <summary>
        /// Получить ключ доступа.
        /// </summary>
        private async Task GetAccessToken()
        {
            await _client.InitializeAccessTokenAsync();
            _isInitialized = true;
        }

        private async Task CheckInitialized()
        {
            if (!_isInitialized)
                await _client.InitializeAccessTokenAsync();
        }

        /// <summary>
        /// Получить изображение исполнителя.
        /// </summary>
        /// <param name="artistName"></param>
        /// <param name="width">Ширина изображения.</param>
        /// <param name="height">Высота изображения.</param>
        public async Task<string> GetArtistImageURL(string artistName, int width, int height)
        {
            string result = String.Empty;
            await CheckInitialized();
            try
            {
                var searchResponse = await _client.SearchAsync(artistName, Region, 1, Artistfilter);
                if (searchResponse != null && searchResponse.Artists != null && searchResponse.Artists.Items.Count != 0)
                {
                    var artist = searchResponse.Artists.Items[0];
                    result = artist.GetCustomImageUri(artist.Id, "en-us", PictureResizeMode.Crop, width, height).ToString();
                }
            }
            catch (ArgumentNullException)
            {
                _isInitialized = false;
            }
            return result;
        }
    }
}
