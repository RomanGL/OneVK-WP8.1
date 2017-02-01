using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using OneVK.Core;
using OneVK.Helpers;
using OneVK.Model.Photo;

namespace OneVK.Controls
{
    /// <summary>
    /// Элемент управления для отображения миниатюры.
    /// </summary>
    public sealed class ThumbnailControl : Control
    {
        public ThumbnailControl()
        {
            this.DefaultStyleKey = typeof(ThumbnailControl);            
        }

        void ThumbnailControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PointerUp", true);
            this.ReleasePointerCapture(e.Pointer);
        }

        void ThumbnailControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.CapturePointer(e.Pointer);
            VisualStateManager.GoToState(this, "PointerDown", true);
        }

        public IThumbnailSupport Thumbnail
        {
            get { return (IThumbnailSupport)GetValue(ThumbnailProperty); }
            set { SetValue(ThumbnailProperty, value); }
        }

        public static readonly DependencyProperty ThumbnailProperty =
            DependencyProperty.Register("Thumbnail", typeof(IThumbnailSupport), typeof(ThumbnailControl), new PropertyMetadata(default(IThumbnailSupport)));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //this.PointerPressed += ThumbnailControl_PointerPressed;
            //this.PointerReleased += ThumbnailControl_PointerReleased;
        }
    }
}
