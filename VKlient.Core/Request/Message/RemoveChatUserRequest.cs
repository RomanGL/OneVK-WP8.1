using System;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Исключает из мультидиалога пользователя, если текущий пользователь был создателем беседы либо пригласил исключаемого пользователя.
    /// Также может быть использован для выхода текущего пользователя из беседы, в которой он состоит. Чтобы пользователю вернуться в беседу,
    /// достаточно отправить сообщение в неё (если есть свободные места).
    /// </summary>
    public class RemoveChatUserRequest : BaseChatUserRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageRemoveChatUser; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами беседы и пользователя.
        /// </summary>
        /// <param name="chatID">Идентификатор беседы.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RemoveChatUserRequest(uint chatID, ulong userID)
            : base(chatID, userID) { }
    }
}