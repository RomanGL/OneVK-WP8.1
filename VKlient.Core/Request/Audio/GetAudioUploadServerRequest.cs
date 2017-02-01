using OneVK.Model.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой завпрос на получение информации
    /// о сервере, чтобы загрузить на него аудиозапись.
    /// </summary>
    public class GetAudioUploadServerRequest : BaseVKRequest<VKUploadServerBase>
    {
        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetUploadServer; }
    }
}
