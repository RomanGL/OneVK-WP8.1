using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на сохранение загруженного в ВКонтакте документа.
    /// </summary>
    public class SaveDocRequest : BaseRequest, IVKRequestOld
    {
        private string _file;
        private List<string> _tags;

        /// <summary>
        /// Параметр, возвращаемый в результате загрузки файла на сервер.
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
        /// Название документа.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Список тегов, используемых при поиске документов.
        /// </summary>
        public List<string> Tags
        {
            get { return _tags; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Tags",
                        "Объект не может быть неопределенным.");
                else if (value.Count == 0)
                    throw new ArgumentException("Tags",
                        "В коллекции должен находиться как минимум один элемент.");
                _tags = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.DocSave; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["file"] = File;
            if (!String.IsNullOrWhiteSpace("Title")) parameters["title"] = Title;
            if (Tags != null) parameters["tags"] = String.Join(",", Tags);

            return parameters;
        }
    }
}
