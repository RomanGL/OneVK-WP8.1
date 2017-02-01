using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace OneVK.Helpers
{
    public static class ListViewExtensions
    {
        /// <summary>
        /// Возвращает первый видимый объект в коллекции, привязанной к ListView.
        /// </summary>
        /// <typeparam name="T">Тип данных в привязанной коллекции.</typeparam>
        /// <param name="list">Элемент управления списка.</param>
        public static T GetFirstVisibleItem<T>(this ListView list)
        {
            int index = ((ItemsStackPanel)list.ItemsPanelRoot).FirstVisibleIndex;
            return ((IList<T>)list.ItemsSource)[index];
        }

        /// <summary>
        /// Возвращает индекс первого видимого элемента в списке.
        /// </summary>
        /// <param name="list">Элемент управления списка.</param>
        public static int GetFirstVisibleIndex(this ListView list)
        {
            if (list == null) return 0;
            return ((ItemsStackPanel)list.ItemsPanelRoot).FirstVisibleIndex;
        }

        /// <summary>
        /// Возвращает пару из индекса первого видимого элемента и его смещения относительно
        /// границ списка.
        /// </summary>
        /// <param name="list">Элемент управления списка.</param>
        public static Tuple<int, double> GetFirstVisibleIndexAndOffset(this ListView list)
        {
            if (list == null || !(list.ItemsPanelRoot is ItemsStackPanel)) return new Tuple<int, double>(0, 0);
            
            var panel = (ItemsStackPanel)list.ItemsPanelRoot;
            int index = panel.FirstVisibleIndex;
            var element = (FrameworkElement)list.ContainerFromIndex(index);
            var rect = element.GetBoundingRect(list);

            return new Tuple<int, double>(index, rect.Y);
        }

        /// <summary>
        /// Возвращает полосу прокрутки в ListView.
        /// </summary>
        /// <param name="list">Список, для которого нужно вернуть полосу прокрутки.</param>
        public static ScrollBar GetListViewScrollBar(this ListView list)
        {
            var sc = list.GetFirstOrDefaultDescendantOfType<ScrollViewer>();
            var scrollBars = sc.GetDescendantsOfType<ScrollBar>().ToList();
            var sb = scrollBars.FirstOrDefault(x => x.Orientation == Orientation.Vertical);
            return sb;
        }

        /// <summary>
        /// Возвращает ScrollViewer из шаблона ListView.
        /// </summary>
        /// <param name="list">Список, для которого нужно вернуть ScrollViewer.</param>
        public static ScrollViewer GetListViewScrollViewer(this ListView list)
        {
            return list.GetFirstOrDefaultDescendantOfType<ScrollViewer>();
        }
    }
}
