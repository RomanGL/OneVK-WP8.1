using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    public class DeleteNewsfeedListRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор пользовательского списка новостей, который необходимо удалить.
        /// </summary>
        public uint ListID { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="listID">Идентификатор пользовательского списка 
        /// новостей, который необходимо удалить.</param>
        public DeleteNewsfeedListRequest(uint listID)
        {
            ListID = listID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["list_id"] = ListID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedDeleteList;
        }
    }
}
