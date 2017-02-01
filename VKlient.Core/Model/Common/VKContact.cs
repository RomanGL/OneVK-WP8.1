using System;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Контакт раздела контактов ВКонтакте.
    /// </summary>
    public sealed class VKContact : IVKObject
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// Описание контакта.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Электронная почта контакта.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Не реализовано. Контакт не имеет идентификатора.
        /// </summary>
        public long ID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
