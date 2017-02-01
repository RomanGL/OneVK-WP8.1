using GalaSoft.MvvmLight.Command;
using System;
using OneVK.Enums.App;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет метод, обрабатывающий событие изменения состояния коллекции. 
    /// </summary>
    /// <param name="sender">Объект, инициализировший событие.</param>
    /// <param name="e">Информация о событии.</param>
    public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);

    /// <summary>
    /// Представляет интерфейс коллекций, поддерживающих уведомления о состоянии внутреннего контента.
    /// </summary>
    public interface IStateProviderCollection
    {
        /// <summary>
        /// Команда попытки перезагрузить данные.
        /// </summary>
        RelayCommand LoadCommand { get; }
        /// <summary>
        /// Возвращает состояние контента.
        /// </summary>
        ContentState State { get; }
        /// <summary>
        /// Происходит при ошибке загрузки.
        /// </summary>
        event StateChangedEventHandler StateChanged;
        /// <summary>
        /// Запускает попытку подгрузить элементы.
        /// </summary>
        void Load();
        /// <summary>
        /// Перезагрузить коллекцию.
        /// </summary>
        void Refresh();
    }
}
