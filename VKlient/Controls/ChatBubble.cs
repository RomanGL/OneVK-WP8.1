using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneVK.Controls.Common;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет собой элемент чата.
    /// </summary>
    public class ChatBubble : ContentControl
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ChatBubble()
        {
            DefaultStyleKey = typeof(ChatBubble);
            IsEnabledChanged += ChatBubbleIsEnabledChanged;
        }

        /// <summary>
        /// Вызывается при изменении состояния доступности элемента чата.
        /// </summary>
        private void ChatBubbleIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateIsEnabledVisualState();
        }        

        /// <summary>
        /// Направление элемента чата.
        /// </summary>
        public ChatBubbleDirection ChatBubbleDirection
        {
            get { return (ChatBubbleDirection)GetValue(ChatBubbleDirectionProperty); }
            set { SetValue(ChatBubbleDirectionProperty, value); }
        }

        public static readonly DependencyProperty ChatBubbleDirectionProperty =
            DependencyProperty.Register("ChatBubbleDirection", typeof(ChatBubbleDirection), 
            typeof(ChatBubble), new PropertyMetadata(default(ChatBubbleDirection), OnChatBubbleDirectionChanged));

        /// <summary>
        /// Вызывается при изменении направления элемента чата.
        /// </summary>
        private static void OnChatBubbleDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as ChatBubble;

            if (sender != null)
            {
                sender.UpdateChatBubbleDirection();
            }
        }

        /// <summary>
        /// Обновить направление элемента чата.
        /// </summary>
        private void UpdateChatBubbleDirection()
        {
            VisualStateManager.GoToState(this, ChatBubbleDirection.ToString(), true);
        }

        /// <summary>
        /// Обновить состояние доступности элемента чата.
        /// </summary>
        private void UpdateIsEnabledVisualState()
        {
            VisualStateManager.GoToState(this, IsEnabled ? "Normal" : "Disabled", true);
        }

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateChatBubbleDirection();
            UpdateIsEnabledVisualState();
        }
    }
}
