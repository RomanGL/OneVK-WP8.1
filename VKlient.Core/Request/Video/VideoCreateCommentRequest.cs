using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Model.Common;
using OneVK.Helpers;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на создание нового комментария к видеозаписи.
    /// </summary>
    public class VideoCreateCommentRequest : BaseVKRequest<long>
    {
        private ulong _videoID, _replyToComment, _stickerID;
        private string _message;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        public ulong VideoID
        {
            get { return _videoID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _videoID = value;
            }
        }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Message
        {
            get { return _message; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _message = value;
            }
        }

        /// <summary>
        /// Вложения.
        /// </summary>
        public List<VKAttachment> Attachments { get; set; }

        /// <summary>
        /// Будет ли комментарий опубликован от имени группы.
        /// </summary>
        public VKBoolean FromGroup { get; set; }

        /// <summary>
        /// Идентификатор комментария, на который требуется ответить.
        /// </summary>
        public ulong ReplyToComment
        {
            get { return _replyToComment; }
            set
            {
                DataValidationHelper.CheckEqualZero(value);
                _replyToComment = value;
            }
        }

        // <summary>
        /// Идентификатор стикера.
        /// </summary>
        public ulong StickerID
        {
            get { return _stickerID; }
            set
            {
                DataValidationHelper.CheckEqualZero(value);
                _stickerID = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();
            if (!string.IsNullOrWhiteSpace(Message)) parameters["message"] = Message;
            if (Attachments != null && Attachments.Count != 0) parameters["attachments"] = string.Join(",", Attachments);
            if (FromGroup != VKBoolean.False && OwnerID < 0) parameters["from_group"] = "1";
            if (ReplyToComment != 0) parameters["reply_to_comment"] = ReplyToComment.ToString();
            if (StickerID != 0 && Message == null && Attachments == null)
                parameters["sticker_id"] = StickerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoCreateComment; }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным текстом сообщения.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <exception cref="ArgumentException"></exception>
        public VideoCreateCommentRequest(string message)
        {
            Initialize(message, null, 0);
        }

        /// <summary>
        /// Инициализирует экземпляр класса со списком прикрепленных к комментарию вложений.
        /// </summary>
        /// <param name="attachments">Вложения.</param>
        public VideoCreateCommentRequest(List<VKAttachment> attachments)
        {
            Initialize(null, attachments, 0);
        }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным текстом сообщения и списком прикрепленных к комментарию вложений.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="attachments">Вложения.</param>
        /// <exception cref="ArgumentException"></exception>
        public VideoCreateCommentRequest(string message, List<VKAttachment> attachments)
        {
            Initialize(message, attachments, 0);
        }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным идентификатором стикера.
        /// </summary>
        /// <param name="stickerID">Идентификатор стикера.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoCreateCommentRequest(ulong stickerID)
        {
            Initialize(null, null, stickerID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="attachments">Вложения.</param>
        /// <param name="stickerID">Идентификатор стикера.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(string message, List<VKAttachment> attachments, ulong stickerID)
        {
            Message = message;
            Attachments = attachments;
            StickerID = stickerID;
        }
    }
}
