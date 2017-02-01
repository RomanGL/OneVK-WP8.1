using OneVK.Model.Video;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение списка альбомов, в которых находится видеозапись.
    /// </summary>
    public class VideoGetAlbumsByVideoExtendedRequest : VideoGetAlbumsByVideoBaseRequest<VKVideoAlbumExtended>
    {
        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["extended"] = "1";
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса c заданными идентификаторами видеоазаписи и
        /// её владельца.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoGetAlbumsByVideoExtendedRequest(ulong videoID, long ownerID)
            : base(videoID, ownerID) { }
    }
}
