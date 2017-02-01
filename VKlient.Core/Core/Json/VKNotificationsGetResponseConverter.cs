using Newtonsoft.Json;
using OneVK.Response;
using OneVK.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OneVK.Enums.Notifications;

namespace OneVK.Core.Json
{
    /// <summary>
    /// Представляет конвертер для типа <seealso cref="VKNotificationsGetResponse"/>.
    /// </summary>
    public class VKNotificationsGetResponseConverter : JsonConverter
    {
        private List<VKNotificationProfile> profiles;
        private List<VKNotificationGroup> groups;

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType) { return objectType == typeof(VKNotificationsGetResponse); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.ReadFrom(reader);
            var result = new VKNotificationsGetResponse();

            result.Items = new List<IVKNotification>();
            result.Count = obj["count"].ToObject<uint>();
            result.LastViewed = (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(obj["last_viewed"].ToObject<long>());

            JToken nextFrom = obj["next_from"];
            if (nextFrom != null) result.NextFrom = nextFrom.ToString(); 

            profiles = obj["profiles"].ToObject<List<VKNotificationProfile>>();
            groups = obj["groups"].ToObject<List<VKNotificationGroup>>();

            JToken[] itemsArray = obj["items"].ToArray();
            foreach (JToken token in itemsArray)
            {
                VKNotificationType type = token["type"].ToObject<VKNotificationType>();

                switch (type)
                {
                    case VKNotificationType.follow:
                        var fNotification = token.ToObject<VKFollowNotification>();

                        foreach (var actionObject in fNotification.Feedback.Items)
                            SetActionObject(actionObject);
                        result.Items.Add(fNotification);
                        break;
                    case VKNotificationType.friend_accepted:
                        var faNotification = token.ToObject<VKFriendAcceptedNotification>();

                        foreach (var actionObject in faNotification.Feedback.Items)
                            SetActionObject(actionObject);
                        result.Items.Add(faNotification);
                        break;
                    case VKNotificationType.mention:
                        var mNotification = token.ToObject<VKMentionNotification>();
                        SetActionObject(mNotification.Feedback);

                        result.Items.Add(mNotification);
                        break;
                    case VKNotificationType.mention_comments:
                        var mcNotification = token.ToObject<VKMentionCommentsNotification>();
                        SetActionObject(mcNotification.Parent);
                        SetActionObject(mcNotification.Feedback);

                        result.Items.Add(mcNotification);
                        break;
                    case VKNotificationType.wall:
                        var wNotification = token.ToObject<VKWallNotification>();
                        SetActionObject(wNotification.Feedback);

                        result.Items.Add(wNotification);
                        break;
                    case VKNotificationType.wall_publish:
                        var wpNotification = token.ToObject<VKWallPublishNotification>();
                        SetActionObject(wpNotification.Feedback);

                        result.Items.Add(wpNotification);
                        break;
                    case VKNotificationType.comment_post:
                        var cPostNotification = token.ToObject<VKCommentPostNotification>();
                        SetActionObject(cPostNotification.Parent);
                        SetActionObject(cPostNotification.Feedback);

                        result.Items.Add(cPostNotification);
                        break;
                    case VKNotificationType.comment_photo:
                        var cPhotoNotification = token.ToObject<VKCommentPhotoNotification>();
                        SetActionObject(cPhotoNotification.Parent);
                        SetActionObject(cPhotoNotification.Feedback);

                        result.Items.Add(cPhotoNotification);
                        break;
                    case VKNotificationType.comment_video:
                        var cvNotification = token.ToObject<VKCommentVideoNotification>();
                        SetActionObject(cvNotification.Parent);
                        SetActionObject(cvNotification.Feedback);

                        result.Items.Add(cvNotification);
                        break;
                    case VKNotificationType.reply_comment:
                        var rcNotification = token.ToObject<VKReplyCommentNotification>();
                        SetActionObject(rcNotification.Parent);
                        SetActionObject(rcNotification.Feedback);

                        result.Items.Add(rcNotification);
                        break;
                    case VKNotificationType.reply_comment_photo:
                        rcNotification = token.ToObject<VKReplyCommentNotification>();
                        SetActionObject(rcNotification.Parent);
                        SetActionObject(rcNotification.Feedback);

                        result.Items.Add(rcNotification);
                        break;
                    case VKNotificationType.reply_comment_video:
                        rcNotification = token.ToObject<VKReplyCommentNotification>();
                        SetActionObject(rcNotification.Parent);
                        SetActionObject(rcNotification.Feedback);

                        result.Items.Add(rcNotification);
                        break;
                    case VKNotificationType.reply_topic:
                        rcNotification = token.ToObject<VKReplyCommentNotification>();
                        SetActionObject(rcNotification.Parent);
                        SetActionObject(rcNotification.Feedback);

                        result.Items.Add(rcNotification);
                        break;
                    case VKNotificationType.like_post:
                        var lPostNotification = token.ToObject<VKLikePostNotification>();
                        SetActionObject(lPostNotification.Parent);

                        foreach (var actionObject in lPostNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lPostNotification);
                        break;
                    case VKNotificationType.like_comment:
                        var lcNotification = token.ToObject<VKLikeCommentNotification>();
                        SetActionObject(lcNotification.Parent);

                        foreach (var actionObject in lcNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lcNotification);
                        break;
                    case VKNotificationType.like_photo:
                        var lPhotoNotification = token.ToObject<VKLikePhotoNotification>();
                        SetActionObject(lPhotoNotification.Parent);

                        foreach (var actionObject in lPhotoNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lPhotoNotification);
                        break;
                    case VKNotificationType.like_video:
                        var lVideoNotification = token.ToObject<VKLikeVideoNotification>();
                        SetActionObject(lVideoNotification.Parent);

                        foreach (var actionObject in lVideoNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lVideoNotification);
                        break;
                    case VKNotificationType.like_comment_photo:
                        lcNotification = token.ToObject<VKLikeCommentNotification>();
                        SetActionObject(lcNotification.Parent);

                        foreach (var actionObject in lcNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lcNotification);
                        break;
                    case VKNotificationType.like_comment_video:
                        lcNotification = token.ToObject<VKLikeCommentNotification>();
                        SetActionObject(lcNotification.Parent);

                        foreach (var actionObject in lcNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lcNotification);
                        break;
                    case VKNotificationType.like_comment_topic:
                        lcNotification = token.ToObject<VKLikeCommentNotification>();
                        SetActionObject(lcNotification.Parent);

                        foreach (var actionObject in lcNotification.Feedback.Items)
                            SetActionObject(actionObject);

                        result.Items.Add(lcNotification);
                        break;
                    case VKNotificationType.copy_post:
                        break;
                    case VKNotificationType.copy_photo:
                        break;
                    case VKNotificationType.copy_video:
                        break;
                    case VKNotificationType.mention_comment_photo:
                        var mcpNotification = token.ToObject<VKMentionCommentPhotoNotification>();
                        SetActionObject(mcpNotification.Parent);
                        SetActionObject(mcpNotification.Feedback);

                        result.Items.Add(mcpNotification);
                        break;
                    case VKNotificationType.mention_comment_video:
                        var mcvNotification = token.ToObject<VKMentionCommentVideoNotification>();
                        SetActionObject(mcvNotification.Parent);
                        SetActionObject(mcvNotification.Feedback);

                        result.Items.Add(mcvNotification);
                        break;
                }
            }

            result.Profiles = profiles;
            result.Groups = groups;

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Установить объект-инициатор оповещения.
        /// </summary>
        /// <param name="container">Контейнер.</param>
        private void SetActionObject(IActionObjectContainer container)
        {
            if (container.FromID < 0)
                container.ActionObject = groups.FirstOrDefault(g => g.ID == ((ulong)-container.FromID));
            else
                container.ActionObject = profiles.FirstOrDefault(p => p.ID == (ulong)container.FromID);
        }       
    }
}
