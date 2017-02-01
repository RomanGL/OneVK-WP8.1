using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls
{
    public class FullscreenControl : ContentControl
    {
        public FullscreenControl()
        {
            DefaultStyleKey = typeof(FullscreenControl);
        }

        /// <summary>
        /// Виден ли в данный момент элемент управления.
        /// </summary>
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(FullscreenControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            VisualStateManager.GoToState(this, IsVisible ? "IsOn" : "IsOff", true);
        }
    }
}
