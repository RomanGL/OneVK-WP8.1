using System;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет исключение, которое выбрасываетсяы при ошибке проигрывателя аудио.
    /// </summary>
    public class PlayerException : Exception
    {
        public PlayerException() { }
        public PlayerException(string message) : base(message) { }
        public PlayerException(string message, Exception inner) : base(message, inner) { }
    }
}
