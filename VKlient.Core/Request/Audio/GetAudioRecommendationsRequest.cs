using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Response;
using OneVK.Model.Audio;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получние рекомендаций на основе пользовательского
    /// списка аудиозаписей или на основе одной заданной аудиозаписи.
    /// </summary>
    public class GetAudioRecommendationsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKAudio>>
    {
        private long _audioID;
        private long _ownerID;
        private long _userID;

        /// <summary>
        /// Идентификатор аудиозаписи, на основе которой необходимо
        /// получить рекомендации.
        /// </summary>
        public long AudioID
        {
            get { return _audioID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("AudioID",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _audioID = value;
            }
        }

        /// <summary>
        /// Идентификатор владельца аудиозаписи, на основе которой необходимо
        /// получить рекомендации.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор владельца аудиозаписи не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор пользователя, на основе коллекции которого
        /// требудется получить рекомендации.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Идентификатор пользователя должен быть положительным числом.");
                _userID = value;
            }
        }

        /// <summary>
        /// Перемешать результаты.
        /// </summary>
        public VKBoolean Shuffle { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (AudioID > 0 && OwnerID != 0) parameters["target_audio"] = OwnerID + "_" + AudioID;
            if (UserID > 0) parameters["user_id"] = UserID.ToString();
            if (Shuffle == VKBoolean.True) parameters["shuffle"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetRecommendations; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для получения
        /// рекомендованных текущему пользователю аудиозаписей.
        /// </summary>
        public GetAudioRecommendationsRequest()
            : this(null, null, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса для получения рекомендованных
        /// аудиозаписей на основе заданной аудиозаписи.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи, на основе которой необходимо
        /// получить рекомендации</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи, на основе которой необходимо
        /// получить рекомендации.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GetAudioRecommendationsRequest(long audioID, long ownerID)
            : this(audioID, ownerID, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса для получения списка 
        /// рекомендованных аудиозаписей на основе коллекции аудиозаписей
        /// заданного пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, на основе коллекции которого
        /// требудется получить рекомендации.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GetAudioRecommendationsRequest(long userID)
            : this(null, null, userID) { }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи, на основе которой необходимо
        /// получить рекомендации.</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи, на основе которой необходимо
        /// получить рекомендации.</param>
        /// <param name="userID">Идентификатор пользователя, на основе коллекции которого
        /// требудется получить рекомендации.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private GetAudioRecommendationsRequest(long? audioID, long? ownerID, long? userID)
        {
            if (audioID.HasValue)
                AudioID = audioID.Value;
            if (ownerID.HasValue)
                OwnerID = ownerID.Value;
            if (userID.HasValue)
                UserID = userID.Value;
            MaxCount = 1000;
            DefaultCount = 100;
        }
    }
}
