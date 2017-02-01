using System;
using System.Collections.Generic;
using OneVK.Model.Common;
using OneVK.Model.Message;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на отправку сообщения.
    /// </summary>
    public class SendMessageRequest : BaseVKRequest<ulong>
    {
        private string _message;

        /// <summary>
        /// Идентификатор пользователя, которому отправляется сообщение.
        /// </summary>
        public ulong UserID { get; private set; }

        /// <summary>
        /// Короткий адрес пользователя (например, illarionov).
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// Идентификатор беседы, к которой будет относиться сообщение.
        /// </summary>
        public uint ChatID { get; private set; }

        /// <summary>
        /// Идентификаторы получателей сообщения (при необходимости отправить сообщение сразу нескольким пользователям).
        /// </summary>
        public List<ulong> UserIDs { get; private set; }

        /// <summary>
        /// Текст личного cообщения (является обязательным, если не задан параметр attachment).
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value.Trim();
                //Guid = _message.GetHashCode();
            }
        }

        /// <summary>
        /// Уникальный идентификатор, предназначенный для предотвращения повторной отправки одинакового сообщения.
        /// </summary>
        public int @Guid { get; private set; }

        /// <summary>
        /// Медиавложения к личному сообщению.
        /// </summary>
        public List<VKMessageAttachment> Attachment { get; set; }

        /// <summary>
        /// latitude, широта при добавлении местоположения.
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// longitude, долгота при добавлении местоположения.
        /// </summary>
        public string Long { get; set; }

        /// <summary>
        /// Идентификаторы пересылаемых сообщений, перечисленные через запятую.
        /// Перечисленные сообщения отправителя будут отображаться в теле письма у получателя.
        /// </summary>
        public List<ulong> ForwardMessages { get; set; }

        /// <summary>
        /// Идентификатор стикера.
        /// </summary>
        public uint StickerID { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageSend; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public SendMessageRequest(ulong userID)
        {
            if (userID == 0)
                throw new ArgumentOutOfRangeException("userID",
                    "Идентификатор пользователя должен быть положительным.");
            UserID = userID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором чата.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public SendMessageRequest(uint chatID)
        {
            if (chatID == 0)
                throw new ArgumentOutOfRangeException("chatID",
                    "Идентификатор чата должен быть положительным.");
            ChatID = chatID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным доменом пользователя.
        /// </summary>
        /// <param name="domain">Домен пользователя.</param>
        /// <exception cref="ArgumentException"/>
        public SendMessageRequest(string domain)
        {
            if (String.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("domain",
                    "Домен пользователя не может быть пустым.");
            Domain = domain;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (ChatID != 0) parameters["chat_id"] = ChatID.ToString();
            else if (UserID != 0) parameters["user_id"] = UserID.ToString();
            else parameters["domain"] = Domain;            
            if (UserIDs != null && UserIDs.Count != 0) parameters["user_ids"] = String.Join(",", UserIDs);
            if (!String.IsNullOrWhiteSpace(Message)) parameters["message"] = Message;
            //if (@Guid != 0) parameters["guid"] = @Guid.ToString();
            if (Lat != null) parameters["lat"] = Lat;
            if (Long != null) parameters["long"] = Long;
            if (Attachment != null && Attachment.Count != 0) parameters["attachment"] = String.Join(",", Attachment);
            if (ForwardMessages != null && ForwardMessages.Count != 0) parameters["forward_messages"] = String.Join(",", ForwardMessages);
            if (StickerID != 0) parameters["sticker_id"] = StickerID.ToString();

            return parameters;
        }
    }
}
