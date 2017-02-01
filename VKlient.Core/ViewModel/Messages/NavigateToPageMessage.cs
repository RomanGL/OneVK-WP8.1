using System;
using OneVK.Enums.App;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Сообщение с требованием навигации на указанную страницу.
    /// </summary>
    public class NavigateToPageMessage
    {
        private NavigationType _operation = NavigationType.New;

        /// <summary>
        /// Имя страницы, на которую нужно перейти.
        /// </summary>
        public AppViews Page { get; set; }
        /// <summary>
        /// Параметр, который будет передан странице.
        /// </summary>
        public object Parameter { get; set; }
        /// <summary>
        /// Тип операции перехода.
        /// </summary>
        public NavigationType Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }
    }
}
