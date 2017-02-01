using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на сохранение ранее загруженной
    /// на сервер для отправки в сообщении фотографии.
    /// </summary>
    public class SaveMessagesPhotoRequest : BaseVKRequest<VKSaveMessagesPhotoObject>
    {
        /// <summary>
        /// Информация о фотографии, полученная после загрузки
        /// на сервер.
        /// </summary>
        public string Photo { get; private set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="photo">Информация о фотографии, полученная 
        /// после загрузки на сервер.</param>
        public SaveMessagesPhotoRequest(string photo)
        {
            Photo = photo;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["photo"] = Photo;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoSaveMessagesPhoto; }
    }
}
