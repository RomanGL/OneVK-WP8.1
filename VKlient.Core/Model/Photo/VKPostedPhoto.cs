namespace OneVK.Model.Photo
{
    /// <summary>
    /// Фотография, непосредственно загруженная в пост
    /// на стене ВКонтакте.
    /// </summary>
    public class VKPostedPhoto : IVKPhotoBase
    {
        /// <summary>
        /// Идентификатор фотографии.
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Идентификатор владельца фотографии.
        /// </summary>
        public long OwnerID { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 130x130px. 
        /// </summary>
        public string Photo130 { get; set; }
        /// <summary>
        /// Ссылка на полноразмерную фотографию. 
        /// </summary>
        public string Photo604 { get; set; }
    }
}
