namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Реализуют объекты, поддерживающие валидацию своих данных.
    /// </summary>
    public interface ISupportValidation
    {
        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        bool IsValid();
    }
}
