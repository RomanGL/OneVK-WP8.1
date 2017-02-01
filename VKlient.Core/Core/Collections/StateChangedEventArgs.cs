using System;
using OneVK.Enums.App;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Содержит информацию о событии изменения состояния коллекции.
    /// </summary>
    public sealed class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Состояние коллекции.
        /// </summary>
        public ContentState State { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданной информации о состоянии коллекции.
        /// </summary>
        /// <param name="state">Информация о состоянии.</param>
        internal StateChangedEventArgs(ContentState state)
        {
            State = state;
        }
    }
}
