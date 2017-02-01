using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using OneVK.Core.VK.Models.Newsfeed;
using OneVK.Core.VK.Models.Common;
using Windows.UI.Xaml;
using OneVK.Helpers;

namespace OneVK.Controls.Wall
{
    /// <summary>
    /// Представляет презентер постов.
    /// </summary>
    public class PostPresenter : Control
    {
        private Panel contentPanel;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PostPresenter"/>.
        /// </summary>
        public PostPresenter()
        {
            this.DefaultStyleKey = typeof(PostPresenter);
        }
        
        /// <summary>
        /// Пост.
        /// </summary>
        public object Post
        {
            get { return (object)GetValue(PostProperty); }
            set { SetValue(PostProperty, value); }
        }
        
        public static readonly DependencyProperty PostProperty =
            DependencyProperty.Register("Post", typeof(object), typeof(PostPresenter), new PropertyMetadata(default(object), OnPostChanged));

        /// <summary>
        /// Отобразить пост целиком.
        /// </summary>        
        public bool IsFull
        {
            get { return (bool)GetValue(IsFullProperty); }
            set { SetValue(IsFullProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFull.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullProperty =
            DependencyProperty.Register("IsFull", typeof(bool), typeof(PostPresenter), new PropertyMetadata(default(bool)));

        public object ParentDataContext
        {
            get { return (object)GetValue(ParentDataContextProperty); }
            set { SetValue(ParentDataContextProperty, value); }
        }
        
        public static readonly DependencyProperty ParentDataContextProperty =
            DependencyProperty.Register("ParentDataContext", typeof(object), typeof(PostPresenter), new PropertyMetadata(default(object)));

        private static void OnPostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PostPresenter)obj).ProcessPost();
        }

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            contentPanel = GetTemplateChild("ContentPanel") as Panel;

            ProcessPost();
            base.OnApplyTemplate();
        }

        private void ProcessPost()
        {
            if (contentPanel == null) return;
            contentPanel.Children.Clear();

            if (Post == null) return;
            if (Post is VKNewsfeedItem)
            {
                var item = (VKNewsfeedItem)Post;

                contentPanel.Children.Add(new PostContent
                {
                    Style = App.Current.Resources["ShortNewsfeedPostContentStyle"] as Style,
                    Post = item,
                    DataContext = this.ParentDataContext
                });
            }
        }
    }
}
