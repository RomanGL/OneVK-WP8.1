using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using OneVK.Controls;
using OneVK.Core;
using OneVK.Enums.App;
using OneVK.Model.Promo;
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
    /// Страница рекламной акции.
    /// </summary>
    public sealed partial class PromoView : Page
    {
        public PromoView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null) return;
            string json = e.Parameter.ToString();

            var promo = JsonConvert.DeserializeObject<OneVKPromo>(json);
            this.DataContext = promo;
        }
    }
}
