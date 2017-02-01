using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Response;
using OneVK.Model.Audio;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение результатов поиска по аудиозаписям.
    /// </summary>
    public class SearchAudiosRequest : BaseVKCountedRequest<VKCountedItemsObject<VKAudio>>
    {
        /// <summary>
        /// Строка поискового запроса.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Нужно ли автоматически исправлять ошибки в запросе.
        /// </summary>
        public VKBoolean AutoComplete { get; set; }

        /// <summary>
        /// Производить поиск только по тем аудиозаписям, у 
        /// еоторых есть текст.
        /// </summary>
        public VKBoolean Lyrics { get; set; }

        /// <summary>
        /// Искать только по названию исполнителя.
        /// </summary>
        public VKBoolean ArtistOnly { get; set; }

        /// <summary>
        /// Метод сортировки результатов.
        /// </summary>
        public VKMediaSearchSortMode SortMode { get; set; }

        /// <summary>
        /// Искать только по аудиозаписям пользователя.
        /// </summary>
        public VKBoolean OwnMode { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public SearchAudiosRequest()
        {
            MaxCount = 300;
            DefaultCount = 30;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(Query)) parameters["q"] = Query;
            if (AutoComplete == VKBoolean.True) parameters["auto_complete"] = "1";
            if (Lyrics == VKBoolean.True) parameters["lyrics"] = "1";
            if (ArtistOnly == VKBoolean.True) parameters["performer_only"] = "1";
            if (SortMode != VKMediaSearchSortMode.ByDate) parameters["sort"] = ((int)SortMode).ToString();
            if (OwnMode == VKBoolean.True) parameters["search_own"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioSearch; }
    }
}
