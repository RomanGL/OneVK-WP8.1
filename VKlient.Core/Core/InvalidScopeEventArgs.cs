using System;
using System.Collections.Generic;

namespace OneVK.Core
{
    /// <summary>
    /// Представляет данные о событии, происходящем при вызове метода без наличия на то необходимых разрешений.
    /// </summary>
    public sealed class InvalidScopeEventArgs : EventArgs
    {
        /// <summary>
        /// Возвращает название вызванного метода.
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// Возвращает словарь параметров, переданных указанному методу.
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным именем метода и словарем параметров.
        /// </summary>
        /// <param name="methodName">Название метода.</param>
        /// <param name="parameters">Словарь параметров.</param>
        internal InvalidScopeEventArgs(string methodName, Dictionary<string,string> parameters)
        {
            MethodName = methodName;
            Parameters = parameters;
        }
    }
}
