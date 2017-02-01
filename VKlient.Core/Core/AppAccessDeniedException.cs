using System;

namespace OneVK.Core
{
    /// <summary>
    /// Исключение, возникающее при попытке входа в приложение с запрещенного аккаунта.
    /// </summary>
    public class AppAccessDeniedException : Exception
    {
        private const string _image = "ms-appx:///Assets/Cat2.png";
        private const string _title = "Ага! Попался!";

        /// <summary>
        /// Ссылка на изображение для страницы ошибок.
        /// </summary>
        public string ImageSource { get { return _image; } }
        /// <summary>
        /// Заголовок ошибки.
        /// </summary>
        public string Title { get { return _title; } }

        public AppAccessDeniedException()
            : base("\nМы очень сожалеем, но у вас нет доступа к тестированию OneVK. Пожалуйста, дождитесь запуска публичного бета-теста.")
        {
        }
    }
}
