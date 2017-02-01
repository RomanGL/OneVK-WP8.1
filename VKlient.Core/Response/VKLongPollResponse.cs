using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Enums.LongPoll;
using OneVK.Model.LongPoll;
using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет LongPoll-ответ от сервера ВКонтакте.
    /// </summary>
    public sealed class VKLongPollResponse
    {
        private bool _isFilled;
        private List<VKLongPollUpdate> _updates = new List<VKLongPollUpdate>();

        /// <summary>
        /// Информация о произошедшей ошибке.
        /// </summary>
        [JsonProperty("failed")]
        public VKLongPollErrors Error { get; internal set; }

        /// <summary>
        /// Последнее событие, начиная с которого требуется получить обновления.
        /// </summary>
        [JsonProperty("ts")]
        public string NewTs { get; set; }

        /// <summary>
        /// Список произошедших обновлений.
        /// </summary>
        public List<VKLongPollUpdate> Updates
        {
            get
            {
                Fill();
                return _updates;
            }
        }

        /// <summary>
        /// Необработанный список обновлений.
        /// </summary>
        [JsonProperty("updates")]
        private List<object[]> RawUpdates { get; set; }

        /// <summary>
        /// Обработать список обновлений и заполнить коллекцию.
        /// </summary>
        private void Fill()
        {
            if (_isFilled)
                return;

            _isFilled = true;

            if (RawUpdates == null || RawUpdates.Count == 0)
                return;

            for (int i = 0; i < RawUpdates.Count; i++)
                _updates.Add(new VKLongPollUpdate(RawUpdates[i]));
        }
    }
}
