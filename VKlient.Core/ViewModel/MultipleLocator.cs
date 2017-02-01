using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет локатор моделей представления и сервисов в нескольких экземплярах.
    /// </summary>
    public class MultipleLocator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public MultipleLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }

        /// <summary>
        /// Возвращает экземпляр модели представления по ее ключу.
        /// </summary>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        public T GetByKey<T>(string viewModelKey)
            where T : class
        {
            return ServiceLocator.Current.GetInstance<T>(viewModelKey);
        }

        /// <summary>
        /// Регистрирует экземпляр модели представления по ключу.
        /// </summary>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        public void RegisterByKey<TClass>(string viewModelKey) 
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>(viewModelKey))
                SimpleIoc.Default.Register<TClass>(() => (TClass)Activator.CreateInstance(
                    typeof(TClass), new object[] { viewModelKey }), viewModelKey);
        }

        /// <summary>
        /// Регистрирует экземпляр модели представления по ключу 
        /// с указанным параметром коснтруктора.
        /// </summary>
        /// <typeparam name="TClass">Тип класса модели представления.</typeparam>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        /// <param name="parameter">Параметр, который будет передан в 
        /// конструктор модели представления.</param>
        public void RegisterByKey<TClass>(string viewModelKey, object parameter)
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>(viewModelKey))
            {
                SimpleIoc.Default.Register<TClass>(() => (TClass)Activator.CreateInstance(
                    typeof(TClass), new object[] { viewModelKey, parameter }), viewModelKey);
            }
        }

        /// <summary>
        /// Уничтожает экземпляр модели представления по ее ключу.
        /// </summary>
        /// <param name="viewModelKey">Уникальный ключ модели представления.</param>
        public void UnregisterByKey<T>(string viewModelKey)
            where T : class
        {
            if (SimpleIoc.Default.IsRegistered<T>(viewModelKey))
                SimpleIoc.Default.Unregister<T>(viewModelKey);
        }
    }
}
