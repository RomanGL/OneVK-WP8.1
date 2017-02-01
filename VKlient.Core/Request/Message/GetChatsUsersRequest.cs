using OneVK.Enums.Profile;
using OneVK.Model.Profile;
using System;
using System.Collections.Generic;

namespace OneVK.Request.Message
{
    /// <summary>
    /// Представляет запрос для получения пользователей указанных чатов.
    /// </summary>
    public class GetChatsUsersRequest : BaseVKRequest<Dictionary<uint, List<VKProfileChat>>>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetChatUsers; }

        /// <summary>
        /// Возвращает или задает идентификаторы чатов.
        /// </summary>
        public List<uint> ChatIDs { get; set; }

        /// <summary>
        /// Падеж, в котором требуется вернуть имена пользователей.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["fields"] = VKMethodsConstants.BaseProfileFields;
            parameters["chat_ids"] = String.Join(",", ChatIDs);
            if (NameCase != VKUserNameCase.nom) parameters["name_case"] = NameCase.ToString();
            return parameters;
        }
    }
}
