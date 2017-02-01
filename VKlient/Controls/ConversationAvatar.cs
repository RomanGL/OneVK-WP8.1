using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using OneVK.Core.Messages;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет аватар беседы.
    /// </summary>
    public class ConversationAvatar : Control
    {
        private const string ELLIPSE_NAME = "Ellipse";
        private const string LEFT_TOP_PIE_NAME = "LeftTopPie";
        private const string RIGHT_TOP_PIE_NAME = "RightTopPie";
        private const string LEFT_BOTTOM_PIE_NAME = "LeftBottomPie";
        private const string RIGHT_BOTTOM_PIE_NAME = "RightBottomPie";
        private const string LEFT_SEMICIRCLE_NAME = "LeftSemicircle";
        private const string RIGHT_SEMICIRCLE_NAME = "RightSemicircle";
        private const string TOP_SEMICIRCLE_NAME = "TopSemicircle";
        private const string BOTTOM_SEMICIRCLE_NAME = "BottomSemicircle";

        private static Random random = new Random(Environment.TickCount);

        private bool loaded;
        private Shape leftTopPie;
        private Shape rightTopPie;
        private Shape leftBottomPie;
        private Shape rightBottomPie;
        private Shape leftSemicircle;
        private Shape rightSemicircle;
        private Shape topSemicircle;
        private Shape bottomSemicircle;
        private Shape ellipse;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConversationAvatar"/>.
        /// </summary>
        public ConversationAvatar()
        {
            this.DefaultStyleKey = typeof(ConversationAvatar);
        }

        /// <summary>
        /// Коллекция элементов аватара.
        /// </summary>
        public object Conversation
        {
            get { return (object)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Conversation", typeof(object), 
                typeof(ConversationAvatar), new PropertyMetadata(default(object), OnConversationChanged));

        /// <summary>
        /// Вызывается при изменении коллекции элементов аватара.
        /// </summary>
        private static void OnConversationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!((ConversationAvatar)obj).loaded) return;
            ((ConversationAvatar)obj).Calculate();
        }

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            leftTopPie = GetTemplateChild(LEFT_TOP_PIE_NAME) as Shape;
            rightTopPie = GetTemplateChild(RIGHT_TOP_PIE_NAME) as Shape;
            leftBottomPie = GetTemplateChild(LEFT_BOTTOM_PIE_NAME) as Shape;
            rightBottomPie = GetTemplateChild(RIGHT_BOTTOM_PIE_NAME) as Shape;
            leftSemicircle = GetTemplateChild(LEFT_SEMICIRCLE_NAME) as Shape;
            rightSemicircle = GetTemplateChild(RIGHT_SEMICIRCLE_NAME) as Shape;
            topSemicircle = GetTemplateChild(TOP_SEMICIRCLE_NAME) as Shape;
            bottomSemicircle = GetTemplateChild(BOTTOM_SEMICIRCLE_NAME) as Shape;
            ellipse = GetTemplateChild(ELLIPSE_NAME) as Shape;

            loaded = true;
            Calculate();
        }

        /// <summary>
        /// Выполняет рассчеты и обновляет аватар.
        /// </summary>
        private void Calculate()
        {
            ClearBackgrounds();

            if (Conversation is IChat)
            {
                List<string> items = ((IChat)Conversation).Users.Take(4).Select(u => u.Photo50).ToList();
                if (items.Count == 1) CreateOne(items);
                else if (items.Count == 2) CreateTwo(items);
                else if (items.Count == 3) CreateThree(items);
                else CreateFour(items);
            }
            else if (Conversation is IDialog)
            {
                CreateOne(new List<string> { ((IDialog)Conversation).User.Photo50 });
            } 
            else if (Conversation is IList<string>)
            {
                IList<string> items = ((IList<string>)Conversation);
                if (items.Count == 1) CreateOne(items);
                else if (items.Count == 2) CreateTwo(items);
                else if (items.Count == 3) CreateThree(items);
                else CreateFour(items);
            }           
        }

        /// <summary>
        /// Создает фон для одного элемента.
        /// </summary>
        private void CreateOne(IList<string> items)
        {
            ellipse.Fill = GetBrush(items[0]);
        }

        /// <summary>
        /// Создает фон для двух элементов.
        /// </summary>
        private void CreateTwo(IList<string> items)
        {
            int seed = random.Next(0, 2);
            switch (seed)
            {
                case 0:
                    leftSemicircle.Fill = GetBrush(items[0]);
                    rightSemicircle.Fill = GetBrush(items[1]);
                    break;
                case 1:
                    topSemicircle.Fill = GetBrush(items[0]);
                    bottomSemicircle.Fill = GetBrush(items[1]);
                    break;
            }
        }

        /// <summary>
        /// Создает фон для трех элементов.
        /// </summary>
        private void CreateThree(IList<string> items)
        {
            int seed = random.Next(0, 4);
            switch (seed)
            {
                case 0:
                    leftSemicircle.Fill = GetBrush(items[0]);
                    rightTopPie.Fill = GetBrush(items[1]);
                    rightBottomPie.Fill = GetBrush(items[2]);
                    break;
                case 1:
                    leftTopPie.Fill = GetBrush(items[0]);
                    leftBottomPie.Fill = GetBrush(items[1]);
                    rightSemicircle.Fill = GetBrush(items[2]);
                    break;
                case 2:
                    topSemicircle.Fill = GetBrush(items[0]);
                    leftBottomPie.Fill = GetBrush(items[1]);
                    rightBottomPie.Fill = GetBrush(items[2]);
                    break;
                case 3:
                    leftTopPie.Fill = GetBrush(items[0]);
                    rightTopPie.Fill = GetBrush(items[1]);
                    bottomSemicircle.Fill = GetBrush(items[2]);
                    break;
            }
        }

        /// <summary>
        /// Создает фон для четырех элементов.
        /// </summary>
        private void CreateFour(IList<string> items)
        {
            leftTopPie.Fill = GetBrush(items[0]);
            rightTopPie.Fill = GetBrush(items[1]);
            leftBottomPie.Fill = GetBrush(items[2]);
            rightBottomPie.Fill = GetBrush(items[3]);
        }

        /// <summary>
        /// Очищает фон всех элементов.
        /// </summary>
        private void ClearBackgrounds()
        {
            leftTopPie.Fill = null;
            rightTopPie.Fill = null;
            leftBottomPie.Fill = null;
            rightBottomPie.Fill = null;

            leftSemicircle.Fill = null;
            rightSemicircle.Fill = null;
            topSemicircle.Fill = null;
            bottomSemicircle.Fill = null;

            ellipse.Fill = null;
        }

        /// <summary>
        /// Возвращает кисть картинки.
        /// </summary>
        /// <param name="imagePath">Путь до изображения.</param>
        private static Brush GetBrush(string imagePath)
        {
            return new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(imagePath)),
                Stretch = Stretch.UniformToFill
            };
        }
    }
}
