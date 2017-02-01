using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using OneVK.Core.VK.Models.Groups;
using Newtonsoft.Json;
using OneVK.Core.Services;
using OneVK.Core.VK;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OneVK.Core.Models.AppNotifications;
using OneVK.Core.VK.Models.Common;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляет модель представления страницы настроек сообщества.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class GroupSettingsViewModel : ViewModelBase
    {
        private IVKService vkService;
        private IAppNotificationsService appNotificationsService;
        private INavigationService navigationService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GroupSettingsViewModel"/>.
        /// </summary>
        public GroupSettingsViewModel(IVKService vkService, IAppNotificationsService appNotificationsService,
            INavigationService navigationService)
        {
            this.vkService = vkService;
            this.appNotificationsService = appNotificationsService;
            this.navigationService = navigationService;

            SaveSettings = new DelegateCommand(OnSaveSettings);
        }

        /// <summary>
        /// Выполняется ли загрузка.
        /// </summary>
        public bool IsLoading { get; private set; }
        /// <summary>
        /// Текущий индекс элемента в Pivot.
        /// </summary>
        public int CurrentPivotIndex { get; set; }
        /// <summary>
        /// Текст страницы загрузки.
        /// </summary>
        public string LoadingText { get; private set; }
        /// <summary>
        /// Информация о сообществе.
        /// </summary>
        public VKGroup Group { get; private set; }
        /// <summary>
        /// Информация о параметрах сообщества.
        /// </summary>
        public VKGroupSettings Settings { get; private set; }
        /// <summary>
        /// Индекс текущего элемента тематики сообщества.
        /// </summary>
        public int CurrentSubjectIndex { get; set; }

        /// <summary>
        /// Команда сохранения параметров.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand SaveSettings { get; private set; }

        /// <summary>
        /// Вызывается при навигации на страницу.
        /// </summary>
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            if (viewModelState.Count != 0)
            {
                Group = (VKGroup)viewModelState["Group"];
                Settings = (VKGroupSettings)viewModelState["Settings"];
                CurrentPivotIndex = (int)viewModelState["PivotIndex"];
                CurrentSubjectIndex = (int)viewModelState["SubjectIndex"];

                return;
            }

            Group = JsonConvert.DeserializeObject<VKGroup>(e.Parameter.ToString());
            await LoadSettings();

            base.OnNavigatedTo(e, viewModelState);
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (Settings != null)
            {
                viewModelState["PivotIndex"] = CurrentPivotIndex;
                viewModelState["Settings"] = Settings;
                viewModelState["SubjectIndex"] = CurrentSubjectIndex;
                viewModelState["Group"] = Group;
            }

            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        /// <summary>
        /// Загружает информацию о настройках.
        /// </summary>
        private async Task LoadSettings()
        {
            LoadingText = "Загрузка информации о настройках";
            IsLoading = true;

            var parameters = new Dictionary<string, string>
            {
                {"group_id", Group.ID.ToString() }
            };
            var request = new Request<VKGroupSettings>("groups.getSettings", parameters);
            var response = await vkService.ExecuteRequestAsync(request);

            if (response.IsSuccess)
            {
                Settings = response.Response;
                CurrentSubjectIndex = Settings.AvailableSubjects.FindIndex(s => s.ID == Settings.Subject);
            }
            else if (response.Error == VKErrors.AccessDenied)
            {
                var notification = new AppNotification
                {
                    Type = AppNotificationType.Error,
                    Title = "Нет доступа к параметрам сообщества",
                    Content = "Не удалось получить текущие значения параметров",
                    ImageUrl = Group.Photo100
                };
                appNotificationsService.SendNotification(notification);

                if (navigationService.CanGoBack())
                    navigationService.GoBack();
            }
            else
            {
                var notification = new AppNotification
                {
                    Type = AppNotificationType.Error,
                    Title = "Не удалось получить текущие значения параметров",
                    Content = "Повторите попытку позднее",
                    ImageUrl = Group.Photo100
                };
                appNotificationsService.SendNotification(notification);

                if (navigationService.CanGoBack())
                    navigationService.GoBack();
            }

            IsLoading = false;
        }

        private async void OnSaveSettings()
        {
            LoadingText = "Сохранение параметров сообщества";
            IsLoading = true;

            await Task.Delay(1200);

            navigationService.Navigate("GroupView", Group.ID);
            navigationService.RemoveLastPage("GroupSettingsView");
            navigationService.RemoveLastPage("GroupView", Group.ID);
        }
    }
}
