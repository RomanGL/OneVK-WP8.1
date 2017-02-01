using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneVK.Core.VK.Models;
using Windows.Web.Http;
using OneVK.Core.Services;
using OneVK.Core.VK.Models.Common;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис для работы с ВКонтакте.
    /// </summary>
    public sealed class VKService : IVKService
    {
        private const string API_ROOT = "https://api.vk.com/method/";

        private IVKLoginService loginService;

        /// <summary>
        /// Метод, выполняющий запрос каптчи у пользователя.
        /// </summary>
        public CaptchaRequestHandler CaptchaRequest { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKService"/> с
        /// необходимым набором сервисов.
        /// </summary>
        /// <param name="loginService">Сервис авторизации ВКонтакте.</param>
        public VKService(IVKLoginService loginService)
        {
            this.loginService = loginService;
        }

        /// <summary>
        /// Выполняет указанный запрос к ВКонтакте и возвращает ответ.
        /// </summary>
        /// <typeparam name="T">Тип результирующих данных.</typeparam>
        /// <param name="request">Объект запроса к ВКонтакте.</param>
        public async Task<VKResponse<T>> ExecuteRequestAsync<T>(IRequest<T> request)
        {
            return await ProcessRequestAsync(request);
        }

        /// <summary>
        /// Выполнить запрос к ВКонтакте.
        /// </summary>
        /// <typeparam name="T">Тип результирующих данных.</typeparam>
        /// <param name="request">Объект запроса к ВКонтакте.</param>
        private async Task<VKResponse<T>> ProcessRequestAsync<T>(IRequest<T> request)
        {
            if (!loginService.IsAuthorized) return new VKResponse<T> { Error = VKErrors.AuthorizationFailed };

            var parameters = request.GetParameters();
            parameters["v"] = Constants.API_VERSION;

            try { parameters["access_token"] = loginService.Token; }
            catch(InvalidOperationException) { return new VKResponse<T> { Error = VKErrors.AuthorizationFailed }; }

            string json = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = null;
                    if (request.HttpMethod == Core.Models.HttpMethod.GET)
                        httpResponse = await client.GetAsync(new Uri(GetRequestUrl(API_ROOT + request.GetMethod(), parameters)))
                            .AsTask(request.Token);
                    else
                        httpResponse = await client.PostAsync(new Uri(API_ROOT + request.GetMethod()), new HttpFormUrlEncodedContent(parameters))
                            .AsTask(request.Token);

                    json = await httpResponse.Content.ReadAsStringAsync().AsTask(request.Token);
                }
            }            
            catch (OperationCanceledException) { return new VKResponse<T> { Error = VKErrors.OperationCanceled }; }
            catch (Exception) { }

            if (String.IsNullOrEmpty(json)) return new VKResponse<T> { Error = VKErrors.ConnectionError };
                        
            //if (json == "{\"response\":[]}") return new VKResponse<T> { Response = Activator.CreateInstance<T>() };

            VKResponse<T> response = null;
            try
            {
                response = JsonConvert.DeserializeObject<VKResponse<T>>(json, 
                    new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });
            }
            catch (Exception) { return new VKResponse<T> { Error = VKErrors.UnknownError }; }

            if (response.Error == VKErrors.CaptchaNeeded)
            {
                try
                {
                    var captchaResponse = await InvokeCaptchaAsync(response.GetCaptcha);
                    if (captchaResponse.Cancel) return new VKResponse<T> { Error = VKErrors.OperationCanceled };

                    parameters["captcha_sid"] = captchaResponse.CaptchaSid;
                    parameters["captcha_key"] = captchaResponse.CaptchaKey;

                    return await ProcessRequestAsync(request);
                }
                catch (Exception) { return new VKResponse<T> { Error = VKErrors.OperationCanceled }; }
            }
            else if (response.Error == VKErrors.AuthorizationFailed)
                loginService.Logout();

            return response;
        }

        /// <summary>
        /// Выполняет пользователю запрос на ввод каптчи.
        /// </summary>
        /// <param name="request">Запрос на ввод каптчи.</param>
        private async Task<VKCaptchaResponse> InvokeCaptchaAsync(VKCaptchaRequest request)
        {
            if (CaptchaRequest == null) return new VKCaptchaResponse(request.CaptchaSid);
            return await CaptchaRequest(request);
        }

        /// <summary>
        /// Возвращает строку для GET-запроса к ВКонтакте.
        /// </summary>
        /// <param name="method">Ссылка на метод.</param>
        /// <param name="parameters">Параметры метода.</param>
        private static string GetRequestUrl(string method, Dictionary<string, string> parameters)
        {
            return method + "?" + String.Join("&", parameters.Select(
                kp => Uri.EscapeDataString(kp.Key) + "=" + Uri.EscapeDataString(kp.Value)));
        }
    }
}
