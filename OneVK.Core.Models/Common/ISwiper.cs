namespace OneVK.Core.Models.Common
{
    public interface ISwiper
    {
        /// <summary>
        /// Получить предыдущий элемент.
        /// </summary>
        object GetBackward();
        /// <summary>
        /// Получить следующий элемент.
        /// </summary>
        object GetForward();
        /// <summary>
        /// Получить текущий элемент.
        /// </summary>
        object GetCurrent();
        /// <summary>
        /// Перейти вперед на один элемент. Возвращает <see cref="false"/>, если перейти не удалось.
        /// </summary>
        bool GoForward();
        /// <summary>
        /// Перейти назад на один элемент. Возвращает <see cref="false"/>, если перейти не удалось.
        /// </summary>
        bool GoBackward();
    }
}
