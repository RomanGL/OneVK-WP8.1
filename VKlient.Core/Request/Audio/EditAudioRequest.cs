using System;
using System.Collections.Generic;
using OneVK.Enums.Audio;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на изменение информации у аудиозаписи.
    /// </summary>
    public class EditAudioRequest : BaseVKRequest<long>
    {
        private long _ownerID;
        private long _audioID;

        /// <summary>
        /// Идентификатор владельца аудиозаписи.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор пользователя или сообщества должен быть положительным числом.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        public long AudioID
        {
            get { return _audioID; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("AudioID",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _audioID = value;
            }
        }

        /// <summary>
        /// Новое имя исполнителя аудиозаписи.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Новое название аудиозаписи.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Новый текст аудиозаписи.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Новый жанр аудиозаписи.
        /// </summary>
        public VKAudioGenre Genre { get; set; }

        /// <summary>
        /// Скрыть аудиозапись из поиска.
        /// </summary>
        public VKBoolean NoSearch { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи.</param>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public EditAudioRequest(long ownerID, long audioID)
        {
            OwnerID = ownerID;
            AudioID = audioID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["audio_id"] = AudioID.ToString();
            if (!String.IsNullOrWhiteSpace(Artist)) parameters["artist"] = Artist;
            if (!String.IsNullOrWhiteSpace(Title)) parameters["title"] = Title;
            if (!String.IsNullOrWhiteSpace(Text)) parameters["text"] = Text;
            if (Genre != VKAudioGenre.Unknown) parameters["genre"] = ((byte)Genre).ToString();
            if (NoSearch == VKBoolean.True) parameters["no_search"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioEdit; }
    }
}
