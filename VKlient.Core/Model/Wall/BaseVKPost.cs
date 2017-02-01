using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Core.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Newsfeed;
using OneVK.Model.Common;

#if ONEVK_CORE
using GalaSoft.MvvmLight;
#endif

namespace OneVK.Model.Wall
{
    /// <summary>
    /// Базовый объект записи ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public abstract class BaseVKPost : ObservableObject, IVKLikeTarget
#else
    public abstract class BaseVKPost
#endif
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public abstract ulong ID { get; set; }

        /// <summary>
        /// Идентификатор владельца записи.
        /// </summary>
        public abstract long OwnerID { get; set; }

        /// <summary>
        /// Дата публикации записи.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Содержит информацию о комментариях к записи.
        /// </summary>
        [JsonProperty("comments")]
        public VKComments Comments { get; set; }

        /// <summary>
        /// Информация о репостах записи.
        /// </summary>
        [JsonProperty("reposts")]
        public VKReposts Reposts { get; set; }

        /// <summary>
        /// Список вложений.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKAttachment> Attachments { get; set; }

        /// <summary>
        /// Способ размещения записи.
        /// </summary>
        [JsonProperty("post_source")]
        public VKPostSource PostSource { get; set; }

        /// <summary>
        /// Информация о местоположении, прикрепленная к записи.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }

        /// <summary>
        /// Тип записи.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("post_type")]
        public VKNewsfeedPostType PostType { get; set; }

        /// <summary>
        /// Была ли запись оставлена с опцией 
        /// "Только друзья".
        /// </summary>
        [JsonProperty("friends_only")]
        public VKBoolean FriendsOnly { get; set; }

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
        public List<VKAttachedPost> CopyHistory { get; set; }

        /// <summary>
        /// Может ли текущий пользователь изменять запись.
        /// </summary>
        [JsonProperty("can_edit")]
        public VKBoolean CanEdit { get; set; }

        /// <summary>
        /// Может ли текущий пользователь удалить запись.
        /// </summary>
        [JsonProperty("can_delete")]
        public VKBoolean CanDelete { get; set; }

        /// <summary>
        /// Полный текст записи.
        /// </summary>
        [JsonProperty("text")]
        public string FullText { get; set; }

        /// <summary>
        /// Была ли запись создана при удалении профиля.
        /// </summary>
        [JsonProperty("final_post")]
        public VKBoolean IsFinal { get; set; }

#if ONEVK_CORE
        private const int PreviewLength = 300;
        private VKLikes _likes;

        /// <summary>
        /// Является ли текст поста большим.
        /// </summary>
        public bool IsLongText { get { return FullText.Count() > PreviewLength; } }

        /// <summary>
        /// Имеется ли в посте текст.
        /// </summary>
        public bool HasText { get { return !String.IsNullOrEmpty(PreviewText); } }

        /// <summary>
        /// Имеются ли какие-либо вложения к посту.
        /// </summary>
        public bool HasAttachments { get { return Attachments != null; } }

        /// <summary>
        /// Возвращает текст для предпросмотра.
        /// </summary>
        public string PreviewText
        {
            get { return IsLongText ? FullText.Substring(0, PreviewLength) + "..." : FullText; }
        }

        /// <summary>
        /// Информация о лайках к записи.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes
        {
            get { return _likes; }
            set { Set<VKLikes>(() => Likes, ref _likes, value); }
        }

        /// <summary>
        /// Возвращает первый в списке скопированный пост (если имеется).
        /// </summary>
        public VKAttachedPost FirstCopyPost { get { return CopyHistory != null ? CopyHistory.First() : null; } }

        /// <summary>
        /// Имеется ли скопированный пост.
        /// </summary>
        public bool HasCopyPost { get { return CopyHistory != null; } }

        /// <summary>
        /// Объект-владелец данной записи.
        /// </summary>
        public IVKItemOwner Owner { get; set; }

        #region IVKLIkeTarget
        /// <summary>
        /// Возвращает тип объекта для отметки "Мне нравится".
        /// </summary>
        public VKLikesTargetType TargetType { get { return VKLikesTargetType.post; } }
        
        /// <summary>
        /// Идентификатор владельца объекта.
        /// </summary>
        public long LikesOwner { get { return OwnerID; } }

        /// <summary>
        /// Идентификатор объекта для отметки "Мне нравится".
        /// </summary>
        public ulong LikesItem { get { return ID; } }
        
        /// <summary>
        /// Ключ доступа в случае работы с приватным объектам.
        /// Не реализовано.
        /// </summary>
        public string AccessKey { get { return string.Empty; } }
        #endregion
#else
        /// <summary>
        /// Информация о лайках к записи.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }
#endif
    }
}
