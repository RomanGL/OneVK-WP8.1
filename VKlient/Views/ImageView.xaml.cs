using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Threading;
using Windows.UI.Core;
using OneVK.Helpers;
using OneVK.Controls;
using OneVK.Model.Photo;
using Microsoft.Practices.ServiceLocation;
using OneVK.ViewModel;
using Newtonsoft.Json;

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ImageView : Page
    {
        public ImageView()
        {
            this.InitializeComponent();
        }

        private ImageViewModel vm;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = JsonConvert.DeserializeObject<Tuple<List<VKPhotoExtended>, int>>(e.Parameter.ToString());
            var firstPhoto = parameter.Item1.First();
            string uniqueKey = CoreHelper.GetImageViewModelKey(firstPhoto.ID, firstPhoto.OwnerID);

            vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new ImageViewModel(uniqueKey, parameter));
            vm.Activate();
            DataContext = vm;

            CoreHelper.UnlockOrientation();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            vm.Deactivate();
            CoreHelper.LockOrientation();
        }

        private void ScrollRoot_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var scrollRoot = (ScrollViewer)sender;
            Point position = e.GetPosition((UIElement)scrollRoot.Content);
            ThreadPoolTimer.CreateTimer(async args => await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    if (scrollRoot.ZoomFactor > 1)
                    {
                        scrollRoot.ChangeView(0, 0, null, false);
                        scrollRoot.ChangeView(null, null, scrollRoot.MinZoomFactor, false);
                    }
                    else
                        scrollRoot.ChangeView(position.X, position.Y, 2, false);
                }), TimeSpan.FromMilliseconds(100));
        }

        private void page_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ChromeFrame.GetIsVisible(this) == ChromeFrame.VisibilityStates.Hided)
            {
                ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.IntermediateFull);
                CounterPanel.Visibility = Visibility.Visible;
                //bar.Visibility = Visibility.Visible;
            }
            else
            {
                ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.Hided);
                CounterPanel.Visibility = Visibility.Collapsed;
                //bar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
