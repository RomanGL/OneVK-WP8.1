using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет базовый класс запроса на получение информации о
    /// фотографиях пользователя или сообщества по их идентификаторам.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
    public abstract class BaseGetPhotosByIDRequest<T> : BaseVKRequest<T>
    {
        private List<string> _photos;

        /// <summary>
        /// Словарь идентификатороф фотографий в формате OwnerID-PhotoID.
        /// </summary>
        public List<string> Photos
        {
            get { return _photos; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Photos",
                        "Словарь должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("Photos",
                        "Словарь должен содержать как минимум одну пару OwnerID-PhotoID.");
                _photos = value;
            }
        }

        /// <summary>
        /// Баозовый конструктор.
        /// </summary>
        /// <param name="photos">Словарь идентификатороф фотографий в формате OwnerID-PhotoID-AccessKey.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BaseGetPhotosByIDRequest(List<string> photos)
        {
            Photos = photos;
        }

        /// <summary>
        /// Возвращает коллекию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["photos"] = String.Join(",", Photos);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        /// <returns></returns>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetByID; }
    }
}
