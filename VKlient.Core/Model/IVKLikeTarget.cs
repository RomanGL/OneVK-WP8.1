using OneVK.Enums.Common;

namespace OneVK.Model
{
    /// <summary>
    /// Интерфейс объекта, которому может быть поставлена отметка "Мне нравится".
    /// </summary>
    public interface IVKLikeTarget
    {
        /// <summary>
        /// Тип целевого объекта для отметки "Мне нравится".
        /// </summary>
        VKLikesTargetType TargetType { get; }

        /// <summary>
        /// Идентификатор владельца объекта.
        /// </summary>
        long LikesOwner { get; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        ulong LikesItem { get; }

        /// <summary>
        /// Ключ доступа в случае работы с приватным объектом.
        /// </summary>
        string AccessKey { get; }
    }
}
