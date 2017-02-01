using OneVK.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls.ImageViewer
{
    public class ImageViewer : Control
    {
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
                
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(ImageViewer), new PropertyMetadata(default(object)));

        private ISwiper SwiperSource
        {
            get { return ItemsSource as ISwiper; }
        }
    }
}
