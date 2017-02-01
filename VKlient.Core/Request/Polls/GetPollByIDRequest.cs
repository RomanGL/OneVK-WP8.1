using OneVK.Model.Polls;
using System;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение детальной информации об опросе по его идентификатору.
    /// </summary>
    public class GetPollByIDRequest : BasePollRequest<VKPoll>
    {
        public override string GetMethod() { return VKMethodsConstants.PollsGetByID; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором опроса.
        /// </summary>
        /// <param name="pollID">Идентификатор опроса.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetPollByIDRequest(ulong pollID)
            : base(pollID) { }
    }
}
