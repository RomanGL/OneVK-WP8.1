using OneVK.Model.Photo;

namespace OneVK.Request.Photos
{
    /// <summary>
    /// Представляет запрос на получение информации о сервере
    /// для последующей загрузки на него фотография для
    /// отправки в сообщении.
    /// </summary>
    public class GetPhotoMessagesUploadServerRequest : BaseVKRequest<VKPhotoUploadServer>
    {
        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetMessagesUploadServer; }
    }
}
