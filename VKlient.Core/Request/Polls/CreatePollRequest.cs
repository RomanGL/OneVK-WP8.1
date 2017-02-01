using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Model.Polls;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на создание опросов, которые впоследствии можно прикреплять к записям на странице пользователя или сообщества.
    /// </summary>
    public class CreatePollRequest : BaseVKRequest<VKPoll>
    {
        private List<string> _addAnswers;

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Указывает, является ли опрос анонимным.
        /// </summary>
        public VKBoolean IsAnonymous { get; set; }

        /// <summary>
        /// Если опрос будет добавлен в группу, необходимо передать отрицательный идентификатор группы. По умолчанию текущий пользователь.
        /// </summary>
        public long OwnerID { get; set; }

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
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PollsCreate; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["question"] = Question;
            if (IsAnonymous != VKBoolean.False) parameters["is_anonymous"] = IsAnonymous.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["add_answers"] = (string)JsonConvert.SerializeObject(AddAnswers);

            return parameters;
        }
     
        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором опроса,
        /// текстом вопроса и списком вариантов ответов.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца опроса.</param>
        /// <param name="question">Текст вопроса.</param>
        /// <param name="addAnswers">Список вариантов ответов.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public CreatePollRequest(long ownerID, string question, List<string> addAnswers)
        {
            OwnerID = ownerID;
            Question = question;
            AddAnswers = addAnswers;
        }
    }
}