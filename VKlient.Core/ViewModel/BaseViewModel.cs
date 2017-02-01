using GalaSoft.MvvmLight;
using OneVK.Enums.App;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Базовая модель представления.
    /// </summary>
    public abstract class BaseViewModel : ViewModelBase
    {
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public virtual void Activate(NavigationMode mode = NavigationMode.New)
        { }

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public virtual void Deactivate(NavigationMode mode = NavigationMode.New)
        { }
        /// <su
        /// mmary>
        /// Возвращает значение, загружены ли данные (исходя из переданного состояния).
        /// </summary>
        /// <param name="state">Состояние, которое требуется проверить.</param>
        public static bool GetIsLoaded(ContentState state)
        {
            return state == ContentState.Normal || state == ContentState.NoData;
        }

    }
}
