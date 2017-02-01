using Microsoft.Practices.ServiceLocation;
using OneVK.Helpers;
using OneVK.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotosView : Page
    {
        public PhotosView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var ownerID = (long)e.Parameter;
            string uniqueKey = CoreHelper.GetPhotosViewModelKey(ownerID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new PhotosViewModel(uniqueKey, ownerID));
            vm.Activate();
            DataContext = vm;
        }
    }
}
