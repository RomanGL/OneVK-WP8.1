using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение порядка альбомов с видео.
    /// </summary>
    public class VideoReorderAlbumsRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private ulong _albumID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит альбом. .
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _ownerID = value;
            }
        }
        
        /// <summary>
        /// Идентификатор альбома, который нужно переместить.
        /// </summary>
        public ulong AlbumID
        {
            get { return _albumID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _albumID = value;
            }
        }

        /// <summary>
        /// Идентификатор альбома, перед которым нужно поместить текущий.
        /// </summary>
        public ulong Before { get; set; }

        /// <summary>
        /// Идентификатор альбома, после которого нужно поместить текущий.
        /// </summary>
        public ulong After { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="audioID">Идентификатор альбома.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoReorderAlbumsRequest(ulong albumID)
        {
            AlbumID = albumID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["album_id"] = AlbumID.ToString();
            if (Before != 0) parameters["before"] = Before.ToString();
            if (After != 0) parameters["after"] = After.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoReorderAlbums; }
    }
}
