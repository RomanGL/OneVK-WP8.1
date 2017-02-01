using OneVK.Enums.Wall;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет базовый запрос на получение записей
    /// со стены пользователя или сообщества.
    /// </summary>
    public abstract class BaseGetWallRequest<T> : BaseVKCountedRequest<T>
    {
        private VKWallPostFilter _filter = VKWallPostFilter.All;

        /// <summary>
        /// Идентификатор владельца стены.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Короткий адрес пользователя или сообщества
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Записи каких типов необходимо получить.
        /// </summary>
        public VKWallPostFilter Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public BaseGetWallRequest()
        {
            MaxCount = 100;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.WallGet; }

        /// <summary>
        /// Возвращает колелкцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (!string.IsNullOrWhiteSpace(Domain)) parameters["domain"] = Domain;
            if (Filter != VKWallPostFilter.All) parameters["filter"] = Filter.ToString().ToLower();

            return parameters;
        }
    }
}
