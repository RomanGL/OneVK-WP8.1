using OneVK.Model.Photo;
using OneVK.Model.Video;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Обеспечивает логику выбора шаблона для медиаэлемента.
    /// </summary>
    public sealed class MediaItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон для изображения.
        /// </summary>
        public DataTemplate ImageTemplate { get; set; }
        /// <summary>
        /// Шаблон для видеозаписи.
        /// </summary>
        public DataTemplate VideoTemplate { get; set; }

        /// <summary>
        /// Возвращает шаблон на основании типа элемента.
        /// </summary>
        /// <param name="item">Элемент, для которого необходимо получить шаблон.</param>
        /// <param name="container">Контейнер, в котором содержится элемент.</param>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is VKPhoto)
                return ImageTemplate;
            else if (item is VKVideoBase)
                return VideoTemplate;
            else
                return null;
        }
    }
}
