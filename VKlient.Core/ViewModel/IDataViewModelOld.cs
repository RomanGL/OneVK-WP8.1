using GalaSoft.MvvmLight.Command;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Интерфейс модели представления, которая содержит данные,
    /// уникальные для конкретного контекста.
    /// </summary>
    public interface IDataViewModelOld
    {
        /// <summary>
        /// Уникальный ключ модели представления.
        /// </summary>
        string ViewModelKey { get; set; }
        /// <summary>
        /// Загружены ли данные в модель представления.
        /// </summary>
        bool IsLoaded { get; }
        /// <summary>
        /// Загрузить данные в модель представления.
        /// </summary>
        void LoadData();
        /// <summary>
        /// Команда обновления данных модели представления.
        /// </summary>
        RelayCommand Refresh { get; }
    }
}
