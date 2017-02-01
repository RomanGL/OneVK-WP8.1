using System;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Интерфейс фотографии альбома ВКонтакте.
    /// </summary>
    public interface IVKPhoto : IVKPhotoBase
    {
        long AlbumID { get; set; }
        long OwnerID { get; set; }
        long UserID { get; set; }
        string Photo75 { get; set; }
        string Photo807 { get; set; }
        string Photo1280 { get; set; }
        string Photo2560 { get; set; }
        string MediumPhoto { get; }
        int Width { get; set; }
        int Height { get; set; }
        string Description { get; set; }
        DateTime Date { get; set; }
    }
}
