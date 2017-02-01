using Microsoft.Practices.ServiceLocation;
using OneVK.Controls;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу профиля сообщества.
    /// </summary>
    public sealed partial class GroupInfoView : Page
    {
        ulong groupID;

        public GroupInfoView()
        {
            InitializeComponent();
        }

        private GroupInfoViewModel vm;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            groupID = (ulong)e.Parameter;
            string uniqueKey = CoreHelper.GetGroupInfoViewModelKey(groupID);

            vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new GroupInfoViewModel(uniqueKey, groupID));
            vm.Activate();
            DataContext = vm;

            WallList.Loaded += WallList_Loaded;
        }

        private void WallList_Loaded(object sender, RoutedEventArgs e)
        {
            var sb = WallList.GetListViewScrollViewer();
            sb.ViewChanging += (s, args) =>
            {
                if (args.FinalView.VerticalOffset > 100)
                    ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.IntermediateFull);
                else
                    ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.Intermediate);
            };
            
            WallList.Loaded -= WallList_Loaded;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //{
            //    vm.WallScrollOffset = 0;
            //    vm.DeleteInstance();
            //}
        }

        private void VideoCounter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.VideosView, -(long)groupID);
        }

        private void AudioCounter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.AudiosView, -(long)groupID);
        }

    }
}
