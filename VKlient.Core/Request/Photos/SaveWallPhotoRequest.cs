using OneVK.Model.Photo;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на сохранение фотографии
    /// на стену пользователя или сообщества.
    /// </summary>
    public class SaveWallPhotoRequest : BaseVKRequest<VKPhoto>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, на
        /// стену которого необходимо сохранить фотографию.
        /// </summary>
        public long ID { get; private set; }

        /// <summary>
        /// Параметр фотографии.
        /// </summary>
        public string Photo { get; private set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public uint Server { get; set; }

        /// <summary>
        /// Парамерт хэша фотографии.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="photo">Параметр фотографии, который был получен
        /// после загрузки фотогарфии на сервер.</param>
        /// <param name="ID">Идентификатор фользователя или сообщества,
        /// на стену которого нужно сохранить фотографию.</param>
        private SaveWallPhotoRequest(string photo, long id)
        {
            Photo = photo;
            ID = id;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["photo"] = Photo;
            if (ID > 0) parameters["user_id"] = ID.ToString();
            else if (ID < 0) parameters["group_id"] = ID.ToString();
            if (Server != 0) parameters["server"] = Server.ToString();
            if (!String.IsNullOrWhiteSpace(Hash)) parameters["hash"] = Hash;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoSaveWallPhoto; }
    }
}
