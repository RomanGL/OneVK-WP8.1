using System;
using OneVK.Response;
using OneVK.Helpers;
using OneVK.Request;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис для работы с хранимыми на сервере процедурами ВКонтакте.
    /// </summary>
    public class VKExecuteService
    {
        /// <summary>
        /// Выполняет указанную хранимую процедуру ВКонтакте.
        /// </summary>
        /// <typeparam name="T">Тип данных, возвращаемых методом.</typeparam>
        /// <param name="callback">Метод, который будет вызван по завершении операции.
        /// Параметр является результатом запроса.</param>
        /// <param name="request">Объект запроса с необходимыми параметрами.</param>
        public void Execute<T>(Action<VKResponse<T>> callback, VKExecuteRequest request)
        {
            VKHelper.GetResponse<T>(request, callback);
        }
    }
}
