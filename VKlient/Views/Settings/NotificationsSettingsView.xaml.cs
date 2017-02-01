using OneVK.Controls;
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
using OneVK.ViewModel.Settings;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NotificationsSettingsView : Page
    {
        public NotificationsSettingsView()
        {
            this.InitializeComponent();
            this.Loaded += NotificationsSettigsView_Loaded;
        }

        private void NotificationsSettigsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= NotificationsSettigsView_Loaded;

            VisualStateManager.GoToState(this,
                ((NotificationsSettingsViewModel)DataContext).EnablePushNotifications ?
                "IsEnabled" : "IsDisabled", true);
        }
    }
}
