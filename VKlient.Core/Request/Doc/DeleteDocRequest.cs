using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление документа.
    /// </summary>
    public class DeleteDocRequest : BaseRequest, IVKRequestOld
    {
        private long _ownerID;
        private long _documentID;

        /// <summary>
        /// Идентификатор владельца документа (пользователь или сообщество).
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Значение не может быть неопределенным.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        public long DocumentID
        {
            get { return _documentID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("DocumentID",
                        "Значение не может быть неопределенным.");
                _documentID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.DocsDelete; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["owner_id"] = OwnerID.ToString();
            parameters["doc_id"] = DocumentID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором 
        /// документа и его владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца документа.</param>
        /// <param name="documentID">Идентификатор документа.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public DeleteDocRequest(long ownerID, long documentID)
        {
            OwnerID = ownerID;
            DocumentID = documentID;
        }
    }
}
