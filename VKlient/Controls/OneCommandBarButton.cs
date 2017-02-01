using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using OneVK.Controls.Common;

namespace OneVK.Controls
{
    /// <summary>
    /// Кнопка панели приложения.
    /// </summary>
    public class OneCommandBarButton : Button, IOneCommandBarElement
    {
        public OneCommandBarButton()
        {
            DefaultStyleKey = typeof(OneCommandBarButton);
        }

        /// <summary>
        /// Иконка кнопки.
        /// </summary>
        public IconElement Icon
        {
            get { return (IconElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(IconElement), typeof(OneCommandBarButton), new PropertyMetadata(default(IconElement)));

        /// <summary>
        /// Метка кнопки.
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(OneCommandBarButton), new PropertyMetadata(default(string)));
    }
}
