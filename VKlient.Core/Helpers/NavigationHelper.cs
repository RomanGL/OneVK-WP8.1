using GalaSoft.MvvmLight.Messaging;
using OneVK.Enums.App;
using OneVK.ViewModel.Messages;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет помощник навигации.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Выполнить преход в указанное представление.
        /// </summary>
        /// <param name="view">Представление, в которое требуется перейти.</param>
        /// <param name="parameter">Параметр навигации.</param>
        public static void Navigate(AppViews view, object parameter = null)
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Page = view,
                Operation = NavigationType.New,
                Parameter = parameter
            });
        }

        /// <summary>
        /// Выполнить преход в указанное представление с очисткорй журнала переходов.
        /// </summary>
        /// <param name="view">Представление, в которое требуется перейти.</param>
        /// <param name="parameter">Параметр навигации.</param>
        public static void NavigateClear(AppViews view, object parameter = null)
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Page = view,
                Operation = NavigationType.NewClear,
                Parameter = parameter
            });
        }

        /// <summary>
        /// Перейти назад.
        /// </summary>
        public static void GoBack()
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Operation = NavigationType.GoBack
            });
        }

        /// <summary>
        /// Перейти вперед.
        /// </summary>
        public static void GoForward()
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Operation = NavigationType.GoForward
            });
        }
    }
}
