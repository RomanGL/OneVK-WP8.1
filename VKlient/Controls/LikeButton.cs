using GalaSoft.MvvmLight;
using OneVK.Core.Xaml;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model;
using OneVK.Model.Common;
using OneVK.Request;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using OneVK.Controls.Common;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет кнопку "Мне нравится".
    /// </summary>
    public class LikeButton : BaseSocActionButton, IOneCommandBarElement
    {
        public LikeButton()
        {
            this.DefaultStyleKey = typeof(LikeButton);
        }

        #region Свойства зависимостей

        #region Like DependencyProperty
        /// <summary>
        /// Объект с информацией о лайках.
        /// </summary>
        public VKLikes Like
        {
            get { return (VKLikes)GetValue(LikeProperty); }
            set { SetValue(LikeProperty, value); }
        }

        public static readonly DependencyProperty LikeProperty =
            DependencyProperty.Register("Like", typeof(VKLikes), 
            typeof(LikeButton), new PropertyMetadata(default(VKLikes), OnLikeChanged));
        
        /// <summary>
        /// Вызывается при изменении отметки "Мне нравится".
        /// </summary>
        private static void OnLikeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var item = (LikeButton)obj;
            item.Update();
        }
        #endregion

        #region LikeTarget DependencyProperty
        /// <summary>
        /// Объект-цели для отметки "Мне нравится".
        /// </summary>
        public object LikeTarget
        {
            get { return (object)GetValue(LikeTargetProperty); }
            set { SetValue(LikeTargetProperty, value); }
        }

        public static readonly DependencyProperty LikeTargetProperty =
            DependencyProperty.Register("LikeTarget", typeof(object), 
            typeof(LikeButton), new PropertyMetadata(default(object)));                
        #endregion

        public string Label { get; set; }

        #endregion

        /// <summary>
        /// Вызывается при построении шаблона элемента.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();                  

            this.Tapped += async (s, e) =>
                {
                    var target = (IVKLikeTarget)LikeTarget;
                    e.Handled = true;
                    if (IsActive)
                    {
                        IsActive = false;
                        Like.Count--;
                        Like.UserLikes = VKBoolean.False;
                        
                        var response = await (new DeleteLikeRequest
                        {
                            OwnerID = target.LikesOwner,
                            TargetType = target.TargetType,
                            ItemID = target.LikesItem
                        }).ExecuteAsync();
                        if (response.Error.ErrorType == VKErrors.None)
                        {
                            Like = new VKLikes
                            {
                                CanLike = VKBoolean.True,
                                CanPublish = VKBoolean.True,
                                UserLikes = VKBoolean.False,
                                Count = response.Response.LikesCount
                            };
                        }                       
                    }
                    else
                    {
                        IsActive = true;
                        Like.Count++;
                        Like.UserLikes = VKBoolean.True;

                        var response = await (new AddLikeRequest
                        {
                            OwnerID = target.LikesOwner,
                            TargetType = target.TargetType,
                            ItemID = target.LikesItem
                        }).ExecuteAsync();
                        if (response.Error.ErrorType == VKErrors.None)
                        {
                            Like = new VKLikes
                            {
                                CanLike = VKBoolean.True,
                                CanPublish = VKBoolean.True,
                                UserLikes = VKBoolean.True,
                                Count = response.Response.LikesCount
                            };
                        }                                   
                    }                    
                };
            //Update();
        }

        /// <summary>
        /// Обновляет все параметры и состояния элемента.
        /// </summary>
        private void Update()
        {
            if (Like == null)
            {
#if DEBUG
                if (ViewModelBase.IsInDesignModeStatic)
                {
                    this.Visibility = Visibility.Visible;
                    return;
                }
#endif
                this.Visibility = Visibility.Collapsed;
                return;
            }
            else this.Visibility = Visibility.Visible;

            if (Like.UserLikes == VKBoolean.True)
                this.IsActive = true;
            else
                this.IsActive = false;
        }
    }
}
