using System;
using System.Collections.Generic;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет базовый запрос на получение результатов поиска
    /// по новостной ленте.
    /// </summary>
    public abstract class BaseSearchNewsfeedRequest<T> : BaseVKCountedRequest<T>
    {
        private double _lat;
        private double _long;
        private long _start;
        private long _end;
        private List<VKUserFields> _fields;

        /// <summary>
        /// Поисковый запрос.
        /// </summary>
        public string Query { get; set; }
        
        /// <summary>
        /// Географическая широта точки, в радиусе от которой необходимо произвести поиск.
        /// </summary>
        public double Latitude
        {
            get { return _lat; }
            set
            {
                if (value < -90 || value > 90)
                    throw new ArgumentOutOfRangeException("Lattitude",
                        "Широта должна не выходить за пределы промежутка от -90 до 90");
                _lat = value;
            }
        }

        /// <summary>
        /// Географическая долгота точки, в радиусе которой необходимо произвести поиск.
        /// </summary>
        public double Longitude
        {
            get { return _long; }
            set
            {
                if (value < -180 || value > 180)
                    throw new ArgumentOutOfRangeException("Longitude",
                        "Долгота должна не выходить за пределы промежутка от -180 до 180");
                _long = value;
            }
        }

        /// <summary>
        /// Самое раннее время, в которое должны были быть опубликованы записи в результатах поиска.
        /// </summary>
        public long StartTime
        {
            get { return _start; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("StartTime",
                        "Время в формате unixtime не может быть меньше 0.");
                _start = value;
            }
        }

        /// <summary>
        /// Самое позднее время, в которое должны были быть опубликованы записи в результатах поиска.
        /// </summary>
        public long EndTime
        {
            get { return _end; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("EndTime",
                        "Время в формате unixtime не может быть меньше 0.");
                _end = value;
            }
        }

        /// <summary>
        /// Параметр, задающий сдвиг относительно начала списка.
        /// </summary>
        public string StartFrom { get; set; }

        /// <summary>
        /// Какие дополнительные поля профилей необходимо вернуть.
        /// </summary>
        public List<VKUserFields> Fields
        {
            get { return _fields; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Fields",
                        "Коллекция должна быть инициализирована и должна иметь хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Fields",
                        "Количество элементов в коллекции должно быть больше нуля.");
                _fields = value;
            }
        }
        
        public BaseSearchNewsfeedRequest()
        {
            MaxCount = 200;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters =  base.GetParameters();
            
            if (!String.IsNullOrWhiteSpace(Query)) parameters["q"] = Query;
            if (Count != 30) parameters["count"] = Count.ToString();
            if (Latitude != 0) parameters["latitude"] = (Latitude.ToString()).Replace(",", ".");
            if (Longitude != 0) parameters["longitude"] = (Longitude.ToString()).Replace(",", ".");
            if (StartTime != 0) parameters["start_time"] = StartTime.ToString();
            if (EndTime != 0) parameters["end_time"] = EndTime.ToString();
            if (!String.IsNullOrWhiteSpace(StartFrom)) parameters["start_from"] = StartFrom;
            if (Fields != null && Fields.Count != 0) parameters["fields"] = String.Join(",", Fields);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedSearch;
        }
    }
}
