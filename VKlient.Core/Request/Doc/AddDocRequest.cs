using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление документа в список пользователя.
    /// </summary>
    public class AddDocRequest : DeleteDocRequest
    {
        /// <summary>
        /// Ключ доступа документа. Этот параметр следует передать, 
        /// если вместе с остальными данными о документе было возвращено поле access_key.
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public new string GetMethod() { return VKMethodsConstants.DocsAdd; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (!String.IsNullOrWhiteSpace(AccessKey)) 
                parameters["access_key"] = AccessKey;
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором 
        /// документа и его владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца документа.</param>
        /// <param name="documentID">Идентификатор документа.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public AddDocRequest(long ownerID, long documentID)
            : base(ownerID, documentID) { }
    }
}
