using OneVK.Model.Audio;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на сохранение ранее загруженной
    /// на сервер ВКонтакте аудиозаписи.
    /// </summary>
    public class SaveAudioRequest : BaseVKRequest<List<VKAudio>>
    {
        private int _server;
        private string _audio;

        /// <summary>
        /// Номер сервера.
        /// </summary>
        public int Server
        {
            get { return _server; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("Server",
                        "Номер сервера не может быть равен нулю.");
                else if (value < 0)
                    throw new ArgumentOutOfRangeException("Server",
                        "Номер сервера не может быть отрицательным числом.");
                _server = value;
            }
        }

        /// <summary>
        /// Параметр, возвращаемый в результате загрузки аудиозаписи на сервер.
        /// </summary>
        public string Audio
        {
            get { return _audio; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Audio",
                        "Параметр загруженной аудиозаписи не может быть пустым.");
                _audio = value;
            }
        }

        /// <summary>
        /// Параметр, возвращаемый в результате загрузки аудиозаписи на сервер.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Исполнитель аудиозаписи.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Заголовок аудиозаписи.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="server">Номер сервера.</param>
        /// <param name="audio">Параметр, возвращаемый в результате загрузки 
        /// аудиозаписи на сервер.</param>
        public SaveAudioRequest(int server, string audio)
        {
            Server = server;
            Audio = audio;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["server"] = Server.ToString();
            parameters["audio"] = Audio;
            if (!String.IsNullOrWhiteSpace(Hash)) parameters["hash"] = Hash;
            if (!String.IsNullOrWhiteSpace(Artist)) parameters["artist"] = Artist;
            if (!String.IsNullOrWhiteSpace(Title)) parameters["title"] = Title;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioSave; }
    }
}
