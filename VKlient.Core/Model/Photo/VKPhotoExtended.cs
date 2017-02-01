using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Model.Common;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Фотография из альбома ВКонтакте с расширенными данными.
    /// </summary>
    public class VKPhotoExtended : VKPhoto
    {
        private VKLikes _likes;
        private VKCount _commentsCount;
        private VKComments _comments;

        /// <summary>
        /// Информация об отметках "Мне нравится" для текущей фотографии.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes
        {
            get { return _likes; }
            set { Set(() => Likes, ref _likes, value); }
        }
        /// <summary>
        /// Количество комментариев к фотографии.
        /// </summary>
        [JsonProperty("comments")]
        public VKCount CommentsCount { get; set; }
        /// <summary>
        /// Количество отметок на фотографии.
        /// </summary>
        [JsonProperty("tags")]
        public VKCount TagsCount { get; set; }
        /// <summary>
        /// Может ли текущий пользователь комментировать фотографию.
        /// </summary>
        [JsonProperty("can_comment")]
        public VKBoolean CanComment { get; set; }
        /// <summary>
        /// Может ли текущий пользователь разместить фотографию у себя.
        /// </summary>
        [JsonProperty("can_repost")]
        public VKBoolean CanRepost { get; set; }
        /// <summary>
        /// Объект комментария.
        /// </summary>
        [JsonIgnore]
        public VKComments Comments
        {
            get
            {
                if (_comments == null && CommentsCount != null)
                    _comments = new VKComments { Count = CommentsCount.Count, CanComment = CanComment };
                return _comments;
            }
            set
            {
                Set(() => Comments, ref _comments, value);
                CommentsCount.Count = value.Count;
                CanComment = value.CanComment;
            }
        }
    }
}
