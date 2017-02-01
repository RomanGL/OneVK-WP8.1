using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneVK.Request
{
    /// <summary>
    /// Получает список идентификаторов пользователей, которые выбрали определенные варианты ответа в опросе.
    /// </summary>
    public class GetVotersRequest : BasePollRequest<VKGetVotersObject>
    {
        private List<long> _answerIDs;
        private uint _count;

        /// <summary>
        /// Идентификаторы вариантов ответа.
        /// </summary>
        public List<long> AnswerIDs
        {
            get { return _answerIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AnswerIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("AnswerIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("AnswerIDs",
                        "Идентификатор варианта ответа должен быть положительным.");
                _answerIDs = value;
            }
        }

        /// <summary>
        /// Указывает, необходимо ли возвращать только пользователей, которые являются друзьями текущего пользователя.
        /// </summary>
        public VKBoolean FriendsOnly { get; set; }

        /// <summary>
        /// Смещение относительно начала списка для выборки определенного подмножества.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Количество возвращаемых идентификаторов пользователей.
        /// </summary>
        public uint Count
        {
            get { return _count; }
            set
            {
                if (value > 1000)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество элементов больше 1000.");
                _count = value;
            }
        }

        /// <summary>
        /// Перечисленные через запятую поля анкет, необходимые для получения.
        /// </summary>
        public List<VKUserFields> Fields { get; set; }

        /// <summary>
        /// Падеж для склонения имени и фамилии пользователя.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PollsGetVoters; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["answer_ids"] = String.Join(",", AnswerIDs);
            if (FriendsOnly != VKBoolean.False) parameters["friends_only"] = FriendsOnly.ToString();
            if (Offset != 0) parameters["offset"] = Offset.ToString();            
            if (Count != 0) parameters["count"] = Count.ToString();
            if (Fields != null)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < Fields.Count; i++)
                    builder.Append(Fields[i] + ",");
                parameters["fields"] = builder.ToString();
            }
            if (NameCase != VKUserNameCase.nom) parameters["name_case"] = NameCase.ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором опроса
        /// и списком идентификаторов вариантов ответа.
        /// </summary>
        /// <param name="pollID">Идентификатор опроса.</param>
        /// <param name="answerIDs">Идентфикаторы вариантов ответа.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public GetVotersRequest(ulong pollID, List<long> answerIDs)
            : base(pollID)
        {
            AnswerIDs = answerIDs;
        }
    }
}
