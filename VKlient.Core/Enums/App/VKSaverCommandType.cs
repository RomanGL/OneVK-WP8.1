namespace OneVK.Enums.App
{
    /// <summary>
    /// Представляет перечисление типов команд для ВКачай.
    /// </summary>
    public enum VKSaverCommandType : byte
    {
        /// <summary>
        /// Открыть приложение.
        /// </summary>
        StartApp = 0,
        /// <summary>
        /// Загрузить файлы.
        /// </summary>
        DownloadFiles,
        /// <summary>
        /// Открывает видеозапись.
        /// </summary>
        OpenVideo
    }
}
