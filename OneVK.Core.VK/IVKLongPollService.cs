using OneVK.Core.VK.Models.LongPoll;
using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис для работы с LongPoll-подключением к ВКонтакте.
    /// </summary>
    public interface IVKLongPollService
    {
        /// <summary>
        /// Просиходит при запуске сервиса.
        /// </summary>
        event TypedEventHandler<IVKLongPollService, EventArgs> ServiceStarted;
        /// <summary>
        /// Происходит при остановке сервиса.
        /// </summary>
        event TypedEventHandler<IVKLongPollService, VKLongPollServiceStopReason> ServiceStopped;
        /// <summary>
        /// Происходит при получении новых обновлений.
        /// </summary>
        event TypedEventHandler<IVKLongPollService, IReadOnlyList<IVKLongPollUpdate>> UpdatesReceived;

        /// <summary>
        /// Режим отладки. Выводятся подробные данные о состоянии сервиса.
        /// </summary>
        bool DebugMode { get; set; }
        /// <summary>
        /// Работает ли в данный момент сервис.
        /// </summary>
        bool IsWorking { get; }

        /// <summary>
        /// Запустить LongPoll сервис.
        /// </summary>
        void Start();
        /// <summary>
        /// Остановить LongPoll сервис.
        /// </summary>
        void Stop();
    }
}
