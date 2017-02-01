using System.Collections.Generic;
using OneVK.Enums.Profile;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка пользователей 
    /// и групп, которых пользователь скрыл из ленты новостей.
    /// </summary>
    public abstract class GetNewsfeedBannedBaseRequest<T> : BaseVKRequest<T>
    {
        /// <summary>
        /// Падеж для склонения имени и фамилии пользователя.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// 
        /// </summary>
        public GetNewsfeedBannedBaseRequest()
        {
            NameCase = VKUserNameCase.nom;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (NameCase != VKUserNameCase.nom) parameters["name_case"] = NameCase.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGetBanned;
        }
    }
}
