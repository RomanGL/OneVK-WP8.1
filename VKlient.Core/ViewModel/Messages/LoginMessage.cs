using OneVK.Enums.Common;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Сообщение авторизации.
    /// </summary>
    public class LoginMessage
    {
        private VKLoginStates _state = VKLoginStates.Login;
        private string _redirectURL;

        /// <summary>
        /// Статус авторизации.
        /// </summary>
        public VKLoginStates State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// Ссылка для валидации.
        /// </summary>
        public string RedirectURL
        {
            get
            {
                if (State == VKLoginStates.NeedValidation)
                    return _redirectURL;
                return null;
            }
            set { _redirectURL = value; }
        }
    }
}
