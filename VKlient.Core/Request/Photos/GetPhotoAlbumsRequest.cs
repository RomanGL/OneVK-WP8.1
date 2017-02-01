using OneVK.Enums.Common;
using OneVK.Model.Photo;
using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на полученик списка фотоальбомов пользователя 
    /// или сообщетсва.
    /// </summary>
    public class GetPhotoAlbumsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKPhotoAlbum>>
    {
        private List<ulong> _albums;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому
        /// фотоальбомы которого небходимо вернуть.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификаторы альбомов, информацию о которых необходимо вернуть.
        /// </summary>
        public List<ulong> Albums
        {
            get { return _albums; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Albums",
                        "Коллекция должна быть инициализирована.");
                else if (value.Count == 0)
                    throw new ArgumentException("Albums",
                        "Коллекция должна содержать как минимум один элемент.");
                _albums = value;
            }
        }

        /// <summary>
        /// Возвращать системные альбомы.
        /// </summary>
        public VKBoolean ReturnSystem { get; set; }

        /// <summary>
        /// Возвращать путь к фотографии, которая является обложкой альбома
        /// в отдельном поле.
        /// </summary>
        public VKBoolean ReturnCovers { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (Albums != null && Albums.Count != 0) parameters["album_ids"] = String.Join(",", Albums);
            if (ReturnSystem == VKBoolean.True) parameters["need_system"] = "1";
            if (ReturnCovers == VKBoolean.True) parameters["need_covers"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetAlbums; }
    }
}
