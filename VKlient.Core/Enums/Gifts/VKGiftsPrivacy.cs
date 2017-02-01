namespace OneVK.Enums.Gifts
{
    /// <summary>
    /// Значение приватности подарка для текущего пользователя.
    /// </summary>
    public enum VKGiftsPrivacy : byte
    {
        /// <summary>
        /// Имя отправителя и сообщение видно всем.
        /// </summary>
        Zero = 0,
        /// <summary>
        /// Имя отправителя видно всем, сообщение видно только получателю.
        /// </summary>
        One = 1,
        /// <summary>
        /// Имя отправителя скрыто, сообщение видно только получателю.
        /// </summary>
        Two = 2
    }
}
