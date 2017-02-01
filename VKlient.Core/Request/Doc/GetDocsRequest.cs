using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка документов 
    /// пользователя или сообщества.
    /// </summary>
    public class GetDocsRequest : BaseCountedRequest
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, 
        /// документы которого требуется получить.
        /// По умолчанию идентификатор текущего пользователя.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.DocsGet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (Count == 50) parameters.Remove("count");
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            return parameters;
        }
    }
}
