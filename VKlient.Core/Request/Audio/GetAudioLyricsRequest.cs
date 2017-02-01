using OneVK.Model.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получения текста аудиозаписи.
    /// </summary>
    public class GetAudioLyricsRequest : BaseVKRequest<VKAudioLyrics>
    {
        private long _lyricsID;

        /// <summary>
        /// Идентификатор текста аудиозаписи.
        /// </summary>
        public long LyricsID
        {
            get { return _lyricsID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("LyricsID",
                        "Идентификатор текста аудиозаписи не может быть отрицательным числом.");
                _lyricsID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="lyricsID">Идентификатор текста аудиозаписи.</param>
        public GetAudioLyricsRequest(long lyricsID)
        {
            LyricsID = lyricsID;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["lyrics_id"] = LyricsID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetLyrics; }
    }
}
