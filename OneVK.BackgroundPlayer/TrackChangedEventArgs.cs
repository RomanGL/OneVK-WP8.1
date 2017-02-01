using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.BackgroundPlayer
{
    /// <summary>
    /// Представляет данные о событии изменения текущего трека.
    /// </summary>
    internal class TrackChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Новый трек.
        /// </summary>
        public AudioTrack Track { get; private set; }
        /// <summary>
        /// Идентификатор трека в текущем плейлисте.
        /// </summary>
        public int TrackID { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным треком и его идентификатором в плейлисте.
        /// </summary>
        /// <param name="track">Трек.</param>
        /// <param name="id">Идентификатор трека в плейлисте.</param>
        public TrackChangedEventArgs(AudioTrack track, int id)
        {
            Track = track;
            TrackID = id;
        }
    }
}
