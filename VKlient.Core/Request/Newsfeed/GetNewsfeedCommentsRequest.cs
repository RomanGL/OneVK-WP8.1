using System;
using System.Collections.Generic;
using OneVK.Enums.Newsfeed;
using OneVK.Enums.Profile;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение раздела
    /// комментариев из новостей пользователя.
    /// </summary>
    public class GetNewsfeedCommentsRequest : ExtendedNewsfeedBaseGetRequest<VKNewsfeedGetObject>
    {
        private List<VKUserFields> _fields;

        /// <summary>
        /// Список дополнительных полей профилей, которые необходимо вернуть.
        /// </summary>
        public List<VKUserFields> Fields
        {
            get { return _fields; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Fields",
                        "Коллекция должна быть инициализирована и должна содержать хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Fields",
                        "Коллекция должна содержать хотя бы один элемент.");
                _fields = value;
            }
        }

        /// <summary>
        /// Идентификатор объекта, комментарии к репостам которого необходимо вернуть.
        /// </summary>
        public string Reposts { get; set; }

        /// <summary>
        /// Количество комментариев, которое необходимо вернуть.
        /// </summary>
        public uint CommentsCount { get; set; }

        /// <summary>
        /// Типы объектов, изменения комментариев к которым нужно вернуть.
        /// </summary>
        public List<VKNewsfeedCommentsFilters> Filters { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        GetNewsfeedCommentsRequest()
            : base()
        {

        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (!String.IsNullOrWhiteSpace(Reposts)) parameters["reposts"] = Reposts;
            if (CommentsCount != 0) parameters["last_comments_count"] = CommentsCount.ToString();
            if (Filters != null && Filters.Count != 0) parameters["filters"] = String.Join(",", Filters);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGetComments;
        }
    }
}
