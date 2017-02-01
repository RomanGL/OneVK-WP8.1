using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет объект запроса для получения списка общих друзей.
    /// </summary>
    public class GetMutualFriendsRequest : BaseCountedRequest, IVKRequestOld
    {
        private VKFriendsOrder? _order = null;
        private long _sourceID;
        private long _targetID;
        private List<long> _targetIDs;

        public bool IsSingle { get; private set; }

        /// <summary>
        /// Метод сортировки результатов.
        /// Поддерживается только значение Random.
        /// </summary>
        public VKFriendsOrder? Order
        {
            get { return _order; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Order",
                        "Объект должен быть инициализирован.");
                else if (value.Value != VKFriendsOrder.Random)
                    throw new ArgumentException("Order",
                        "Поддерживается только значение Random.");
                _order = value;
            }
        }

        /// <summary>
        /// Идентификатор пользователя, чьи друзья пересекаются с
        /// друзьями пользователя TargetID.
        /// </summary>
        public long SourceID
        {
            get { return _sourceID; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("SourceID",
                        "Идентификатор пользователя не может быть отрицательным.");
                _sourceID = value;
            }
        }

        /// <summary>
        /// Идентификатор пользователя, общих друзей с которым нужно найти.
        /// </summary>
        public long TargetID
        {
            get { return _targetID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("TargetID",
                        "Значение должно быть положительным.");
                _targetID = value;
            }
        }

        /// <summary>
        /// Список идентификаторов пользователей, с которыми нужно найти общих друзей.
        /// </summary>
        public List<long> TargetIDs
        {
            get { return _targetIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("TargetIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("TargetIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("TargetIDs",
                        "Идентификатор пользователя должен быть положительным.");
                _targetIDs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.FriendsGetMutual;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (SourceID > 0) parameters["source_uid"] = SourceID.ToString();
            if (Order.Value == VKFriendsOrder.Random) parameters["order"] = "random";
            if (IsSingle) parameters["target_uid"] = TargetID.ToString();
            else parameters["target_uids"] = String.Join(",", TargetIDs);

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным значением
        /// цели для поиска общих друзей с текущим пользователем.
        /// </summary>
        /// <param name="targetID">Идентификатор пользователя, общих друзей 
        /// с которым нужно найти.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetMutualFriendsRequest(long targetID)
            : this(0, targetID, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса со списком идентификаторов
        /// целей для поиска общих друзей с текущим пользователем.
        /// </summary>
        /// <param name="targetIDs">Список идентификаторов пользователей,
        /// общих друзей с которыми нужно найти.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetMutualFriendsRequest(List<long> targetIDs)
            : this(0, null, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным значением цели
        /// для поиска общих друзей с указанным пользователем.
        /// </summary>
        /// <param name="sourceID">Идентификатор пользователя, для которого 
        /// выполняется поиск общих друзей.</param>
        /// <param name="targetID">Идентификатор пользователя, общих друзей 
        /// с которым требуется найти.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetMutualFriendsRequest(long sourceID, long targetID)
            : this(sourceID, targetID, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса со списком идентификаторов 
        /// целей для поиска общих друзей с указанным пользователем.
        /// </summary>
        /// <param name="sourceID">Идентификатор пользователя, для которого
        /// выполняется поиск общих друзей.</param>
        /// <param name="targetIDs">Список идентификаторов пользователей,
        /// общих друзей с которыми нужно найти.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetMutualFriendsRequest(long sourceID, List<long> targetIDs)
            : this(sourceID, null, targetIDs) { }

        /// <summary>
        /// Приватный конструктор. Получает данные от публичных конструкторов
        /// и заполняет поля.
        /// </summary>
        /// <param name="sourceID">Идентификатор пользователя, для которого
        /// выполняется поиск общих друзей.</param>
        /// <param name="targetID">Идентификатор пользователя, общих друзей 
        /// с которым требуется найти.</param>
        /// <param name="targetIDs">Список идентификаторов пользователей,
        /// общих друзей с которыми нужно найти.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        private GetMutualFriendsRequest(long sourceID, long? targetID, List<long> targetIDs)
        {
            SourceID = sourceID;
            if (targetID != null)
            {
                TargetID = targetID.Value;
                IsSingle = true;
            }
            else
            {
                TargetIDs = targetIDs;
                IsSingle = false;
            }
        }
    }
}
