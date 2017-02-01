using OneVK.Enums.Profile;
using OneVK.Model.Profile;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка друзей пользователя.
    /// </summary>
    public class GetFriendsRequest : BaseGetProfilesRequest<VKCountedItemsObject<VKProfileShort>>
    {
        /// <summary>
        /// Падеж, в котором возвращаются имена пользователей.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Поля профиля, возвращаемые в ответе.
        /// </summary>
        public string Fields
        {
            get { return VKMethodsConstants.BaseProfileFields; }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (NameCase != VKUserNameCase.nom)
                parameters["name_case"] = NameCase.ToString();
            parameters["fields"] = Fields;
            parameters["order"] = "name";

            return parameters;
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.FriendsGet;
        }
    }
}
