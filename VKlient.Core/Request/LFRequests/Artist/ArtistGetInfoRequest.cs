using System;
using OneVK.Response;

namespace OneVK.Request.LFRequests
{
    public class ArtistGetInfoRequest : BaseArtistRequest<LFArtistInfoResponse>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен этот запрос.
        /// </summary>
        public override string GetMethod() { return LFMethodsConstants.ArtistsGetInfo; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbidOrName">Идентификатор исполнителя или его имя.</param>
        /// <param name="isMBID">Является ли переданная строка идентификатором исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public ArtistGetInfoRequest(string mbidOrName, bool isMBID = true)
            : base(mbidOrName, isMBID) { }
    }
}
