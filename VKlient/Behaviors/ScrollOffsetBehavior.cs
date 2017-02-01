using Microsoft.Xaml.Interactivity;
using OneVK.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Behaviors
{
    /// <summary>
    /// Represents a scroll offset behavior for <see cref="ScrollViewer"/>,
    /// <see cref="ListView"/> and <see cref="GridView"/>.
    /// </summary>
    public class ScrollOffsetBehavior : DependencyObject, IBehavior
    {        
        private ScrollViewer element;
        private bool elementLoaded;
        private bool isWorking;

        #region IBehavior members

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        public DependencyObject AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches to the specified object.
        /// </summary>
        /// <param name="associatedObject">The <see cref="DependencyObject"/> to
        /// which the behavior will be attached.</param>
        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == this.AssociatedObject || DesignMode.DesignModeEnabled)
                return;
            this.AssociatedObject = associatedObject;

            if (this.AssociatedObject is FrameworkElement)
                ((FrameworkElement)this.AssociatedObject).Loaded += OnAssociatedObjectLoaded;            
        }

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)this.AssociatedObject).Loaded -= OnAssociatedObjectLoaded;

            if (this.AssociatedObject is ListViewBase)
                this.element = ((ListViewBase)this.AssociatedObject).GetFirstOrDefaultDescendantOfType<ScrollViewer>();
            else if (this.AssociatedObject is ScrollViewer)
            {
                this.element = (ScrollViewer)this.AssociatedObject;
                this.elementLoaded = true;
                this.element.ChangeView(this.HorizontalOffset, this.VerticalOffset, null, true);
                this.element.ViewChanging += this.OnElementViewChanging;
                return;
            }

            if (this.element == null)
                return;

            this.element.Loaded += this.OnElementLoaded;
            this.element.ViewChanging += this.OnElementViewChanging;
        }

        /// <summary>
        /// Detaches this instance from it's associated object.
        /// </summary>
        public void Detach()
        {
            this.AssociatedObject = null;

            if (this.element == null) return;
            if (this.elementLoaded)
                this.elementLoaded = false;
            else
                this.element.Loaded -= this.OnElementLoaded;

            this.element.ViewChanging -= this.OnElementViewChanging;
            this.element = null;
        }

        #endregion

        /// <summary>
        /// Invoked when <see cref="ScrollViewer"/> element loaded.
        /// </summary>
        private void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            this.element.Loaded -= this.OnElementLoaded;
            this.elementLoaded = true;

            if (this.FirstVisibleIndex != 0 && this.AssociatedObject is ListView)
            {
                this.isWorking = true;
                var list = (ListView)this.AssociatedObject;
                this.element.ViewChanged += OnLoadedViewChanged;
                list.ScrollIntoView(((IList)list.ItemsSource)[FirstVisibleIndex], ScrollIntoViewAlignment.Leading);
            }
            else
                this.element.ChangeView(this.HorizontalOffset, this.VerticalOffset, null, true);
        }

        private void OnLoadedViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            this.element.ViewChanged -= OnLoadedViewChanged;
            double result = this.element.VerticalOffset - this.VerticalOffset;
            if (result != this.element.VerticalOffset)
                this.element.ChangeView(this.HorizontalOffset, this.element.VerticalOffset - this.VerticalOffset, null, true);
            isWorking = false;
        }

        /// <summary>
        /// Invoked when <see cref="ScrollViewer"/> element view changing.
        /// </summary>
        private void OnElementViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            if (this.isWorking) return;

            this.HorizontalOffset = e.NextView.HorizontalOffset;

            if (UseIndexMethod && this.AssociatedObject is ListView)
            {
                var list = (ListView)this.AssociatedObject;
                var data = list.GetFirstVisibleIndexAndOffset();
                this.FirstVisibleIndex = data.Item1;
                this.VerticalOffset = data.Item2;
            }
            else
                this.VerticalOffset = e.NextView.VerticalOffset;
        }

        #region Dependency properties

        /// <summary>
        /// Gets or sets a value use index method.
        /// </summary>
        public bool UseIndexMethod
        {
            get { return (bool)GetValue(UseIndexMethodProperty); }
            set { SetValue(UseIndexMethodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseIndexMethod.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseIndexMethodProperty =
            DependencyProperty.Register("UseIndexMethod", typeof(bool), typeof(ScrollOffsetBehavior), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Gets or sets index of first visible item;
        /// </summary>
        public int FirstVisibleIndex
        {
            get { return (int)GetValue(FirstVisibleIndexProperty); }
            set { SetValue(FirstVisibleIndexProperty, value); }
        }
        
        public static readonly DependencyProperty FirstVisibleIndexProperty =
            DependencyProperty.Register("FirstVisibleIndex", typeof(int), 
                typeof(ScrollOffsetBehavior), new PropertyMetadata(default(int)));

        /// <summary>
        /// Gets or sets vertical scroll offset.
        /// </summary>
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), 
                typeof(ScrollOffsetBehavior), new PropertyMetadata(default(double), OnVerticalOffsetChanged));

        /// <summary>
        /// Gets or sets horizontal scroll offset.
        /// </summary>
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), 
                typeof(ScrollOffsetBehavior), new PropertyMetadata(default(double), OnHorizontalOffsetChanged));

        /// <summary>
        /// Invoked when vertical offset property changed.
        /// </summary>
        private static void OnVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var behavior = obj as ScrollOffsetBehavior;
            if (!behavior.elementLoaded) return;

            //behavior.element.ChangeView(null, (double)e.NewValue, null);
        }

        /// <summary>
        /// Invoked when horizontal offset property changed.
        /// </summary>
        private static void OnHorizontalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var behavior = obj as ScrollOffsetBehavior;
            if (!behavior.elementLoaded) return;

            //behavior.element.ChangeView((double)e.NewValue, null, null);
        }

        #endregion
    }
}
