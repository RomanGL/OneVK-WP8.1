using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.VK.Models.Newsfeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OneVK.Core.Services;
using OneVK.Core.ViewModels.Collections;
using OneVK.Core.VK;
using OneVK.Core.Models.AppNotifications;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляфет модель представления страницы новостной ленты.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class NewsfeedViewModel : ViewModelBase
    {
        private string nextFrom;

        private INavigationService navigationService;
        private IAppNotificationsService appNotificationsService;
        private IVKService vkService;
        private ISessionStateService sessionStateService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NewsfeedViewModel"/>.
        /// </summary>
        public NewsfeedViewModel(INavigationService navigationService, IAppNotificationsService appNotificationsService,
            IVKService vkService, ISessionStateService sessionStateService)
        {
            this.navigationService = navigationService;
            this.appNotificationsService = appNotificationsService;
            this.vkService = vkService;
            this.sessionStateService = sessionStateService;
            
            OpenNewsfeedItem = new DelegateCommand<VKNewsfeedItem>(OnOpenNewsfeedItem);
            OpenNewsfeedItemOwner = new DelegateCommand<VKNewsfeedItem>(OnOpenNewsfeedItemOwner);
        }

        /// <summary>
        /// Команда открытия поста.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand<VKNewsfeedItem> OpenNewsfeedItem { get; private set; }

        /// <summary>
        /// Команда открытия владельца поста.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand<VKNewsfeedItem> OpenNewsfeedItemOwner { get; private set; }

        /// <summary>
        /// Коллекция новостей.
        /// </summary>
        public PaginatedCollection<VKNewsfeedItem> Newsfeed { get; private set; }

        /// <summary>
        /// Индекс первого видимого элемента.
        /// </summary>
        public int FirstVisibleIndex { get; set; }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            if (sessionStateService.SessionState.ContainsKey("NewsfeedNextFrom"))
                nextFrom = (string)sessionStateService.SessionState["NewsfeedNextFrom"];

            if (sessionStateService.SessionState.ContainsKey("Newsfeed"))
            {
                var newsfeed = (PaginatedCollection<VKNewsfeedItem>)sessionStateService.SessionState["Newsfeed"];
                newsfeed.LoadMoreItems = LoadMoreItems;
                Newsfeed = newsfeed;

                if (sessionStateService.SessionState.ContainsKey("NewsfeedSuspended"))
                {
                    sessionStateService.SessionState.Remove("NewsfeedSuspended");
                    var notification = new AppNotification
                    {
                        Type = AppNotificationType.Warning,
                        Title = "Новости могли устареть",
                        Content = "Коснитесь, чтобы обновить",
                        Duration = TimeSpan.FromSeconds(10),
                        ActionToDo = () =>
                        {
                            FirstVisibleIndex = 0;
                            nextFrom = null;
                            Newsfeed.Refresh();
                        }
                    };
                    appNotificationsService.SendNotification(notification);
                }

                if (sessionStateService.SessionState.ContainsKey("FirstVisibleNewsfeedIndex"))
                    FirstVisibleIndex = (int)sessionStateService.SessionState["FirstVisibleNewsfeedIndex"];
            }
            else
                Newsfeed = new PaginatedCollection<VKNewsfeedItem>(LoadMoreItems);

            base.OnNavigatedTo(e, viewModelState);
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            sessionStateService.SessionState["Newsfeed"] = Newsfeed;
            sessionStateService.SessionState["NewsfeedNextFrom"] = nextFrom;
            sessionStateService.SessionState["FirstVisibleNewsfeedIndex"] = FirstVisibleIndex;
            if (suspending) sessionStateService.SessionState["NewsfeedSuspended"] = true;

            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        private void OnOpenNewsfeedItem(VKNewsfeedItem item)
        {

        }

        private void OnOpenNewsfeedItemOwner(VKNewsfeedItem item)
        {
            if (item.SourceID > 0) navigationService.Navigate("ProfileView", (ulong)item.SourceID);
            else navigationService.Navigate("GroupInfoView", (ulong)-item.SourceID);
        }

        public async Task<IEnumerable<VKNewsfeedItem>> LoadMoreItems(uint page)
        {
            var parameters = new Dictionary<string, string>
            {
                { "filters", "post" },
                { "count", "30" }
            };
            if (!String.IsNullOrEmpty(nextFrom))
                parameters["start_from"] = nextFrom;

            var request = new Request<VKNewsfeedGetResponse>("newsfeed.get", parameters);
            var response = await vkService.ExecuteRequestAsync(request);

            if (response.IsSuccess)
            {
                foreach (var item in response.Response.Items)
                {
                    if (item.SourceID > 0)
                        item.Owner = response.Response.Profiles.FirstOrDefault(u => u.ID == item.SourceID);
                    else
                        item.Owner = response.Response.Groups.FirstOrDefault(g => g.ID == -item.SourceID);
                }

                nextFrom = response.Response.NextFrom;
                return response.Response.Items;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
