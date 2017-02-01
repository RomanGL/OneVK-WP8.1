using System.Threading.Tasks;
using OneVK.Core.Services;
using OneVK.Core.VK.Models;
using OneVK.Core.VK.Models.Common;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис для работы с ВКонтакте.
    /// </summary>
    public interface IVKService
    {
        /// <summary>
        /// Метод, выполняющий запрос каптчи у пользователя.
        /// </summary>
        CaptchaRequestHandler CaptchaRequest { get; set; }

        /// <summary>
        /// Выполняет указанный запрос к ВКонтакте и возвращает ответ.
        /// </summary>
        /// <typeparam name="T">Тип результирующих данных.</typeparam>
        /// <param name="request">Объект запроса к ВКонтакте.</param>
        Task<VKResponse<T>> ExecuteRequestAsync<T>(IRequest<T> request);
    }
}
