namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение сервера для загрузки документа 
    /// в папку "Отправленные" для последующей отправки на стену или 
    /// личным сообщением.
    /// </summary>
    public class GetDocsWallUploadServerRequest : GetDocsUploadServerRequest
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public new string GetMethod() { return VKMethodsConstants.DocsGetWallUploadServer; }
    }
}
