using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет сообщение о старте воспроизведения новых треков.
    /// </summary>
    public sealed class PlayTracksMessage<T> where T : IAudioTrack
    {
        /// <summary>
        /// Возвращает или задает идентификатор трека, который требуется воспроизвести.
        /// </summary>
        [JsonProperty("track_id")]
        public int TrackID { get; set; }

        /// <summary>
        /// Список треков для воспроизведения.
        /// </summary>
        [JsonProperty("tracks")]
        public List<T> TracksToPlay { get; set; }
    }
}
