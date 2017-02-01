using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Сообщение на добавление новых вложений.
    /// </summary>
    public class AddAttachmentsMessage
    {
        /// <summary>
        /// Указывает на необходимость отмены регистрации без выполнения действий.
        /// </summary>
        public bool Cancel { get; set; }
    }
}
