using OneVK.Enums.Common;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на возвращение списка диалогов текущего пользователя.
    /// </summary>
    public class GetDialogsRequest : BaseVKCountedRequest<VKGetDialogsObject>
    {
        /// <summary>
        /// Количество символов, по которому нужно обрезать сообщение.
        /// </summary>
        public ulong PreviewLength { get; set; }

        /// <summary>
        /// Статусы сообщений.
        /// </summary>
        public VKBoolean Unread { get; set; }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetDialogs; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();
            parameters["unread"] = ((byte)Unread).ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GetDialogsRequest()
        {
            MaxCount = 200;
            DefaultCount = 20;
        }
    }
}
