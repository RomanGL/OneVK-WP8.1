using Newtonsoft.Json;
using OneVK.Enums.Common;
#if ONEVK_CORE
using GalaSoft.MvvmLight;
#endif

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой объект отметок "Мне нравится" ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public sealed class VKLikes : ObservableObject
#else
    public sealed class VKLikes
#endif    
    {         
        /// <summary>
        /// Может ли текущий пользователь "лайкнуть" данную фотографию.
        /// </summary>
        [JsonProperty("can_like")]
        public VKBoolean CanLike { get; set; }
        
        /// <summary>
        /// Может ли текущий пользователь скопировать запись себе на стену.
        /// </summary>
        [JsonProperty("can_publish")]
        public VKBoolean CanPublish { get; set; }

#if ONEVK_CORE
        private uint _count = 0;
        private VKBoolean _userLikes;

        /// <summary>
        /// Количество отметок "Мне нравится" на текущей фотографии.
        /// </summary>
        [JsonProperty("count")]
        public uint Count
        {
            get { return _count; }
            set { Set<uint>(() => Count, ref _count, value); }
        }

        /// <summary>
        /// Установил ли пользователь отметку "Мне нравится" на текущей фотографии.
        /// </summary>
        [JsonProperty("user_likes")]
        public VKBoolean UserLikes
        {
            get { return _userLikes; }
            set { Set<VKBoolean>(() => UserLikes, ref _userLikes, value); }
        }
#else
        /// <summary>
        /// Количество отметок "Мне нравится" на текущей фотографии.
        /// </summary>
        [JsonProperty("count")]
        public uint Count { get; set; }

        /// <summary>
        /// Установил ли пользователь отметку "Мне нравится" на текущей фотографии.
        /// </summary>
        [JsonProperty("user_likes")]
        public VKBoolean UserLikes { get; set; }
#endif
    }
}
