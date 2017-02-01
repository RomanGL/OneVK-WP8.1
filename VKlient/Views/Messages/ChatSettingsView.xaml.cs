using Microsoft.Practices.ServiceLocation;
using OneVK.Controls;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Profile;
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
using Windows.Phone.UI;
using Windows.Phone.UI.Input;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ChatSettingsView : Page
    {
        ChatSettingsViewModel vm;

        public ChatSettingsView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var chatID = (uint)e.Parameter;
            string uniqueKey = CoreHelper.GetChatSettingsUniqueViewModelKey(chatID);

            vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new ChatSettingsViewModel(chatID));
            DataContext = vm;
            vm.Activate();

            UsersListView.SelectionChanged += UsersListView_SelectionChanged;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            UsersListView.SelectionChanged -= UsersListView_SelectionChanged;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            vm.Deactivate();            
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.ProfileView, ((VKProfileChat)e.ClickedItem).ID);
        }

        private void EditUsersCommandBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListView.SelectionMode == ListViewSelectionMode.None)
            {
                App.CancelGoBack = true;

                UsersListView.IsItemClickEnabled = false;
                UsersListView.SelectionMode = ListViewSelectionMode.Multiple;

                RemoveButton.Visibility = Visibility.Visible;
                LeaveChatButton.IsEnabled = false;

                AddButton.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;

                vm.RemoveUsers.RaiseCanExecuteChanged();
            }
            else
            {
                TurnOffSelectionMode();                
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItems.Count() == 0)
            {
                TurnOffSelectionMode();
            }   
            else
            {
                RemoveButton.IsEnabled = true;
            }
            
            vm.UsersToRemove = UsersListView.SelectedItems.Cast<VKProfileChat>().ToList();
            vm.RemoveUsers.RaiseCanExecuteChanged();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (UsersListView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                e.Handled = true;
                TurnOffSelectionMode();
            }
        }

        private void TurnOffSelectionMode()
        {
            App.CancelGoBack = false;

            UsersListView.IsItemClickEnabled = true;
            UsersListView.SelectionMode = ListViewSelectionMode.None;

            RemoveButton.Visibility = Visibility.Collapsed;
            RemoveButton.IsEnabled = false;
            LeaveChatButton.IsEnabled = true;

            AddButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
        }
    }
}
