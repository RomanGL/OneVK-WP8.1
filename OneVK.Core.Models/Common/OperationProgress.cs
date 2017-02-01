using System;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет прогресс операции запроса к веб-серверу.
    /// </summary>
    public struct OperationProgress
    {
        /// <summary>
        /// Возвращает процент выполенения операции.
        /// </summary>
        public byte Percent { get; private set; }
        
        /// <summary>
        /// Инициализирует новый экземпляр стркутуры <see cref="OperationProgress"/> с заданным
        /// процентом выполнения.
        /// </summary>
        /// <param name="percent">Процент выполнения операции.</param>
        public OperationProgress(byte percent)
        {
            if (percent > 100) throw new InvalidOperationException("Процент выполнения не может быть больше 100.");
            Percent = percent;
        }
    }
}
