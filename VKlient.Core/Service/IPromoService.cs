using OneVK.Model.Promo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис рекламных акций OneVK.
    /// </summary>
    public interface IPromoService
    {
        /// <summary>
        /// Коллекция текущих рекламных акций.
        /// </summary>
        IEnumerable<OneVKPromo> Promos { get; }

        /// <summary>
        /// Обновить рекламные акции.
        /// </summary>
        void Update();
    }
}
