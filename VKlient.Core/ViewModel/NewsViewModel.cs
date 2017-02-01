using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Enums.Common;
using OneVK.Model.Profile;
using OneVK.Service;
using OneVK.Helpers;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Core.Collections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OneVK.Model.Newsfeed;
using OneVK.Enums.Newsfeed;
using OneVK.Helpers;

namespace OneVK.ViewModel
{
    public class NewsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public NewsViewModel()
        {
            Refresh = new RelayCommand(() => News.Refresh());
            Ignore = new RelayCommand<VKNewsfeedPost>(post =>
            {
                throw new NotImplementedException();
                //ServiceHelper.VKNewsfeedService.IgnoreItem(response =>
                //    {
                //        if (response.Error.ErrorType == VKErrors.None)
                //            News.Remove(post);
                //        else
                //            ServiceHelper.DialogService.ShowMessageBox("Произошла ошибка: " + response.Error.ErrorType.ToString(), 
                //                "Не удалось скрыть новость");
                //    }, new Request.IgnoreNewsfeedItemRequest(post.OwnerID, VKNewsfeedItemType.Wall, post.ID));
            });
            News = new NewsfeedCollection();

#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                for (int i = 0; i < 3; i++)
                    News.Add(DesignDataHelper.GetNewsfeedPost());
            }
#endif
        }
        #endregion

        #region Приватные поля
        //private NewsfeedCollection _news = new NewsfeedCollection();
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция ленты новостей.
        /// </summary>
        public NewsfeedCollection News { get; set; }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления ленты.
        /// </summary>
        public RelayCommand Refresh { get; private set; }
        /// <summary>
        /// Позяоляет скрыть новость из списка.
        /// </summary>
        public RelayCommand<VKNewsfeedPost> Ignore { get; private set; }
        #endregion

        #region Приватные методы
        #endregion

        #region Публичные методы
        #endregion
    }
}
