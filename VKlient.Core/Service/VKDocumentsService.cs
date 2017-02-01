using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Common;
using OneVK.Model.Doc;
using OneVK.Request;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис для работы с документами.
    /// </summary>
    public sealed class VKDocumentsService
    {    
        /// <summary>
        /// Возвращает список документов пользователя или сообщества.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetDocs(Action<VKResponse<VKCountedItemsObject<VKDocument>>> callback, GetDocsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKDocument>>(request, callback);
        }

        /// <summary>
        /// Возвращает список документов пользователя или сообщества по их идентификаторам.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetDocsByID(Action<VKResponse<List<VKDocument>>> callback, GetDocsByIDRequest request)
        {
            VKHelper.GetResponse<List<VKDocument>>(request, callback);
        }
        
        /// <summary>
        /// Возвращает сервер для загрузки документа.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetDocsUploadServer(Action<VKResponse<VKUploadServerBase>> callback, GetDocsUploadServerRequest request)
        {
            VKHelper.GetResponse<VKUploadServerBase>(request, callback);
        }

        /// <summary>
        /// Возвращает сервер для загрузки документа в папку "Отправленные" для 
        /// последующей отправки на стену или личным сообщением.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetDocsWallUploadServer(Action<VKResponse<VKUploadServerBase>> callback, GetDocsWallUploadServerRequest request)
        {
            VKHelper.GetResponse<VKUploadServerBase>(request, callback);
        }

        /// <summary>
        /// Добавляет документ в коллекцию пользователя. 
        /// Возвращает идентификатор добавленного документа.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void AddDocument(Action<VKResponse<long>> callback, AddDocRequest request)
        {
            VKHelper.GetResponse<long>(request, callback);
        }

        /// <summary>
        /// Удаляет документ пользователя или группы.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void DeleteDocument(Action<VKResponse<VKOperationIsSuccess>> callback, DeleteDocRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);            
        }

        /// <summary>
        /// Сохраняет документ после успешной загрузки.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void SaveDocument(Action<VKResponse<List<VKDocument>>> callback, SaveDocRequest request)
        {
            VKHelper.GetResponse<List<VKDocument>>(request, callback);
        }
    }
}
