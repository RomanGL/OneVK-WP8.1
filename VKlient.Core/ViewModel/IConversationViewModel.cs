using OneVK.Model.Message;
using System.Collections.ObjectModel;
using OneVK.Core.Collections;
using GalaSoft.MvvmLight.Command;
using OneVK.Model.LongPoll;
using System.Collections.Generic;
using OneVK.Core;
using OneVK.Core.Messages;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет интерфейс модели представления беседы.
    /// </summary>
    public interface IConversationViewModel
    {
        /// <summary>
        /// Обозреваемая коллекция сообщений.
        /// </summary>
        MessagesCollection Messages { get; }
        /// <summary>
        /// Информация о беседе.
        /// </summary>
        IConversation Conversation { get; }

        /// <summary>
        /// Команда отправки сообщения.
        /// </summary>
        RelayCommand SendMessageCommand { get; }
        /// <summary>
        /// Команда перезагрузки при ошибке.
        /// </summary>
        RelayCommand ReloadCommand { get; }
        /// <summary>
        /// Перезагрузка списка сообщений.
        /// </summary>
        RelayCommand RefreshCommand { get; }

        /// <summary>
        /// Текст, введенный пользователем в поле ввода.
        /// </summary>
        string EnteredText { get; set; }

        /// <summary>
        /// Является ли беседа чатом.
        /// </summary>
        bool IsChat { get; }

        /// <summary>
        /// Идентификатор пользователя, с которым ведется диалог.
        /// </summary>
        ulong UserID { get; }

        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        uint ChatID { get; }
    }
}
