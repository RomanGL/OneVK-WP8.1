using Newtonsoft.Json;
using OneVK.Model.Common;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Представляет собой отметку пользователя на фотографии ВКонтакте.
    /// </summary>
    public sealed class VKPhotoTag : VKTag
    {
        /// <summary>
        /// Координата X верхнего левого угла прямоугольной области,
        /// на которой сделана отметка в процентах.
        /// </summary>
        [JsonProperty("x")]
        public int X { get; set; }

        /// <summary>
        /// Координата Y верхнего левого угла прямоугольной области,
        /// на которой сделана отметка в процентах.
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; set; }

        /// <summary>
        /// Координата X праого нижнего угла прямоугольной области,
        /// на которой сделана отметка в процентах.
        /// </summary>
        [JsonProperty("x2")]
        public int X2 { get; set; }

        /// <summary>
        /// Координата Y правого нижнего угла прямоугольной области,
        /// на которой сделана отметка в процентах.
        /// </summary>
        [JsonProperty("y2")]
        public int Y2 { get; set; }
    }
}
