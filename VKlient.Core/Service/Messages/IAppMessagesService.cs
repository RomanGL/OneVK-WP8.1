using OneVK.Model.Message;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис приложения для обработки сообщений.
    /// </summary>
    public interface IAppMessagesService
    {
        /// <summary>
        /// Возвращает или задает идентификатор текущей открытой беседы.
        /// </summary>
        long CurrentConversationID { get; set; }

        /// <summary>
        /// Запустить сервис сообщений и восстановить состояние.
        /// </summary>
        void StartAndRestore();

        /// <summary>
        /// Остановить сервис сообщений и сохранить состояние.
        /// </summary>
        void StopAndSave();
    }
}
