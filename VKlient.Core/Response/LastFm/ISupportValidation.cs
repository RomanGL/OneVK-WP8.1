namespace OneVK.Response
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
