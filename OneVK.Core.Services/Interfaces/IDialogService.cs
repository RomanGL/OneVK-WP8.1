using System.Threading.Tasks;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет интерфейс для отображения диалоговых окон.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Отобразить сообщение с указанным сообщением и заголовком.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="title">Заголовок собщения.</param>
        void Show(string message, string title);
    }
}
