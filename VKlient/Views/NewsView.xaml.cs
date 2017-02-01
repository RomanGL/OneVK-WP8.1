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
using OneVK.Controls;
using Microsoft.Practices.ServiceLocation;
using OneVK.ViewModel;
using OneVK.Helpers;
using Newtonsoft.Json;
using OneVK.Core.Collections;

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NewsView : Page
    {
        public NewsView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New && e.Parameter == null)
            {
                ServiceLocator.Current.GetInstance<NewsViewModel>().Refresh.Execute(null);
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Вызывается при удерживаниие поста в ленте.
        /// </summary>
        private void PostItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.GetAttachedFlyout((FrameworkElement)sender).ShowAt(BottomAppBar);
            //FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void NewsListView_Loaded(object sender, RoutedEventArgs e)
        {
            //var sv = NewsListView.GetFirstOrDefaultDescendantOfType<ScrollViewer>();
            //double lastOffset = 0;
            //sv.ViewChanging += (s, args) =>
            //{
            //    if (args.FinalView.VerticalOffset > lastOffset + 100)
            //        ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.IntermediateFull);
            //    else if (args.FinalView.VerticalOffset < lastOffset - 100)
            //        ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.Visible);
            //    else
            //        return;
            //    lastOffset = args.NextView.VerticalOffset;
            //};
        }
    }
}
