using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using OneVK.Request;
using OneVK.Enums.Common;
using OneVK.Enums.Account;

namespace OneVK.Helpers
{
    public static class NotificationsHelper
    {
        private static PushNotificationChannel _chanel;

        /// <summary>
        /// Подписывает приложение на получение Push-уведомлений.
        /// </summary>
        /// <param name="updateChanel">Требуется ли обновить канал получения уведомлений.</param>
        public static async Task<bool> Connect(bool updateChanel = false)
        {
            if (_chanel == null)
            {
                _chanel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                _chanel.PushNotificationReceived += chanel_PushNotificationReceived;
            }

            var request = new RegisterDeviceRequest(_chanel.Uri)
            {
                DeviceID = CoreHelper.GetUniqueDeviceID(),
                DeviceModel = CoreHelper.GetDeviceName(),
                Subscribe = new List<VKSubscribeTypes>
                {
                    VKSubscribeTypes.Friend, VKSubscribeTypes.Group,
                    VKSubscribeTypes.Like, VKSubscribeTypes.Mention,
                    VKSubscribeTypes.Message, VKSubscribeTypes.Reply
                }            
            };
            var result = await request.ExecuteAsync();

            return result.Error.ErrorType == VKErrors.None ? true : false;                
        }

        private static void chanel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            
        }
    }
}
