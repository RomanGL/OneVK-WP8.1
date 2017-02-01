using System;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Comment;
using OneVK.Model.Common;
using OneVK.Model.Video;
using OneVK.Request;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис для работы с видеозаписями ВКонтакте.
    /// </summary>
    public sealed class VKVideoService
    {
        /// <summary>
        /// Возвращает список видеозаписей пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Get(Action<VKResponse<VKCountedItemsObject<VKVideoBase>>> callback, GetVideosRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoBase>>(request, callback);
        }

        /// <summary>
        /// Возвращает список видеозаписей пользователя или сообщества с расширенной информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetExtended(Action<VKResponse<VKCountedItemsObject<VKVideoExtended>>> callback, GetVideosRequest request)
        {
            request.IsExtended = true;
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoExtended>>(request, callback);
        }

        /// <summary>
        /// Редактирует данные видеозаписи на странице пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Edit(Action<VKResponse<VKOperationIsSuccess>> callback, EditVideoRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Добавляет видеозапись в список пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Add(Action<VKResponse<VKOperationIsSuccess>> callback, AddVideoRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает адрес сервера (необходимый для загрузки) и данные видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Save(Action<VKResponse<VKVideoUploadServer>> callback, SaveVideoRequest request)
        {
            VKHelper.GetResponse<VKVideoUploadServer>(request, callback);
        }

        /// <summary>
        /// Удаляет видеозапись со страницы пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Delete(Action<VKResponse<VKOperationIsSuccess>> callback, DeleteVideoRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Восстанавливает удаленную видеозапись.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Restore(Action<VKResponse<VKOperationIsSuccess>> callback, RestoreVideoRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает список найденных личных сообщений текущего пользователя по введенной строке поиска.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Search(Action<VKResponse<VKCountedItemsObject<VKVideoBase>>> callback, SearchVideosRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoBase>>(request, callback);
        }

        /// <summary>
        /// Возвращает список найденных личных сообщений текущего пользователя по введенной строке поиска с расширенной информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void SearchExtended(Action<VKResponse<VKVideoExtendedItemsObject<VKVideoBase>>> callback, SearchVideosRequest request)
        {
            request.IsExtended = true;
            VKHelper.GetResponse<VKVideoExtendedItemsObject<VKVideoBase>>(request, callback);
        }

        /// <summary>
        /// Возвращает список видеозаписей, на которых отмечен пользователь.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetUserVideos(Action<VKResponse<VKCountedItemsObject<VKVideoBase>>> callback, GetUserVideosRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoBase>>(request, callback);
        }

        /// <summary>
        /// Возвращает список альбомов видеозаписей пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetAlbums(Action<VKResponse<VKCountedItemsObject<VKVideoAlbum>>> callback, GetVideoAlbumsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoAlbum>>(request, callback);
        }

        /// <summary>
        /// Возвращает список альбомов видеозаписей пользователя или сообщества с расширенной информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetAlbumsExtended(Action<VKResponse<VKCountedItemsObject<VKVideoAlbumExtended>>> callback, GetVideoAlbumsRequest request)
        {
            request.IsExtended = true;
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoAlbumExtended>>(request, callback);
        }

        /// <summary>
        /// Позволяет получить информацию об альбоме с видео.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetAlbumByID(Action<VKResponse<VKCountedItemsObject<VKVideoAlbumExtended>>> callback, GetVideoAlbumByIDRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoAlbumExtended>>(request, callback);
        }

        /// <summary>
        /// Создает пустой альбом видеозаписей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void AddAlbum(Action<VKResponse<VKVideoAddAlbumObject>> callback, AddVideoAlbumRequest request)
        {
            VKHelper.GetResponse<VKVideoAddAlbumObject>(request, callback);
        }

        /// <summary>
        /// Редактирует название альбома видеозаписей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void EditAlbum(Action<VKResponse<VKOperationIsSuccess>> callback, EditVideoAlbumRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Удаляет альбом видеозаписей. 
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void DeleteAlbum(Action<VKResponse<VKOperationIsSuccess>> callback, DeleteVideoAlbumRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Позволяет изменить порядок альбомов с видео.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void ReorderAlbums(Action<VKResponse<VKOperationIsSuccess>> callback, ReorderVideoAlbumsRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Позволяет добавить видеозапись в альбом.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void AddToAlbum(Action<VKResponse<VKOperationIsSuccess>> callback, AddVideoToAlbumRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Позволяет убрать видеозапись из альбома.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void RemoveFromAlbum(Action<VKResponse<VKOperationIsSuccess>> callback, RemoveVideoFromAlbumRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает список альбомов, в которых находится видеозапись.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetAlbumsByVideo(Action<VKResponse<VKCountedItemsObject<long>>> callback, GetAlbumsByVideoRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<long>>(request, callback);
        }

        /// <summary>
        /// Возвращает список альбомов, в которых находится видеозапись (с расширенной информацией).
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetAlbumsByVideoExtended(Action<VKResponse<VKCountedItemsObject<VKVideoAlbumExtended>>> callback, GetAlbumsByVideoRequest request)
        {
            request.IsExtended = true;
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoAlbumExtended>>(request, callback);
        }

        /// <summary>
        /// Возвращает список комментариев к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetComments(Action<VKResponse<VKCountedItemsObject<VKComment>>> callback, GetVideoCommentsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKComment>>(request, callback);
        }

        /// <summary>
        /// Возвращает список комментариев к видеозаписи с расширенной информацией.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetCommentsExtended(Action<VKResponse<VKCommentExtendedObject>> callback, GetVideoCommentsRequest request)
        {
            request.IsExtended = true;
            VKHelper.GetResponse<VKCommentExtendedObject>(request, callback);
        }

        /// <summary>
        /// Cоздает новый комментарий к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void CreateComment(Action<VKResponse<long>> callback, CreateVideoCommentRequest request)
        {
            VKHelper.GetResponse<long>(request, callback);
        }

        /// <summary>
        /// Удаляет комментарий к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void DeleteComment(Action<VKResponse<VKOperationIsSuccess>> callback, DeleteVideoCommentRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Восстанавливает удаленный комментарий к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void RestoreComment(Action<VKResponse<VKOperationIsSuccess>> callback, RestoreVideoCommentRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Изменяет комментарий к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void EditComment(Action<VKResponse<VKOperationIsSuccess>> callback, EditVideoCommentRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает список отметок на видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetTags(Action<VKResponse<VKCountedItemsObject<VKTag>>> callback, GetVideoTagsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKTag>>(request, callback);
        }

        /// <summary>
        /// Добавляет отметку на видеозапись.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void PutTag(Action<VKResponse<long>> callback, PutVideoTagRequest request)
        {
            VKHelper.GetResponse<long>(request, callback);
        }

        /// <summary>
        /// Удаляет отметку с видеозаписи. 
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void RemoveTag(Action<VKResponse<VKOperationIsSuccess>> callback, RemoveVideoTagRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает список видеозаписей, на которых есть непросмотренные отметки.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetNewTags(Action<VKResponse<VKCountedItemsObject<VKVideoTagged>>> callback, GetNewVideoTagsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKVideoTagged>>(request, callback);
        }

        /// <summary>
        /// Позволяет пожаловаться на видеозапись.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Report(Action<VKResponse<VKOperationIsSuccess>> callback, ReportVideoRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Позволяет пожаловаться на комментарий к видеозаписи.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void ReportComment(Action<VKResponse<VKOperationIsSuccess>> callback, ReportVideoCommentRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }
    }
}
