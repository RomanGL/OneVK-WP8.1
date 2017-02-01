namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет основу для всех моделей представления, которые могут 
    /// храниться в локаторе в нескольких экземплярах с уникальным ключом.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseKeyedViewModelOld : BaseViewModel, IKeyedViewModel
    {
        private readonly string _uniqueKey;
        private readonly object _parameter;

        /// <summary>
        /// Возвращает уникальный ключ модели представления.
        /// </summary>
        public string UniqueKey { get { return _uniqueKey; } }

        /// <summary>
        /// Возвращает параметр, который будет использовать модель представления.
        /// </summary>
        public object Parameter { get { return _parameter; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным уникальным ключом, 
        /// точно идентифицирующим его.
        /// </summary>
        /// <param name="uniqueKey">Уникальный ключ.</param>
        /// <param name="parameter">Объект параметра.</param>
        public BaseKeyedViewModelOld(string uniqueKey, object parameter)
        {
            _uniqueKey = uniqueKey;
            _parameter = parameter;
        }
    }
}
