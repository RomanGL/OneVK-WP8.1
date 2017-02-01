using System;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Фотография, размещенная сторонним приложением
    /// в пост на стене ВКонтакте.
    /// </summary>
    public class VKPostedAppPhoto : IVKPhotoBase
    {
        /// <summary>
        /// Идентификатор приложения, разместившего фотографию.
        /// </summary>
        public long AppID { get; set; }
        /// <summary>
        /// Название приложения, разместившего фотографию.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 130x130px. 
        /// </summary>
        public string Photo130 { get; set; }
        /// <summary>
        /// Ссылка на полноразмерную фотографию. 
        /// </summary>
        public string Photo604 { get; set; }

        /// <summary>
        /// Идентификатор фотографии. Не реализован, так как фотография,
        /// размещенная сторонним приложением не имеет идентификатора.
        /// </summary>
        public long ID
        {
            get { throw new NotImplementedException("Фотография, размещенная сторонним приложением не имеет идентификатора."); }
            set { throw new NotImplementedException("Фотография, размещенная сторонним приложением не имеет идентификатора."); }
        }
    }
}
