using GalaSoft.MvvmLight.Command;
using OneVK.Enums.App;
using OneVK.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет базовую обозреваемую коллекцию с поддержкой уведомления о своем состоянии.
    /// Это абстрактный класс.
    /// </summary>
    /// <typeparam name="T">Тип хранимых данных.</typeparam>
    public abstract class StateSupportCollection<T> : ObservableCollection<T>, IStateProviderCollection
    {
        private ContentState _state;

        /// <summary>
        /// Возвращает команду загрузки элементов.
        /// </summary>
        public RelayCommand LoadCommand { get; set; }

        /// <summary>
        /// Возвращает текущее состояние коллекции.
        /// </summary>
        public ContentState State
        {
            get { return _state; }
            protected set
            {
                if (value == _state)
                    return;

                _state = value;                
                OnStateChanged();
            }
        }

        /// <summary>
        /// Просиходит при изменении состояния коллекции.
        /// </summary>
        public event StateChangedEventHandler StateChanged;

        /// <summary>
        /// Загрузить данные в коллекцию.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Перезагрузить коллекцию. 
        /// Требуется выполнить необходимые действия для подготовки коллекции к
        /// перезагрузке, а затем вызвать базовый метод.
        /// </summary>
        public virtual void Refresh()
        {
            Clear();
            Reset();
            Load();
        }

        /// <summary>
        /// Сбрасывает коллекцию к значениям по умолчанию.
        /// </summary>
        protected virtual void Reset()
        {

        }

        /// <summary>
        /// Вызвать при изменении состояния коллекции.
        /// </summary>
        private async void OnStateChanged()
        {
            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                OnPropertyChanged(new PropertyChangedEventArgs("State"));
                if (StateChanged != null)
                    StateChanged(this, new StateChangedEventArgs(State));
            });            
        }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции.
        /// </summary>
        public StateSupportCollection()
        {
            LoadCommand = new RelayCommand(() => Load());
        }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции с элементами по умолчанию.
        /// </summary>
        /// <param name="collection">Коллекция, из которой копируются элементы.</param>
        public StateSupportCollection(IEnumerable<T> collection)
            :base(collection)
        {
            LoadCommand = new RelayCommand(() => Load());
        }
    }
}
