using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using OneVK.ViewModel;
using OneVK.Controls;
using Windows.Phone.UI.Input;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу диалогов.
    /// </summary>
    public sealed partial class MessagesView : Page
    {
        public MessagesView()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((MessagesViewModel)DataContext).Activate();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ((MessagesViewModel)DataContext).Deactivate();
        }      

        private void Dialogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            ((MessagesViewModel)DataContext).OpenConversationCommand.Execute(e.ClickedItem);
        }
    }
}
