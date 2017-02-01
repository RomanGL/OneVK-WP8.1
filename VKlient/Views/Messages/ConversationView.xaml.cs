using Microsoft.Practices.ServiceLocation;
using OneVK.Controls;
using OneVK.Helpers;
using OneVK.Model.Message;
using OneVK.ViewModel;
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

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу диалога или чата.
    /// </summary>
    public sealed partial class ConversationView : Page
    {
        IConversationViewModel vm;

        public ConversationView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var convID = (long)e.Parameter;
            string uniqueKey = CoreHelper.GetConversationUniqueViewModelKey(convID);

            vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey<IConversationViewModel>(uniqueKey, () =>
                {
                    if (convID < 0) return new ChatViewModel((uint)-convID);
                    else return new DialogViewModel((ulong)convID);
                });
            DataContext = vm;
            ((BaseViewModel)vm).Activate();

            CoreHelper.UnlockOrientation();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            CoreHelper.LockOrientation();
            ((BaseViewModel)vm).Deactivate();
        }
    }
}
