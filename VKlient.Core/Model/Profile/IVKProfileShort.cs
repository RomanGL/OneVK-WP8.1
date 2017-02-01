using OneVK.Enums.Common;
using OneVK.Enums.Profile;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Представляет краткий профиль пользователя.
    /// </summary>
    public interface IVKProfileShort
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        ulong ID { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Фотография пользователя в размере 50 пикселей.
        /// </summary>
        string Photo50 { get; set; }

        /// <summary>
        /// Статус пользователя на сайте.
        /// </summary>
        VKBoolean Online { get; set; }
        /// <summary>
        /// Пол пользователя.
        /// </summary>
        VKUserSex Sex { get; set; }

    }
}