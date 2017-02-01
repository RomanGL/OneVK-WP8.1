using OneVK.Enums.Common;
using OneVK.Enums.Video;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка видеозаписей в соответствии с заданным критерием поиска.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoSearchBaseRequest<T> : BaseVKCountedRequest<T> 
    {
        private string _query;

        /// <summary>
        /// Строка поискового запроса.
        /// </summary>
        public string Query
        {
            get { return _query; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _query = value;
            }
        }

        /// <summary>
        /// Вид сортировки.
        /// </summary>
        public VKMediaSearchSortMode Sort { get; set; }

        /// <summary>
        /// Поиск производится только по видеозаписям высокого качества.
        /// </summary>
        public VKBoolean HD { get; set; }

        /// <summary>
        /// Безопасный поиск.
        /// </summary>
        public VKBoolean Adult { get; set; }

        /// <summary>
        /// Длительность видеозаписи.
        /// </summary>
        public VKSearchVideoLength Length { get; set; }

        /// <summary>
        /// Тип видеозаписи.
        /// </summary>
        public VKSearchVideoType Type { get; set; }

        /// <summary>
        /// Искать по видеозаписям пользователя.
        /// </summary>
        public VKBoolean SearchOwn { get; set; }

        /// <summary>
        /// Количество секунд, видеозаписи длиннее которого необходимо вернуть.
        /// </summary>
        public ulong Longer { get; set; }

        /// <summary>
        /// Количество секунд, видеозаписи короче которого необходимо вернуть.
        /// </summary>
        public ulong Shorter { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["q"] = Query.ToString();
            if (Sort != VKMediaSearchSortMode.ByDate) parameters["sort"] = ((byte)Sort).ToString();
            if (HD != VKBoolean.False) parameters["hd"] = "1";
            if (Adult != VKBoolean.False) parameters["adult"] = "1";
            string filter = null;
            if (Type != VKSearchVideoType.All)
                filter = Type.ToString();
            if (Length != VKSearchVideoLength.All && filter == null)
                switch (Length)
                {
                    case VKSearchVideoLength.shortv: filter = "short"; break;
                    case VKSearchVideoLength.longv: filter = "long"; break;
                }
            else if (Length != VKSearchVideoLength.All && filter != null)
                switch (Length)
                {
                    case VKSearchVideoLength.shortv: filter += ",short"; break;
                    case VKSearchVideoLength.longv: filter += ",long"; break;
                }
            parameters["filters"] = filter;
            if (SearchOwn != VKBoolean.False) parameters["search_own"] = "1";
            if (Longer != 0) parameters["longer"] = Longer.ToString();
            if (Shorter != 0) parameters["shorter"] = Shorter.ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса c заданной строкой поискового запроса.
        /// </summary>
        /// <param name="query">Строка поискового запроса.</param>
        /// <exception cref="ArgumentException"/>
        protected VideoSearchBaseRequest(string query)
        {
            Query = query;
            MaxCount = 200;
            DefaultCount = 20;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoSearch; }
    }
}
