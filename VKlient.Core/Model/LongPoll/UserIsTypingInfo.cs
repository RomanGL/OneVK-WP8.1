namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Представляет информацию о том, что пользователь начал набирать текст в указанном диалоге или чате.
    /// </summary>
    public class UserIsTypingInfo
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }
        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public uint ChatID { get; set; }
        /// <summary>
        /// Набирается ли сообщение в чате.
        /// </summary>
        public bool IsChat { get { return ChatID > 0; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public UserIsTypingInfo(ulong userID)
        {
            UserID = userID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором пользователя и чата.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="chatID">Идентификатор чата.</param>
        public UserIsTypingInfo(ulong userID, uint chatID)
            : this(userID)
        {
            ChatID = chatID;
        }
    }
}
