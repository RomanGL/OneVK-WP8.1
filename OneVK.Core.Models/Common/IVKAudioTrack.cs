using System;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет аудиотрек ВКонтакте.
    /// </summary>
    public interface IVKAudioTrack : IVKMediaItem, IAudioTrack, IEquatable<IVKAudioTrack>
    {
        /// <summary>
        /// Идентификатор текста аудиозаписи ВКонтакте.
        /// </summary>
        long LyricsID { get; }

        /// <summary>
        /// Длительность аудиозаписи.
        /// </summary>
        TimeSpan Duration { get; }
    }
}
