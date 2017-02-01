﻿using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на запрещение показа новостей от заданных
    /// пользователей или групп в ленте текущего пользователя.
    /// </summary>
    public class AddNewsfeedBanRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private List<long> _ids;

        /// <summary>
        /// Создан ли запрос для блокировки новостей от сообществ.
        /// </summary>
        public bool ForGroups { get; set; }

        /// <summary>
        /// Список идентификаторов друзей или сообществ, новости
        /// от которых он хочет скрыть.
        /// </summary>
        public List<long> IDs
        {
            get { return _ids; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("IDs",
                        "Коллекция должна быть инициализирована и должна иметь хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("IDs",
                        "Количество элементов должно быть больше нуля.");
                _ids = value;
            }
        }

        /// <summary>
        /// Создает новый экземпляр класса с указанными параметрами.
        /// </summary>
        /// <param name="ids">Идентификаторы пользователей или сообществ, для
        /// которых создается запрос блокировки.</param>
        /// <param name="isGroups">Создается ли запрос для блокировки новостей 
        /// от сообществ.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public AddNewsfeedBanRequest(List<long> ids, bool isGroups = false)
        {
            IDs = ids;
            ForGroups = isGroups;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (ForGroups) parameters["group_ids"] = String.Join(",", IDs);
            else parameters["user_ids"] = String.Join(",", IDs);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedAddBan;
        }
    }
}
