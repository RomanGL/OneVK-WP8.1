using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    public class RepostWallRequest:IVKRequestOld
    {
        private string _targetObject;
        private string _message;
        private long _groupID;

        /// <summary>
        /// ID объекта, которым необходимо поделиться.
        /// </summary>
        public string TargetObject
        {
            get { return _targetObject; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(TargetObject.GetType().Name,
                        "Необходимо указать значение");
                _targetObject = value;
            }
        }

        /// <summary>
        /// Сообщение к репосту
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
        /// ID группы при репосте в нее
        /// </summary>
        public long GroupID
        {
            get { return _groupID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(GroupID.GetType().Name,
                            "Значение должно быть положительным");
                _groupID = value;
            }
        }

        public RepostWallRequest(string targetObject)
        {
            TargetObject = targetObject;
        }

        public Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>()
            {
                {"object", TargetObject}
            };

            if (Message != null)
                parameters["message"] = Message;
            // TODO: Решить с группами
            //if(GroupID)

            return parameters;
        }

        public string GetMethod()
        {
            return VKMethodsConstants.WallRepost;
        }
    }
}
