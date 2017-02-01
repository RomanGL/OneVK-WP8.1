using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Web.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет помощник работы с изображениями.
    /// Позволяет кэшировать изображения и получть их из кэша. 
    /// </summary>
    public static class ImagesHelper
    {
        private const string ArtistsFolderName = "Artists";

        /// <summary>
        /// Возвращает путь к кэшированному изображению исполнителя.
        /// </summary>
        /// <param name="artistName">Имя исполнителя, картинку которого нужно получить.</param>
        public static async Task<Uri> GetCachedArtistImage(string artistName)
        {
            try
            {
                var artistsFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ArtistsFolderName, CreationCollisionOption.OpenIfExists);
                var file = await artistsFolder.GetFileAsync(artistName.ToLower() + ".jpg");
                var size = (await file.GetBasicPropertiesAsync()).Size;
                if (size > 0)
                    return new Uri(file.Path);
                await file.DeleteAsync();
                return null;
            }
            catch (Exception)
            {
                return null;
            } 
        } 

        /// <summary>
        /// Кэширует указанное изображение и возвращает путь к его кэшированной копии.
        /// </summary>
        /// <param name="artistName">Имя исполнителя.</param>
        /// <param name="imageUrl">Ссылка на изображение.</param>
        public static async Task<Uri> CacheAndGetArtistsImage(string artistName, string imageUrl)
        {
            StorageFile file = null;
            try
            {
                var artistsFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ArtistsFolderName, CreationCollisionOption.OpenIfExists);
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(imageUrl));
                    file = await artistsFolder.CreateFileAsync(artistName.ToLower() + ".jpg", CreationCollisionOption.FailIfExists);
                    await FileIO.WriteBufferAsync(file, await response.Content.ReadAsBufferAsync());
                    var pr = await file.GetBasicPropertiesAsync();
                    return new Uri(file.Path);
                }
            }
            catch (Exception) { }

            try
            {
                if (file != null)
                    await file.DeleteAsync();
            }
            catch (Exception) { }

            return null;
        }
    }
}
