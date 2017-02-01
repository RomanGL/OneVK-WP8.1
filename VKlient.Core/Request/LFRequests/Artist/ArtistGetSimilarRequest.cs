using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Response;

namespace OneVK.Request.LFRequests
{
    public class ArtistGetSimilarRequest : BaseArtistRequest<LFSimilarArtistsResponse>
    {
        private int _count = 10;

        /// <summary>
        /// Количество возвращаемых элементов.
        /// По умолчанию 10.
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество элементов не может быть отрицательным.");
                _count = value;
            }
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return LFMethodsConstants.ArtistsGetSimilar; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbidOrName">Идентификатор исполнителя или его имя.</param>
        /// <param name="isMBID">Является ли переданная строка идентификатором исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public ArtistGetSimilarRequest(string mbidOrName, bool isMBID = true)
            : base(mbidOrName, isMBID)
        {
            if (isMBID)
                base.MBID = mbidOrName;
            else
                base.ArtistsName = mbidOrName;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["limit"] = Count.ToString();
            return parameters;
        }
    }
}
