using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OneVK.Enums.App;
using OneVK.Model.Photo;
using OneVK.ViewModel.Messages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OneVK.Helpers;
using OneVK.Core;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using System;
using System.Linq;
using OneVK.Model.Video;
using Newtonsoft.Json;
using OneVK.Model.Common;

namespace OneVK.Controls
{
    /// <summary>
    /// Является элементом для визуализации списка медиаэлементов.
    /// </summary>
    [TemplatePart(Name = ContentRootName, Type = typeof(Canvas))]
    public class MediaItemsPresenter : Control
    {
        private const string ContentRootName = "ContentRoot";
        private const double ThumbnailsMargin = 4;

        private Canvas _contentRoot;

        /// <summary>
        /// Инициализирует новый экземрляр класса.
        /// </summary>
        public MediaItemsPresenter()
        {
            this.DefaultStyleKey = typeof(MediaItemsPresenter);
            this.Items = new ObservableCollection<IThumbnailSupport>();
        }      

        #region Свойства
        /// <summary>
        /// Коллекция медиаэлементов.
        /// </summary>
        public ObservableCollection<IThumbnailSupport> Items { get; private set; }
        /// <summary>
        /// Максимальный размер прямоугольной области, которую может занять сетка с элементами.
        /// </summary>
        public Rectangle MaxRectSize { get; set; }
        /// <summary>
        /// Стикер.
        /// </summary>
        public VKSticker Sticker { get; set; }
        #endregion

        /// <summary>
        /// Вызывается при построении шаблона элемента.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentRoot = GetTemplateChild(ContentRootName) as Canvas;
            Update();

            //_itemsContainer = GetTemplateChild(ItemsContainerName) as ListViewBase;
            //_itemsContainer.ItemClick += (s, e) => ServiceHelper.CoreService.ExecuteItem(e.ClickedItem);
            //this.Tapped += (s, e) => e.Handled = true;

            for (int i = 0; i < _contentRoot.Children.Count; i++)
                _contentRoot.Children[i].Tapped += (s, e) =>
                {
                    e.Handled = true;
                    var tc = s as ThumbnailControl;
                    if (tc == null) return;
                    var thumb = tc.Thumbnail;
                    if (thumb is VKVideoBase)
                    {
                        ServiceHelper.CoreService.ExecuteItem(thumb);
                        return;
                    }

                    var photos = Items.Where(t => t is VKPhoto).Cast<VKPhoto>().ToList();
                    int index = photos.IndexOf((VKPhoto)thumb);
                    var parameter = JsonConvert.SerializeObject(new Tuple<List<VKPhoto>, int>(photos, index));
                    NavigationHelper.Navigate(AppViews.ImageView, parameter);                    
                };

            //((FrameworkElement)Window.Current.Content).SizeChanged += (s, e) => Update();
        }

        /// <summary>
        /// Обновить элемент управления.
        /// </summary>
        public void Update()
        {          
            if (MaxRectSize.Height == 0 && MaxRectSize.Width == 0)
                MaxRectSize = new Rectangle(300, 300);

            _contentRoot.Children.Clear();

            if (Sticker != null)
            {
                var img = new Image
                {
                    Source = new BitmapImage(new Uri(Sticker.Photo128)),
                    Height = 128,
                };
                Canvas.SetLeft(img, 20);
                _contentRoot.Children.Add(img);
                this.Height = 105;
                return;
            }

            ThumbnailsLayoutHelper.CalculateThumbnailSizes(MaxRectSize, Items, ThumbnailsMargin);

            double currentWidth = 0;
            double currentHeight = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                var size = Items[i].ThumbnailSize;
                var item = new ThumbnailControl 
                { 
                    Thumbnail = Items[i],
                    Width = size.Width,
                    Height = size.Height
                };
                Canvas.SetLeft(item, currentWidth);
                Canvas.SetTop(item, currentHeight);
                _contentRoot.Children.Add(item);

                if (size.LastRow)
                {
                    currentHeight += (size.Height + ThumbnailsMargin);
                    if (i == Items.Count - 1)
                        currentWidth += (size.Width + ThumbnailsMargin);
                }
                else
                    currentWidth += (size.Width + ThumbnailsMargin);

                if (size.LastColumn)
                {
                    currentHeight += (size.Height + ThumbnailsMargin);
                    currentWidth = 0;
                }

                //if (i == Items.Count - 1 && currentHeight == 0)
                //    currentHeight += size.Height;                  
            }

            currentHeight = 0;
            currentWidth = 0;

            for (int i = 0; i < this._contentRoot.Children.Count; i++)
            {
                var child = this._contentRoot.Children[i] as FrameworkElement;
                if (child == null) continue;

                double h = Canvas.GetTop(child) + child.Height;
                double w = Canvas.GetLeft(child) + child.Width;
                if (h > currentHeight) currentHeight = h;
                if (w > currentWidth) currentWidth = w;
            }

            this.Height = currentHeight;
            this.Width = currentWidth;
        }
    }
}
