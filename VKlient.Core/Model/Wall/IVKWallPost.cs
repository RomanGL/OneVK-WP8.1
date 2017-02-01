using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKlient.Enums.Wall;
using VKlient.Model.Common;

namespace VKlient.Model.Wall
{
    /// <summary>
    /// Интерфейс объекта post.
    /// </summary>
    public interface IVKWallPost : IVKObject
    {
        public long OwnerID { get; set; }
        public long FromID { get; set; }
        public VKDate Date { get; set; }
        public string Text { get; set; }
        public long ReplyOwnerID { get; set; }
        public long ReplyPostID { get; set; }
        public VKFriendsOnly FriendsOnly { get; set; }
        public VKComments Comments { get; set; }
        public VKLikes Likes { get; set; }
        public VKReposts Reposts { get; set; }
        public VKPostType PostType { get; set; }
        public VKPostSource PostSource { get; set; }
        // attachments -- https://vk.com/dev/post
        public VKGeo Geo { get; set; }
        public long SignerID { get; set; }
        public List<VKWallPost> CopyHistory { get; set; }
        public VKCanPin CanPin { get; set; }
        public VKIsPinned IsPinned { get; set; }
    }
}
