namespace OneVK.Request
{
    /// <summary>
    /// Интерфейс запроса к ВКонтакте.
    /// </summary>
    public interface IVKRequestOld : IRequest
    {
        string GetMethod();
    }
}
