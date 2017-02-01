using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using OneVK.Core.Messages;
using System;
using System.Collections.ObjectModel;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Представляет диалог ВКонтакте.
    /// </summary>
    public class VKDialog : ObservableObject, IEquatable<VKDialog>
    {
        private ObservableCollection<string> photos;

        /// <summary>
        /// Является ли диалог чатом.
        /// </summary>
        public bool IsChat { get { return Message.ChatID > 0; } }

        /// <summary>
        /// Количество непрочитанных входящих сообщений в диалоге.
        /// </summary>
        [JsonProperty("unread")]
        public int Unread { get; set; }

        /// <summary>
        /// Личное сообщение ВКонтакте.
        /// </summary>
        [JsonProperty("message")]
        public VKDialogMessage Message { get; set; }

        /// <summary>
        /// Коллекция фотографий диалога.
        /// </summary>
        [JsonProperty("photos")]
        public ObservableCollection<string> Photos
        {
            get { return photos; }
            set { Set(() => Photos, ref photos, value); }
        }
        
        /// <summary>
        /// Сравнивает указанный экземпляр с текущим.
        /// </summary>
        /// <param name="other">Экземпляр, с которым требуется сранить текущий.</param>
        public bool Equals(VKDialog other)
        {
            if (this.IsChat != other.IsChat ||
                this.Unread != other.Unread ||
                this.Message.ChatID != this.Message.ChatID ||
                this.Message.UserID != this.Message.UserID) 
                return false;

            return true;
        }
    }
}
