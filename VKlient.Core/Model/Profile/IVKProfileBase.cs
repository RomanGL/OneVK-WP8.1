using System;
using OneVK.Enums.Profile;
using OneVK.Enums.Common;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Интерфейс стандартного профиля пользователя.
    /// </summary>
    public interface IVKProfileBase : IVKObject
    {
        string FullName { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Status { get; set; }

        string Photo50 { get; set; }
        string Photo100 { get; set; }
        string Photo200 { get; set; }

        VKBoolean Online { get; set; }
        VKIsDeactivated Deactivated { get; set; }
    }
}
