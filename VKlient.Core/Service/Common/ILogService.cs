using System;

namespace OneVK.Service.Common
{
    /// <summary>
    /// Представляет сервис логирования в ходе работы сообщения.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Логирует указанное исключение.
        /// </summary>
        /// <param name="ex">Исключения для логирования.</param>
        void Log(Exception ex);

        /// <summary>
        /// Логирует указанное сообщение.
        /// </summary>
        /// <param name="message">Сообщение для логирования.</param>
        void Log(string message);
    }
}
