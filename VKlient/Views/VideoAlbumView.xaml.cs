using Microsoft.Practices.ServiceLocation;
using OneVK.Helpers;
using OneVK.Model.Video;
using OneVK.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OneVK.Views
{
    /// <summary>
    /// Представление страницы видеоальбома.
    /// </summary>
    public sealed partial class VideoAlbumView : Page
    {
        public VideoAlbumView()
        {
            InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ServiceHelper.CoreService.ExecuteItem(e.ClickedItem);
        }

        private void VideoHolding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var album = (VKVideoAlbumExtended)e.Parameter;
            string uniqueKey = CoreHelper.GetVideoAlbumViewModelKey(album.ID, album.OwnerID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new VideoAlbumViewModel(uniqueKey, album));
            vm.Activate();
            DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                ((VideoAlbumViewModel)DataContext).DeleteInstance();
        }
    }
}
