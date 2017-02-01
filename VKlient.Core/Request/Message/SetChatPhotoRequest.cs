using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос установить фотографию мультидиалога, загруженную с помощью метода photos.getChatUploadServer.
    /// </summary>
    public class SetChatPhotoRequest : BaseRequest, IVKRequestOld
    {
        private string _file;
        
        /// <summary>
        /// Содержимое поля response из ответа специального upload-сервера, полученного в результате загрузки изображения на адрес,
        /// полученный методом photos.getChatUploadServer.
        /// </summary>
        public string File
        {
            get { return _file; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("File",
                        "Строка не может быть пустой.");
                _file = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageSetChatPhoto; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["file"] = File;
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="file">Содержимое поля response из ответа специального upload-сервера, полученного в результате загрузки изображения
        /// на адрес, полученный методом photos.getChatUploadServer.</param>
        /// <exception cref="ArgumentException"/>
        public SetChatPhotoRequest(string file)
        {
            File = file;
        }
    }
}
