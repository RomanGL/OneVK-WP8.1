using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Response;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение истории сообщний диалога или чата.
    /// </summary>
    public class GetHistoryRequest : BaseChatUserRequest<VKGetMessagesHistoryObject>
    {
        /// <summary>
        /// False - вернуть в обратном хронологическом порядке (по умолчанию).
        /// True - в хронологическом порядке.
        /// </summary>
        public VKBoolean Reverse { get; set; }

        /// <summary>
        /// Смещение, необходимое для выборки определенного подмножества сообщений, должен быть больше 
        /// либо равен 0, если не передан параметр StartMessageID, и должен быть меньше либо равен 0, если передан. 
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Количество сообщений, которое требуется вернуть. Не более 200.
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Если значение больше 0, то это идентификатор сообщения, начиная с 
        /// которого нужно вернуть историю переписки, если же передано 
        /// значение -1, то к значению параметра offset прибавляется количество 
        /// входящих непрочитанных сообщений в конце диалога.
        /// </summary>
        public long StartMessageID { get; set; }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetHistory; }

        /// <summary>
        /// Инициализирует новый экзепляр класса.
        /// </summary>
        public GetHistoryRequest() 
            : base() { }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Count != 20) parameters["count"] = Count.ToString();
            if (Offset != 0) parameters["offset"] = Offset.ToString();
            if (StartMessageID != 0) parameters["start_message_id"] = StartMessageID.ToString();
            if (Reverse == VKBoolean.True) parameters["rev"] = "1";

            return parameters;
        }
    }
}
