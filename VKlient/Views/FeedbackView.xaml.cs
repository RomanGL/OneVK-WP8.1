using GalaSoft.MvvmLight.Messaging;
using OneVK.Controls;
using OneVK.Core;
using OneVK.Enums.App;
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
using OneVK.ViewModel;

namespace OneVK.Views
{
    /// <summary>
    /// Страница ответов.
    /// </summary>
    public sealed partial class FeedbackView : Page
    {
        public FeedbackView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((BaseViewModel)DataContext).Activate(e.NavigationMode);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ((BaseViewModel)DataContext).Deactivate(e.NavigationMode);
        }
    }
}
