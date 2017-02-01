using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет регистратор множественных моделей представления.
    /// </summary>
    public class KeyedViewModelLocator
    {
        /// <summary>
        /// Возвращает экземпляр класса по его ключу.
        /// При необходимости предварительно регистрирует экземпляр
        /// в Ioc-контейнере.
        /// </summary>
        /// <typeparam name="TClass">Тип регистрируемого класса.</typeparam>
        /// <param name="key">Уникальный ключ для регистрации класса.</param>
        /// <param name="factory">Фабрика, возвращающая экземпляр класса.</param>
        public TClass GetByKey<TClass>(string key, Func<TClass> factory)
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>(key))
                SimpleIoc.Default.Register<TClass>(factory, key);
            return ServiceLocator.Current.GetInstance<TClass>(key);
        }

        /// <summary>
        /// Уничтожает экземпляр модели представления по ее ключу.
        /// </summary>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        public void UnregisterByKey<TClass>(string viewModelKey)
            where TClass : class
        {
            if (IsRegistered<TClass>(viewModelKey))
                SimpleIoc.Default.Unregister<TClass>(viewModelKey);
        }

        /// <summary>
        /// Зарегистрирован ли экземпляр модели представления с указанным ключом.
        /// </summary>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        public bool IsRegistered<TClass>(string viewModelKey)
        {
            bool result = SimpleIoc.Default.IsRegistered<TClass>(viewModelKey);
            return result;
        }

        /// <summary>
        /// Возвращает количество зарегистрированных экземпляров модели представления.
        /// </summary>
        /// <typeparam name="T">Тип класса, для получения кэшированных экземпляров.</typeparam>
        public int GetInstancesCount<T>()
        {
            int count = 0;
            var enumerable = SimpleIoc.Default.GetAllCreatedInstances<T>();
            foreach (var instance in enumerable)
                count++;
            return count;
        }
    }
}
