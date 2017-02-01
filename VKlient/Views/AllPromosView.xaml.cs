using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using OneVK.Controls;
using OneVK.Core;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Promo;
using OneVK.ViewModel;
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

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Views
{
    /// <summary>
    /// Страница всех рекламных акций.
    /// </summary>
    public sealed partial class AllPromosView : Page
    {
        public AllPromosView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((BaseViewModel)DataContext).Activate(e.NavigationMode);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.PromoView, JsonConvert.SerializeObject(e.ClickedItem));
        }
    }
}
