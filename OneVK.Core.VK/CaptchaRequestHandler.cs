using OneVK.Core.VK.Models.Common;
using System.Threading.Tasks;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет метод, обрабатывающий запрос ввода каптчи.
    /// </summary>
    /// <param name="request">Запрос каптчи.</param>
    public delegate Task<VKCaptchaResponse> CaptchaRequestHandler(VKCaptchaRequest request);
}
