using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс запроса для методов работы с новостной лентой.
    /// </summary>
    public abstract class BaseNewsfeedGetRequest<T> : BaseVKCountedRequest<T>
    {
        /// <summary>
        /// Время, начиная с которого нужно получить новости в формате unixtime.
        /// </summary>
        public ulong Start { get; set; }

        /// <summary>
        /// Время, до которого нужно получить новости в формате unixtime.
        /// </summary>
        public ulong End { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Start != 0) parameters["start_time"] = Start.ToString();
            if (End != 0) parameters["end_time"] = End.ToString();

            return parameters;
        }
    }
}
