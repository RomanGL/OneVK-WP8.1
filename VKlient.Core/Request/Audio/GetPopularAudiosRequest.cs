using System.Collections.Generic;
using OneVK.Enums.Audio;
using OneVK.Enums.Common;
using OneVK.Model.Audio;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение популярных аудиозаписей.
    /// </summary>
    public class GetPopularAudiosRequest : BaseVKCountedRequest<List<VKAudio>>
    {
        /// <summary>
        /// Возвращать только зарубежные аудиозаписи.
        /// </summary>
        public VKBoolean OnlyEng { get; set; }

        /// <summary>
        /// Аудиозаписи какого жанра необходимо получить.
        /// </summary>
        public VKAudioGenre Genre { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetPopularAudiosRequest()
        {
            MaxCount = 1000;
            DefaultCount = 100;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OnlyEng == VKBoolean.True) parameters["only_eng"] = "1";
            if (Genre != VKAudioGenre.Instrumental) parameters["genre_id"] = ((byte)Genre).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetPopular; }
    }
}
