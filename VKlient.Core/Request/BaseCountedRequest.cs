using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс для запросов, связанных с методами,
    /// возвращающими коллекцию элементов.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseCountedRequest : BaseRequest, IVKRequestOld
    {
        private uint _maxCount = int.MaxValue;
        private uint _count;
        private int _offset;

        /// <summary>
        /// Значение количества элементов по умолчанию.
        /// </summary>
        protected int DefaultCount { get; set; }

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
        /// Возвращает метод, связанный с запросом.
        /// Это абстрактный метод.
        /// </summary>
        public abstract string GetMethod();

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
