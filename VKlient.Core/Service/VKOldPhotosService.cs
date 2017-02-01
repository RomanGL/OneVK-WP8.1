using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Enums.Common;
using OneVK.Enums.Newsfeed;
using OneVK.Enums.Photo;
using OneVK.Helpers;
using OneVK.Model.Comment;
using OneVK.Model.Common;
using OneVK.Model.Photo;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Сервис для работы с фотографиями ВКонтакте.
    /// </summary>
    public sealed class VKOldPhotosService
    {
        /// <summary>
        /// Выполнить поиск по фотографиям.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="q">Строка поискового запроса.</param>
        /// <param name="lat">Географическая широта отметки, заданная в градусах.</param>
        /// <param name="long">Географическая долгота отметки, заданная в градусах.</param>
        /// <param name="start">Время в формате unixtime, не раньше которого должны были 
        /// быть загружены найденные фотографии.</param>
        /// <param name="end">Время в формате unixtime, не позже которого должны были 
        /// быть загружены найденные фотографии.</param>
        /// <param name="sort">Сортировка результатов поиска.</param>
        /// <param name="offset">Смещение относительно первой найденной фотографии 
        /// для выборки определенного подмножества.</param>
        /// <param name="count">Количество фотографий, которое нужно вернуть.</param>
        /// <param name="radius">Радиус поиска в метрах.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Search(Action<VKResponse<VKCountedItemsObject<VKPhoto>>> callback,
            string q, double lat = 0, double @long = 0, long start = 0, long end = 0, VKPhotoSortDateLikes sort = VKPhotoSortDateLikes.ByDate, 
            int offset = 0, int count = 0, VKPhotoSearchRadius radius = VKPhotoSearchRadius.FiveThousand)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException("lat", "Географическая широта отметки должна быть не меньше -90 и не больше 90.");
            if (@long < -180 || @long > 180)
                throw new ArgumentOutOfRangeException("@long", "Географическая долгота отметки должна быть не меньше - 180 и не больше 180.");

            var parameters = new Dictionary<string, string>();

            if (!String.IsNullOrWhiteSpace(q)) parameters["q"] = q;
            if (lat != 0) parameters["lat"] = lat.ToString();
            if (@long != 0) parameters["long"] = @long.ToString();
            if (start != 0) parameters["start_time"] = start.ToString();
            if (end != 0) parameters["end_time"] = end.ToString();
            if (sort != VKPhotoSortDateLikes.ByDate) parameters["sort"] = "1";
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (radius != VKPhotoSearchRadius.FiveThousand) parameters["radius"] = radius.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKPhoto>>(VKMethodsConstants.PhotoSearch, parameters, callback);
        }

        /// <summary>
        /// Сохраняет фотографии после успешной загрузки на сервер.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photos">Словарь с параметрами, возвращаемыми в результате загрузки фотографий на сервер.</param>
        /// <param name="caption">Описание фотографии.</param>
        /// <param name="hash">Параметр, возвращаемый в результате загрузки фотографий на сервер.</param>
        /// <param name="albumID">Идентификатор альбома, в который нужно сохранить фотографии.</param>
        /// <param name="groupID">Идентификатор сообщества, в которое нужно сохранить фотографии.</param>
        /// <param name="server">Параметр, возвращаемый в результате загрузки фотографий на сервер.</param>
        /// <param name="lat">Географическая широта отметки, заданная в градусах.</param>
        /// <param name="long">Географическая долгота отметки, заданная в градусах.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void Save(Action<VKResponse<VKCountedItemsObject<VKPhoto>>> callback,
            Dictionary<string, string> photos = null, string caption = null, string hash = null, long albumID = 0, 
            long groupID = 0, int server = 0, double lat = 0, double @long = 0)
        {
            if (photos == null)
                throw new ArgumentNullException("photos", "Словарь с идентификаторами фотографий не может быть пустым.");
            if (photos.Count == 0)
                throw new ArgumentOutOfRangeException("photos", "Словарь с идентификаторами фотографий не может быть пустым.");
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException("lat", "Географическая широта отметки должна быть не меньше -90 и не больше 90.");
            if (@long < -180 || @long > 180)
                throw new ArgumentOutOfRangeException("@long", "Географическая долгота отметки должна быть не меньше - 180 и не больше 180.");

            var parameters = new Dictionary<string, string>();

            if (lat != 0) parameters["latitude"] = lat.ToString();
            if (@long != 0) parameters["longitude"] = @long.ToString();
            if (albumID != 0) parameters["album_id"] = albumID.ToString();
            if (groupID != 0) parameters["group_id"] = groupID.ToString();
            if (server != 0) parameters["server"] = server.ToString();
            if (!String.IsNullOrWhiteSpace(hash)) parameters["hash"] = hash;
            if (!String.IsNullOrWhiteSpace(caption)) parameters["caption"] = caption;

            if (photos.Count != 0) parameters["photos_list"] = String.Join(",",
                 Enumerable.Select<KeyValuePair<string, string>, string>(photos,
                 (Func<KeyValuePair<string, string>, string>)
                 (kp => kp.Key.ToString() + "_" + kp.Value)));

            VKHelper.GetResponse<VKCountedItemsObject<VKPhoto>>(VKMethodsConstants.PhotoSave, parameters, callback);
        }

        /// <summary>
        /// Сохраняет фото в альбом "Сохраненные фотографии" пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="key">Специальный код доступа для приватных фотографий.</param>
        /// <param name="ownerID">Идентификатор владельца фотогарфии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Copy(Action<VKResponse<long>> callback,
            long ownerID, long photoID, string key = null)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["owner_id"] = ownerID.ToString();
            parameters["photo_id"] = photoID.ToString();
            if (!String.IsNullOrWhiteSpace(key)) parameters["access_key"] = key;

            VKHelper.GetResponse<long>(VKMethodsConstants.PhotoCopy, parameters, callback);
        }

        /// <summary>
        /// Изменить описание фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="caption">Новое описание фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Edit(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, string caption = null, long ownerID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (!String.IsNullOrWhiteSpace(caption)) parameters["caption"] = caption;

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoEdit, parameters, callback);
        }

        /// <summary>
        /// Переместить фотографию в другой альбом.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="targetID">Идентификатор альбома, в который нужно переметить фотографию.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Move(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, long targetID, long ownerID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            parameters["target_album_id"] = targetID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoMove, parameters, callback);
        }

        /// <summary>
        /// Сделать фотографию обложкой альбома.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MakeCover(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, long ownerID = 0, long albumID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (albumID != 0) parameters["album_id"] = albumID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoMakeCover, parameters, callback);
        }

        /// <summary>
        /// Изменить порядок альбомов в списке пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца альбома.</param>
        /// <param name="albumID">Идентификатор ал</param>
        /// <param name="before">Идентификатор альбома, перед которым следует поместить альбом.</param>
        /// <param name="after">Идентификатор альбома, после которого следует поместить альбом.</param>
        public void ReorderAlbums(Action<VKResponse<VKOperationIsSuccess>> callback,
            long albumID, long ownerID = 0, int before = 0, int after = 0)
        {
            var parameters = new Dictionary<string, string>();

            parameters["album_id"] = albumID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (before != 0) parameters["before"] = before.ToString();
            if (after != 0) parameters["after"] = after.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoReorderAlbums, parameters, callback);
        }

        /// <summary>
        /// Изменить порядок фотографий в списке пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="before">Идентификатор фотографии, перед которой следует поместить фотографию.</param>
        /// <param name="after">Идентификатор фотографии, после которой следует поместить фотографию.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ReorderPhotos(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, long ownerID = 0, int before = 0, int after = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (before != 0) parameters["before"] = before.ToString();
            if (after != 0) parameters["after"] = after.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoReorderPhotos, parameters, callback);
        }

        /// <summary>
        /// Удалить альбом пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, в котором находится альбом.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteAlbum(Action<VKResponse<VKOperationIsSuccess>> callback,
            long albumID , long groupID = 0)
        {
            if (groupID < 0)
                throw new ArgumentOutOfRangeException("groupID", "Идентификатор сообщества не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["album_id"] = albumID.ToString();
            if (groupID != 0) parameters["group_id"] = groupID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoDeleteAlbum, parameters, callback);
        }

        /// <summary>
        /// Удалить фотографию пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии, которую нужно удалить.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Delete(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, long ownerID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.AudioDelete, parameters, callback);
        }

        /// <summary>
        /// Подтвердить отметку на фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владелца фотграфии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="tagID">Идентификатор отметки.</param>
        public void ConfirmTag(Action<VKResponse<VKOperationIsSuccess>> callback,
            long ownerID, long tagID, long photoID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (tagID < 0)
                throw new ArgumentOutOfRangeException("tagID", "Идентификатор отметки на фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["tag_id"] = tagID.ToString();
            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoComfirmTag, parameters, callback);
        }

        /// <summary>
        /// Возвращает список фотографий, на которых отмечен пользователь.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="sort">Сортировка результатов.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetUserPhotos(Action<VKResponse<VKCountedItemsObject<VKPhoto>>> callback,
            long userID = 0, int offset = 0, int count = 0, VKSortByDate sort = VKSortByDate.desc)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");
            if (userID < 0)
                throw new ArgumentOutOfRangeException("userID", "Идентификатор пользователя не можеть быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            if (userID != 0) parameters["user_id"] = userID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (sort != VKSortByDate.desc) parameters["sort"] = "1";

            VKHelper.GetResponse<VKCountedItemsObject<VKPhoto>>(VKMethodsConstants.PhotoGetUserPhotos, parameters, callback);
        }

        /// <summary>
        /// Возвращает список фотографий, на которых отечен пользователь с развернутой информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="sort">Сортировка результатов.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetUserPhotosExtended(Action<VKResponse<VKCountedItemsObject<VKPhotoExtended>>> callback,
            long userID = 0, int offset = 0, int count = 0, VKSortByDate sort = VKSortByDate.desc)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");
            if (userID < 0)
                throw new ArgumentOutOfRangeException("userID", "Идентификатор пользователя не можеть быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            if (userID != 0) parameters["user_id"] = userID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (sort != VKSortByDate.desc) parameters["sort"] = "1";
            parameters["extended"] = "1";

            VKHelper.GetResponse<VKCountedItemsObject<VKPhotoExtended>>(VKMethodsConstants.PhotoGetUserPhotos, parameters, callback);
        }

        /// <summary>
        /// Возвращает все фотографии пользователя или сообщества в антихронологическом порядке.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, фотографии которого нужно получить.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="system">Нужно ли возвращать системные альбомы. По умолчанию включено.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetAll(Action<VKResponse<VKCountedItemsObject<VKPhoto>>> callback,
            long ownerID = 0, int offset = 0, int count = 0, VKAlbumNeedSystem system = VKAlbumNeedSystem.True)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (system == VKAlbumNeedSystem.False) parameters["no_service_albums"] = "1";

            VKHelper.GetResponse<VKCountedItemsObject<VKPhoto>>(VKMethodsConstants.PhotoGetAll, parameters, callback);
        }

        /// <summary>
        /// Возвращает все фотографии пользователя или сообщества в антихронологическом порядке с развернутой информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, фотографии которого нужно получить.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="system">Нужно ли возвращать системные альбомы. По умолчанию включено.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetAllExtended(Action<VKResponse<VKCountedItemsObject<VKPhotoExtended>>> callback,
            long ownerID = 0, int offset = 0, int count = 0, VKAlbumNeedSystem system = VKAlbumNeedSystem.True)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (system == VKAlbumNeedSystem.False) parameters["no_service_albums"] = "1";
            parameters["extended"] = "1";

            VKHelper.GetResponse<VKCountedItemsObject<VKPhotoExtended>>(VKMethodsConstants.PhotoGetAll, parameters, callback);
        }

        /// <summary>
        /// Удалить комментарий к фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="commentID">Идентификатор комментария.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteComment(Action<VKResponse<VKOperationIsSuccess>> callback,
            long commentID, long ownerID = 0)
        {
            if (commentID < 0)
                throw new ArgumentOutOfRangeException("commentID", "Идентификатор комментария не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["comment_id"] = commentID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoDeleteComment, parameters, callback);
        }

        /// <summary>
        /// Восстановить комментарий фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="commentID">Идентификатор комментария.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RestoreComment(Action<VKResponse<VKOperationIsSuccess>> callback,
            long commentID, long ownerID = 0)
        {
            if (commentID < 0)
                throw new ArgumentOutOfRangeException("commentID", "Идентификатор комментария не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["comment_id"] = commentID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoRestoreComment, parameters, callback);
        }

        /// <summary>
        /// Получить список отметок на фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="key">Ключ доступа к фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetTags(Action<VKResponse<List<VKPhotoTag>>> callback,
            long photoID, long ownerID = 0, string key = null)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (!String.IsNullOrWhiteSpace(key)) parameters["access_key"] = key;

            VKHelper.GetResponse<List<VKPhotoTag>>(VKMethodsConstants.PhotoGetTags, parameters, callback);
        }

        /// <summary>
        /// Добавить отметку на фотографию.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="userID">Идентификатор пользователя, которого нужно отмеить на фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="x">Координата X верхнего левого угла прямоугольной области,
        /// на которой нужно сделать отметку в процентах.</param>
        /// <param name="y">Координата Y верхнего левого угла прямоугольной области,
        /// на которой нужно сделать отметку в процентах.</param>
        /// <param name="x2">Координата X праого нижнего угла прямоугольной области,
        /// на которой нужно сделать отметку в процентах.</param>
        /// <param name="y2">Координата Y праого нижнего угла прямоугольной области,
        /// на которой нужно сделать отметку в процентах.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void PutTag(Action<VKResponse<long>> callback,
            long userID, long photoID, long ownerID = 0, int x = 0, int y = 0, int x2 = 0, int y2 = 0)
        {
            if (userID < 0)
                throw new ArgumentOutOfRangeException("userID", "Идентификатор пользователя не может быть меньше нуля.");
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (x < 0 || y < 0 || x2 < 0 || y2 < 0)
                throw new ArgumentOutOfRangeException("x, y, x2, y2", 
                    "Ни одна из координат прямоугольной области, на которой сделана отметка не может быть меньше нуля.");
            if (x > 100 || y > 100 || x2 > 100 || y2 > 100)
                throw new ArgumentOutOfRangeException("x, y, x2, y2",
                    "Ни одна из координат прямоугольной области, на которой сделана отметка не может быть больше 100.");

            var parameters = new Dictionary<string, string>();

            parameters["user_id"] = userID.ToString();
            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (x != 0 && y != 0 && x2 != 0 && y2 != 0)
            {
                parameters["x"] = x.ToString();
                parameters["y"] = y.ToString();
                parameters["x2"] = x2.ToString();
                parameters["y2"] = y2.ToString();
            }

            VKHelper.GetResponse<long>(VKMethodsConstants.PhotoPutTag, parameters, callback);
        }

        /// <summary>
        /// Удалить отметку с фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="tagID">Идентификатор отметки.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RemoveTag(Action<VKResponse<VKOperationIsSuccess>> callback,
            long photoID, long tagID, long ownerID = 0)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (tagID < 0)
                throw new ArgumentOutOfRangeException("tagID", "Идентификатор отметки не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            parameters["tag_id"] = tagID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoRemoveTag, parameters, callback);
        }

        /// <summary>
        /// Получить список фотографий, на которых есть непросмотренные отметки.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetNewTags(Action<VKResponse<VKCountedItemsObject<VKPhotoWithTag>>> callback,
            int offset = 0, int count = 0)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            if (count != 0) parameters["count"] = count.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKPhotoWithTag>>(VKMethodsConstants.PhotoGetNewTags, parameters, callback);
        }

        /// <summary>
        /// Получить комментарии к фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="sort">Сортировка фотографий по дате.</param>
        /// <param name="key">Ключ доступа к фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetComments(Action<VKResponse<VKCountedItemsObject<VKComment>>> callback,
            long photoID, long ownerID = 0, int offset = 0, int count = 0,
            VKSortByDate sort = VKSortByDate.asc, string key = null)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            parameters["need_likes"] = "1";
            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (sort == VKSortByDate.asc) parameters["sort"] = "asc";
            else parameters["sort"] = "desc";
            if (!String.IsNullOrWhiteSpace(key)) parameters["access_key"] = key;

            VKHelper.GetResponse<VKCountedItemsObject<VKComment>>(VKMethodsConstants.PhotoGetComments, parameters, callback);
        }

        /// <summary>
        /// Получить комментарии к фотографии в развернутом виде.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <param name="sort">Сортировка фотографий по дате.</param>
        /// <param name="key">Ключ доступа к фотографии.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetCommentsExtended(Action<VKResponse<VKCommentExtendedObject>> callback,
            long photoID, long ownerID = 0, int offset = 0, int count = 0,
            VKSortByDate sort = VKSortByDate.asc, string key = null)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            parameters["need_likes"] = "1";
            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (sort == VKSortByDate.asc) parameters["sort"] = Enum.GetName(sort.GetType(), sort);
            if (!String.IsNullOrWhiteSpace(key)) parameters["access_key"] = key;
            parameters["extended"] = "1";

            VKHelper.GetResponse<VKCommentExtendedObject>(VKMethodsConstants.PhotoGetComments, parameters, callback);
        }

        /// <summary>
        /// Возвращает отсортированный в антихронологическом порядке список всех
        /// комментариев к конкретному альбому или ко всем альбомам пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографий.</param>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetAllComments(Action<VKResponse<VKCountedItemsObject<VKComment>>> callback,
            long ownerID = 0, long albumID = 0, int offset = 0, int count = 0)
        {
            if (albumID < 0)
                throw new ArgumentOutOfRangeException("albumID", "Идентификатор альбома не может быть меньше нуля.");
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            if (albumID != 0) parameters["album_id"] = albumID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKComment>>(VKMethodsConstants.PhotoGetAllComments, parameters, callback);
        }

        /// <summary>
        /// Возвращает список фотографий со страницы пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографий.</param>
        /// <param name="photoIDs">Список с идентификаторами фотографий.</param>
        /// <param name="rev">Сортировка результатов.</param>
        /// <param name="feedType">Тип новости, получаемый в поле type метода newsfeed.get.</param>
        /// <param name="feed">unixtime, который может быть получен методом newsfeed.get в поле date, для получения 
        /// всех фотографий загруженных пользователем в определённый день либо на которых пользователь был отмечен.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetProfile(Action<VKResponse<VKCountedItemsObject<VKPhoto>>> callback,
            long ownerID = 0, List<string> photoIDs = null, VKSortByDate rev = VKSortByDate.asc,
            VKNewsfeedType feedType = VKNewsfeedType.post, long feed = 0, int offset = 0, int count = 0)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            parameters["feed_type"] = Enum.GetName(feedType.GetType(), feedType);
            if (photoIDs != null) parameters["photo_ids"] = String.Join(",", photoIDs);
            parameters["rev"] = ((int)rev).ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (feed != 0) parameters["feed"] = feed.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKPhoto>>(VKMethodsConstants.PhotoGetProfile, parameters, callback);
        }

        /// <summary>
        /// Возвращает список фотографий со страницы пользователя или сообщества с расширенной информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="ownerID">Идентификатор владельца фотографий.</param>
        /// <param name="photoIDs">Список с идентификаторами фотографий.</param>
        /// <param name="rev">Сортировка результатов.</param>
        /// <param name="feedType">Тип новости, получаемый в поле type метода newsfeed.get.</param>
        /// <param name="feed">unixtime, который может быть получен методом newsfeed.get в поле date, для получения 
        /// всех фотографий загруженных пользователем в определённый день либо на которых пользователь был отмечен.</param>
        /// <param name="offset">Смещение относительно начала коллекции пользователя.</param>
        /// <param name="count">Количество фотографий, которое требуется вернуть.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GetProfileExtended(Action<VKResponse<VKCountedItemsObject<VKPhotoExtended>>> callback,
            long ownerID = 0, List<string> photoIDs = null, VKSortByDate rev = VKSortByDate.asc,
            VKNewsfeedType feedType = VKNewsfeedType.all, long feed = 0, int offset = 0, int count = 0)
        {
            if (count < 0 || count > 1000)
                throw new ArgumentOutOfRangeException("count", "Количество элементов должно быть больше нуля, но меньше 1000.");
            if (offset < 0 || offset > 6000)
                throw new ArgumentOutOfRangeException("offset", "Смешение должно быть больше нуля, но меньше 6000.");

            var parameters = new Dictionary<string, string>();

            if (feedType!= VKNewsfeedType.all) parameters["feed_type"] = Enum.GetName(feedType.GetType(), feedType);
            if (photoIDs != null) parameters["photo_ids"] = String.Join(",", photoIDs);
            parameters["rev"] = ((int)rev).ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (feed != 0) parameters["feed"] = feed.ToString();
            if (count != 0) parameters["count"] = count.ToString();
            if (offset != 0) parameters["offset"] = offset.ToString();

            VKHelper.GetResponse<VKCountedItemsObject<VKPhotoExtended>>(VKMethodsConstants.PhotoGetProfile, parameters, callback);
        }

        /// <summary>
        /// Создает новый комментарий к фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="message">Текст комментария.</param>
        /// <param name="attachment">Вложения.</param>
        /// <param name="fromGroup">Опубликовать ли пост от имени группы.</param>
        /// <param name="reply">Идентификатор комментария, на который нужно ответить.</param>
        /// <param name="stickerID">Идентификатор стикера.</param>
        /// <param name="key">Ключ доступа.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void CreateComment(Action<VKResponse<long>> callback,
            long photoID, long ownerID = 0, string message = null, VKAttachment attachment = null,
            VKBoolean fromGroup = VKBoolean.False, long reply = 0, long stickerID = 0, string key = null)
        {
            if (photoID < 0)
                throw new ArgumentOutOfRangeException("photoID", "Идентификатор фотографии не может быть меньше нуля.");
            if (stickerID < 0)
                throw new ArgumentOutOfRangeException("stickerID", "Идентификатор стикера не может быть меньше нуля.");

            var parameters = new Dictionary<string, string>();

            parameters["photo_id"] = photoID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (!String.IsNullOrWhiteSpace(message)) parameters["message"] = message;
            //VKAttachment
            parameters["from_group"] = ((int)fromGroup).ToString();
            if (stickerID != 0) parameters["sticker_id"] = stickerID.ToString();
            if (!String.IsNullOrWhiteSpace(key)) parameters["access_key"] = key;
            if (reply != 0) parameters["reply_to_comment"] = reply.ToString();

            VKHelper.GetResponse<long>(VKMethodsConstants.PhotoCreateComment, parameters, callback);
        }

        /// <summary>
        /// Изменяет текст комментария к фотографии.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="commentID">Идентификатор комментария.</param>
        /// <param name="ownerID">Идентификатор владльца фотографии.</param>
        /// <param name="message">Новый текст комментария.</param>
        /// <param name="attachment">Вложения.</param>
        public void EditComment(Action<VKResponse<VKOperationIsSuccess>> callback,
            long commentID, long ownerID = 0, string message = null, VKAttachment attachment = null)
        {
            if (message.Length > 2048)
                throw new ArgumentException("message", "Максимальное количество символов в комментарии - 2048.");

            var parameters = new Dictionary<string, string>();

            parameters["comment_id"] = commentID.ToString();
            if (ownerID != 0) parameters["owner_id"] = ownerID.ToString();
            if (!String.IsNullOrWhiteSpace(message)) parameters["message"] = message;
            //VKAttachment

            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.PhotoEditComment, parameters, callback);
        }
    }
}