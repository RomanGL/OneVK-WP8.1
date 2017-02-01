using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using OneVK.Core.VK;
using OneVK.Core.Services;
using OneVK.Core.VK.Models.Groups;
using OneVK.Core.VK.Models.Common;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OneVK.Core.Models.AppNotifications;
using Newtonsoft.Json;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляет модель представления страницы сообщества.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class GroupViewModel : ViewModelBase
    {
        private long groupID;

        private IVKService vkService;
        private IAppNotificationsService appNotificationsService;
        private INavigationService navigationService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GroupViewModel"/>.
        /// </summary>
        /// <param name="vkService">Сервис для работы с ВКонтакте.</param>
        /// <param name="appNotificationsService">Сервис внутренних увдеомлений.</param>
        /// <param name="navigationService">Сервис навигации.</param>
        public GroupViewModel(IVKService vkService, IAppNotificationsService appNotificationsService,
            INavigationService navigationService)
        {
            this.vkService = vkService;
            this.appNotificationsService = appNotificationsService;
            this.navigationService = navigationService;

            OpenGroupSettings = new DelegateCommand(OnOpenGroupSettings);
            OpenGroupDescription = new DelegateCommand(OnOpenGroupDescription);
            JoinGroup = new DelegateCommand(OnJoinGroup, () => Group != null && !Group.IsMember);
            ExitGroup = new DelegateCommand(OnExitGroup, () => Group != null && Group.IsMember);
        }

        /// <summary>
        /// Название сообщества.
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// Объект сообщества.
        /// </summary>
        public VKGroupExtended Group { get; private set; }
        /// <summary>
        /// Доступны ли пользователю настройки сообщества.
        /// </summary>
        [AlsoNotifyFor(nameof(Group))]
        public bool IsAdmin { get { return Group == null ? false : Group.AdminLevel == VKUserIsAdmin.Admin; } }

        /// <summary>
        /// Команда открытия представления настроек сообщества.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand OpenGroupSettings { get; private set; }
        /// <summary>
        /// Команда открытия представления описания сообщества.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand OpenGroupDescription { get; private set; }
        /// <summary>
        /// Команда подписки на сообщество.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand JoinGroup { get; private set; }
        /// <summary>
        /// Команда отписки от сообщества.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand ExitGroup { get; private set; }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            groupID = (long)e.Parameter;

            if (viewModelState.Count != 0)
            {
                Group = (VKGroupExtended)viewModelState["Group"];
                GroupName = Group.Name;

                return;
            }

            await LoadGroupInfo();

            base.OnNavigatedTo(e, viewModelState);
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (Group != null) viewModelState["Group"] = Group;

            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        /// <summary>
        /// Загрузить информацию о сообществе.
        /// </summary>
        private async Task LoadGroupInfo()
        {
            GroupName = "Загрузка";

            var parameters = new Dictionary<string, string>
            {
                { "group_id", groupID.ToString() },
                { "fields", "city,country,place,description,members_count,counters,start_date,finish_date,can_post,can_see_all_posts,activity,status,contacts,links,fixed_post,verified,site,ban_info" }
            };
            var request = new Request<List<VKGroupExtended>>("groups.getById", parameters);
            var response = await vkService.ExecuteRequestAsync(request);

            if (response.IsSuccess)
            {
                Group = response.Response[0];
                GroupName = Group.Name;

                JoinGroup.RaiseCanExecuteChanged();
                ExitGroup.RaiseCanExecuteChanged();
            }
        }

        private void OnOpenGroupSettings()
        {
            navigationService.Navigate("GroupSettingsView", JsonConvert.SerializeObject(Group));
        }

        private void OnOpenGroupDescription()
        {
            navigationService.Navigate("GroupDescriptionView", JsonConvert.SerializeObject(Group));
        }

        private async void OnJoinGroup()
        {
            var request = new Request<VKOperationIsSuccess>("groups.join", 
                new Dictionary<string, string>
                {
                    { "group_id", groupID.ToString() }
                });
            var response = await vkService.ExecuteRequestAsync(request);

            if (response.IsSuccess)
            {
                await LoadGroupInfo();

                JoinGroup.RaiseCanExecuteChanged();
                ExitGroup.RaiseCanExecuteChanged();
            }
            else
            {
                var notification = new AppNotification
                {
                    Type = AppNotificationType.Error,
                    Title = $"Не удалось вступить в сообщество {Group.Name}",
                    Content = "Повторите попытку позднее"
                };
                appNotificationsService.SendNotification(notification);
            }
        }

        private async void OnExitGroup()
        {
            var request = new Request<VKOperationIsSuccess>("groups.leave",
                new Dictionary<string, string>
                {
                    { "group_id", groupID.ToString() }
                });
            var response = await vkService.ExecuteRequestAsync(request);

            if (response.IsSuccess)
            {
                await LoadGroupInfo();

                JoinGroup.RaiseCanExecuteChanged();
                ExitGroup.RaiseCanExecuteChanged();
            }
            else
            {
                var notification = new AppNotification
                {
                    Type = AppNotificationType.Error,
                    Title = $"Не удалось покинуть сообщество {Group.Name}",
                    Content = "Повторите попытку позднее"
                };
                appNotificationsService.SendNotification(notification);
            }
        }
    }
}
