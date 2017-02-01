using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка видеозаписей, на которых отмечен пользователь.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoGetUserVideosBaseRequest<T> : BaseVKCountedRequest<T>
    {
        private ulong _userID;

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID
        {
            get { return _userID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _userID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        protected VideoGetUserVideosBaseRequest()
        {
            DefaultCount = 20;
            MaxCount = 100;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["user_id"] = UserID.ToString();
            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetUserVideos; }
    }
}
