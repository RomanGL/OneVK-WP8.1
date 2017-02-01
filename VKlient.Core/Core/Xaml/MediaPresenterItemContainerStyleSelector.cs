using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Core;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OneVK.Helpers;
using OneVK.Model.Photo;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Обеспечивает логику выбора стиля для представителя медиаэлементов.
    /// </summary>
    public class MediaPresenterItemContainerStyleSelector : StyleSelector
    {
        /// <summary>
        /// Стиль для контейнера с одним медиаэлементом.
        /// </summary>
        public Style SingleItemStyle { get; set; }
        /// <summary>
        /// Стиль для контейнера с двумя и более медиаэлементами.
        /// </summary>
        public Style ManyItemsStyle { get; set; }        

        /// <summary>
        /// Возвращает стиль на основании типа элемента.
        /// </summary>
        /// <param name="item">Элемент, для которого необходимо получить шаблон.</param>
        /// <param name="container">Контейнер, в котором содержится элемент.</param>
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            return null;
        }
    }
}
