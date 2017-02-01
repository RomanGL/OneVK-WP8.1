using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление фотографии мультидиалога.
    /// </summary>
    public class DeleteChatPhotoRequest : BaseRequest, IVKRequestOld
    {
        private long _chatID;

        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public long ChatID
        {
            get { return _chatID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("ChatID",
                        "Значение является обязательным параметром и должно быть больше нуля.");
                _chatID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageDeleteChatPhoto; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["chat_id"] = ChatID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором беседы.
        /// </summary>
        /// <param name="chatID">Идентификатор мультидиалога, фотографию из которого требуется удалить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public DeleteChatPhotoRequest(long chatID)
        {
            ChatID = chatID;
        }
    }
}
