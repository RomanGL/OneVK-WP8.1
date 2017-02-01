using OneVK.Response;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Представляет запрос на получение списка топовых альбомов исполнителя.
    /// </summary>
    public class ArtistGetTopAlbumsRequest : ItemsArtistRequest<LFTopAlbumsResponse>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен этот запрос.
        /// </summary>
        public override string GetMethod() { return LFMethodsConstants.ArtistGetTopAlbums; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором исполнителя.
        /// </summary>
        /// <param name="mbidOrName">Идентификатор исполнителя или его имя.</param>
        /// <param name="isMBID">Является ли переданная строка идентификатором исполнителя.</param>
        /// <exception cref="ArgumentException"/>
        public ArtistGetTopAlbumsRequest(string mbidOrName, bool isMBID = true)
            : base(mbidOrName, isMBID) { }
    }
}
