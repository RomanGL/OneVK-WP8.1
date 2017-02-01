using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на сохранение главной 
    /// фотографии пользователя или сообщества.
    /// </summary>
    public class SaveOwnerPhotoRequest : BaseVKRequest<VKSaveOwnerPhotoObject>
    {
        /// <summary>
        /// Номер сервера.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Параметр хэша фотографии.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Идентификатор фотографии.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(Server)) parameters["server"] = Server;
            if (!String.IsNullOrWhiteSpace(Hash)) parameters["hash"] = Hash;
            if (!String.IsNullOrWhiteSpace(Photo)) parameters["photo"] = Photo;

            return parameters;
        }

        /// <summary>
        /// Возвращает свзанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoSaveOwnerPhoto; }
    }
}
