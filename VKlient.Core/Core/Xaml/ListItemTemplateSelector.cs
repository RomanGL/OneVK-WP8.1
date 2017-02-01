using OneVK.Model.Common;
using OneVK.Model.Doc;
using OneVK.Model.Polls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Обеспечивает логику выбора шаблона для элемента ListItemsPresenter.
    /// </summary>
    public sealed class ListItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон для отображения ссылки.
        /// </summary>
        public DataTemplate LinkTemplate { get; set; }
        /// <summary>
        /// Шаблон для отображения опроса.
        /// </summary>
        public DataTemplate PollTemplate { get; set; }
        /// <summary>
        /// Шаблон для отображения документа.
        /// </summary>
        public DataTemplate DocumentTemplate { get; set; }

        /// <summary>
        /// Возвращает шаблон на основании типа элемента.
        /// </summary>
        /// <param name="item">Элемент, для которого необходимо получить шаблон.</param>
        /// <param name="container">Контейнер, в котором содержится элемент.</param>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is VKLink)
                return LinkTemplate;
            else if (item is VKPoll)
                return PollTemplate;
            else if (item is VKDocument)
                return DocumentTemplate;
            else
                return null;
        }
    }
}
