using System.Collections.ObjectModel;
using OneVK.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneVK.Core.Player;
using OneVK.Enums.App;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using OneVK.ViewModel.Messages;
using System.Collections.Generic;

namespace OneVK.Controls
{
    /// <summary>
    /// Отображает список вложений в виде списка.
    /// Поддерживает документы и ссылки.
    /// </summary>
    [TemplatePart(Name = ItemsContainerName, Type = typeof(ListViewBase))]
    public class ListItemsPresenter : Control
    {
        private const string ItemsContainerName = "ItemsContainer";
        private object track;
        private ListViewBase _itemsContainer;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ListItemsPresenter()
        {
            this.DefaultStyleKey = typeof(ListItemsPresenter);
            this.Items = new ObservableCollection<object>();
        }

        #region Свойства
        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        public ObservableCollection<object> Items { get; private set; }
        #endregion

        /// <summary>
        /// Вызывается при построении шаблона элемента.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsContainer = GetTemplateChild(ItemsContainerName) as ListViewBase;

            _itemsContainer.ItemClick += (s, e) =>
            {
                ServiceHelper.CoreService.ExecuteItem(e.ClickedItem);
            };
            this.Tapped += (s, e) => e.Handled = true;
        }
    }
}
