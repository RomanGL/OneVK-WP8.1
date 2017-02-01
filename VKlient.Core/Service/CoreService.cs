using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Ioc;
using OneVK.ViewModel.Messages;
using OneVK.Enums.App;
using OneVK.Model.Video;
using OneVK.Model.Newsfeed;
using OneVK.Model.Doc;
using OneVK.Model.Common;
using OneVK.Model.Photo;
using Windows.System;
using OneVK.Model.Group;
using OneVK.Enums.Common;
using OneVK.Helpers;
using Microsoft.Practices.ServiceLocation;
using OneVK.ViewModel;

namespace OneVK.Service
{
    /// <summary>
    /// Сервис для работы с базовыми функциями приложения.
    /// </summary>
    public class CoreService
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public CoreService()
        {
            VKHelper.LoginNeeded += VKHelper_LoginNeeded;
            Messenger.Default.Register<LoginMessage>(this, OnLogin);
        }        

        /// <summary>
        /// Выбросить исключение о том, что метод не реализован.
        /// </summary>
        private static void ThrowNotImplementedException()
        {
            throw new NotImplementedException(
                "Не реализовано. Нажмите кнопку назад, чтобы вернуться туда, откуда вы сюда попали.");
        }

        /// <summary>
        /// Выполнить операцию над элементом.
        /// </summary>
        /// <param name="item">Объект, для которого нужно выполнить действие.</param>
        public async void ExecuteItem(object item)
        {
            if (item is VKPhoto)
            {
                Messenger.Default.Send((VKPhoto)item);
                Messenger.Default.Send(new NavigateToPageMessage
                {
                    Page = AppViews.ImageView,
                    Operation = NavigationType.New
                });
            }
            else if (item is VKVideoBase)
                NavigationHelper.Navigate(AppViews.VideoInfoView, item);
            else if (item is VKVideoAlbumExtended)
            {
                Messenger.Default.Send((VKVideoAlbumExtended)item);
                Messenger.Default.Send(new NavigateToPageMessage
                {
                    Page = AppViews.VideoAlbumView,
                    Operation = NavigationType.New
                });
            }
            else if (item is VKLink)
                await Launcher.LaunchUriAsync(new Uri(((VKLink)item).URL));
            else if (item is VKDocument)
                await Launcher.LaunchUriAsync(new Uri(((VKDocument)item).URL));
            else if (item is VKGroupExtended)
                NavigationHelper.Navigate(AppViews.GroupInfoView, item);
        }

        /// <summary>
        /// Вызывается при авторизации в приложении.
        /// </summary>
        private void OnLogin(LoginMessage message)
        {
            if (message.State != VKLoginStates.Login) return;

            (ServiceLocator.Current.GetInstance<SidebarViewModel>()).Activate();
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage()
            {
                Operation = NavigationType.NewClear,
                Page = AppViews.NewsView
            });            
        }

        /// <summary>
        /// Вызывается при необходимости авторизироваться.
        /// </summary>
        private void VKHelper_LoginNeeded()
        {
            (ServiceLocator.Current.GetInstance<SidebarViewModel>()).Deactivate();
            ServiceHelper.VKLongPollService.StopLongPolling();
            NavigationHelper.Navigate(AppViews.LoginView);
        }
    }
}
