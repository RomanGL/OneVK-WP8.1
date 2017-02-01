using System;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет аудиотрек проигрывателя ВКачай.
    /// </summary>
    public interface IAudioTrack : IEquatable<IAudioTrack>
    {
        /// <summary>
        /// Заголовок трека.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Исполнитель трека.
        /// </summary>
        string Artist { get; }
        /// <summary>
        /// Путь к источнику воспроизведения трека.
        /// </summary>
        string Source { get; }
    }
}
