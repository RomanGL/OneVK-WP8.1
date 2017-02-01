using Microsoft.Practices.ServiceLocation;
using OneVK.Helpers;
using OneVK.Model.Video;
using OneVK.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу сведений о видеозаписи.
    /// </summary>
    public sealed partial class VideoInfoView : Page
    {
        public VideoInfoView()
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
            VKVideoBase video = (VKVideoBase)e.Parameter;
            string uniqueKey = CoreHelper.GetVideoInfoViewModelKey(video.OwnerID, (ulong)video.ID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new VideoInfoViewModel(uniqueKey, video));
            vm.Activate();
            DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                ((VideoInfoViewModel)DataContext).DeleteInstance();
        }
    }
}
