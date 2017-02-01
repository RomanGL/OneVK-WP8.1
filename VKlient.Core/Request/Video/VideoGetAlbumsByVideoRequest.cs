namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение списка альбомов, в которых находится видеозапись.
    /// </summary>
    public class VideoGetAlbumsByVideoRequest : VideoGetAlbumsByVideoBaseRequest<ulong>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса c заданными идентификаторами видеозаписи и
        /// её владельца.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoGetAlbumsByVideoRequest(ulong videoID, long ownerID)
            : base(videoID, ownerID) { }
    }
}
