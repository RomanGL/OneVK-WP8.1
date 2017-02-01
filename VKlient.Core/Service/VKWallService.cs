using System;
using System.Collections.Generic;
using OneVK.Helpers;
using OneVK.Model.Wall;
using OneVK.Request;
using OneVK.Response;

namespace OneVK.Service
{
    /// <summary>
    /// Сервис для работы со стеной.
    /// </summary>
    public class VKWallService
    {
        /// <summary>
        /// Возвращает список записей со страницы
        /// </summary>
        /// <param name="callback">Метод, который будет вызван по завершении операции</param>
        /// <param name="request">Объект запроса с необходимыми параметрами</param>
        public void GetWallPosts(Action<VKResponse<List<VKBaseWallPost>>> callback,
            GetWallRequest request)
        {
            VKHelper.GetResponse<List<VKBaseWallPost>>(request, callback);
        }


    }
}
