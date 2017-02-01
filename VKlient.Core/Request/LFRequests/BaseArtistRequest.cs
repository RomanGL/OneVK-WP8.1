using System;
using System.Collections.Generic;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Базовый класс запроса к методам исполнителей Last.fm.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseArtistRequest<T> : BaseLFRequest<T>
    {
        private string _mbid;
        private string _artistsName;

        /// <summary>
        /// Идентификатор Musicbrainz исполнителя.
        /// </summary>
        public string MBID
        {
            get { return _mbid; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("MBID",
                        "Строка не может быть пустой.");
                _mbid = value;
            }
        }

        /// <summary>
        /// Имя исполнителя.
        /// </summary>
        public string ArtistsName
        {
            get { return _artistsName; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ArtistName",
                        "Строка не может быть пустой.");
                _artistsName = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbidOrName">Идентификатор исполнителя или его имя.</param>
        /// <param name="isMBID">Является ли переданная строка идентификатором исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public BaseArtistRequest(string mbidOrName, bool isMBID = true)
        {
            if (isMBID)
                MBID = mbidOrName;
            else
                ArtistsName = mbidOrName;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(MBID)) parameters["mbid"] = MBID;
            else
            {
                parameters["artist"] = ArtistsName;
                parameters["autocorrect"] = "1";
            }

            return parameters;
        }
    }
}
