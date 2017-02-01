using OneVK.Core.Player;
using OneVK.Helpers;
using OneVK.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OneVK.Controls;

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PlayerView : Page
    {
        private PlayerViewModel vm;

        public PlayerView()
        {
            this.InitializeComponent();
            vm = (PlayerViewModel)DataContext;
            TracksList.Loaded += TracksList_Loaded;
            MainPivot.SelectionChanged += MainPivot_SelectionChanged;
        }

        private void TracksList_Loaded(object sender, RoutedEventArgs e)
        {
            TracksList.ScrollIntoView(vm.CurrentTrack);
            TracksList.Loaded -= TracksList_Loaded;
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 1)
            {
                TracksList.ScrollIntoView(vm.CurrentTrack);
                vm.TrackChanged += TrackChanged;
            }
            else
            {
                vm.TrackChanged -= TrackChanged;
            }
        }

        private void TrackChanged(object sender, IAudioTrack e)
        {
            if (TracksList == null) return;
            TracksList.ScrollIntoView(vm.CurrentTrack);
        }

        private void Slider_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ((BindingExpression)((Slider)sender).GetBindingExpression(Slider.ValueProperty)).UpdateSource();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm.Activate();
            PlBackground.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            PlBackground.Stop();
            vm.Deactivate();
        }
    }
}

