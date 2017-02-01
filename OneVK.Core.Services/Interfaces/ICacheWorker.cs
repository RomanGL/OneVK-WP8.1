using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Позволяет кэшировать медиа-файлы. Уведомляет о состоянии кэширования.
    /// </summary>
    public interface ICacheWorker
    {
        /// <summary>
        /// Происходит при завершении первичной буферизации.
        /// </summary>
        event TypedEventHandler<ICacheWorker, EventArgs> BufferingCompleted;
        /// <summary>
        /// Происходит при возникновении ошибки в процессе кэширования.
        /// </summary>
        event TypedEventHandler<ICacheWorker, EventArgs> CachingError;
        /// <summary>
        /// Происходит при отмене операции кэширования до ее завершения.
        /// </summary>
        event TypedEventHandler<ICacheWorker, EventArgs> CachingCanceled;
        /// <summary>
        /// Происходит при завершении операции кэширования.
        /// </summary>
        event TypedEventHandler<ICacheWorker, EventArgs> CachingCompleted;

        /// <summary>
        /// Возвращает значение, завершена ли буферизация.
        /// </summary>
        bool IsBufferingCompleted { get; }
        /// <summary>
        /// Возвращает значение, завершено ли кэширование.
        /// </summary>
        bool IsCachingCompleted { get; }
        /// <summary>
        /// Возвращает поток случайного доступа к кэшированному содержимому.
        /// </summary>
        IRandomAccessStream GetResultStream();

        /// <summary>
        /// Запускает процесс кэширования.
        /// </summary>
        void Start();
        /// <summary>
        /// Останавливает процесс кэширования.
        /// </summary>
        void Stop();
    }
}
