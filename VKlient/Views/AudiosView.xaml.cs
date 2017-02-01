using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OneVK.Helpers;
using OneVK.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу аудиозаписей.
    /// </summary>
    public sealed partial class AudiosView : Page
    {
        public AudiosView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            long ownerID = (long)e.Parameter;
            string uniqueKey = CoreHelper.GetAudiosViewModelKey(ownerID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new AudiosViewModel(uniqueKey, ownerID));
            vm.Activate();
            this.DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    ((AudiosViewModel)DataContext).DeleteInstance();
        }

        private void AudioHolding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void EditAudioItemFlyout_Click(object sender, RoutedEventArgs e)
        {

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
                case 2:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshRecommended") });
                    break;
                case 3:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshPopular") });
                    break;
            }
        }
    }
}
