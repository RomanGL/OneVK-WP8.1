using OneVK.Enums.Message;
using OneVK.Model.Message;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Представляет селектор шаблонов для собщений.
    /// </summary>
    public sealed class MessageTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон отправленного сообщения.
        /// </summary>
        public DataTemplate SentTemplate { get; set; }
        /// <summary>
        /// Шаблон полученного сообщения.
        /// </summary>
        public DataTemplate ReceivedTemplate { get; set; }
        /// <summary>
        /// Шаблон отправленного сообщения от того же пользователя.
        /// </summary>
        //public DataTemplate SentSimilarTemplate { get; set; }
        /// <summary>
        /// Шаблон полученного сообщения от того же пользователя.
        /// </summary>
        //public DataTemplate ReceivedSimilarTemplate { get; set; }
        /// <summary>
        /// Шаблон полученного сообщения чата.
        /// </summary>
        public DataTemplate ReceivedChatTemplate { get; set; }
        /// <summary>
        /// Шаблон полученного сообщения чата от того же пользователя.
        /// </summary>
        //public DataTemplate ReceivedChatSimilarTemplate { get; set; }
        /// <summary>
        /// Шаблон отправленного сообщения чата.
        /// </summary>
        public DataTemplate SentChatTemplate { get; set; }
        /// <summary>
        /// Шаблон отправленного сообщения чата от того же пользователя.
        /// </summary>
        //public DataTemplate SentChatSimilarTemplate { get; set; }
        /// <summary>
        /// Шаблон системного сообщения ВКонтакте.
        /// </summary>
        public DataTemplate SystemActionTemplate { get; set; }

        /// <summary>
        /// Возвращает шаблон для этого элемента.
        /// </summary>
        /// <param name="item">Элемент, для которого требуется вернуть шаблон.</param>
        /// <param name="container">Контейнер, в котором содержится элемент.</param>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var msg = (VKMessage)item;
            if (msg.Action != VKChatMessageActionType.None) return SystemActionTemplate;
            if (msg.Type == VKMessageType.Sent)
            {
                //if (msg.SimilarNextSender)
                //    return msg.IsChatMessage ? SentChatSimilarTemplate : SentSimilarTemplate;
                //else
                return msg.IsChatMessage ? SentChatTemplate : SentTemplate;
            }
            else
            {
                //if (msg.SimilarNextSender)
                //    return msg.IsChatMessage ? ReceivedChatSimilarTemplate : ReceivedSimilarTemplate;
                //else
                return msg.IsChatMessage ? ReceivedChatTemplate : ReceivedTemplate;
            }
        }
    }
}
