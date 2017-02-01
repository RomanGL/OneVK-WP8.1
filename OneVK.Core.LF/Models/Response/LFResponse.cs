using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет ответ сервера Last.fm.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class LFResponse : ISupportValidation
    {
        /// <summary>
        /// Тип ошибки.
        /// </summary>
        [JsonProperty("error")]
        public LFErrors ErrorType { get; set; }

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Устанавливает ошибку.
        /// </summary>
        /// <param name="error">Тип произошедшей ошибки.</param>
        /// <param name="errorMessage">Описание произошедшей ошибки.</param>
        public void SetError(LFErrors error, string errorMessage)
        {
            ErrorType = error;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Являются ли данные валидными.
        /// Это абстрактный метод.
        /// </summary>
        public abstract bool IsValid();
    }
}
