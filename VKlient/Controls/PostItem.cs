using OneVK.Model.Newsfeed;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Enums.App;
using OneVK.Helpers;
using System;
using OneVK.Model.Wall;
using Newtonsoft.Json;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет элемент поста.
    /// </summary>
    public sealed class PostItem : Control
    {
        private UIElement _ownerPanel;

        public PostItem()
        {
            this.DefaultStyleKey = typeof(PostItem);         
        }

        /// <summary>
        /// Данные поста.
        /// </summary>
        public object Post
        {
            get { return (object)GetValue(PostProperty); }
            set { SetValue(PostProperty, value); }
        }

        public static readonly DependencyProperty PostProperty =
            DependencyProperty.Register("Post", typeof(object), 
            typeof(PostItem), new PropertyMetadata(default(object)));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _ownerPanel = GetTemplateChild("OwnerPanel") as UIElement;

            this.Tapped += (s, e) => NavigationHelper.Navigate(AppViews.PostView, Post);
            _ownerPanel.Tapped += (s, e) =>
            {
                long ownerID = 0;

                if (Post is VKWallPost)
                    ownerID = ((VKWallPost)Post).FromID;
                else if (Post is VKNewsfeedPost)
                    ownerID = ((VKNewsfeedPost)Post).OwnerID;

                if (ownerID > 0)
                    NavigationHelper.Navigate(AppViews.ProfileView, (ulong)ownerID);
                else
                    NavigationHelper.Navigate(AppViews.GroupInfoView, (ulong)-ownerID);
                e.Handled = true;
            };
        }
    }
}
