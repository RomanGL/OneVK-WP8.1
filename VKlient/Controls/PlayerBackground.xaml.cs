using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using OneVK.Controls.Common;
using Windows.UI;
using Windows.Foundation;
using OneVK.Model.Common;
using GalaSoft.MvvmLight;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет анимированый фон аудиопроигрывателя.
    /// </summary>
    public partial class PlayerBackground : UserControl
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public PlayerBackground()
        {
            InitializeComponent();
        }

        #region События
        /// <summary>
        /// Происходит при изменении темы проигрывателя.
        /// </summary>
        //public event EventHandler<Color> ThemeChanged;
        #endregion

        #region Указатели свойств для анимаций
        private const string OpacityPropertyPath = "(UIElement.Opacity)";
        private const string FillProperty = "(Shape.Fill).(SolidColorBrush.Color)";        
        
        //private const int SubstrateColorCount = 4;
        //private readonly Random RandomFactory = new Random(Environment.TickCount);
                
        //private static readonly Color[] SubstracteColors = new Color[]
        //{
        //    new Color { A = 255, R = 255, G = 0, B = 255 },
        //    new Color { A = 255, R = 0, G = 150, B = 0 },
        //    new Color { A = 255, R = 255, G = 100, B = 0 },
        //    new Color { A = 255, R = 0, G = 100, B = 255 }
        //};

        //private Theme _backgroundTheme;
        #endregion

        #region Свойства
        //public Theme BackgroundTheme
        //{
        //    get { return _backgroundTheme; }
        //    private set
        //    {
        //        _backgroundTheme = value;
        //        ChangeSubstracteColor(SubstracteColors[(byte)value - 1]);
        //        OnThemeChanged(SubstracteColors[(byte)value - 1]);
        //    }
        //}
        #endregion

        #region Свойства зависимостей
        
        #region ArtistImage DependencyProperty
        /// <summary>
        /// Изображение исполнителя.
        /// </summary>
        public ImageSource ArtistImage
        {
            get { return (ImageSource)GetValue(ArtistImageProperty); }
            set { SetValue(ArtistImageProperty, value); }
        }

        public static readonly DependencyProperty ArtistImageProperty =
            DependencyProperty.Register("ArtistImage", typeof(ImageSource), typeof(PlayerBackground), new PropertyMetadata(default(ImageSource), OnArtistImageChanged));

        /// <summary>
        /// Вызывается при изменении изображения исполнителя.
        /// </summary>
        private static void OnArtistImageChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = (PlayerBackground)obj;

            //control.OldImage.Source = control.NewImage.Source;
            //control.NewImage.Source = (ImageSource)e.NewValue;
            control.OldImage.Fill = control.NewImage.Fill;
            control.NewImage.Fill = new ImageBrush 
            { 
                ImageSource = (ImageSource)e.NewValue, 
                AlignmentX = AlignmentX.Center, 
                AlignmentY = AlignmentY.Center, 
                Stretch = Stretch.UniformToFill 
            };

            if (e.OldValue != null)            
                control.AnimateOut.Begin();
            if (e.NewValue != null)
                control.AnimateIn.Begin();
            //control.NextTheme();
        }
        #endregion

        #endregion

        
        /// <summary>
        /// Запускается отсчет времени для обновления экрана.
        /// </summary>
        public void Start()
        {
            //NextTheme();
        }

        /// <summary>
        /// Останавливает отсчет времени для обновления экрана.
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Сменяет тему проигрывателя.
        /// </summary>
        //public void NextTheme()
        //{
        //    Theme theme = BackgroundTheme;
        //    while (theme == BackgroundTheme)
        //        theme = (Theme)RandomFactory.Next(1, 5);            
        //    BackgroundTheme = theme;
        //}

        #region Обработчики событий
        /// <summary>
        /// Вызывает событие изменения темы.
        /// </summary>
        /// <param name="newColor">Новый акцентный цвет темы.</param>
        //private void OnThemeChanged(Color newColor)
        //{
        //    if (ThemeChanged != null)
        //        ThemeChanged(this, newColor);
        //}
        #endregion

        /// <summary>
        /// Анимировано изменяет цвет подложки.
        /// </summary>
        /// <param name="fillColor">Новый цвет подложки.</param>
        private void ChangeSubstracteColor(Color fillColor)
        {
            var storyboard = new Storyboard();
            storyboard.Children.Add(CreateChangeColorTimeline(ColorSubstrate, fillColor, 3));
            storyboard.Begin();
        }

        /// <summary>
        /// Возвращает анимацию изменения фонового цвета элемента.
        /// </summary>
        /// <param name="element">Элемент, к которому применяется анимация.</param>
        /// <param name="fillColor">Цвет, на который требуется изменить цвет.</param>
        /// <param name="duration">ДЛительность анимации.</param>
        private static Timeline CreateChangeColorTimeline(UIElement element, Color fillColor, double duration)
        {
            var timeline = new ColorAnimation();
            timeline.Duration = TimeSpan.FromSeconds(duration);
            timeline.To = fillColor;

            StoryboardServices.SetTarget(timeline, element);
            Storyboard.SetTargetProperty(timeline, FillProperty);

            return timeline;
        }

        /// <summary>
        /// Перечисление доступных тем.
        /// </summary>
        public enum Theme : byte
        {
            None = 0,
            Magenta,
            Green,
            Orange,
            Blue
        }
    }
}
