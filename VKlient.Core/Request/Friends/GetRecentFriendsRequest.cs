using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка последних добавленных друзей.
    /// </summary>
    public class GetRecentFriendsRequest : BaseRequest, IVKRequestOld
    {
        private int _count = 100;

        /// <summary>
        /// Количество результатов, которое требуется вернуть.
        /// По умолчанию 100. Максимальное значение 1000.
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Count",
                        "Значение не может быть отрицательным.");
                else if (value > 1000)
                    throw new ArgumentOutOfRangeException("Count",
                        "Значение не должно превышать 1000.");
                _count = value;
            }
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.FriendsGetRecent; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (Count != 100 && Count > 0) parameters["count"] = Count.ToString();
            return parameters;
        }
    }
}
