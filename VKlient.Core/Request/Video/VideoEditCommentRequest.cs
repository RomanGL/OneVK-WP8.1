using System.Collections.Generic;
using OneVK.Model.Common;
using OneVK.Enums.Common;
using OneVK.Helpers;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение комментария к видеозаписи.
    /// </summary>
    public class VideoEditCommentRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _commentID;
        private string _message;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentID
        {
            get { return _commentID; }
            private set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _commentID = value;
            }
        }

        /// <summary>
        /// Новый текст комментария.
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
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["comment_id"] = CommentID.ToString();
            if (!string.IsNullOrWhiteSpace(Message)) parameters["message"] = Message;
            if (Attachments != null && Attachments.Count != 0) parameters["attachments"] = string.Join(",", Attachments);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoEditComment; }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным новым текстом сообщения.
        /// </summary>
        /// <param name="message">Новый текст сообщения.</param>
        /// <exception cref="ArgumentException"></exception>
        public VideoEditCommentRequest(string message)
        {
            Initialize(message, null);
        }

        /// <summary>
        /// Инициализирует экземпляр класса со новым списком прикрепленных к комментарию вложений.
        /// </summary>
        /// <param name="attachments">Вложения.</param>
        public VideoEditCommentRequest(List<VKAttachment> attachments)
        {
            Initialize(null, attachments);
        }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным текстом сообщения и новым списком прикрепленных к комментарию вложений.
        /// </summary>
        /// <param name="message">Новый текст сообщения.</param>
        /// <param name="attachments">Вложения.</param>
        /// <exception cref="ArgumentException"></exception>
        public VideoEditCommentRequest(string message, List<VKAttachment> attachments)
        {
            Initialize(message, attachments);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="message">Новый текст сообщения.</param>
        /// <param name="attachments">Вложения.</param>
        /// <exception cref="ArgumentException"></exception>
        private void Initialize(string message, List<VKAttachment> attachments)
        {
            Message = message;
            Attachments = attachments;
        }
    }
}
