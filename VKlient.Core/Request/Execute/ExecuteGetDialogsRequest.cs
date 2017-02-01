using OneVK.Enums.Common;
using OneVK.Response.Execute;
using System;
using System.Collections.Generic;

namespace OneVK.Request.Execute
{
    /// <summary>
    /// Представляет запрос на получения списка диалогов вместе со списком пользователей.
    /// </summary>
    public sealed class ExecuteGetDialogsRequest : VKExecuteRequest<ExecuteGetDialogsResponse>
    {
        private uint _count;

        /// <summary>
        /// Количество сообщений, которое необходимо получить.
        /// </summary>
        public uint Count
        {
            get { return _count; }
            set
            {
                if (value > 200)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество сообщений должно быть не больше 200.");
                _count = value;
            }
        }

        /// <summary>
        /// Смещение относительно начала списка.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Количество символов, по которому нужно обрезать сообщение.
        /// </summary>
        public uint PreviewLength { get; set; }

        /// <summary>
        /// Статусы сообщений.
        /// </summary>
        public VKBoolean Unread { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Count != 20) parameters["count"] = Count.ToString();
            if (Offset > 0) parameters["offset"] = Offset.ToString();
            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();
            parameters["unread"] = ((byte)Unread).ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ExecuteGetDialogsRequest()
            : base("getDialogs")
        { }
    }
}
