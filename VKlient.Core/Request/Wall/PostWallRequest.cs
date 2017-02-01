using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Публикует новую запись на свой или чужой стене
    /// </summary>
    public class PostWallRequest : BaseVKRequest<object>
    {
        private long _ownerID;
        private int _friendsOnly;
        private int _fromGroup;
        private string _message;
        private string _attachments;
        private string _services;
        private int _signed;
        //TODO: publish date
        private int? _lat;
        private int? _long;
        private long? _placeID;
        //TODO: отложенный ID 

        /// <summary>
        /// ID пользователя или сообщества, на стене которого
        /// должна быть опубликована запись
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            set
            {
                _ownerID = value;

                // Если указан ID сообщества, то нужно задать по-умолчанию запись от имени пользователя
                FromGroup = 0;
            }
        }

        /// <summary>
        /// Будет ли доступна запись только друзьям
        /// </summary>
        public int FriendsOnly
        {
            get { return _friendsOnly; }
            set
            {
                if ((value < 0) || (value > 1))
                    throw new ArgumentOutOfRangeException(FriendsOnly.GetType().Name,
                        "Значение может равняться либо 0, либо 1.");

                _friendsOnly = value;
            }
        }

        /// <summary>
        /// Если OwnerID отрицательный, то запись может быть
        /// опубликована либо от имени группы, либо от имени пользователя
        /// </summary>
        public int FromGroup
        {
            get { return _fromGroup; }
            set
            {
                //if (OwnerID >= 0)
                //    throw new ArgumentException(FromGroup.GetType().Name,
                //        "Значение не может быть задано, если " + OwnerID.GetType().Name + " не отрицательный");
                //if ((value < 0) || (value > 1))
                //    throw new ArgumentOutOfRangeException(FromGroup.GetType().Name,
                //        "Значение может равняться либо 0, либо 1.");
                _fromGroup = value;
            }
        }

        /// <summary>
        /// Сообщение для публикации
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(Message.GetType().Name,
                        "Необходимо указать значение");
                _message = value;
            }
        }

        /// <summary>
        /// Вложения
        /// </summary>
        public string Attachments
        {
            get { return _attachments; }
            set
            {
                _attachments = value;
            }
        }

        /// <summary>
        /// Список сервисов или сайтов, на которые
        /// необходимо экспортировать запись
        /// </summary>
        public string Services
        {
            get { return _services; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(Services.GetType().Name,
                        "Необходимо указать значение");
                _services = value;
            }
        }

        /// <summary>
        /// Опубликовывает подпись к записе на стене сообщества
        /// </summary>
        public int Signed
        {
            get { return _signed; }
            set
            {
                if ((value < 0) || (value > 1))
                    throw new ArgumentOutOfRangeException(Signed.GetType().Name,
                        "Значение может равняться либо 0, либо 1.");
                _signed = value;
            }
        }

        /// <summary>
        /// Географическая широта
        /// </summary>
        public int? Latitude
        {
            get { return _lat; }
            set
            {
                if (value == null)
                    _lat = null;
                if ((value < -90) || (value > 90))
                    throw new ArgumentOutOfRangeException(Latitude.GetType().Name,
                        "Значение должно быть в пределах от -90 до 90");
                _lat = value;
            }
        }

        /// <summary>
        /// Географическая долгота
        /// </summary>
        public int? Longitude
        {
            get { return _long; }
            set
            {
                if (value == null)
                    _long = null;
                if ((value < -180) || (value > 180))
                    throw new ArgumentOutOfRangeException(Longitude.GetType().Name,
                        "Значение должно быть в пределах от -180 до 180");
                _long = value;
            }
        }

        /// <summary>
        /// ID места, где отмечен пользователь
        /// </summary>
        public long? PlaceID
        {
            get { return _placeID; }
            set
            {
                if (value == null)
                    _placeID = null;
                if (value < 0)
                    throw new ArgumentOutOfRangeException(PlaceID.GetType().Name,
                            "Значение должно быть положительным");
                _placeID = value;
            }
        }

        public PostWallRequest(string message = null, string attachments = null)
        {
            if ((message == null) && (attachments == null))
                throw new ArgumentException("Необходимо указать хотя бы один аргумент");
            
            Message = message;
            Attachments = attachments;
        }

        /// <summary>
        /// Возвращает словарь параметров
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (FriendsOnly != 0) parameters["friends_only"] = FriendsOnly.ToString();
            if((OwnerID<0)&&(FromGroup!=0)) 
                parameters["from_group"] = FromGroup.ToString();
            if (Message != null) parameters["message"] = Message;
            if (Attachments != null) parameters["attachments"] = Attachments;
            if (Services != null) parameters["services"] = Services;
            if (Signed != 0) parameters["signed"] = Signed.ToString();
            if (Latitude != null) parameters["lat"] = Latitude.ToString();
            if (Longitude != null) parameters["long"] = Longitude.ToString();
            if (PlaceID != null) parameters["place_id"] = PlaceID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.WallPost;
        }
    }
}
