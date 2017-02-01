using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request.Status
{
    /// <summary>
    /// Представляет запрос на установку статуса пользователю или сообществу.
    /// </summary>
    public class StatusSetRequest : BaseStatusRequest<VKBoolean>
    {
        /// <summary>
        /// Новый текст статуса.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.StatusSet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (!String.IsNullOrWhiteSpace(Text)) parameters["text"] = Text;
            return parameters;
        }
    }
}
