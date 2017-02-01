using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Group;
using OneVK.ViewModel.Messages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;

namespace OneVK.Views
{
    public sealed partial class GroupsView : Page
    {
        public GroupsView()
        {
            InitializeComponent();
        }       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tag = e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void ContentPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ContentPivot.SelectedIndex)
            {
                case 0:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                        new Binding { Path = new PropertyPath("Refresh") });
                    break;
                case 1:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshEvents") });
                    break;
                case 2:
                    RefreshButton.SetBinding(ButtonBase.CommandProperty,
                            new Binding { Path = new PropertyPath("RefreshGroupsManage") });
                    break;
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            long parameter;
            //AppViews page;
            parameter = (long)((VKGroupExtended)e.ClickedItem).ID;
            //page = AppViews.GroupInfoView;
            //Messenger.Default.Send(new NavigateToPageMessage
            //{
            //    Page = page,
            //    Operation = NavigationType.New,
            //    Parameter = parameter
            //});

            ((App)App.Current).Container.Resolve<INavigationService>().Navigate("GroupView", parameter);
        }
    }
}
