using OneVK.Model.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    public class GetPhotosChatUploadServer :  BaseVKRequest<VKUploadServerBase>
    {
        private ulong _chatID;
        private uint _width;

        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public ulong ChatID
        {
            get { return _chatID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("ChatID",
                        "Идентификатор чата не может быть равен нулю.");
                _chatID = value;
            }
        }

        /// <summary>
        /// Координата X для обрезки фотографии.
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// Координата Y для обрезки фотографии.
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// Ширина фотографии после обрезки.
        /// </summary>
        public uint Width
        {
            get { return _width; }
            set
            {
                if (value < 200)
                {
                    throw new ArgumentOutOfRangeException("Width",
                        "Ширина фотографии должна быть не меньше 200 пикселей.");
                }
                _width = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        public GetPhotosChatUploadServer(ulong chatID)
        {
            ChatID = chatID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["chat_id"] = ChatID.ToString();
            if (X != 0) parameters["crop_x"] = X.ToString();
            if (Y != 0) parameters["crop_y"] = Y.ToString();
            if (Width != 0) parameters["crop_width"] = Width.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetChatUploadServer; }
    }
}
