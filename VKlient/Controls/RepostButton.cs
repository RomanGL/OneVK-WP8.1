using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model;
using OneVK.Model.Common;
using OneVK.Request;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneVK.Core.Xaml;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет кнопку "Поделиться".
    /// </summary>
    public class RepostButton : BaseSocActionButton
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public RepostButton()
        {
            this.DefaultStyleKey = typeof(RepostButton);
        }

        /// <summary>
        /// Информация о репосте.
        /// </summary>
        public VKReposts Repost
        {
            get { return (VKReposts)GetValue(RepostProperty); }
            set { SetValue(RepostProperty, value); }
        }

        public static readonly DependencyProperty RepostProperty =
            DependencyProperty.Register("Repost", typeof(VKReposts), 
            typeof(RepostButton), new PropertyMetadata(default(VKReposts), OnRepostChanged));

        /// <summary>
        /// Вызывается при изменении информации о репосте.
        /// </summary>
        private static void OnRepostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var item = (RepostButton)obj;

            if (e.NewValue == null) return;

            if (((VKReposts)e.NewValue).UserReposted == VKBoolean.True)
                item.IsActive = true;
            else
                item.IsActive = false;
        }
    }
}
