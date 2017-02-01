using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис управления жизненным циклом приложения. 
    /// </summary>
    public sealed class SuspensionService : ISuspensionService
    {
        private const string SessionStateFileName = "_sessionState.xml";

        private List<Type> _knownTypes = new List<Type>();
        private List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();
        private Dictionary<string, object> _sessionState = new Dictionary<string, object>();

        private static DependencyProperty FrameSessionStateKeyProperty =
            DependencyProperty.RegisterAttached("FrameSessionStateKey", typeof(String), typeof(SuspensionService), null);
        private static DependencyProperty FrameSessionBaseKeyProperty =
            DependencyProperty.RegisterAttached("FrameSessionBaseKeyParams", typeof(String), typeof(SuspensionService), null);
        private static DependencyProperty FrameSessionStateProperty =
            DependencyProperty.RegisterAttached("FrameSessionState", typeof(Dictionary<String, Object>), typeof(SuspensionService), null);

        /// <summary>
        /// Список пользовательских типов, предоставляемых сериализатору <see cref="DataContractSerializer"/> при
        /// чтении или записи состояния сеанса.  Первоначально список является пустым; для настройки процесса сериализации
        /// можно добавить дополнительные типы.
        /// </summary>
        public List<Type> KnownTypes { get { return _knownTypes; } }

        /// <summary>
        /// Предоставление доступа к глобальному состоянию сеанса для текущего сеанса.  Это состояние
        /// сериализуется методом <see cref="SaveAsync"/> и восстанавливается
        /// методом <see cref="RestoreAsync"/>, поэтому значения обязаны поддерживать сериализацию
        /// классом <see cref="DataContractSerializer"/> и должны быть максимально компактными.  Настоятельно рекомендуется
        /// использовать строки и другие самодостаточные типы данных.
        /// </summary>
        public Dictionary<string, object> SessionState { get { return _sessionState; } }

        /// <summary>
        /// Регистрирует экземпляр <see cref="Frame"/> для возможности сохранения его состояния.
        /// </summary>
        /// <param name="frame">Экземпляр для регистрации.</param>
        /// <param name="sessionStateKey">Уникальный ключ в <see cref="SessionState"/>, используемый для 
        /// хранения данных, связанных с навигацией.</param>
        /// <param name="sessionBaseKey">Необязательный ключ, определяющий тип сеанса.</param>
        /// <exception cref="InvalidOperationException"/>
        public void RegisterFrame(Frame frame, string sessionStateKey, string sessionBaseKey = null)
        {
            if (frame.GetValue(FrameSessionStateKeyProperty) != null)
            {
                throw new InvalidOperationException("Frames can only be registered to one session state key");
            }
            if (frame.GetValue(FrameSessionStateProperty) != null)
            {
                throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
            }
            if (!string.IsNullOrEmpty(sessionBaseKey))
            {
                frame.SetValue(FrameSessionBaseKeyProperty, sessionBaseKey);
                sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
            }

            frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
            _registeredFrames.Add(new WeakReference<Frame>(frame));

            RestoreFrameNavigationState(frame);
        }

        /// <summary>
        /// Предоставляет хранилище для состояния сеанса, связанного с указанным объектом <see cref="Frame"/>.
        /// Состояние сеанса фреймов, ранее зарегистрированных с помощью <see cref="RegisterFrame"/>,
        /// сохраняется и восстанавливается автоматически в составе глобального объекта
        /// <see cref="SessionState"/>.  Незарегистрированные фреймы имеют переходное состояние,
        /// которое, тем не менее, можно использовать при восстановлении страниц, удаленных из кэша навигации.
        /// </summary>
        public Dictionary<string, object> SessionStateForFrame(Frame frame)
        {
            var frameState = (Dictionary<string, object>)frame.GetValue(FrameSessionStateProperty);

            if (frameState == null)
            {
                var frameSessionKey = (string)frame.GetValue(FrameSessionStateKeyProperty);
                if (frameSessionKey != null)
                {
                    // Зарегистрированные фреймы отражают соответствующее состояние сеанса
                    if (!_sessionState.ContainsKey(frameSessionKey))
                    {
                        _sessionState[frameSessionKey] = new Dictionary<string, object>();
                    }
                    frameState = (Dictionary<string, object>)_sessionState[frameSessionKey];
                }
                else
                {
                    // Незарегистрированные фреймы имеют переходное состояние
                    frameState = new Dictionary<string, object>();
                }
                frame.SetValue(FrameSessionStateProperty, frameState);
            }
            return frameState;
        }

        /// <summary>
        /// Отменяет регистрацию ранее зарегистрированного экземпляра <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame">Экземпляр для отмены регистрации.</param>
        public void UnregisterFrame(Frame frame)
        {
            SessionState.Remove((string)frame.GetValue(FrameSessionStateKeyProperty));
            _registeredFrames.RemoveAll(reference =>
            {
                Frame testFrame;
                return !reference.TryGetTarget(out testFrame) || testFrame == frame;
            });
        }

        /// <summary>
        /// Восстанавливает глобальное состояние сеанса и состояние каждого зарегистрированного фрейма.
        /// </summary>
        /// <param name="sessionBaseKey">Необязательный ключ, определяющий тип сеанса.</param>
        public async Task RestoreAsync(string sessionBaseKey = null)
        {
            _sessionState = new Dictionary<string, object>();
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(SessionStateFileName);
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
                _sessionState = (Dictionary<string, object>)serializer.ReadObject(inputStream.AsStreamForRead());
            }

            foreach (var weakFrameReference in _registeredFrames)
            {
                Frame frame;
                if (weakFrameReference.TryGetTarget(out frame) && (string)frame.GetValue(FrameSessionBaseKeyProperty) == sessionBaseKey)
                {
                    frame.ClearValue(FrameSessionStateProperty);
                    RestoreFrameNavigationState(frame);
                }
            }
        }

        /// <summary>
        /// Сохраняет состояние всех зарегистрированных <see cref="Frame"/> и глобальное состояние сеанса.
        /// </summary>
        public async Task SaveAsync()
        {
            foreach (var reference in _registeredFrames)
            {
                Frame frame;
                if (reference.TryGetTarget(out frame))
                    SaveFrameNavigationState(frame);
            }

            var sessionData = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
            serializer.WriteObject(sessionData, _sessionState);

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(SessionStateFileName, CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                sessionData.Seek(0, SeekOrigin.Begin);
                await sessionData.CopyToAsync(fileStream);
            }
        }

        /// <summary>
        /// Восстанавливает состояние навигации указанного экземпляра <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame">Экземпляр, состояние навигации которого требуется восстановить.</param>
        private void RestoreFrameNavigationState(Frame frame)
        {
            var frameState = SessionStateForFrame(frame);
            if (frameState.ContainsKey("Navigation"))
            {
                frame.SetNavigationState((String)frameState["Navigation"]);
            }
        }

        /// <summary>
        /// Сохраняет состояние навигации указанного экземпляра <see cref="Frame"/>.
        /// </summary>
        /// <param name="frame"></param>
        private void SaveFrameNavigationState(Frame frame)
        {
            var frameState = SessionStateForFrame(frame);
            frameState["Navigation"] = frame.GetNavigationState();
        }
    }
}
