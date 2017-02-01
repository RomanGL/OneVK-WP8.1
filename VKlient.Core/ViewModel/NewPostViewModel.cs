using GalaSoft.MvvmLight.Command;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы создания новой записи на стене.
    /// </summary>
    public class NewPostViewModel : BaseViewModel
    {
        private bool _isWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public NewPostViewModel()
        {
            PublishPost = new RelayCommand<string>(async text =>
            {
                IsWork = true;

                var request = new PostWallRequest(text) { OwnerID = OwnerID };
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                    NavigationHelper.GoBack();
                else
                    await ServiceHelper.DialogService.ShowMessage("Не удалось опубликовать эту запись. Повторите попытку позднее.",
                        "Ошибка при публикации");

                IsWork = false;
            });
        }

        /// <summary>
        /// Идентификатор пользователя или сообщества, куда требуется разместить запись.
        /// </summary>
        public long OwnerID { get; set; }   

        /// <summary>
        /// Возвращает или задает значение, выполняется ли сейчас работа.
        /// </summary>
        public bool IsWork
        {
            get { return _isWork; }
            set { Set(() => IsWork, ref _isWork, value); }
        }

        /// <summary>
        /// Команда публикации новой записи.
        /// </summary>
        public RelayCommand<string> PublishPost { get; private set; }
    }
}
