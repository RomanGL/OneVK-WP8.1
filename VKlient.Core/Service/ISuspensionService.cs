using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис управления жизненным циклом приложения.
    /// </summary>
    public interface ISuspensionService
    {
        /// <summary>
        /// Сохраняет состояние всех зарегистрированных <see cref="Frame"/> и глобальное состояние сеанса.
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// Восстанавливает глобальное состояние сеанса и состояние каждого зарегистрированного фрейма.
        /// </summary>
        /// <param name="sessionBaseKey">Необязательный ключ, определяющий тип сеанса.</param>
        Task RestoreAsync(string sessionBaseKey = null);

        /// <summary>
        /// Регистрирует экземпляр <see cref="Frame"/> для возможности сохранения его состояния.
        /// </summary>
        /// <param name="frame">Экземпляр для регистрации.</param>
        /// <param name="sessionStateKey">Уникальный ключ в <see cref="SessionState"/>, используемый для 
        /// хранения данных, связанных с навигацией.</param>
        /// <param name="sessionBaseKey">Необязательный ключ, определяющий тип сеанса.</param>
        void RegisterFrame(Frame frame, string sessionStateKey, string sessionBaseKey = null);

        /// <summary>
        /// Отменяет регистрацию ранее зарегистрированного экземпляра <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame">Экземпляр для отмены регистрации.</param>
        void UnregisterFrame(Frame frame);

        /// <summary>
        /// Предоставляет хранилище для состояния сеанса, связанного с указанным объектом <see cref="Frame"/>.
        /// Состояние сеанса фреймов, ранее зарегистрированных с помощью <see cref="RegisterFrame"/>,
        /// сохраняется и восстанавливается автоматически в составе глобального объекта
        /// <see cref="SessionState"/>.  Незарегистрированные фреймы имеют переходное состояние,
        /// которое, тем не менее, можно использовать при восстановлении страниц, удаленных из кэша навигации.
        /// </summary>
        Dictionary<string, object> SessionStateForFrame(Frame frame);

        /// <summary>
        /// Список пользовательских типов, предоставляемых сериализатору <see cref="DataContractSerializer"/> при
        /// чтении или записи состояния сеанса.  Первоначально список является пустым; для настройки процесса сериализации
        /// можно добавить дополнительные типы.
        /// </summary>
        List<Type> KnownTypes { get; }

        /// <summary>
        /// Предоставление доступа к глобальному состоянию сеанса для текущего сеанса.  Это состояние
        /// сериализуется методом <see cref="SaveAsync"/> и восстанавливается
        /// методом <see cref="RestoreAsync"/>, поэтому значения обязаны поддерживать сериализацию
        /// классом <see cref="DataContractSerializer"/> и должны быть максимально компактными.  Настоятельно рекомендуется
        /// использовать строки и другие самодостаточные типы данных.
        /// </summary>
        Dictionary<string, object> SessionState { get; }
    }
}
