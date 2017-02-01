using Microsoft.Practices.ServiceLocation;
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
using Windows.Phone.UI.Input;
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
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ChoiceFriendsView : Page
    {
        ChoiceFriendsViewModel vm;

        public ChoiceFriendsView()
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
            vm = ServiceLocator.Current.GetInstance<ChoiceFriendsViewModel>();
            vm.Activate();

            FriendsListView.SelectionChanged += UsersListView_SelectionChanged;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            FriendsListView.SelectionChanged -= UsersListView_SelectionChanged;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1);
            vm.Deactivate();            
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            vm.SelectedUsers = new List<VKProfileShort> { (VKProfileShort)e.ClickedItem };
            vm.AddUsers.Execute(null);
        }

        private void EditUsersCommandBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (FriendsListView.SelectionMode == ListViewSelectionMode.None)
            {
                App.CancelGoBack = true;

                FriendsListView.IsItemClickEnabled = false;
                FriendsListView.SelectionMode = ListViewSelectionMode.Multiple;                
            }
            else
            {
                TurnOffSelectionMode();
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FriendsListView.SelectedItems.Count() == 0)
            {
                TurnOffSelectionMode();
            }
            else
            {
                vm.SelectedUsers = FriendsListView.SelectedItems.Cast<VKProfileShort>().ToList();
            }

            vm.AddUsers.RaiseCanExecuteChanged();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (FriendsListView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                e.Handled = true;
                TurnOffSelectionMode();
            }
        }

        private void TurnOffSelectionMode()
        {
            App.CancelGoBack = false;

            FriendsListView.IsItemClickEnabled = true;
            FriendsListView.SelectionMode = ListViewSelectionMode.None;
        }
    }
}
