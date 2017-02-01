using OneVK.Model.Video;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка видеозаписей, на которых есть непросмотренные отметки.
    /// </summary>
    public class VideoGetNewTagsRequest : BaseVKCountedRequest<VKVideoTagged>
    {
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public VideoGetNewTagsRequest(ulong videoID)
        {
            MaxCount = 100;
            DefaultCount = 20;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetNewTags; }
    }
}
