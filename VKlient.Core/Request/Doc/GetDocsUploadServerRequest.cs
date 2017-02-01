using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение сервера для загрузки документа.
    /// </summary>
    public class GetDocsUploadServerRequest : BaseRequest, IVKRequestOld
    {
        private long _groupID;

        /// <summary>
        /// Идентификатор сообщества, если документ требуется 
        /// загрузить в него.
        /// </summary>
        public long GroupID
        {
            get { return _groupID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("GroupID",
                        "Значение не может быть отрицательным.");
                _groupID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.DocsGetUploadServer; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();
            return parameters;
        }
    }
}
