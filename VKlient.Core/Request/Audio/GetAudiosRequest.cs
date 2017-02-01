using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Response;
using OneVK.Model.Audio;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение списка аудиозаписей 
    /// пользователя или сообщества.
    /// </summary>
    public class GetAudiosRequest : BaseVKCountedRequest<VKCountedItemsObject<VKAudio>>
    {
        private List<long> _audios;
        private long _albumID;

        /// <summary>
        /// Идентификатор владельца аудиозаписей.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор альбома с аудиозаписями.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("AlbumID",
                        "Идентификатор альбома не может быть отрицательным числом.");
                _albumID = value;
            }
        }

        /// <summary>
        /// Идентификаторы аудиозаписей, информацию о которых необходимо вернуть.
        /// </summary>
        public List<long> Audios
        {
            get { return _audios; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Audios",
                        "Коллекция должна быть инициализирована и должна содержать хотябы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Audios",
                        "Коллекция должна содержать хотя бы один элемент.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("Audios",
                        "Идентификатор аудиозаписи не может быть отрицательным числом.");
                _audios = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GetAudiosRequest()
        {
            MaxCount = 6000;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();
            if (Audios != null && Audios.Count != 0) parameters["audio_ids"] = String.Join(",", Audios);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGet; }
    }
}
