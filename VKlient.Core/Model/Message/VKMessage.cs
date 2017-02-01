using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Message;
using OneVK.Model.Common;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
#if ONEVK_CORE
using OneVK.Model.Profile;
#endif

namespace OneVK.Model.Message
{
    /// <summary>
    /// Представляет личное сообщение ВКонтакте.
    /// </summary>
    public class VKMessage : ObservableObject
    {
        private string _body;
        private VKMessageType _type;
        private List<VKMessage> _forwardedMessages;
        private List<VKMessageAttachment> _attachments;        
        private VKBoolean _readState;

        /// <summary>
        /// Идентификатор автора сообщения.
        /// </summary>
        [JsonProperty("user_id")]
        public ulong UserID { get; set; }
        /// <summary>
        /// Идентификатор чата, если сообщение принадлежит ему.
        /// </summary>
        [JsonProperty("chat_id")]
        public uint ChatID { get; set; }
        /// <summary>
        /// Дата отправки сообщения.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }
        /// <summary>
        /// Статус прочтения ообщения.
        /// </summary>
        [JsonProperty("read_state")]
        public VKBoolean ReadState
        {
            get { return _readState; }
            set
            {
                _readState = value;
                if (value == VKBoolean.True)
                    SentType = VKSentMessageType.Read;
            }
        }
        /// <summary>
        /// Тип сообщения (отправленное/полученное).
        /// </summary>
        [JsonProperty("out")]
        public VKMessageType Type
        {
            get { return _type; }
            set { Set(() => Type, ref _type, value); }
        }     
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        [JsonProperty("body")]
        public string Body
        {
            get { return _body; }
            set { Set(() => Body, ref _body, value); }
        }
        /// <summary>
        /// Информация о местоположении.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }
        /// <summary>
        /// Коллекция медиавложений.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKMessageAttachment> Attachments
        {
            get { return _attachments; }
            set { Set(() => Attachments, ref _attachments, value); }
        }
        /// <summary>
        /// Коллекция пересланных сообщений (если есть).
        /// </summary>
        [JsonProperty("forwaded_messages")]
        public List<VKMessage> ForwardedMessages
        {
            get { return _forwardedMessages; }
            set { Set(() => ForwardedMessages, ref _forwardedMessages, value); }
        }
        /// <summary>
        /// Содержатся ли в сообщении Emoji-смайлы.
        /// </summary>
        [JsonProperty("has_emoji")]
        public VKBoolean HasEmoji { get; set; }
        /// <summary>
        /// Является ли сообщение важным.
        /// </summary>
        [JsonProperty("important")]
        public VKBoolean IsImportant { get; set; }
        /// <summary>
        /// Удалено ли сообщение.
        /// </summary>
        [JsonProperty("deleted")]
        public VKBoolean IsDeleted { get; set; }
        /// <summary>
        /// Идентификатор сообщения (отсутствует у пересланных).
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
        /// <summary>
        /// Тип служебного сообщения, если применимо.
        /// </summary>
        [JsonProperty("action")]
        public VKChatMessageActionType Action { get; set; }
        /// <summary>
        /// Идентификатор пользователя (если > 0) или email (если < 0), которого исключили или пригласили.
        /// </summary>
        [JsonProperty("action_mid")]
        public long ActionMid { get; set; }
        /// <summary>
        /// Название беседы. Строка, для служебных сообщений с <see cref="Action"/> равным 
        /// <see cref="VKChatMessageActionType.ChatCreate"/> или <see cref="VKChatMessageActionType.ChatTitleUpdate"/>.
        /// </summary>
        [JsonProperty("action_text")]
        public string ActionText { get; set; }
        /// <summary>
        /// Email, который пригласили или исключили.
        /// </summary>
        [JsonProperty("action_email")]
        public string ActionEmail { get; set; }

#if ONEVK_CORE
        private VKProfileShort _sender;
        private VKProfileChat _actionUser;
        private VKSentMessageType _sentType;
        private bool _similarNextSender;

        /// <summary>
        /// Профиль отправителя. (Задается отдельно).
        /// </summary>
        public VKProfileShort Sender
        {
            get { return _sender; }
            set { Set(() => Sender, ref _sender, value); }
        }
        /// <summary>
        /// Профиль участника действия. (Задается отдельно).
        /// </summary>
        public VKProfileChat ActionUser
        {
            get { return _actionUser; }
            set { Set(() => ActionUser, ref _actionUser, value); }
        }
        /// <summary>
        /// Сообщение находится в чате.
        /// </summary>
        [JsonProperty("is_chat_message")]
        public bool IsChatMessage { get { return ChatID != 0; } }
        /// <summary>
        /// Имеется ли в сообщении текст.
        /// </summary>
        [JsonIgnore]
        public bool HasText { get { return !String.IsNullOrEmpty(Body); } }

        /// <summary>
        /// Состояние исходящего сообщения.
        /// </summary>
        public VKSentMessageType SentType
        {
            get { return _sentType; }
            set { Set(() => SentType, ref _sentType, value); }
        }

        /// <summary>
        /// Указывает на то, что отправитель предыдущего сообщения такой же.
        /// </summary>
        public bool SimilarNextSender
        {
            get { return _similarNextSender; }
            set { Set(nameof(SimilarNextSender), ref _similarNextSender, value); }
        }
#endif
    }
}
