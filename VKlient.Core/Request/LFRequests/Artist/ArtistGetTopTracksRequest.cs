using OneVK.Response;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Представляет метод на получение списка топовых треков исполнителя.
    /// </summary>
    public class ArtistGetTopTracksRequest : ItemsArtistRequest<LFTopTracksResponse>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен этот запрос.
        /// </summary>
        public override string GetMethod() { return LFMethodsConstants.ArtistGetTopTracks; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbidOrName">Идентификатор исполнителя или его имя.</param>
        /// <param name="isMBID">Является ли переданная строка идентификатором исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public ArtistGetTopTracksRequest(string mbidOrName, bool isMBID = true)
            : base(mbidOrName, isMBID) { }
    }
}
