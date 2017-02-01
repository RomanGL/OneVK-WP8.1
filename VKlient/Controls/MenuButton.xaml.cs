using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет кнопку гамбургера.
    /// </summary>
    public sealed partial class MenuButton : UserControl
    {
        private const string HasNotificationsStateName = "HasNotificationsState";
        private const string HasntNotificationsStateName = "HasntNotificationsState";

        public MenuButton()
        {
            this.InitializeComponent();
            Loaded += (s, e) => 
            {
                VisualStateManager.GoToState(this, HasNotification ?
                HasNotificationsStateName : HasntNotificationsStateName,
                true);
            };
            PointerPressed += (s, e) => e.Handled = true;
        }

        /// <summary>
        /// Имеются ли уведомления.
        /// </summary>
        public bool HasNotification
        {
            get { return (bool)GetValue(HasNotificationProperty); }
            set { SetValue(HasNotificationProperty, value); }
        }
        
        public static readonly DependencyProperty HasNotificationProperty =
            DependencyProperty.Register("HasNotification", typeof(bool), typeof(MenuButton), 
                new PropertyMetadata(default(bool), OnHasNotificationChanged));
        
        /// <summary>
        /// Вызвается при изменении значения, указывающего на наличие уведомлений.
        /// </summary>
        private static void OnHasNotificationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((MenuButton)obj, (bool)e.NewValue ?
                HasNotificationsStateName : HasntNotificationsStateName,
                true);
        }
    }
}
