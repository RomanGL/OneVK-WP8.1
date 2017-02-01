using System;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Добавляет в мультидиалог нового пользователя.
    /// Чтобы пользователю вернуться в беседу, которую он сам покинул, достаточно отправить сообщение в неё (если есть свободные места).
    /// </summary>
    public class AddChatUserRequest : BaseChatUserRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageAddChatUser; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами беседы и пользователя.
        /// </summary>
        /// <param name="chatID">Идентификатор беседы.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public AddChatUserRequest(uint chatID, ulong userID)
            : base(chatID, userID) { }
    }
}
