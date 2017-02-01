namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Содержит информацию о событии удаления сообщения.
    /// </summary>
    public sealed class MessageDeletedInfo
    {
        /// <summary>
        /// Идентификатор удаленного сообщения.
        /// </summary>
        public ulong MessageID { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором удаленного сообщения.
        /// </summary>
        /// <param name="messageID">Идентификатор удаленного сообщения.</param>
        internal MessageDeletedInfo(ulong messageID)
        {
            MessageID = messageID;
        }
    }
}
