using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls.Wall
{
    /// <summary>
    /// Представляет содержимое поста.
    /// </summary>
    public class PostContent : Control
    {
        /// <summary>
        /// Пост.
        /// </summary>
        public object Post
        {
            get { return (object)GetValue(PostProperty); }
            set { SetValue(PostProperty, value); }
        }

        public static readonly DependencyProperty PostProperty =
            DependencyProperty.Register("Post", typeof(object), typeof(PostContent), new PropertyMetadata(default(object)));
    }
}
