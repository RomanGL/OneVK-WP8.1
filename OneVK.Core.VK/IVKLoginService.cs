using OneVK.Core.Services;
using OneVK.Core.VK.Models.Common;
using System;
using System.ComponentModel;
using Windows.Foundation;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Проедставляет сервис авторизации ВКонтакте.
    /// </summary>
    public interface IVKLoginService : ILoginService
    {
        /// <summary>
        /// Просиходит при успешной авторизации пользователя.
        /// </summary>
        event TypedEventHandler<IVKLoginService, EventArgs> UserLogin;
        /// <summary>
        /// Происходит при выходе пользователя.
        /// </summary>
        event TypedEventHandler<IVKLoginService, EventArgs> UserLogout;

        /// <summary>
        /// Выполняет авторизацию по полученному токену.
        /// </summary>
        /// <param name="token">Токен авторизации.</param>
        void Login(VKAccessToken token);

        /// <summary>
        /// Возвращает идентификатор текущего авторизованного пользователя.
        /// </summary>
        long UserID { get; }

        /// <summary>
        /// Возвращает токен авторизации текущего пользователя.
        /// </summary>
        string Token { get; }
    }
}
