using OneVK.Model.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой класс запроса на получение информации об аудиозаписях 
    /// по идентификатору валдельца и аудиозаписи.
    /// </summary>
    public class GetAudiosByIDRequest : BaseVKRequest<List<VKAudio>>
    {
        private Dictionary<long, long> _audios;

        /// <summary>
        /// Словарь аудиозаписей по типу ownerID - audioID.
        /// </summary>
        public Dictionary<long, long> Audios
        {
            get { return _audios; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Audios",
                        "Словарь должен быть инициализирован и должен содержать хотя бы одну пару ownerID - audioID.");
                if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Audios",
                        "Словарь должен содержать хотя бы одну пару ownerID - audioID.");
                if (!value.Values.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("Audios",
                        "Идентификатор аудиозаписи не может былть отрицательным числом.");
                _audios = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="audios">Словарь аудиозаписей по типу ownerID - audioID.</param>
        public GetAudiosByIDRequest(Dictionary<long, long> audios)
        {
            Audios = audios;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["audios"] = String.Join(",", Audios.Select(kp => kp.Key.ToString() + "_" + kp.Value));

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetByID; }
    }
}
