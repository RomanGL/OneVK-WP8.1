using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Group;
using OneVK.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет аватар пользователя в чате.
    /// </summary>
    public class UserChatAvatar : Control
    {
        public UserChatAvatar()
        {
            this.DefaultStyleKey = typeof(UserChatAvatar);
        }

        /// <summary>
        /// Профиль пользователя.
        /// </summary>
        public object User
        {
            get { return (object)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(object), 
                typeof(UserChatAvatar), new PropertyMetadata(default(object)));

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var user = User as VKProfileShort;
            if (user != null)
                VisualStateManager.GoToState(this, user.Online == VKBoolean.True ? "Online" : "Offline", true);

            this.Tapped += (s, e) =>
            {
                if (User is VKProfileShort)
                {
                    NavigationHelper.Navigate(AppViews.ProfileView, ((VKProfileShort)User).ID);
                }
                else
                {
                    var group = User as VKGroupBase;
                    if (group != null)
                        NavigationHelper.Navigate(AppViews.GroupInfoView, group.ID);
                }
            };
        }
    }
}
