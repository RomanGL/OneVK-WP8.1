using Microsoft.Practices.ServiceLocation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет базовый класс для множественных моделей представления.
    /// </summary>
    public abstract class BaseKeyedViewModel<T> : BaseViewModel, IKeyedViewModel
        where T : class
    {
        private readonly string _key;

        /// <summary>
        /// Уникальный ключ модели представления.
        /// </summary>
        public string UniqueKey { get { return _key; } }
        /// <summary>
        /// Максимальное количество экземпляров модели представления.
        /// </summary>
        public int MaxInstanceCount { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным ключом модели представления.
        /// </summary>
        /// <param name="uniqueKey"></param>
        protected BaseKeyedViewModel(string uniqueKey)
        {
            _key = uniqueKey;
            MaxInstanceCount = 5;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным ключом модели представления и
        /// максимальным числом экземпляров.
        /// </summary>
        /// <param name="uniqueKey"></param>
        /// <param name="maxInstanceCount">Максимальное число экземпляров.</param>
        protected BaseKeyedViewModel(string uniqueKey, int maxInstanceCount)
        {
            _key = uniqueKey;
            MaxInstanceCount = maxInstanceCount;
        }

        /// <summary>
        /// Удаляет экземпляр модели представления из кэша, если достигнуто предельное количество.
        /// </summary>
        public void DeleteInstance()
        {
            var locator = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>();
            if (locator.GetInstancesCount<T>() > MaxInstanceCount)
                locator.UnregisterByKey<T>(UniqueKey);
        }
    }
}
