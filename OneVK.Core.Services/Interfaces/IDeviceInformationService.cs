namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для получения информации об устройстве.
    /// </summary>
    public interface IDeviceInformationService
    {
        /// <summary>
        /// Возвращает идентификатор оборудования.
        /// </summary>
        string GetHardwareID();
    }
}
