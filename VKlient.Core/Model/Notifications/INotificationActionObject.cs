using OneVK.Enums.Notifications;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет объект, который является объектом действия уведомления.
    /// </summary>
    public interface INotificationActionObject
    {
        /// <summary>
        /// Тип объекта действия.
        /// </summary>
        ActionObjectType Type { get; }

        /// <summary>
        /// Ссылка на фото размером 50 пикселей.
        /// </summary>
        string Photo50 { get; }

        /// <summary>
        /// Ссылка на фото размером 100 пикселей.
        /// </summary>
        string Photo100 { get; }

        /// <summary>
        /// Заголовок объекта.
        /// </summary>
        string Title { get; }
    }
}
