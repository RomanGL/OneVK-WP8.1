using System.Collections.Generic;
using OneVK.Core.Player;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Сообщение с информацией о необходимости воспроизвести трек.
    /// </summary>
    public class PlayTrackMessage
    {
        /// <summary>
        /// Список всех треков.
        /// </summary>
        public IEnumerable<IAudioTrack> Tracks { get; set; }
        /// <summary>
        /// Трек для воспроизведения.
        /// </summary>
        public IAudioTrack TrackToPlay { get; set; }
    }
}
