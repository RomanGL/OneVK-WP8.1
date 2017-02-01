using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKlient.Enums.Wall;
using VKlient.Model.Common;

namespace VKlient.Model.Wall
{
    public class VKWallPost
    {
        private const string _post = "post";
        private const string _copy = "copy";
        private const string _reply = "reply";
        private const string _postpone = "postpone";
        private const string _suggest = "suggest";

        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, на котором размещена запись.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор автора записи.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }

        /// <summary>
        /// Время публикации записи.
        /// </summary>
        [JsonProperty("date")]
        public VKDate Date { get; set; }

        /// <summary>
        /// Текст записи.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Идентификатор владельца записи, в ответ
        /// на которую была оставлена текущая.
        /// </summary>
        [JsonProperty("reply_owner_id")]
        public long ReplyOwnerID { get; set; }

        /// <summary>
        /// Идентификатор записи, в ответ на которую
        /// была оставлена текущая.
        /// </summary>
        [JsonProperty("reply_post_id")]
        public long ReplyPostID { get; set; }

        /// <summary>
        /// Была ли запись оставлена с опцией 
        /// "Только друзья".
        /// </summary>
        [JsonProperty("friends_only")]
        public VKFriendsOnly FriendsOnly { get; set; }

        /// <summary>
        /// Комментарии к записи.
        /// </summary>
        [JsonProperty("comments")]
        public VKComments Comments { get; set; }

        /// <summary>
        /// Информация о лайках к записи.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }

        /// <summary>
        /// Информация о репостах записи.
        /// </summary>
        [JsonProperty("reposts")]
        public VKReposts Reposts { get; set; }

        /// <summary>
        /// Тип записи в виде строки.
        /// </summary>
        [JsonProperty("post_type")]
        private string _postType { get; set; }

        /// <summary>
        /// Тип записи.
        /// </summary>
        public VKPostType PostType
        {
            get
            {
                switch (_postType)
                {
                    case _copy:
                        return VKPostType.Copy;
                    case _reply:
                        return VKPostType.Reply;
                    case _postpone:
                        return VKPostType.Postpone;
                    case _suggest:
                        return VKPostType.Suggest;
                    default:
                        return VKPostType.Post;
                }
            }
            set
            {
                switch (value)
                {
                    case VKPostType.Post:
                        _postType = _post;
                        break;
                    case VKPostType.Copy:
                        _postType = _copy;
                        break;
                    case VKPostType.Reply:
                        _postType = _reply;
                        break;
                    case VKPostType.Postpone:
                        _postType = _postpone;
                        break;
                    case VKPostType.Suggest:
                        _postType = _suggest;
                        break;
                }
            }
        }

        /// <summary>
        /// Способ размещения записи.
        /// </summary>
        [JsonProperty("post_source")]
        public VKPostSource PostSource { get; set; }

        // attachments -- https://vk.com/dev/post

        /// <summary>
        /// Информация о местоположении.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }

        /// <summary>
        /// Идентификатор автора, если запись была опубликована
        /// от имени сообщества и подписана пользователем.
        /// </summary>
        [JsonProperty("signer_id")]
        public long SignerID { get; set; }

        /// <summary>
        /// История всех репостов записи.
        /// </summary>
        [JsonProperty("copy_history")]
        public List<VKWallPost> CopyHistory { get; set; }

        /// <summary>
        /// Может ли теущий пользователь прикрепить запись.
        /// </summary>
        [JsonProperty("can_pin")]
        public VKCanPin CanPin { get; set; }

        /// <summary>
        /// Прикреплена ли запись.
        /// </summary>
        [JsonProperty("is_pinned")]
        public VKIsPinned IsPinned { get; set; }
    }
}
