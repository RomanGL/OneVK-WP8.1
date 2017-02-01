using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс для запросов, связанных с методами,
    /// возвращающими коллекцию элементов.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public abstract class BaseVKCountedRequest<T> : BaseVKRequest<T>
    {
        private uint _maxCount = uint.MaxValue;
        private uint _count;
        private uint _offset;

        /// <summary>
        /// Значение количества элементов по умолчанию.
        /// </summary>
        protected uint DefaultCount { get; set; }

        /// <summary>
        /// Максимальное значение количества элементов.
        /// </summary>
        protected uint MaxCount
        {
            get { return _maxCount; }
            set { _maxCount = value; }
        }

        /// <summary>
        /// Количество элементов, которое требуется вернуть.
        /// Выполняет проверку на отрицательность.
        /// </summary>
        public virtual uint Count
        {
            get { return _count; }
            set
            {
                if (value > MaxCount)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество элементов не попадает в допустимый диапазон.");
                _count = value;
            }
        }

        /// <summary>
        /// Смещение, относительно начала списка.
        /// </summary>
        public virtual uint Offset { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Count != DefaultCount) parameters["count"] = Count.ToString();
            if (Offset > 0) parameters["offset"] = Offset.ToString();

            return parameters;
        }
    }
}
