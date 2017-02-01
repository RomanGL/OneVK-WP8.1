using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneVK.Enums.Common;
using OneVK.Request;
using System.Net;
using System.IO;
using Windows.Web.Http;
using System.Threading;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет метод, обрабатывающий ответ от сервиса.
    /// </summary>
    /// <param name="response">Ответ в виде строки.</param>
    //internal delegate void GetResponseHandler(string response);

    /// <summary>
    /// Помощник для работы с сервисом Last.fm.
    /// </summary>
    internal static class LFHelper
    {
        private const string ApiKey = "899e9385201630378c0561cfd831b05f";
        private const string ApiSecret = "c0b640e0aedfce6d8633838731727214";
        
        /// <summary>
        /// Запускает процесс получения результата запроса.
        /// </summary>
        /// <typeparam name="T">Тип результирующего объекта</typeparam>
        /// <param name="request">Объект запроса.</param>
        /// <param name="ct">Токен для отмены операции.</param>
        public static async Task<T> GetResponse<T>(ILFRequest<T> request, CancellationToken ct = default(CancellationToken))
        {
            string response = await GetResponseString(GetFullQueryString(request.GetMethod(), request.GetParameters()), ct);

            dynamic result = null;

            if (String.IsNullOrEmpty(response))
            {
                result = Activator.CreateInstance<T>();
                result.SetError(LFErrors.ConnectionError, "Connection error.");
            }
            else
            {
                try { result = JsonConvert.DeserializeObject<T>(response); }
                catch (Exception)
                {
                    result = Activator.CreateInstance<T>();
                    result.SetError(LFErrors.DeserializationError, "Data is corrupted.\nJSON: " + response);
                }
            }

            return result;         
        }

        /// <summary>
        /// Возвращает итоговую строку для запроса.
        /// </summary>
        /// <param name="methodName">Название метода, по которому требуется сделать запрос.</param>
        /// <param name="parameters">Словарь параметров, специфичных для конкретного метода.</param>
        private static string GetFullQueryString(string methodName, Dictionary<string, string> parameters)
        {
            var parms = new Dictionary<string, string>(parameters);
            parms["method"] = methodName;
            parms["api_key"] = ApiKey;
            parms["format"] = "json";

            return LFMethodsConstants.ApiRoot + String.Join(
                "&", parms.Select(kp => Uri.EscapeDataString(kp.Key) + "=" + Uri.EscapeDataString(kp.Value)));
        }

        /// <summary>
        /// Возвращает ответ сервера Last.fm в ввиде строки.
        /// </summary>
        /// <param name="query">Строка запроса, результат которого требуется получить.</param>
        /// <param name="ct">Токен для отмены операции.</param>
        private static async Task<string> GetResponseString(string query, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                string result;
                using (var client = new HttpClient())
                {     
                    var response = await client.GetAsync(new Uri(query));
                    result = await response.Content.ReadAsStringAsync();                    
                }
                return result;
            }
            catch (Exception) { return ""; }
        }

        /// <summary>
        /// Выполняет получение ответа от сервиса Last.fm.
        /// </summary>
        /// <param name="query">Строка запроса.</param>
        /// <param name="callback">Метод, обрабатывающий ответ сервиса.</param>
        //private static void GetResponse(string query, GetResponseHandler callback)
        //{
        //    try
        //    {
        //        var request = (HttpWebRequest)WebRequest.CreateHttp(query);
        //        request.BeginGetResponse(async e =>
        //            {
        //                try
        //                {
        //                    var response = request.EndGetResponse(e);
        //                    var reader = new StreamReader(response.GetResponseStream());
        //                    callback(await reader.ReadToEndAsync());
        //                }
        //                catch (Exception) { callback(String.Empty); }                        
        //            }, request);
        //    }
        //    catch (Exception) { callback(String.Empty); }
        //}
    }
}
