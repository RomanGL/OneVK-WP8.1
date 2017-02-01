using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Video;
using OneVK.ViewModel;
using OneVK.ViewModel.Messages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу видеозаписей.
    /// </summary>
    public sealed partial class VideosView : Page
    {
        public VideosView()
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
            long ownerID = (long)e.Parameter;
            string uniqueKey = CoreHelper.GetVideosViewModelKey(ownerID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new VideosViewModel(uniqueKey, ownerID));
            vm.Activate();
            DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                ((VideosViewModel)DataContext).DeleteInstance();
        }

        private void VideoHolding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppViews page;
            if (e.ClickedItem is VKVideoBase)
            {
                VKVideoBase parameter;
                parameter = (VKVideoBase)e.ClickedItem;
                page = AppViews.VideoInfoView;
                Messenger.Default.Send(new NavigateToPageMessage
                {
                    Page = page,
                    Operation = NavigationType.New,
                    Parameter = parameter
                });
            }
            if (e.ClickedItem is VKVideoAlbumExtended)
            {
                VKVideoAlbumExtended parameter;
                parameter = (VKVideoAlbumExtended)e.ClickedItem;
                page = AppViews.VideoAlbumView;
                Messenger.Default.Send(new NavigateToPageMessage
                {
                    Page = page,
                    Operation = NavigationType.New,
                    Parameter = parameter
                });
            }
        }

        private void ContentPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ContentPivot.SelectedIndex)
            {
                case 0:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                        new Binding { Path = new PropertyPath("Refresh") });
                    break;
                case 1:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshAlbums") });
                    break;
            }
        }
    }
}
