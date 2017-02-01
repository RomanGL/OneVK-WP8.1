using System;
using System.Collections.Generic;
using OneVK.Response;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Представляет запрос на получение информации о треке.
    /// </summary>
    public class TrackGetInfoRequest : BaseArtistRequest<LFTrackInfoResponse>
    {
        private string _title;

        /// <summary>
        /// Заголовок трека.
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title", "Заголовок не может быть пустым.");
                _title = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbid">Идентификатор исполнителя или его имя.</param>
        /// <exception cref="ArgumentException"/>
        public TrackGetInfoRequest(string mbid)
            : base(mbid, true) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным заголовком трека и именем исполнителя.
        /// </summary>
        /// <param name="trackTitle">Заголовок трека.</param>
        /// <param name="artistName">Имя исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public TrackGetInfoRequest(string trackTitle, string artistName)
            : base (artistName, false)
        {
            Title = trackTitle;
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return LFMethodsConstants.TrackGetInfo; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (String.IsNullOrWhiteSpace(MBID))
                parameters["track"] = Title;
            return parameters;
        }
    }
}
