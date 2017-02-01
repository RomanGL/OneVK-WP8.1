using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.App;
using OneVK.Model.Promo;
using OneVK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления всех акций.
    /// </summary>
    public class AllPromosViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AllPromosViewModel"/>.
        /// </summary>
        public AllPromosViewModel()
        {
        }
        #endregion

        #region Приватные поля
        private bool _isNothing;
        #endregion

        #region Свойства
        /// <summary>
        /// Акции отсутствуют.
        /// </summary>
        public bool IsNothing
        {
            get { return _isNothing; }
            set { Set(() => IsNothing, ref _isNothing, value); }
        }
        /// <summary>
        /// Включены ли рекламные акции.
        /// </summary>
        public bool PromosDisabled { get { return !ServiceLocator.Current.GetInstance<SettingsService>().EnablePromo; } }
        /// <summary>
        /// Коллекция рекламных акций.
        /// </summary>
        public IEnumerable<OneVKPromo> Promos { get; private set; }
        #endregion

        #region Команды
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        /// <param name="mode">Режим навигации.</param>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            if (PromosDisabled)
            {
                Promos = null;
                IsNothing = false;
                return;
            }

            var service = ServiceLocator.Current.GetInstance<IPromoService>();
            if (service.Promos == null || service.Promos.Count() == 0)
            {
                IsNothing = true;
                return;
            }

            IsNothing = false;
            Promos = service.Promos;
            RaisePropertyChanged(() => Promos);
        }
        #endregion

        #region Приватные методы
        #endregion
    }
}
