using OneVK.Model.Profile;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Представляет чат.
    /// </summary>
    public interface IChat : IConversation
    {
        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        uint ChatID { get; set; }
        /// <summary>
        /// Идентификатор создателя чата.
        /// </summary>
        ulong AdminID { get; set; }
        /// <summary>
        /// Коллекция пользователей чата.
        /// </summary>
        ObservableCollection<VKProfileChat> Users { get; set; }
    }
}
