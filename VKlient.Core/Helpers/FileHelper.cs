using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет помощник работы с файлами.
    /// </summary>
#if PLAYER_TASK
    internal static class FileHelper
#else
    public static class FileHelper
#endif
    {
        /// <summary>
        /// Возвращает файл по его имени или null, если файл отсутствует.
        /// </summary>
        /// <param name="fileName">Имя файла, включая расширение.</param>
        public static async Task<StorageFile> GetLocalFileFromName(string fileName)
        {
            try
            {
                return await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Создает указанный файл в локальной папке приложения и возвращает его.
        /// Возвращает null при ошибке.
        /// </summary>
        /// <param name="fileName">Имя файла, включая расширение.</param>
        public static async Task<StorageFile> CreateLocalFile(string fileName)
        {
            try
            {
                return await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Записывает указанный текст в файл и возвращает успешность операции.
        /// </summary>
        /// <param name="file">Файл для записи.</param>
        /// <param name="textToWrite">Текст для записи.</param>
        public static async Task<bool> WriteTextToFile(StorageFile file, string textToWrite)
        {
            try
            {
                await FileIO.WriteTextAsync(file, textToWrite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Считывает текст из переданного файла.
        /// </summary>
        /// <param name="file">Файл для считывания.</param>
        public static async Task<string> ReadTextFromFile(StorageFile file)
        {
            try
            {
                return await FileIO.ReadTextAsync(file);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Возвращает поток для чтения из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public static async Task<Stream> GetFileStreamForRead(Uri path)
        {
            try
            {
                return (await (await StorageFile.GetFileFromApplicationUriAsync(path))
                        .OpenReadAsync()).AsStreamForRead();
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Возвращает поток файла, считанного в память.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public static async Task<MemoryStream> GetFileMemoryStream(Uri path)
        {
            var stream = await GetFileStreamForRead(path);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, (int)stream.Length);

            return new MemoryStream(buffer);
        }
    }
}
