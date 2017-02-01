using Microsoft.Practices.ServiceLocation;
using OneVK.Model.Promo;
using OneVK.Service;
using System.Collections.Generic;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Сообщение о необходимости обновить рекламные акции в приложении.
    /// </summary>
    public sealed class UpdatePromosMessage
    {
        /// <summary>
        /// Коллекция рекламных акций.
        /// </summary>
        public IEnumerable<OneVKPromo> Promos { get { return ServiceLocator.Current.GetInstance<IPromoService>().Promos; } }
    }
}
