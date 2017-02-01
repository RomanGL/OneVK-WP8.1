using Newtonsoft.Json;
using OneVK.Core.VK.Json;

namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о событии LongPoll сервера.
    /// </summary>
    [JsonConverter(typeof(VKLongPollUpdateConverter))]
    public interface IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        VKLongPollUpdateType Type { get; }
    }
}
