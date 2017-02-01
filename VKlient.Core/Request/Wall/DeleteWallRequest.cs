using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Удаляет запись со стены
    /// </summary>
    public class DeleteWallRequest : IVKRequestOld
    {
        private long _postID;

        /// <summary>
        /// Идентификатор пользователя или сообщества.
        /// </summary>
        public long OwnerID {get; set; }

        /// <summary>
        /// ID записи со стены.
        /// </summary>
        public long PostId
        {
            get { return _postID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(PostId.GetType().Name,
                        "ID записи должен быть положительным");
                _postID = value;
            }
        }

        public DeleteWallRequest(long postId)
        {
            PostId = postId;

            // Значения по умолчанию
            OwnerID = 0;
        }

        /// <summary>
        /// Возвращает словарь параметров
        /// </summary>
        public Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>()
            {
                {"owner_id", OwnerID.ToString()}
            };

            if (PostId != 0)
                parameters["post_id"] = PostId.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public string GetMethod()
        {
            return VKMethodsConstants.WallDelete;
        }
    }
}
