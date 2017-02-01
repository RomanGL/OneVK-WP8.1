using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Helpers;
using OneVK.Model.Common;
using OneVK.Model.Profile;
using OneVK.Request;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Сервис для работы с друзьями ВКонтакте.
    /// </summary>
    public sealed class VKFriendsService
    {   
        /// <summary>
        /// Возвращает список друзей пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        //public void GetFriends(Action<VKResponse<VKCountedItemsObject<VKProfileBase>>> callback, 
        //    GetFriendsRequest request)
        //{
        //    VKHelper.GetResponse<VKCountedItemsObject<VKProfileBase>>(request, callback);
        //}

        /// <summary>
        /// Возвращает список идентификаторов друзей пользователя, находящихся 
        /// в данные момент в сети.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        //public void GetOnlineFriends(Action<VKResponse<VKGetOnlineFriendsObject>> callback, 
        //    GetOnlineFriendsRequest request)
        //{
        //    if (request.OnlineMobile == VKBoolean.True)
        //        VKHelper.GetResponse<VKGetOnlineFriendsObject>(request, callback);
        //    else
        //        VKHelper.GetResponse<List<long>>(request, (response) =>
        //            {
        //                var result = new VKResponse<VKGetOnlineFriendsObject>();
        //                result.Error = response.Error;
        //                result.Response.Online = response.Response;
        //                callback(result);
        //            });
        //}  
     
        /// <summary>
        /// Возвращаетс идентификаторы общих с пользователем друзей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetMutualFrends(Action<VKResponse<List<VKCommonFriendsObject>>> callback, 
            GetMutualFriendsRequest request)
        {
            if (!request.IsSingle)
                VKHelper.GetResponse<List<VKCommonFriendsObject>>(request, callback);
            else
                VKHelper.GetResponse<List<long>>(request, (response) =>
                    {
                        var result = new VKResponse<List<VKCommonFriendsObject>> { Error = response.Error };
                        result.Response.Add(new VKCommonFriendsObject 
                            { ID = request.TargetID, CommonFriends = response.Response, CommonCount = -1 });
                        callback(result);
                    });
        }

        /// <summary>
        /// Возвращает список идентификаторов последних добавленных друзей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void GetRecentFriends(Action<VKResponse<List<long>>> callback, 
            GetRecentFriendsRequest request)
        {
            VKHelper.GetResponse<List<long>>(request, callback);
        }

        /// <summary>
        /// Одобряет или создает заявку на добавление в друзья.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void AddFriend(Action<VKResponse<VKAddFriend>> callback, AddFriendRequest request)
        {
            VKHelper.GetResponse<VKAddFriend>(request, callback);
        }

        /// <summary>
        /// Редактирует списки для друга.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void EditFriend(Action<VKResponse<VKOperationIsSuccess>> callback, 
            EditFriendRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Удаляет пользователя из списка друзей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void DeleteFriend(Action<VKResponse<VKDeleteFriendObject>> callback,
            DeleteFriendRequest request)
        {
            VKHelper.GetResponse<VKDeleteFriendObject>(request, callback);
        }

        /// <summary>
        /// Возвращает список списков друзей пользователя ВКонтакте.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        //public void GetFriendsLists(Action<VKResponse<VKCountedItemsObject<VKFriendsList>>> callback,
        //    GetFriendsListsRequest request)
        //{
        //    VKHelper.GetResponse<VKCountedItemsObject<VKFriendsList>>(request, callback);
        //}

        /// <summary>
        /// Создает новый список друзей.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void AddFriendsList(Action<VKResponse<int>> callback, 
            AddFriendsListRequest request)
        {
            VKHelper.GetResponse<AddListResponse>(request,
                (response) =>
                {
                    var result = new VKResponse<int>() { Error = response.Error };
                    result.Response = response.Response.ListID;
                    callback(result);
                });
        }

        /// <summary>
        /// Представляет ответ на запрос 
        /// добавления нового списка друзей.
        /// </summary>
        private class AddListResponse
        {
            /// <summary>
            /// Идентификатор созданного списка.
            /// </summary>
            [JsonProperty("list_id")]
            public int ListID { get; set; } 
        }

        /// <summary>
        /// Редактирует список друзей ВКонтакте.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void EditFriendsList(Action<VKResponse<VKOperationIsSuccess>> callback,
            EditFriendsListRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Удалить список друзей ВКонтакте.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void DeleteFriendsList(Action<VKResponse<VKOperationIsSuccess>> callback,
            DeleteFriendsListRequest request)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(request, callback);
        }

        /// <summary>
        /// Возвращает список идентификаторов друзей текущего 
        /// пользователя, которые установили данное приложение.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        public void GetFriendsAppUsers(Action<VKResponse<List<long>>> callback)
        {
            VKHelper.GetResponse<List<long>>(VKMethodsConstants.FriendsGetAppUsers, 
                callback);
        }

        /// <summary>
        /// Отмечает все входящие заявки на добавление в друзья как просмотренные.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        public void DeleteAllFriendsRequests(Action<VKResponse<VKOperationIsSuccess>> callback)
        {
            VKHelper.GetResponse<VKOperationIsSuccess>(VKMethodsConstants.FriendsDeleteAllRequests,
                callback);
        }

        /// <summary>
        /// Выполняет поиск по друзьям пользователя.
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void SearchFriends(Action<VKResponse<VKCountedItemsObject<VKProfileBase>>> callback,
            SearchFriendsRequest request)
        {
            VKHelper.GetResponse<VKCountedItemsObject<VKProfileBase>>(request, callback);
        }
    }
}
