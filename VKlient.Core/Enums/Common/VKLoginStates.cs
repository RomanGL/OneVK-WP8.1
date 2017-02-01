namespace OneVK.Enums.Common
{
    /// <summary>
    /// Перечисление статусов авторизации пользователя.
    /// </summary>
    public enum VKLoginStates : byte
    {
        Login,
        Logout,
        InvalidClient,
        NeedValidation,
        ValidationCanceled,
        ConnectionError,
        UnknownError,
        Nothing
    }
}
