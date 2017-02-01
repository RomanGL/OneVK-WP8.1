namespace OneVK.ViewModel
{
    /// <summary>
    /// Интерфейс множественной модели представления с ключом.
    /// </summary>
    public interface IKeyedViewModel
    {
        /// <summary>
        /// Уникальный ключ модели представления.
        /// </summary>
        string UniqueKey { get; }
    }
}
