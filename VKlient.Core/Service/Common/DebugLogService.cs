using Newtonsoft.Json;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.ViewModel.Messages;
using System;
using System.Diagnostics;

namespace OneVK.Service.Common
{
    /// <summary>
    /// Представляет DEBUG-сервис логирования работы приложения.
    /// </summary>
    public sealed class DebugLogService : ILogService
    {
        /// <summary>
        /// Логирует указанное сообщение.
        /// </summary>
        /// <param name="message">Сообщение для логирования.</param>
        public void Log(string message)
        {
            string name = "Debug Logger Service";
            string data = String.Format("Залогировано сообщение:\n{0}", message);

            CoreHelper.SendInAppPush(name, "Log",
                PopupMessageType.Error, null, TimeSpan.FromSeconds(7),
                new NavigateToPageMessage
                {
                    Page = AppViews.ErrorView,
                    Parameter = JsonConvert.SerializeObject(new Tuple<string, string>(name, data))
                });

            Debug.WriteLine("Log message: \"{0}\"", message);
        }

        /// <summary>
        /// Логирует указанное исключение.
        /// </summary>
        /// <param name="ex">Исключения для логирования.</param>
        public void Log(Exception ex)
        {
            var data = ex.ToString();
            string name = ex.GetType().ToString();

            CoreHelper.SendInAppPush(name, "Произошла ошибка\nКоснитесь для подробностей",
                PopupMessageType.Error, null, TimeSpan.FromSeconds(7),
                new NavigateToPageMessage
                {
                    Page = AppViews.ErrorView,
                    Parameter = JsonConvert.SerializeObject(new Tuple<string, string>(name, data))
                });

            Debug.WriteLine(ex);
        }
    }
}
