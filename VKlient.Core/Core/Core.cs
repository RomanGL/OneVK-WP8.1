using OneVK.Response;
using System;
using System.Threading.Tasks;
using OneVK.Model.LongPoll;

namespace OneVK.Core
{
    /// <summary>
    /// Представляет метод, обрабатывающий ответ на запрос каптчи.
    /// </summary>
    /// <param name="response">Ответ на запрос каптчи.</param>
    public delegate void CaptchaRequestHandler(VKCaptchaRequest request, Action<VKCaptchaResponse> callback);

    /// <summary>
    /// Представляет метод, обрабатывающий событие, возникающее при вызове метода 
    /// ВКонтакте без необходимых на то разрешений.
    /// </summary>
    /// <param name="e">Информация о событии.</param>
    public delegate void InvalidScopeEventHandler(InvalidScopeEventArgs e);

#if ONEVK_CORE
    /// <summary>
    /// Представляет метод, обрабатывающий запрос на отображение всплывающего уведомления.
    /// </summary>
    /// <param name="title">Заголовок уведомления.</param>
    /// <param name="message">Текст уведомления.</param>
    public delegate void ShowToastHandler(string title, string message);
#endif
}
