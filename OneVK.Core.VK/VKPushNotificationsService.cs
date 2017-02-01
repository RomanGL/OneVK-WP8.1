using Newtonsoft.Json;
using OneVK.Core.Models;
using OneVK.Core.Services;
using OneVK.Core.VK.Models.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.PushNotifications;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис для работы с Push-уведомлениями ВКонтакте.
    /// </summary>
    public sealed class VKPushNotificationsService : IPushNotificationsService
    {
        private const string WNS_CHANNEL_URI = "WNSChannelUri";
        private const string ENABLE_PUSH_NOTIFICATIONS = "EnablePushNotifications";

        private ISettingsService settingsService;
        private IVKService vkService;
        private IDeviceInformationService deviceInformationService;
        private PushNotificationChannel channel;        

        /// <summary>
        /// Возвращает или задает сохраненный URI WNS канала Push-уведомлений.
        /// </summary>
        private string WNSChannelUri
        {
            get { return settingsService.GetNoCache<string>(WNS_CHANNEL_URI, null); }
            set { settingsService.Set(WNS_CHANNEL_URI, value); }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKPushNotificationsService"/>.
        /// </summary>
        /// <param name="settingsService">Сервис настроек приложения.</param>
        /// <param name="vkService">Сервис для работы с ВКонтакте.</param>
        public VKPushNotificationsService(ISettingsService settingsService, IVKService vkService,
            IDeviceInformationService deviceInformationService)
        {
            this.settingsService = settingsService;
            this.vkService = vkService;
            this.deviceInformationService = deviceInformationService;
        }

        /// <summary>
        /// Регистрирует устройство на получение Push-уведомлений.
        /// </summary>
        public async Task RegisterDevice()
        {
            if (channel == null)
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            if (!String.IsNullOrEmpty(WNSChannelUri) && channel.Uri != WNSChannelUri)
                await UnregisterDevice();

            var parameters = new Dictionary<string, string>
            {
                ["device_id"] = deviceInformationService.GetHardwareID(),
                ["token"] = channel.Uri,
                ["settings"] = "{\"msg\":\"on\",\"chat\":\"on\",\"friend\":\"on\",\"reply\":\"on\",\"mention\":\"on\"}"
            };
            var request = new Request<VKOperationIsSuccess>("account.registerDevice", parameters) { HttpMethod = HttpMethod.POST };
            var response = await vkService.ExecuteRequestAsync(request);

            if (!response.IsSuccess) throw new Exception("Не удалось зарегистрировать устройство");
            WNSChannelUri = channel.Uri;
            channel.PushNotificationReceived += Channel_PushNotificationReceived;
        }

        /// <summary>
        /// Отписывает устройство от получения Push-уведомлений
        /// </summary>
        public async Task UnregisterDevice()
        {
            if (channel == null)
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var parameters = new Dictionary<string, string>
            {
                ["device_id"] = deviceInformationService.GetHardwareID()
            };
            var request = new Request<VKOperationIsSuccess>("account.unregisterDevice", parameters) { HttpMethod = HttpMethod.POST };
            var response = await vkService.ExecuteRequestAsync(request);

            if (!response.IsSuccess) throw new Exception("Не удалось отменить регистрацию устройства");

            channel.Close();
            channel.PushNotificationReceived -= Channel_PushNotificationReceived;
            channel = null;
            WNSChannelUri = null;
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
#if DEBUG
            Debug.WriteLine($"New push type {args.NotificationType}");
#endif
            if (args.NotificationType == PushNotificationType.Toast)
                args.Cancel = true;
        }
    }
}
