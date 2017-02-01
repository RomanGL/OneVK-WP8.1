using OneVK.Enums.Account;
using OneVK.Enums.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос, подписывающий устройство на получение Push-уведомлений.
    /// </summary>
    public sealed class RegisterDeviceRequest : BaseVKRequest<VKBoolean>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором токена для доставки уведомлений.
        /// </summary>
        /// <param name="token">Идентификатор устройства, используемый для отправки уведомлений. 
        /// Для mpns идентификатор должен представлять из себя URL для отправки уведомлений.</param>
        /// <exception cref="ArgumentException"/>
        public RegisterDeviceRequest(string token)
        {
            Token = token;
        }

        private string _token;
               
        /// <summary>
        /// Идентификатор устройства, используемый для отправки уведомлений. 
        /// Для mpns идентификатор должен представлять из себя URL для отправки уведомлений.
        /// </summary>
        public string Token
        {
            get { return _token; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Token",
                        "Токен для отправки уведомлений не может быть пустым.");
                _token = value;
            }
        }

        /// <summary>
        /// Модель устройства.
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Уникальный идентификатор устройства.
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Версия ОС устройства.
        /// </summary>
        public string SystemVersion { get; set; }

        /// <summary>
        /// Не передавать текст в уведомлениях.
        /// По умолчанию False (передавать).
        /// </summary>
        public VKBoolean NoText { get; set; }

        /// <summary>
        /// Список типов уведомления для получения.
        /// </summary>
        public List<VKSubscribeTypes> Subscribe { get; set; }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AccountRegisterDevice; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["token"] = Uri.EscapeDataString(Token);
            if (!String.IsNullOrEmpty(DeviceModel)) parameters["device_model"] = DeviceModel;
            if (!String.IsNullOrEmpty(DeviceID)) parameters["device_id"] = DeviceID;
            if (!String.IsNullOrEmpty(SystemVersion)) parameters["system_version"] = SystemVersion;
            if (NoText == VKBoolean.True) parameters["no_text"] = "1";
            if (Subscribe != null && Subscribe.Count > 0) parameters["subscribe"] = GetSusbcribeFromList(Subscribe);
            else parameters["subscribe"] = "msg,friend";

            return parameters;
        }

        /// <summary>
        /// Возвращает строковое представление списка подписок на уведомления.
        /// </summary>
        /// <param name="subscribe">Список подписок.</param>
        private static string GetSusbcribeFromList(List<VKSubscribeTypes> subscribe)
        {
            var builder = new StringBuilder(40);
            for (int i = 0; i < subscribe.Count; i++)
                switch (subscribe[i])
                {
                    case VKSubscribeTypes.Message:
                        builder.Append("msg,");
                        break;
                    case VKSubscribeTypes.Friend:
                        builder.Append("friend,");
                        break;
                    case VKSubscribeTypes.Call:
                        builder.Append("call,");
                        break;
                    case VKSubscribeTypes.Reply:
                        builder.Append("reply,");
                        break;
                    case VKSubscribeTypes.Mention:
                        builder.Append("mention,");
                        break;
                    case VKSubscribeTypes.Group:
                        builder.Append("group,");
                        break;
                    case VKSubscribeTypes.Like:
                        builder.Append("like,");
                        break;
                }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }
    }
}
