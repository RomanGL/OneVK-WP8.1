using OneVK.Core.Xaml;
using OneVK.Enums.Common;
using OneVK.Model.Common;
using Windows.UI.Xaml;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет кнопку с количеством комментариев.
    /// </summary>
    public class CommentsButton : BaseSocActionButton
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public CommentsButton()
        {
            this.DefaultStyleKey = typeof(CommentsButton);
        }
        
        /// <summary>
        /// Представляет информацию о комментаричх к элементу.
        /// </summary>
        public VKComments Comments
        {
            get { return (VKComments)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register("Comments", typeof(VKComments), 
            typeof(CommentsButton), new PropertyMetadata(null, OnCommentsChanged));

        /// <summary>
        /// Вызывается при изменении информации о комментариях.
        /// </summary>
        private static void OnCommentsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var item = (CommentsButton)obj;
            var comments = e.NewValue as VKComments;

            if (comments == null || comments.CanComment == VKBoolean.False)
            {
                item.Visibility = Visibility.Collapsed;
                return;
            }
            else
                item.Visibility = Visibility.Visible;
        }
    }
}
