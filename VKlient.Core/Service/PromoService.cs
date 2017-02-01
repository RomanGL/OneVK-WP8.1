using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Model.Promo;
using OneVK.Request;
using OneVK.Enums.Common;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Helpers;
using OneVK.Enums.App;
using Newtonsoft.Json;
using OneVK.Model.Photo;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис рекламных акций OneVK.
    /// </summary>
    public class PromoService : IPromoService
    {
        private const int MAX_RETRIES_NUMBER = 5;

        /// <summary>
        /// Коллекция текущих рекламных акций.
        /// </summary>
        public IEnumerable<OneVKPromo> Promos { get; private set; } = new List<OneVKPromo>();

        /// <summary>
        /// Обновить рекламные акции.
        /// </summary>
        public async void Update()
        {
            if (!ServiceHelper.SettingsService.EnablePromo) return;

            ulong promoGroupID = 0;
            var request = new VKExecuteRequest<uint>(VKMethodsConstants.ExecuteGetPromoGroup);
            for (int i = 0; i < MAX_RETRIES_NUMBER; i++)
            {
                var response = await request.ExecuteAsync();
                if (response.Error.ErrorType != VKErrors.None)
                    continue;

                promoGroupID = response.Response;
                break;
            }

            if (promoGroupID == 0) return;

            var wallRequest = new GetWallRequest { Count = 10, OwnerID = -((long)promoGroupID) };
            for (int i = 0; i < MAX_RETRIES_NUMBER; i++)
            {
                var wall = await wallRequest.ExecuteAsync();
                if (wall.Error.ErrorType != VKErrors.None)
                    continue;

                var promos = new List<OneVKPromo>();
                foreach (var post in wall.Response.Items)
                {
                    try
                    {
                        var promo = JsonConvert.DeserializeObject<OneVKPromo>(post.FullText);
                        promo.PromoImage = ((VKPhoto)post.Attachments[0].Attachment).Photo604;
                        promos.Add(promo);
                    }
                    catch (Exception) { }                    
                }

                Promos = promos;
                Messenger.Default.Send(new UpdatePromosMessage());

                break;
            }
        }

        /// <summary>
        /// Отправить уведомление об акции.
        /// </summary>
        /// <param name="promo">Рекламная акция.</param>
        private void SendPush(OneVKPromo promo)
        {
            CoreHelper.SendInAppPush(promo.Subtitle, promo.Title, PopupMessageType.Info,
                null, TimeSpan.FromSeconds(6),
                new NavigateToPageMessage { Page = AppViews.PromoView, Parameter = JsonConvert.SerializeObject(promo) });
        }
    }
}
