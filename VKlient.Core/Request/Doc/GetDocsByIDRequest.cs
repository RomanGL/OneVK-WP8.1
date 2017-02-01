using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации о документах 
    /// по их идентификаторам.
    /// </summary>
    public class GetDocsByIDRequest : BaseRequest, IVKRequestOld
    {
        private Dictionary<long, long> _docs;

        /// <summary>
        /// Словарь пар OwnerID - DocID - идентификаторы документов, 
        /// информацию о которых требуется получить.
        /// </summary>
        public Dictionary<long, long> Docs
        {
            get { return _docs; }
            private set
            {
                if (_docs == null)
                    throw new ArgumentNullException("Docs",
                        "Объект должен быть инициализирован.");
                else if (Docs.Count == 0)
                    throw new ArgumentException("Docs",
                        "Количество пар OwnerID - DocID должно быть больше нуля.");
                _docs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.DocsGetByID; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["docs"] = String.Join(",", Docs.Select(kp => kp.Key.ToString() + "_" + kp.Value));
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным словарем идентификаторов 
        /// документов, информацию о которых требуется получить.
        /// </summary>
        /// <param name="docs">Словарь пар OwnerID - DocID - идентификаторы документов, 
        /// информацию о которых требуется получить.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public GetDocsByIDRequest(Dictionary<long, long> docs)
        {
            Docs = docs;
        }
    }
}
