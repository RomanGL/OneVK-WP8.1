using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка лайков.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGetLikesListRequest<T> : BaseLikeRequest<T>
    {
        /// <summary>
        /// Количество элементов, которое требуется вернуть.
        /// Выполняет проверку на отрицательность.
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Смещение, относительно начала списка.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Возвращать только отметки от пользователей,
        /// которые являются друзьями текущего.
        /// </summary>
        public VKBoolean FriendsOnly { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public sealed override string GetMethod() { return VKMethodsConstants.LikesGetList; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (FriendsOnly == VKBoolean.True)
            {
                parameters["friends_only"] = "1";
                if (Count != 10) parameters["count"] = Count.ToString();
            }
            else if (Count != 100) parameters["count"] = Count.ToString();

            if (Offset > 0) parameters["offset"] = Offset.ToString();

            return parameters;
        }
    }
}
