using System;
using Windows.UI.Popups;

namespace OneVK.Core.Services
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Отобразить сообщение с указанным заголовком и сообщением.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="title">Заголовок собщения.</param>
        public async void Show(string message, string title = "")
        {
            var msg = new MessageDialog(message, title);
            await msg.ShowAsync();
        }
    }
}
