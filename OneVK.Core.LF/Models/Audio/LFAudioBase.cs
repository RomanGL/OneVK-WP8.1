﻿using Newtonsoft.Json;
using System;
using OneVK.Core.Models.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет трек Last.fm.
    /// </summary>
    public class LFAudioBase : LFMediaElement
    {       
        /// <summary>
        /// Длительность композиции.
        /// </summary>
        [JsonConverter(typeof(SecondsToTimeSpanConverter))]
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Количество воспроизведений.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Количество слушателей.
        /// </summary>
        [JsonProperty("listeners")]
        public int ListenersCount { get; set; }

        /// <summary>
        /// Исполнитель трека.
        /// </summary>
        [JsonProperty("artist")]
        public LFArtistBase Artist { get; set; }
    }
}
