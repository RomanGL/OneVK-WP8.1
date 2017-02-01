using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Notifications;
using OneVK.Request;
using OneVK.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace OneVK.Core.Collections
{
    public class NotificationsCollection : StateSupportCollection<IVKNotification>, ISupportUpDownIncrementalLoading
    {
        private string nextFrom;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NotificationsCollection"/>.
        /// </summary>
        public NotificationsCollection()
        {
            HasMoreDownItems = true;
        }

        /// <summary>
        /// Имеются ли элементы для подгрузки вниз.
        /// </summary>
        public bool HasMoreDownItems { get; private set; }

        /// <summary>
        /// Имеются ли элементы для подгрузки вверх.
        /// </summary>
        public bool HasMoreUpItems { get { return false; } }

        /// <summary>
        /// Подгружает элементы вверх списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public Task<object> LoadUpAsync(uint count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Подгружает элементы в конец списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public async Task<object> LoadDownAsync(uint count)
        {
            if (State == ContentState.Loading) return false;
            State = ContentState.Loading;

            var result = await GetNextNotifications(count);
            if (result == null)
            {
                State = ContentState.Error;
                return null;
            }

            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var n in result)
                    this.Add(n);
            });

            HasMoreDownItems = !String.IsNullOrEmpty(nextFrom);
            if (this.Count == 0) State = ContentState.NoData;
            else State = ContentState.Normal;

            return null;  
        }

        /// <summary>
        /// Обновить коллекцию оповещений.
        /// </summary>
        public async Task Update()
        {
            if (this.Count == 0)
            {
                await LoadDownAsync(20);
                return;
            }

            State = ContentState.Loading;
            var result = await GetNextNotifications();
        }

        /// <summary>
        /// Загрузить элементы в коллекцию.
        /// </summary>
        public override async void Load()
        {
            await LoadDownAsync(20);
        }

        /// <summary>
        /// Сбрасывает состояние коллекции.
        /// </summary>
        protected override void Reset()
        {
            nextFrom = null;
        }

        /// <summary>
        /// Возвращает следующую партию оповещений.
        /// </summary>
        /// <param name="count">Количество элементов.</param>
        private async Task<List<IVKNotification>> GetNextNotifications(uint count = 20)
        {
            var parameters = new Dictionary<string, string>();
            parameters["count"] = count.ToString();
            if (!String.IsNullOrEmpty(nextFrom)) parameters["start_from"] = nextFrom;

            var request = new UniversalVKRequest<VKNotificationsGetResponse>(VKMethodsConstants.NotificationsGet, parameters);
            var response = await request.ExecuteAsync();

            if (response.Error.ErrorType == VKErrors.None)
            {
                nextFrom = response.Response.NextFrom;
                return response.Response.Items;
            }
            else
                return null;
        }
    }
}
