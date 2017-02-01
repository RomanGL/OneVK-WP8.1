namespace OneVK.ViewModel
{
    /// <summary>
    /// Интерфейс модели представления, представляющей контент
    /// пользовательского интерфейса.
    /// </summary>
    public interface IContentViewModel
    {
        /// <summary>
        /// Загружены ли данные в модель представления.
        /// </summary>
        bool IsLoaded { get; }
        /// <summary>
        /// Выполняется ли в данный момент загрузка данных в 
        /// модель представления.
        /// </summary>
        bool IsLoading { get; }
    }
}
