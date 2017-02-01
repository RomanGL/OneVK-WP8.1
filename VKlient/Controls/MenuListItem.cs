using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет элемент списка меню.
    /// </summary>
    public class MenuListItem : ListViewItem
    {
        private const string CounterElementName = "CounterElement";
        private UIElement _counterElement;

        public MenuListItem()
        {
            this.DefaultStyleKey = typeof(MenuListItem);
        }

        /// <summary>
        /// Счетчик.
        /// </summary>
        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Counter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(MenuListItem), new PropertyMetadata(default(int)));
        

        /// <summary>
        /// Команад нажатия на счетчик.
        /// </summary>
        public ICommand CounterTapCommand
        {
            get { return (ICommand)GetValue(CounterTapCommandProperty); }
            set { SetValue(CounterTapCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CounterTapCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CounterTapCommandProperty =
            DependencyProperty.Register("CounterTapCommand", typeof(ICommand), typeof(MenuListItem), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _counterElement = GetTemplateChild(CounterElementName) as UIElement;
            _counterElement.Tapped += (s, e) =>
            {
                if (CounterTapCommand != null)
                    CounterTapCommand.Execute(null);
                e.Handled = true;
            };
        }
    }
}
