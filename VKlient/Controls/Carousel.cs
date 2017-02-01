using OneVK.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using OneVK.Helpers;

namespace OneVK.Controls
{
    public class Carousel : Control
    {
        public Carousel()
        {
            this.DefaultStyleKey = typeof(Carousel);
        }
        
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(Carousel), new PropertyMetadata(default(object)));
        
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(Carousel), new PropertyMetadata(default(DataTemplate)));

        private ISwiper Swiper
        {
            get { return ItemsSource as ISwiper; }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = GetTemplateChild("RootCanvas") as Canvas;
            _canvas.Loaded += Canvas_Loaded;
            _canvas.ManipulationDelta += Canvas_ManipulationDelta;
            _canvas.ManipulationCompleted += Canvas_ManipulationCompleted;
            _canvas.SizeChanged += Canvas_SizeChanged;
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_isInitialized) return;

            SetItemsSize();
            ArrangeItems();
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItems();
            SetItemsSize();
            AddItemsToCanvas();
            ArrangeItems();

            _currentInScreen = InScreen.Second;
            _isInitialized = true;
        }

        private void Canvas_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (Math.Abs(e.Cumulative.Translation.X) >= this.ActualWidth)
            {
                e.Complete();
                return;
            }

            if (_firstItem != null)
            {
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX += e.Delta.Translation.X;
            }

            if (_secondItem != null)
            {
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX += e.Delta.Translation.X;
            }

            if (_thirdItem != null)
            {
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX += e.Delta.Translation.X;
            }
        }

        private void Canvas_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            UIElement screenItem = null;
            switch (_currentInScreen)
            {
                case InScreen.First:
                    screenItem = _firstItem;
                    break;
                case InScreen.Second:
                    screenItem = _secondItem;
                    break;
                case InScreen.Third:
                    screenItem = _thirdItem;
                    break;
            }

            if (e.Cumulative.Translation.X <= -(this.ActualWidth / 2) + 100)
            {
                GoForward();
            }
            else if (e.Cumulative.Translation.X - 50 >= (this.ActualWidth / 2) - 100)
            {
                GoBackward();
            }
            else
            {
                GoCurrent();
            }

            GetFinalStoryboard().Begin();
        }

        private void GoForward()
        {
            if (_currentInScreen == InScreen.First)
            {
                _currentInScreen = InScreen.Second;
                double translateX = _firstItem.GetBoundingRect(_canvas).Right;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }
            else if (_currentInScreen == InScreen.Second)
            {
                _currentInScreen = InScreen.Third;

                double translateX = _secondItem.GetBoundingRect(_canvas).Right;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }
            else if (_currentInScreen == InScreen.Third)
            {
                _currentInScreen = InScreen.First;

                double translateX = _thirdItem.GetBoundingRect(_canvas).Right;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }

            Swiper?.GoForward();
            ArrangeItems();
            LoadItems();
        }

        private void GoBackward()
        { 
            if (_currentInScreen == InScreen.First)
            {
                _currentInScreen = InScreen.Third;
                
                double translateX = _thirdItem.GetBoundingRect(_canvas).Left;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }
            else if (_currentInScreen == InScreen.Second)
            {
                _currentInScreen = InScreen.First;

                double translateX = _firstItem.GetBoundingRect(_canvas).Left;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }
            else if (_currentInScreen == InScreen.Third)
            {
                _currentInScreen = InScreen.Second;

                double translateX = _secondItem.GetBoundingRect(_canvas).Left;
                ((CompositeTransform)_firstItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_secondItem.RenderTransform).TranslateX = translateX;
                ((CompositeTransform)_thirdItem.RenderTransform).TranslateX = translateX;
            }

            Swiper?.GoBackward();
            ArrangeItems();
            LoadItems();
        }

        private void GoCurrent()
        {
            
        }

        private void LoadItems()
        {
            if (_firstItem == null)
            {
                _firstItem = new Border();
                _firstItem.RenderTransform = new CompositeTransform();
                _firstItem.Child = ItemTemplate?.LoadContent() as UIElement;
            }

            if (_secondItem == null)
            {
                _secondItem = new Border();
                _secondItem.Child = ItemTemplate?.LoadContent() as UIElement;
                _secondItem.RenderTransform = new CompositeTransform();
            }

            if (_thirdItem == null)
            {
                _thirdItem = new Border();
                _thirdItem.Child = ItemTemplate?.LoadContent() as UIElement;
                _thirdItem.RenderTransform = new CompositeTransform();
            }

            switch (_currentInScreen)
            {
                case InScreen.First:
                    _thirdItem.DataContext = Swiper?.GetBackward();
                    _firstItem.DataContext = Swiper?.GetCurrent();
                    _secondItem.DataContext = Swiper?.GetForward();
                    break;
                case InScreen.Second:
                    _firstItem.DataContext = Swiper?.GetBackward();
                    _secondItem.DataContext = Swiper?.GetCurrent();
                    _thirdItem.DataContext = Swiper?.GetForward();
                    break;
                case InScreen.Third:
                    _secondItem.DataContext = Swiper?.GetBackward();
                    _thirdItem.DataContext = Swiper?.GetCurrent();
                    _firstItem.DataContext = Swiper?.GetForward();
                    break;
            }
        }

        private void SetItemsSize()
        {
            if (_firstItem != null)
            {
                _firstItem.Width = this.ActualWidth;
                _firstItem.Height = this.ActualHeight;
            }

            if (_secondItem != null)
            {
                _secondItem.Width = this.ActualWidth;
                _secondItem.Height = this.ActualHeight;
            }

            if (_thirdItem != null)
            {
                _thirdItem.Width = this.ActualWidth;
                _thirdItem.Height = this.ActualHeight;
            }
        }

        private void AddItemsToCanvas()
        {
            if (_firstItem != null) _canvas.Children.Add(_firstItem);
            if (_secondItem != null) _canvas.Children.Add(_secondItem);
            if (_thirdItem != null) _canvas.Children.Add(_thirdItem);
        }

        private void ArrangeItems()
        {
            switch (_currentInScreen)
            {
                case InScreen.First:
                    Canvas.SetLeft(_thirdItem, -this.ActualWidth);
                    Canvas.SetLeft(_firstItem, 0);
                    Canvas.SetLeft(_secondItem, this.ActualWidth);
                    break;
                case InScreen.Second:
                    Canvas.SetLeft(_firstItem, -this.ActualWidth);
                    Canvas.SetLeft(_secondItem, 0);
                    Canvas.SetLeft(_thirdItem, this.ActualWidth);
                    break;
                case InScreen.Third:
                    Canvas.SetLeft(_secondItem, -this.ActualWidth);
                    Canvas.SetLeft(_thirdItem, 0);
                    Canvas.SetLeft(_firstItem, this.ActualWidth);
                    break;
            }
        }

        private Storyboard GetFinalStoryboard()
        {
            var sb = new Storyboard();

            var fAnim = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 2 }
            };
            Storyboard.SetTarget(fAnim, _firstItem);
            Storyboard.SetTargetProperty(fAnim, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            var sAnim = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 2 }
            };
            Storyboard.SetTarget(sAnim, _secondItem);
            Storyboard.SetTargetProperty(sAnim, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            var tAnim = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut, Exponent = 2 }
            };
            Storyboard.SetTarget(tAnim, _thirdItem);
            Storyboard.SetTargetProperty(tAnim, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            sb.Children.Add(fAnim);
            sb.Children.Add(sAnim);
            sb.Children.Add(tAnim);

            return sb;
        }

        private Canvas _canvas;
        private Border _firstItem;
        private Border _secondItem;
        private Border _thirdItem;
        private bool _isInitialized;
        private InScreen _currentInScreen;

        private enum InScreen : byte
        {
            First,
            Second,
            Third
        }
    }
}
