using System;
using System.Collections.Generic;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос поиска друзей пользователя.
    /// </summary>
    public class SearchFriendsRequest : BaseCountedRequest, IVKRequestOld
    {
        private long _userID;

        /// <summary>
        /// Идентификатор пользователя, по друзьям которого требуется искать.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Идентификатор пользователя не может быть отрицательным.");
                _userID = value;
            }
        }

        /// <summary>
        /// Падеж, в котором будут возвращаться имена пользователей.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Строка запроса.
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.FriendsSearch; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (NameCase != VKUserNameCase.nom) 
                parameters["name_case"] = NameCase.ToString();
            if (!String.IsNullOrWhiteSpace(SearchString))
                parameters["q"] = SearchString;
            if (Count == 20) parameters.Remove("count");

            return parameters;
        }
    }
}
