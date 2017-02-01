using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Profile;
using OneVK.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace OneVK.Views
{
    public sealed partial class FriendsView : Page
    {
        public FriendsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            long userID = (long)e.Parameter;
            string uniqueKey = CoreHelper.GetFriendsViewModelKey((ulong)userID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new FriendsViewModel(uniqueKey, (ulong)userID));
            vm.Activate();
            DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    ((FriendsViewModel)DataContext).DeleteInstance();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.ProfileView, ((VKProfileShort)e.ClickedItem).ID);
        }

        private void ContentPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ContentPivot.SelectedIndex)
            {
                case 0:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                        new Binding { Path = new PropertyPath("RefreshCommand") });
                    break; 
                case 1:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshOnlineCommand") });
                    break;
                case 2:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshListCommand") });
                    break;
            }
        }
    }
}
