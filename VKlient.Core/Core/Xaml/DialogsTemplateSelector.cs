using OneVK.Enums.Common;
using OneVK.Enums.Message;
using OneVK.Model.Message;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Представляет логику выбора шаблона в списке диалогов.
    /// </summary>
    public sealed class DialogsTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон при отсутствии новых сообщений диалоге.
        /// </summary>
        public DataTemplate DialogNoMessagesTemplate { get; set; }
        /// <summary>
        /// Шаблон при непрочитанном отправленном сообщении в диалоге.
        /// </summary>
        public DataTemplate DialogSentUnreadTemplate { get; set; }
        /// <summary>
        /// Шаблон при наличии новых сообщений в диалоге.
        /// </summary>
        public DataTemplate DialogNewMessagesTemplate { get; set; }
        /// <summary>
        /// Шаблон при наличии отправленного непрочитанного сообщения в чате.
        /// </summary>
        public DataTemplate ChatSentUnreadTemplate { get; set; }
        /// <summary>
        /// Шаблон при наличии новых сообщений в чате.
        /// </summary>
        public DataTemplate ChatNewMessagesTemplate { get; set; }
        /// <summary>
        /// Шаблон при отсутствии новых сообщений в чате.
        /// </summary>
        public DataTemplate ChatNoMessagesTemplate { get; set; }

        /// <summary>
        /// Возвращает шаблон для этого элемента.
        /// </summary>
        /// <param name="item">Элемент, для которого требуется вернуть шаблон.</param>
        /// <param name="container">Контейнер, в котором содержится элемент.</param>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dialog = (VKDialog)item;

            if (dialog.IsChat)
            {
                if (dialog.Message.Type == VKMessageType.Sent)
                {
                    if (dialog.Message.ReadState == VKBoolean.False) return ChatSentUnreadTemplate;
                }
                else
                {
                    if (dialog.Message.ReadState == VKBoolean.False) return ChatNewMessagesTemplate;
                }

                return ChatNoMessagesTemplate;
            }
            else
            {
                if (dialog.Message.Type == VKMessageType.Sent)
                {
                    if (dialog.Message.ReadState == VKBoolean.False) return DialogSentUnreadTemplate;
                }
                else
                {
                    if (dialog.Message.ReadState == VKBoolean.False) return DialogNewMessagesTemplate;
                }

                return DialogNoMessagesTemplate;
            }
        }
    }
}
