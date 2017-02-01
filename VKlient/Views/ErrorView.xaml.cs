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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using OneVK.Core;
using OneVK.Controls;
using Newtonsoft.Json;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу для отображения ошибок.
    /// </summary>
    public sealed partial class ErrorView : Page
    {
        public ErrorView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
                return;
                        
            if (e.Parameter is string)
            {                
                var exception = JsonConvert.DeserializeObject<Tuple<string, string>>(e.Parameter.ToString());
                //ExceptionName.Text = exception.Item1;
                ExceptionData.Text = exception.Item2;
            }            
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            MoreData.Visibility = Visibility.Visible;
            MoreButton.IsEnabled = false;
        }
    }
}
