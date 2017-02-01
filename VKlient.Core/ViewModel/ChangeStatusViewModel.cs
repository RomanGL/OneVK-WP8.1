using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Request.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы смены статуса пользователя.
    /// </summary>
    public class ChangeStatusViewModel : BaseViewModel
    {       
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public ChangeStatusViewModel()
        {
            ChangeStatus = new RelayCommand<string>(async s =>
            {
                IsWork = true;
                var request = new StatusSetRequest() { Text = s, GroupID = GroupID };
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                {
                    var profile = ServiceLocator.Current.GetInstance<SidebarViewModel>().Profile;
                    if (profile != null) profile.Status = s;

                    NavigationHelper.GoBack();
                }
                else
                    await ServiceHelper.DialogService.ShowMessage("При изменении статуса произошла ошибка. Повторите попытку позднее.",
                        "Не удалось изменить статус");

                IsWork = false;
            });
        }

        /// <summary>
        /// Команда смены статус пользователя.
        /// </summary>
        public RelayCommand<string> ChangeStatus { get; private set; }

        /// <summary>
        /// Возвращает или задает идентификатор группы, для которой требуется изменить статус.
        /// </summary>
        public ulong GroupID { get; set; }

        private bool _isWork;

        /// <summary>
        /// Возвращает или задает значение, выполняется ли сейчас работа.
        /// </summary>
        public bool IsWork
        {
            get { return _isWork; }
            set { Set(() => IsWork, ref _isWork, value); }
        }
    }
}
