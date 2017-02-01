using OneVK.Model.Audio;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение списка альбомов
    /// с аудиозаписям пользователя или сообщества.
    /// </summary>
    public class GetAudioAlbumsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKAudioAlbum>>
    {
        /// <summary>
        /// Идентификатор пользователя, альбмы с аудиозаписями которого
        /// нужно получить.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetAudioAlbumsRequest()
        {
            MaxCount = 100;
            DefaultCount = 50;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetAlbums; }
    }
}
