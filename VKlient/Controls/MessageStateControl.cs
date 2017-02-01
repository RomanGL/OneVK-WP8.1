using OneVK.Enums.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет элемент управления для отображения состояние прочтения сообщения.
    /// </summary>
    public class MessageStateControl : Control
    {
        public MessageStateControl()
        {
            this.DefaultStyleKey = typeof(MessageStateControl);
        }

        /// <summary>
        /// Состояние исходящего сообщения.
        /// </summary>
        public VKSentMessageType SentType
        {
            get { return (VKSentMessageType)GetValue(SentTypeProperty); }
            set { SetValue(SentTypeProperty, value); }
        }
        
        public static readonly DependencyProperty SentTypeProperty =
            DependencyProperty.Register("SentType", typeof(VKSentMessageType), 
                typeof(MessageStateControl), new PropertyMetadata(default(VKSentMessageType), OnSentTypeChanged));

        /// <summary>
        /// Команда повтора отправки сообщения.
        /// </summary>
        public ICommand RetryCommand
        {
            get { return (ICommand)GetValue(RetryCommandProperty); }
            set { SetValue(RetryCommandProperty, value); }
        }
        
        public static readonly DependencyProperty RetryCommandProperty =
            DependencyProperty.Register("RetryCommand", typeof(ICommand), typeof(MessageStateControl), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Параметр команды повтора отправки сообщения.
        /// </summary>
        public object RetryCommandParameter
        {
            get { return (object)GetValue(RetryCommandParameterProperty); }
            set { SetValue(RetryCommandParameterProperty, value); }
        }
        
        public static readonly DependencyProperty RetryCommandParameterProperty =
            DependencyProperty.Register("RetryCommandParameter", typeof(object), typeof(MessageStateControl), new PropertyMetadata(default(object)));

        /// <summary>
        /// Вызывается при изменении состояния исходящего сообщения.
        /// </summary>
        private static void OnSentTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState(obj as Control, ((VKSentMessageType)e.NewValue).ToString(), true);
        }

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            VisualStateManager.GoToState(this, SentType.ToString(), true);
        }
    }
}
