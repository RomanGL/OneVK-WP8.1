using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на удаление всех сообщений в диалоге.
    /// </summary>
    public class DeleteDialogRequest : BaseCountedRequest, IVKRequestOld
    {
        private long _userID;

        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Значение не должно быть равно 0.");
                _userID = value;
            }
        }
        
        /// <summary>
        /// Количество сообщений для удаления.
        /// </summary>
        public override uint Count
        {
            get { return base.Count; }
            set
            {
                if (value > 10000)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество сообщений должно быть не меньше нуля, но не больше 10000.");
                base.Count = value;
            }
        }
        
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageDeleteDialog; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters["user_id"] = UserID.ToString();
            return parameters;
        }
    }
}
