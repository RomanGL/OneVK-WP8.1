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
using OneVK.ViewModel.Messages;
using GalaSoft.MvvmLight.Messaging;
using OneVK.Helpers;
using OneVK.Response;
using OneVK.Enums.App;
using GalaSoft.MvvmLight.Command;
using OneVK.Enums.Common;
using OneVK.Core;
using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.VK.Models.Common;
using OneVK.Core.ViewModels;

namespace OneVK.Views
{
    /// <summary>
    /// Представление страницы браузерной авторизации.
    /// </summary>
    public sealed partial class LoginView : VisualStateAwarePage
    {
        private bool _isCompleted;
        private bool _isLoading;
        public RelayCommand LoginCommand { get; private set; }

        public LoginView()
        {
            this.InitializeComponent();
            LoginCommand = new RelayCommand(
                () => browser.Source = new Uri(ServiceHelper.VKLoginService.GetOAuthURL()),
                () => !_isLoading);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }

            base.OnNavigatedTo(e);
        }

        private void browser_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            _isLoading = true;
            _isCompleted = false;
            LoginCommand.RaiseCanExecuteChanged();
            VisualStateManager.GoToState(this, "Loading", true);
            if (args.Uri.AbsoluteUri.Contains("token="))
            {
                _isCompleted = true;
                var parts = args.Uri.Fragment.Substring(1).Split('&').ToArray();
                string token = parts[0].Split('=')[1];
                ulong userID = ulong.Parse(parts[2].Split('=')[1]);

                var accessToken = new VKAccessToken { AccessToken = token, UserID = (long)userID };

                var response = new VKLoginResponse(token, userID);
                ServiceHelper.VKLoginService.CompleteLogIn(response);

                if (!((LoginViewModel)DataContext).LoginToken(accessToken))
                {
                    VisualStateManager.GoToState(this, "Normal", true);
                    return;
                }            

                //Messenger.Default.Send(new LoginMessage { State = VKLoginStates.Login });
            }
            else if (args.Uri.AbsoluteUri.Contains("error="))
            {
                var parts = args.Uri.Fragment.Substring(1).Split('&').ToArray();
                string error = parts[0].Split('=')[1];

                if (error == "access_denied")
                {
                    ServiceHelper.DialogService.ShowMessage(
                        "Не удалось выполнить авторизацию. Возможно, вы ее отменили.", 
                        "Ошибка авторизации");
                }
                _isCompleted = true;
                VisualStateManager.GoToState(this, "Normal", true);
            }
        }

        private void browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (_isCompleted)
            {
                _isLoading = false;
                LoginCommand.RaiseCanExecuteChanged();
                VisualStateManager.GoToState(this, "Normal", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Auth", true);
            }
        }

        private void browser_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            ServiceHelper.DialogService.ShowMessage(
                        "Не удалось подключиться к ВКонтакте. Авторизация не удалась.",
                        "Ошибка соединения");
            _isCompleted = true; 
            _isLoading = false;
            LoginCommand.RaiseCanExecuteChanged();
            VisualStateManager.GoToState(this, "Normal", true);
        }
    }
}
