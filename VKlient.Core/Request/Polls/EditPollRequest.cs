using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на редактирование созданного опроса.
    /// </summary>
    public class EditPollRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private List<string> _addAnswers;
        private Dictionary<long, string> _editAnswers;
        private List<long> _deleteAnswers;

        /// <summary>
        /// Идентификатор владельца опроса.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор редактируемого опроса.
        /// </summary>
        public ulong PollID { get; set; }

        /// <summary>
        /// Новый текст редактируемого опроса.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Список вариантов ответов.
        /// </summary>
        public List<string> AddAnswers
        {
            get { return _addAnswers; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AddAnswers",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("AddAnswers",
                        "Количество элементов должно быть больше нуля.");
                else if (value.Count > 10)
                    throw new ArgumentException("AddAnswers",
                        "Количество элементов должно быть меньше 10.");
                _addAnswers = value;
            }
        }

        /// <summary>
        /// Объект, содержащий варианты ответов, которые необходимо отредактировать.
        /// </summary>
        public Dictionary<long, string> EditAnswers
        {
            get { return _editAnswers; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AddAnswers",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("EditAnswers",
                        "Количество элементов должно быть больше нуля.");
                _editAnswers = value;
            }
        }

        /// <summary>
        /// Список идентификаторов ответов, которые необходимо удалить.
        /// </summary>
        public List<long> DeleteAnswers
        {
            get { return _deleteAnswers; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AddAnswers",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("DeleteAnswers",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("DeleteAnswers",
                        "Идентификатор варианта ответа должен быть положительным.");
                _deleteAnswers = value;
            }
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PollsEdit; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["poll_id"] = PollID.ToString();
            if (!string.IsNullOrWhiteSpace(Question)) parameters["question"] = Question;
            if (AddAnswers != null) parameters["add_answers"] = (string)JsonConvert.SerializeObject(AddAnswers);
            if (EditAnswers != null) parameters["edit_answers"] = (string)JsonConvert.SerializeObject(EditAnswers);
            if (DeleteAnswers != null) parameters["delete_answers"] = (string)JsonConvert.SerializeObject(DeleteAnswers);

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с идентификаторами
        /// редактируемого опроса и его владельца.
        /// </summary>
        /// <param name="pollID">Идентификатор опроса.</param>
        /// <param name="ownerID">Идентификатор владельца опроса.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public EditPollRequest(ulong pollID, long ownerID)
        {
            PollID = pollID;
            OwnerID = ownerID;
        }
    }
}
