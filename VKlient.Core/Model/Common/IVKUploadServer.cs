namespace OneVK.Model.Common
{
    /// <summary>
    /// Интерфейс объекта сервера для отправки данных ВКонтакте.
    /// </summary>
    public interface IVKUploadServer
    {
        string UploadURL { get; set; }
    }
}
