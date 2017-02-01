using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения информации об опросах и выполенения голосования.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BasePollRequest<T> : BaseVKRequest<T>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит опрос.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Находится ли опрос в обсуждении.
        /// </summary>
        public VKBoolean IsBoard { get; set; }

        /// <summary>
        /// Идентификатор опроса.
        /// </summary>
        public ulong PollID { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (IsBoard != VKBoolean.False) parameters["is_board"] = ((byte)IsBoard).ToString();
            parameters["poll_id"] = PollID.ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором опроса.
        /// </summary>
        /// <param name="pollID">Идентификатор опроса.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        protected BasePollRequest(ulong pollID)
        {
            PollID = pollID;
        }
    }
}
