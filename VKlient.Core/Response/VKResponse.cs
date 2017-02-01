using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой базовый класс ответа 
    /// сервера ВКонтакте в формате Json.
    /// </summary>
    /// <typeparam name="T">Тип содержимого ответа.</typeparam>
    public class VKResponse<T>
    {        
        /// <summary>
        /// Объект ответа сервера ВКонтакте.
        /// </summary>
        [JsonProperty("response")]
        public T Response { get; set; }

        /// <summary>
        /// Объект ошибки ВКонтакте.
        /// </summary>
        [JsonProperty("error")]
        public VKError Error { get; set; }

        public VKResponse()
        {
            Error = new VKError() { ErrorType = VKErrors.None };
        }
    }
}
