using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OneVK.Model.Newsfeed;
using OneVK.Helpers;
using OneVK.ViewModel;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight;
using OneVK.Model.Wall;

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PostView : Page
    {
        public PostView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var post = (BaseVKPost)e.Parameter;
            string uniqueKey = CoreHelper.GetPostViewModelKey(post.ID, post.OwnerID);

            var vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new PostViewModel(uniqueKey, post));
            vm.Activate();
            this.DataContext = vm;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                ((PostViewModel)this.DataContext).DeleteInstance();
        }
    }
}
