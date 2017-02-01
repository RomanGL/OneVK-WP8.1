using System;

namespace OneVK.Helpers
{
    /// <summary>
    /// Класс-помощник для валидации данных в классах запросов.
    /// </summary>
    internal static class DataValidationHelper
    {
        #region GreaterThanZero
        /// <summary>
        /// Выбрасывает исключение, если значение меньше либо равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckGreaterThanZero(long value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Значение не может быть отрицательным.");
        }

        /// <summary>
        /// Выбрасывает исключение, если значение меньше либо равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckGreaterThanZero(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Значение не может быть отрицательным.");
        }

        /// <summary>
        /// Выбрасывает исключение, если значение меньше либо равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckGreaterThanZero(double value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Значение не может быть отрицательным.");
        }
        #endregion

        #region LessThanZero
        /// <summary>
        /// Выбрасывает исключение, если значение больше нуля.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckLessThanZero(long value)
        {
            if (value >= 0)
                throw new ArgumentOutOfRangeException("Значение должно быть меньше либо равно нулю.");
        }

        /// <summary>
        /// Выбрасывает исключение, если значение больше нуля.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckLessThanZero(int value)
        {
            if (value >= 0)
                throw new ArgumentOutOfRangeException("Значение должно быть меньше либо равно нулю.");
        }

        /// <summary>
        /// Выбрасывает исключение, если значение больше нуля.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void CheckLessThanZero(double value)
        {
            if (value >= 0)
                throw new ArgumentOutOfRangeException("Значение должно быть меньше либо равно нулю.");
        }
        #endregion

        #region NullOrWhiteSpace
        /// <summary>
        /// Выбрасывает исключение, если значение пустое или состоит лишь из пробельных символов.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentException"/>
        public static void CheckNullOrWhiteSpace(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Значение не должно быть пустым или состоять лишь из пробельных символов.");
        }
        #endregion

        #region EqualZero
        /// <summary>
        /// Выбрасывает исключение, если значение равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentException"/>
        public static void CheckEqualZero(long value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException("Значение является обязательным параметром.");
        }
        /// <summary>
        /// Выбрасывает исключение, если значение равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentException"/>
        public static void CheckEqualZero(int value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException("Значение является обязательным параметром.");
        }
        /// <summary>
        /// Выбрасывает исключение, если значение равно нулю.
        /// </summary>
        /// <param name="value">Значение, которое требуется проверить.</param>
        /// <exception cref="ArgumentException"/>
        public static void CheckEqualZero(double value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException("Значение является обязательным параметром.");
        }
        #endregion
    }
}
