using OneVK.Enums.Photo;
using OneVK.Model.Photo;
using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на поиск по фотографиям.
    /// </summary>
    public class SearchPhotosRequest : BaseVKCountedRequest<VKCountedItemsObject<VKPhoto>>
    {
        /// <summary>
        /// Поисковой запрос.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Географическая широта отметки, заданная в градусах.
        /// </summary>
        public int Latitude { get; set; }

        /// <summary>
        /// Географическая долгота отметки, заданная в градусах.
        /// </summary>
        public int Longitude { get; set; }

        /// <summary>
        /// Время в формате unixtime, не раньше которого должны были 
        /// быть загружены найденные фотографии.
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// Время в формате unixtime, не позже которого должны были 
        /// быть загружены найденные фотографии.
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// Сортировка результатов поиска.
        /// </summary>
        public VKPhotoSortDateLikes SortOrder { get; set; }

        /// <summary>
        /// Радиус поиска в метрах.
        /// </summary>
        public VKPhotoSearchRadius Radius { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public SearchPhotosRequest()
        {
            MaxCount = 1000;
            DefaultCount = 100;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(Query)) parameters["q"] = Query;
            if (Latitude != 0) parameters["lat"] = Latitude.ToString();
            if (Longitude != 0) parameters["long"] = Longitude.ToString();
            if (StartTime != 0) parameters["start_time"] = StartTime.ToString();
            if (EndTime != 0) parameters["end_time"] = EndTime.ToString();
            if ((int)SortOrder == 1) parameters["sort"] = "1";

            if (Radius != VKPhotoSearchRadius.FiveThousand)
                switch (Radius)
                {
                    case VKPhotoSearchRadius.EightHundred:
                        parameters["radius"] = "800";
                        break;
                    case VKPhotoSearchRadius.FiftyThousand:
                        parameters["radius"] = "50000";
                        break;
                    case VKPhotoSearchRadius.Hundred:
                        parameters["radius"] = "100";
                        break;
                    case VKPhotoSearchRadius.SixThousand:
                        parameters["radius"] = "6000";
                        break;
                    case VKPhotoSearchRadius.Ten:
                        parameters["radius"] = "10";
                        break;
                }

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoSearch; }
    }
}
