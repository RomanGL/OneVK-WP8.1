using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Enums.Common;
using OneVK.Enums.LongPoll;
using OneVK.Response;
using OneVK.Request;
using OneVK.Service;
#if ONEVK_CORE
using Microsoft.Practices.ServiceLocation;
#endif
using System.Text;
using System.Net.Http;
using OneVK.Core;

namespace OneVK.Helpers
{  
    /// <summary>
    /// Класс-помощник для работы с ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public static class VKHelper
#else
    internal static class VKHelper
#endif
    {
        /// <summary>
        /// Сервис работы с параметрами.
        /// </summary>
#if ONEVK_CORE
        private static readonly SettingsService _settings = ServiceLocator.Current.GetInstance<SettingsService>();
#else
		private static VKAccessToken _accessToken;

		/// <summary>
		/// Выполняет инициализацию помощника.
		/// </summary>
		/// <param name="accessToken">Ключ доступа.</param>
		/// <param name="userID">Идентификатор текущего пользователя.</param>
		internal static void Initialize(string accessToken, ulong userID)
		{
			if (_accessToken != null)
				throw new InvalidOperationException("Инициализация помощника уже выполнена.");
			_accessToken = new VKAccessToken { AccessToken = accessToken, UserID = userID, ExpiresIn = 0 };
		}        
#endif
        private static readonly string longPollMask = "http://{0}?act=a_check&key={1}&ts={2}&wait=25&mode={3}";

        /// <summary>
        /// Представляет метод, обрабатывающий запрос ввода каптчи.
        /// </summary>
        public static CaptchaRequestHandler CaptchaHandler { private get; set; }

        /// <summary>
        /// Событие указывающее на необходимость повторной авторизации.
        /// </summary>
        public static event Action LoginNeeded;
        /// <summary>
        /// Происходит, когда вызван метод, который требует наличия необъявленного разрешения.
        /// </summary>
        public static event InvalidScopeEventHandler InvalidScope;

        #region Legacy
        internal static void GetResponse<T>(IVKRequestOld request, Action<VKResponse<T>> callback)
        {
            throw new NotSupportedException("Метод более не поддерживается.");
        }

        internal static void GetResponse<T>(string method, Action<VKResponse<T>> callback)
        {
            throw new NotSupportedException("Метод более не поддерживается.");
        }

        internal static async void GetResponse<T>(string methodName,
            Dictionary<string, string> parameters,
            Action<VKResponse<T>> callback)
        {
            throw new NotSupportedException("Метод более не поддерживается.");
        }
        #endregion

        /// <summary>
        /// Сбрасывает состояние обработчика.
        /// </summary>
        public static void Reset()
        {
#if ONEVK_CORE
            ServiceHelper.VKLongPollService.StopLongPolling();
            _settings.AccessToken = null;
#else
            _accessToken = null;
#endif
            if (LoginNeeded != null)
                LoginNeeded();            
        }

        /// <summary>
        /// Запускает процесс получения результата на запрос прямой авторизации
        /// и при необходимости обрабатывает каптчу.
        /// </summary>
        /// <param name="parameters">Словарь параметров.</param>
        /// <param name="callback">Метод, который будет вызван после завершения запроса.
        /// Параметр является результатом запроса.</param>
        internal static async void GetLoginResponse(Dictionary<string, string> parameters, Action<VKLoginResponse> callback)
        {
            string json = await GetAsync(GetFullAuthQueryString(parameters));

            if (String.IsNullOrEmpty(json))
            {
                try { callback(new VKLoginResponse { ErrorName = AppConstants.ConnectionError }); }
                catch (Exception) { }
                return;
            }

            var response = JsonConvert.DeserializeObject<VKLoginResponse>(json);
            if (response.ErrorName == AppConstants.NeedCaptcha)
            {
                #region Legacy
                //InvokeCaptchaRequest1(response.GetCaptcha,
                //    captchaResponse =>
                //    {
                //        if (!captchaResponse.IsCanceled)
                //        {
                //            var newParameters = new Dictionary<string, string>(parameters);
                //            newParameters["captcha_sid"] = captchaResponse.Request.CaptchaSid;
                //            newParameters["captcha_key"] = captchaResponse.UserResponse;

                //            GetLoginResponse(newParameters, callback);
                //        }
                //        else
                //        {
                //            try { callback(new VKLoginResponse { ErrorName = AppConstants.CaptchaCanceled }); }
                //            catch (Exception) { }
                //        }
                //    }); 
                #endregion
            }
            else
            {
                try { callback(response); }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Возвращает строку метода.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        private static string GetMethodString(string methodName)
        {
            return VKMethodsConstants.VKRequestURL + methodName;
        }

        /// <summary>
        /// Возвращает строку параметров.
        /// </summary>
        /// <param name="parameters">Словарь параметров.</param>
        private static string GetParametersString(Dictionary<string, string> parameters)
        {
            return String.Join("&", parameters.Select(
                kp => Uri.EscapeDataString(kp.Key) + "=" + Uri.EscapeDataString(kp.Value)));
        }

        /// <summary>
        /// Возвращает полную строку для запроса авторизации.
        /// </summary>
        /// <param name="parameters">Словарь параметров.</param>
        private static string GetFullAuthQueryString(Dictionary<string, string> parameters)
        {
            return VKMethodsConstants.VKDirectAuthURL + String.Join(
                "&", parameters.Select(kp => Uri.EscapeDataString(kp.Key) + "=" + Uri.EscapeDataString(kp.Value)));
        }               

        /// <summary>
        /// Выполняет запрос к ВКонтакте и возвращает результат.
        /// </summary>
        /// <typeparam name="T">Тип результирующих данных.</typeparam>
        /// <param name="request">Объект запроса.</param>
        public static async Task<VKResponse<T>> GetResponse<T>(IVKRequest<T> request, CancellationToken ct = default(CancellationToken))
        {
            return await GetResponse<T>(request.GetMethod(), request.GetParameters(), ct);
        }

        /// <summary>
        /// Выполняет запрос к ВКонтакте и возвращает результат.
        /// </summary>
        /// <typeparam name="T">Тип результирующего объекта.</typeparam>
        /// <param name="methodName">Имя метода, к которому требуется обратиться.</param>
        /// <param name="parameters">Словарь параметров, специфичных для этого метода.</param>
        private static async Task<VKResponse<T>> GetResponse<T>(string methodName, Dictionary<string,string> parameters,
            CancellationToken ct = default(CancellationToken))
        {
#if ONEVK_CORE
            if (_settings.AccessToken == null)
#else
            if (_accessToken == null)
#endif
            {
                if (LoginNeeded != null)
                    LoginNeeded();
                return new VKResponse<T> { Error = new VKError { ErrorType = VKErrors.AuthorizationFailed } };
            }
            
            parameters["v"] = VKMethodsConstants.ApiVersion;
#if ONEVK_CORE
            parameters["access_token"] = _settings.AccessToken.AccessToken;
#else
            parameters["access_token"] = _accessToken.AccessToken;
#endif

            string method = GetMethodString(methodName);
            string query = String.Format("{0}&{1}", method, GetParametersString(parameters));
            string json = Encoding.Unicode.GetByteCount(query) < 4096 ?
                await GetAsync(query, ct: ct) : await PostAsync(method, parameters, ct: ct);

            if (String.IsNullOrEmpty(json))
                return new VKResponse<T> { Error = new VKError { ErrorType = VKErrors.ConnectionError } };

            VKResponse<T> response = null;

            if (json == "{\"response\":[]}")
                return new VKResponse<T> { Response = Activator.CreateInstance<T>() };
            try { response = JsonConvert.DeserializeObject<VKResponse<T>>(json); }
            catch (JsonSerializationException e) { throw new JsonSerializationException(json, e); }
            catch (JsonReaderException e) { throw new JsonReaderException(json, e); }
            catch (Exception) { return new VKResponse<T> { Error = new VKError { ErrorType = VKErrors.UnknownError } }; }

            if (response.Error.ErrorType == VKErrors.CaptchaNeeded)
            {
                try
                {
                    response = await Task.Run(async () =>
                    {
                        var captchaResponse = await InvokeCaptchaRequest(response.Error.Captcha);
                        if (captchaResponse == null) throw new ArgumentNullException("Ответ на каптчу не может быть null.");
                        if (!captchaResponse.IsCanceled)
                        {
                            var newParameters = new Dictionary<string, string>(parameters);
                            newParameters["captcha_sid"] = captchaResponse.Request.CaptchaSid;
                            newParameters["captcha_key"] = captchaResponse.UserResponse ?? String.Empty;

                            return await GetResponse<T>(methodName, newParameters, ct);
                        }
                        return new VKResponse<T> { Error = new VKError { ErrorType = VKErrors.CaptchaCanceled } };
                    });
                }
                catch (Exception)
                { return new VKResponse<T> { Error = new VKError { ErrorType = VKErrors.CaptchaCanceled } }; }
            }
            else if (response.Error.ErrorType == VKErrors.AuthorizationFailed)
            {
                if (LoginNeeded != null)
                    LoginNeeded();
            }
            else if (response.Error.ErrorType == VKErrors.PermissionIsDenied)
            {
                if (InvalidScope != null)
                    InvalidScope(new InvalidScopeEventArgs(methodName, parameters));
            }

            return response;         
        }

        /// <summary>
        /// Выполняет запрос к LongPoll-серверу ВКонтакте и возвращает ответ при его получении.
        /// </summary>
        /// <param name="server">Адрес сервера.</param>
        /// <param name="key">Ключ для подключения.</param>
        /// <param name="ts">Номер последнего события, начиная с которого требуется 
        /// получить изменения.</param>
        /// <param name="mode">Параметр, определяющий наличие поля прикреплений в 
        /// получаемых данных с помощью битовой маски.</param>
        /// <param name="ct">Токен для отмены операции.</param>
        internal static async Task<VKLongPollResponse> GetLongPollResponse(string server, string key, 
            string ts, byte mode, CancellationToken ct)
        {
            string json = await GetAsync(String.Format(longPollMask, server, key, ts, mode), 
                TimeSpan.FromSeconds(26), ct);

            if (String.IsNullOrEmpty(json))
                return new VKLongPollResponse { Error = VKLongPollErrors.ConnectionError };

            return JsonConvert.DeserializeObject<VKLongPollResponse>(json);
        }        

        /// <summary>
        /// Делает GET-запрос указанному ресурсу и асинхронно возвращает результат.
        /// </summary>
        /// <param name="query">Строка запроса (URL).</param>
        /// <param name="timeout">Таймаут операции.</param>
        /// <param name="ct">Токен отмены.</param>
        private static async Task<string> GetAsync(string query, TimeSpan timeout = default(TimeSpan),
            CancellationToken ct = default(CancellationToken))
        {
            string result = String.Empty;
            using (var client = new HttpClient())
            {
                if (timeout != TimeSpan.Zero)
                    client.Timeout = timeout;
                try
                {
                    var response = await client.GetAsync(query, ct);
                    result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception) { }                
            }
            return result;
        }

        /// <summary>
        /// Делает POST-запрос указанному ресурсу указанных параметров и 
        /// асинхронно возвращает результат.
        /// </summary>
        /// <param name="requestURL">URL запроса.</param>
        /// <param name="parameters">Словарь передаваемых параметров.</param>
        /// <param name="timeout">Таймаут операции.</param>
        /// <param name="ct">Токен отмены.</param>
        private static async Task<string> PostAsync(string requestURL, Dictionary<string, string> parameters,
            TimeSpan timeout = default(TimeSpan), CancellationToken ct = default(CancellationToken))
        {
            string result = String.Empty;
            using (var client = new HttpClient())
            {
                if (timeout != TimeSpan.Zero)
                    client.Timeout = timeout;
                try
                {
                    var response = await client.PostAsync(requestURL, new FormUrlEncodedContent(parameters));
                    result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception) { }
            }
            return result;
        }

        /// <summary>
        /// Запустить процесс получения каптчи. Асинхронно возвращает введенную каптчу.
        /// </summary>
        /// <param name="request">Объект запроса каптчи.</param>
        private static async Task<VKCaptchaResponse> InvokeCaptchaRequest(VKCaptchaRequest request)
        {
            if (CaptchaHandler == null)
                return new VKCaptchaResponse(request) { UserResponse = String.Empty, IsCanceled = true };
            else
                return await Task.Run<VKCaptchaResponse>(() =>
                {
                    VKCaptchaResponse result = null;
                    var resetEvent = new AutoResetEvent(false);
                    CaptchaHandler(request, response =>
                    {
                        result = response;
                        resetEvent.Set();
                    });
                    resetEvent.WaitOne();
                    return result;
                });
        }
    }
}
