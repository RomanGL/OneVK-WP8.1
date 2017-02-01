using System.Threading.Tasks;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис авторизации.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Выполняет авторизацию.
        /// </summary>
        Task Login();

        /// <summary>
        /// Завершает авторизацию.
        /// </summary>
        void Logout();

        /// <summary>
        /// Возвращает значение, выполнена ли авторизация.
        /// </summary>
        bool IsAuthorized { get; }
    }
}
