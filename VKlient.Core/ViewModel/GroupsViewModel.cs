using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы со списком сообществ.
    /// </summary>
    public class GroupsViewModel : BaseKeyedViewModelOld
    {
        #region Конструкторы
        public GroupsViewModel(string key, ulong parameter)
            : base(key, parameter)
        {
            _groups = new GroupsCollection(parameter);
            _events = new EventsCollection(parameter);
            _manage = new GroupsManageCollection(parameter);
            Refresh = new RelayCommand(() => Groups.Refresh());
            RefreshEvents = new RelayCommand(() => Events.Refresh());
            RefreshGroupsManage = new RelayCommand(() => GroupsManage.Refresh());
        }
        #endregion

        #region Приватные поля
        private GroupsCollection _groups;
        private EventsCollection _events;
        private GroupsManageCollection _manage;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция списка сообществ.
        /// </summary>
        public GroupsCollection Groups { get { return _groups; } }
        /// <summary>
        /// Коллекция списка событий.
        /// </summary>
        public EventsCollection Events { get { return _events; } }
        /// <summary>
        /// Коллекция списка групп, которыми текущий пользователь может управлять.
        /// </summary>
        public GroupsManageCollection GroupsManage { get { return _manage; } }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления списка сообществ.
        /// </summary>
        public RelayCommand Refresh { get; private set; }
        /// <summary>
        /// Команда обновления списка cобытий.
        /// </summary>
        public RelayCommand RefreshEvents { get; private set; }
        /// <summary>
        /// Команда обновления списка управляемых пользователем сообществ.
        /// </summary>
        public RelayCommand RefreshGroupsManage { get; private set; }
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
