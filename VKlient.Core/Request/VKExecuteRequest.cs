using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет универсальный запрос ко всем методам execute, хранящимся на сервере.
    /// </summary>
    public class VKExecuteRequest<T> : BaseVKRequest<T>
    {
        private readonly string _executeMethodName;
        private Dictionary<string, string> _parameters;

        /// <summary>
        /// Возвращает название хранимой на сервере процедуры.
        /// </summary>
        public string ExecuteMethodName { get { return _executeMethodName; } }

        /// <summary>
        /// Возвращает название метода.
        /// </summary>
        public override sealed string GetMethod() { return VKMethodsConstants.ExecuteSubstring + _executeMethodName + "?"; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters() { return new Dictionary<string,string>(); }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным названием процедуры.
        /// </summary>
        /// <param name="executeMethodName">Название хранимой процедуры.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        public VKExecuteRequest(string executeMethodName)
        {
            _executeMethodName = executeMethodName;
        }
    }
}
