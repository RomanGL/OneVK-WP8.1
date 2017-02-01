using System;
using System.Collections.Generic;
using OneVK.Model.Common;

namespace OneVK.Model.Comment
{
    /// <summary>
    /// Интерфейс комментария ВКонтакте
    /// </summary>
    public interface IVKComment : IVKObject
    {
        long FromID { get; set; }
        DateTime Date { get; set; }
        string Comment { get; set; }
        long ReplyToUserID { get; set; }
        long ReplayToCommentID { get; set; }
        List<VKAttachment> Attachments { get; set; }
    }
}
