using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.ApplicationModel;

namespace OneVK.Behaviors
{
    /// <summary>
    /// Представляет поведение для <see cref="Pivot"/>. Сохраняет и восстанавливает
    /// позицию в элементе управления.
    /// </summary>
    public class PivotSelectedIndexBehavior : DependencyObject, IBehavior
    {
        private Pivot pivot;
        private bool elementLoaded;

        /// <summary>
        /// Объект, к которому прикреплено поведение.
        /// </summary>
        public DependencyObject AssociatedObject { get; private set; }

        /// <summary>
        /// Прикрепляет поведение к объекту.
        /// </summary>
        /// <param name="associatedObject">Объект, к которому требуется прикрепить поведение.</param>
        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == AssociatedObject || DesignMode.DesignModeEnabled)
                return;
            this.AssociatedObject = associatedObject;
            pivot = AssociatedObject as Pivot;

            if (pivot != null)
            {
                pivot.Loaded += OnLoaded;
                pivot.SelectionChanged += OnSelectedChanged;
            }
        }

        /// <summary>
        /// Вызывается, когда <see cref="Pivot"/> загружен.
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            pivot.Loaded -= OnLoaded;
            elementLoaded = true;
            pivot.SelectedIndex = CurrentIndex;
        }

        /// <summary>
        /// Вызывается при изменении выделенного в данный момент элемента.
        /// </summary>
        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!elementLoaded || CurrentIndex == pivot.SelectedIndex) return;
            CurrentIndex = pivot.SelectedIndex;
        }

        /// <summary>
        /// Открепляет поведение от объекта.
        /// </summary>
        public void Detach()
        {
            this.AssociatedObject = null;

            if (pivot == null) return;

            if (elementLoaded) elementLoaded = false;
            else pivot.Loaded -= OnLoaded;

            pivot.SelectionChanged -= OnSelectedChanged;
            pivot = null;
        }
        
        /// <summary>
        /// Индекс текущего элемента в <see cref="Pivot"/>.
        /// </summary>
        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }
        
        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), 
                typeof(PivotSelectedIndexBehavior), new PropertyMetadata(default(int)));
    }
}
