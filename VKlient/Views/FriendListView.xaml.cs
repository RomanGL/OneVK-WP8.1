using Microsoft.Practices.ServiceLocation;
using OneVK.Helpers;
using OneVK.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FriendListView : Page
    {
        public FriendListView()
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
            var listID = (uint)e.Parameter;
            string uniqueKey = CoreHelper.GetFriendListViewModelKey(listID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new FriendListViewModel(uniqueKey, listID));
            vm.Activate();
            DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                ((FriendListViewModel)DataContext).DeleteInstance();
        }
    }
}
