using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на передачу голоса текущего пользователя за выбранный вариант ответа в указанном опросе.
    /// </summary>
    public class AddVoteRequest : BasePollRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор варианта ответа.
        /// </summary>
        public ulong AnswerID { get; set; }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PollsAddVote; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["answer_id"] = AnswerID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором опроса.
        /// </summary>
        /// <param name="pollID">Идентификатор опроса.</param>
        /// <param name="answerID">Идентификатор варианта ответа.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public AddVoteRequest(ulong pollID, ulong answerID)
            : base(pollID)
        {
            AnswerID = answerID;
        }
    }
}
