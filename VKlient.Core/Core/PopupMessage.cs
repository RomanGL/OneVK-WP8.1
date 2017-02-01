using OneVK.Enums.App;
using OneVK.ViewModel.Messages;
using System;

namespace OneVK.Core
{
    /// <summary>
    /// Представляет собой всплывающее сообщение.
    /// </summary>
    public class PopupMessage
    {
        private string imageUrl;

        /// <summary>
        /// Заголовок сообщения.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Ссылка на картинку.
        /// </summary>
        public string ImageUrl
        {
            get
            {
                if (String.IsNullOrEmpty(imageUrl))
                {
                    switch (Type)
                    {
                        case PopupMessageType.Error:
                            imageUrl = "ms-appx:///Assets/Popups/Error.png";
                            break;
                        case PopupMessageType.Warning:
                            imageUrl = "ms-appx:///Assets/Popups/Warning.png";
                            break;
                        case PopupMessageType.Info:
                            imageUrl = "ms-appx:///Assets/Popups/Info.png";
                            break;
                        default:
                            break;
                    }
                }

                return imageUrl;
            }
            set { imageUrl = value; }
        }
        /// <summary>
        /// Параметр сообщения.
        /// </summary>
        public NavigateToPageMessage Parameter { get; set; }
        /// <summary>
        /// Действие для выполнения.
        /// </summary>
        public Action ActionToDo { get; set; }
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public PopupMessageType Type { get; set; }
        /// <summary>
        /// Длительность уведомления.
        /// </summary>
        public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(6000);
    }
}
