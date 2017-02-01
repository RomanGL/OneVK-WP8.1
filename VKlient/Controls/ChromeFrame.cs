using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Microsoft.Practices.Unity;
using OneVK.Core;
using OneVK.Core.Models.AppNotifications;
using OneVK.Core.Services;
using OneVK.Helpers;
using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет элемент управления, предоставляющий верхнюю 
    /// строку с заголовком и боковое меню.
    /// </summary>
    [TemplatePart(Name = MenuPanelName, Type = typeof(UIElement))]
    [TemplatePart(Name = LeftManipulationBorderName, Type = typeof(UIElement))]
    [TemplatePart(Name = MenuButtonName, Type = typeof(UIElement))]
    [TemplatePart(Name = FinalaizeTranslateName, Type = typeof(Storyboard))]
    [TemplatePart(Name = DimmingRectName, Type = typeof(UIElement))]
    [TemplatePart(Name = MenuListName, Type = typeof(ListView))]
    [TemplatePart(Name = TopBarButtonsPanelName, Type = typeof(Panel))]
    [TemplatePart(Name = CommandBarPanelName, Type = typeof(ContentPresenter))]
    public sealed class ChromeFrame : Frame, IAppNotificationsPresenter
    {
        private const string MenuPanelName = "MenuPanel";
        private const string LeftManipulationBorderName = "LeftManipulationBorder";
        private const string MenuButtonName = "MenuBtn";
        private const string DimmingRectName = "DimmingRect";
        private const string FinalaizeTranslateName = "FinalizeTranslate";
        private const string MenuListName = "MenuList";
        private const string TopBarButtonsPanelName = "TopBarButtonPanel";
        private const string CommandBarPanelName = "CommandBarPanel";
        private const string NotificationsPanelName = "NotificationsPanel";        
        
        private Service.SettingsService _settings;
        private INavigationService navigationService;

        private readonly List<AppNotification> WaitingNotifications = new List<AppNotification>();

        private List<NavMenuItem> navlist = new List<NavMenuItem>
        {
            new NavMenuItem
            {
                Label = "Новости",
                Symbol = '\uE206',
                DestPage = "NewsfeedView",
            },
            new NavMenuItem
            {
                Label = "Новости Old",
                Symbol = '\uE206',
                DestPage = "NewsView",
            },
            new NavMenuItem
            {
                Label = "Ответы",
                Symbol = '\uE89B',
                DestPage = "FeedbackView"
            },
            new NavMenuItem
            {
                Label = "Сообщения",
                Symbol = '\uE119',
                DestPage = "MessagesView"
            },
            new NavMenuItem
            {
                Label = "Друзья",
                Symbol = '\uE13D',
                DestPage = "FriendsView",
                Arguments = 0L
            },
            new NavMenuItem
            {
                Label = "Сообщества",
                Symbol = '\uE125',
                DestPage = "GroupsView",
                Arguments = 0UL
            },
            new NavMenuItem
            {
                Label = "Фотографии",
                Symbol = '\uE187',
                DestPage = "PhotoAlbumsView",
                Arguments = 0L
            },
            new NavMenuItem
            {
                Label = "Видеозаписи",
                Symbol = '\uE173',
                DestPage = "VideosView",
                Arguments = 0L
            },
            new NavMenuItem
            {
                Label = "Аудиозаписи",
                Symbol = '\uE189',
                DestPage = "AudiosView",
                Arguments = 0L
            },
            new NavMenuItem
            {
                Label = "Закладки",
                Symbol = '\uE1CE',
                DestPage = "ErrorView"
            },
            new NavMenuItem
            {
                Label = "Настройки",
                Symbol = '\uE115',
                DestPage = "SettingsView"
            },
            new NavMenuItem
            {
                Label = "God Mode Old",
                Symbol = '\uE115',
                DestPage = "TestPageOld"
            },
            new NavMenuItem
            {
                Label = "Тестовая страница",
                Symbol = '\uE115',
                DestPage = "TestView"
            },
        };

        #region Части шаблона
        private UIElement MenuPanel;
        private UIElement LeftManipulationBorder;
        private UIElement MenuBtn;
        private UIElement DimmingRect;
        private NavMenuListView MenuList;
        private Storyboard FinalizeTranslate;
        private Panel TopBarButtonPanel;
        private Panel CommandBarPanel;
        private Panel NotificationsPanel;
        #endregion

        public ChromeFrame()
        {
            this.DefaultStyleKey = typeof(ChromeFrame);
        }        

        #region Свойства зависимостей              

        #region CommonState DependencyProperty
        /// <summary>
        /// Текущее базовое состояние элемента.
        /// </summary>
        public CommonStates CommonState
        {
            get { return (CommonStates)GetValue(CommonStateProperty); }
            set { SetValue(CommonStateProperty, value); }
        }

        public static readonly DependencyProperty CommonStateProperty =
            DependencyProperty.Register("CommonState", typeof(CommonStates), 
            typeof(ChromeFrame), new PropertyMetadata(default(CommonStates), OnCommonStateChanged));

        /// <summary>
        /// Вызывается при изменении базового состояния элемента.
        /// </summary>
        private static void OnCommonStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var frame = (ChromeFrame)obj;
            frame.UpdateBaseVisualState((CommonStates)e.NewValue);
            
        }
        #endregion

        #region VisibilityState DependencyProperty
        /// <summary>
        /// Текущее состояние видимости верхней панели.
        /// </summary>
        public VisibilityStates VisibilityState
        {
            get { return (VisibilityStates)GetValue(VisibilityStateProperty); }
            set { SetValue(VisibilityStateProperty, value); }
        }

        public static readonly DependencyProperty VisibilityStateProperty =
            DependencyProperty.Register("VisibilityState", typeof(VisibilityStates), 
            typeof(ChromeFrame), new PropertyMetadata(default(VisibilityStates), OnVisibilityStateChanged));

        /// <summary>
        /// Просиходит при изменении состояния видимости верхней панели.
        /// </summary>
        private static void OnVisibilityStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var sender = obj as ChromeFrame;
            var value = (VisibilityStates)e.NewValue;

            VisualStateManager.GoToState(sender, e.NewValue.ToString(), true);

            //var content = sender.Content as FrameworkElement;
            //if (content != null && !GetNoMargin(content))
            //{
            //    var oldMargin = content.Margin;
            //    switch (value)
            //    {                    
            //        case VisibilityStates.Visible:
            //            content.Margin = new Thickness(oldMargin.Left, oldMargin.Top, oldMargin.Right, 72);
            //            break;
            //        default:
            //            content.Margin = new Thickness(0);
            //            break;
            //    }
            //}

            sender.UpdateStatusBar();
        }

        public static VisibilityStates GetIsVisible(DependencyObject obj)
        {
            if (obj == null) return VisibilityStates.Visible;
            return (VisibilityStates)obj.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject obj, VisibilityStates value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(VisibilityStates), 
            typeof(ChromeFrame), new PropertyMetadata(default(VisibilityStates),
                (s, e) =>
                {
#if DEBUG
                    if (ViewModelBase.IsInDesignModeStatic) return;
#endif
                    if (!(s is Page)) return;

                    var page = (Page)s;
                    if (page.Frame == null) return;
                    var frame = (ChromeFrame)page.Frame;
                    if (frame.CurrentSourcePageType == ((Page)s).GetType())
                        frame.VisibilityState = (VisibilityStates)e.NewValue;
                }));
        #endregion
        
        #region Title DependencyProperty
        /// <summary>
        /// Заголовок страницы, который будет отображаться в 
        /// </summary>        
        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), 
            typeof(ChromeFrame), new PropertyMetadata(default(string)));

        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), 
            typeof(ChromeFrame), new PropertyMetadata("", OnTitleChanged));

        /// <summary>
        /// Вызывается при изменении заголовка.
        /// </summary>
        private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (Window.Current.Content is ChromeFrame)
                ((ChromeFrame)Window.Current.Content).PageTitle = e.NewValue as String;
        }
        #endregion

        #region FinalTranslateX DependencyProperty
        /// <summary>
        /// Финальное положение анимации сдвига по горизонтали.
        /// </summary>
        internal double FinalTranslateX
        {
            get { return (double)GetValue(FinalTranslateXProperty); }
            set { SetValue(FinalTranslateXProperty, value); }
        }
        internal static readonly DependencyProperty FinalTranslateXProperty =
            DependencyProperty.Register("FinalTranslateX", typeof(double), typeof(ChromeFrame), new PropertyMetadata(default(double)));
        #endregion
                
        #region TopBarButtons DependencyProperty
        /// <summary>
        /// Возвращает коллекцию кнопок верхней панели.
        /// </summary>
        public static OneCommandBarElementsCollection GetTopBarButtons(DependencyObject obj)
        {
            return (OneCommandBarElementsCollection)obj.GetValue(TopBarButtonsProperty);
        }

        /// <summary>
        /// Задает коллекцию кнопок верхней панели.
        /// </summary>
        public static void SetTopBarButtons(DependencyObject obj, OneCommandBarElementsCollection value)
        {
            obj.SetValue(TopBarButtonsProperty, value);
        }
        
        public static readonly DependencyProperty TopBarButtonsProperty =
            DependencyProperty.RegisterAttached("TopBarButtons", typeof(OneCommandBarElementsCollection), 
                typeof(ChromeFrame), new PropertyMetadata(default(OneCommandBarElementsCollection)));
        #endregion

        #region OneCommandBar DependencyProperty
        
        public static BaseOneCommandBar GetCommandBar(DependencyObject obj)
        {
            return (BaseOneCommandBar)obj.GetValue(CommandBarProperty);
        }

        public static void SetCommandBar(DependencyObject obj, BaseOneCommandBar value)
        {
            obj.SetValue(CommandBarProperty, value);
        }

        // Using a DependencyProperty as the backing store for CommandBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandBarProperty =
            DependencyProperty.RegisterAttached("CommandBar", typeof(BaseOneCommandBar), typeof(ChromeFrame), new PropertyMetadata(default(BaseOneCommandBar), OnCommandBarChanged));

        /// <summary>
        /// Вызывается при изменении панели приложения.
        /// </summary>
        private static void OnCommandBarChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var c = (FrameworkElement)obj;
            var bar = (BaseOneCommandBar)e.NewValue;
            var frame = obj.GetFirstAncestorOfType<ChromeFrame>();

            if (frame != null)
                frame.UpdateOneCommandBar(bar);
        }
        #endregion

        #region OneCommandBarIsVisible DependencyProperty

        /// <summary>
        /// Возвращает значением, видима ли панель приложения.
        /// </summary>
        public static bool GetCommandBarIsVisible(DependencyObject obj)
        {
            if (obj == null) return false;
            return (bool)obj.GetValue(CommandBarIsVisibleProperty);
        }

        /// <summary>
        /// Задает значение видимости панели приложения.
        /// </summary>
        public static void SetCommandBarIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(CommandBarIsVisibleProperty, value);
        }
        
        public static readonly DependencyProperty CommandBarIsVisibleProperty =
            DependencyProperty.RegisterAttached("CommandBarIsVisible", typeof(bool), 
                typeof(ChromeFrame), new PropertyMetadata(true, OnCommandBarIsVisibleChanged));

        /// <summary>
        /// Вызывается при изменении видимости панели приложения.
        /// </summary>
        private static void OnCommandBarIsVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var frame = obj.GetFirstAncestorOfType<ChromeFrame>();
            if (frame == null) return;

            if (frame.Content.GetType() != obj.GetType()) return;

            var flag = (bool)e.NewValue;
            if (flag)
                frame.UpdateOneCommandBar(GetCommandBar(frame.Content as DependencyObject));
            else
                frame.UpdateOneCommandBar(null);
        }

        #endregion

        #endregion

        /// <summary>
        /// Обновить состояние статусбара.
        /// </summary>
        private async void UpdateStatusBar()
        {
#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic) return;
#endif
            if ((VisibilityState == VisibilityStates.Visible || VisibilityState == VisibilityStates.Tool) 
                && ActualWidth < ActualHeight)
                await StatusBar.GetForCurrentView().ShowAsync();
            else
                await StatusBar.GetForCurrentView().HideAsync();
        }

        /// <summary>
        /// Обновить базовое визуальное состояние элемента управления.
        /// </summary>
        /// <param name="state">Состояние, на которое требутся поменять.</param>
        private void UpdateBaseVisualState(CommonStates state)
        {      
            VisualStateManager.GoToState(this, state.ToString(), true);

            var page = Content as Page;
            if (page == null) return;

            switch (state)
            {
                case CommonStates.Normal:
                    if (page.BottomAppBar != null)
                        page.BottomAppBar.Visibility = Visibility.Visible;
                    UpdateOneCommandBar(GetCommandBar(Content as DependencyObject));
                    break;
                case CommonStates.MenuOpened:
                    if (page.BottomAppBar != null)
                        page.BottomAppBar.Visibility = Visibility.Collapsed;
                    UpdateOneCommandBar(null);
                    break;
            }
        }
        
        /// <summary>
        /// Отображает всплывающее сообщение в приложении.
        /// </summary>
        public void ShowPopup(PopupMessage message)
        {
            var notification = new AppNotification
            {
                Type = (AppNotificationType)message.Type,
                Title = message.Title,
                Content = message.Content,
                ImageUrl = message.ImageUrl,
                ActionToDo = message.ActionToDo,
                Duration = message.Duration
            };

            if (message.Parameter != null)
            {
                notification.DestinationView = message.Parameter.Page.ToString();
                notification.NavigationParameter = message.Parameter.Parameter;
            }

            ((App)App.Current).Container.Resolve<IAppNotificationsService>().SendNotification(notification);
        }

        /// <summary>
        /// Вызывается при построении макета.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();            

            MenuPanel = GetTemplateChild(MenuPanelName) as UIElement;
            LeftManipulationBorder = GetTemplateChild(LeftManipulationBorderName) as UIElement;
            MenuBtn = GetTemplateChild(MenuButtonName) as UIElement;
            DimmingRect = GetTemplateChild(DimmingRectName) as UIElement;
            MenuList = GetTemplateChild(MenuListName) as NavMenuListView;
            TopBarButtonPanel = GetTemplateChild(TopBarButtonsPanelName) as Panel;
            CommandBarPanel = GetTemplateChild(CommandBarPanelName) as Panel;
            FinalizeTranslate = GetTemplateChild(FinalaizeTranslateName) as Storyboard;
            NotificationsPanel = GetTemplateChild(NotificationsPanelName) as Panel;

            MenuList.ItemsSource = navlist;

            if (Content != null)
            {
                VisibilityState = GetIsVisible(Content as DependencyObject);
                PageTitle = GetTitle(Content as DependencyObject);
                UpdateTopBarButtons(GetTopBarButtons(Content as DependencyObject));
                UpdateOneCommandBar(GetCommandBar(Content as DependencyObject));
            }

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                _settings = ServiceHelper.SettingsService;
                navigationService = ((App)Application.Current).Container.Resolve<INavigationService>();

                SubscribeToEvents();

                if (WaitingNotifications.Count > 0)
                {
                    foreach (var notification in WaitingNotifications)
                        ShowNotification(notification);
                    WaitingNotifications.Clear();
                }
            }
        }

        /// <summary>
        /// Обновить панель команд.
        /// </summary>
        /// <param name="bar">Новая панель.</param>
        private void UpdateOneCommandBar(BaseOneCommandBar bar)
        {
            try
            {
                CommandBarPanel.Children.Clear();
                if (bar == null || !GetCommandBarIsVisible(this.Content as DependencyObject)) return;
                var content = Content as FrameworkElement;
                var binding = new Binding() { Mode = BindingMode.TwoWay, Path = new PropertyPath("DataContext"), Source = content };
                bar.SetBinding(FrameworkElement.DataContextProperty, binding);
                CommandBarPanel.Children.Add(bar);
            }
            catch (Exception)
            {        
            }
        }

        /// <summary>
        /// Обновить кнопки верхней панели.
        /// </summary>
        /// <param name="elements">Коллекция кнопок.</param>
        private void UpdateTopBarButtons(OneCommandBarElementsCollection elements)
        {
            if (TopBarButtonPanel == null) return;
            try
            {
                TopBarButtonPanel.Children.Clear();
                if (elements == null) return;

                var content = Content as FrameworkElement;
                var binding = new Binding() { Mode = BindingMode.TwoWay, Path = new PropertyPath("DataContext"), Source = content };
                TopBarButtonPanel.SetBinding(FrameworkElement.DataContextProperty, binding);

                for (int i = 0; i < elements.Count; i++)
                    TopBarButtonPanel.Children.Add((UIElement)elements[i]);
            }
            catch (Exception)
            {
            }            
        }

        /// <summary>
        /// Подписка на необходимые события.
        /// </summary>
        private void SubscribeToEvents()
        {
            SizeChanged += (s, e) => UpdateStatusBar();
            LeftManipulationBorder.ManipulationDelta += (s, e) => TransformMenuPanel(e.Delta.Translation.X);
            MenuPanel.ManipulationDelta += (s, e) => TransformMenuPanel(e.Delta.Translation.X);
            DimmingRect.ManipulationDelta += (s, e) => TransformMenuPanel(e.Delta.Translation.X);

            LeftManipulationBorder.ManipulationCompleted += (s, e) => CompleteTransformMenuPanel();
            MenuPanel.ManipulationCompleted += (s, e) => CompleteTransformMenuPanel();
            DimmingRect.ManipulationCompleted += (s, e) => CompleteTransformMenuPanel();

            DimmingRect.Tapped += (s, e) => CommonState = CommonStates.Normal;
            MenuBtn.Tapped += (s, e) =>
            {
                CommonState =
                    CommonState == CommonStates.Normal ? CommonStates.MenuOpened : CommonStates.Normal;
                e.Handled = true;
            };

            //for (int i = 0; i < MenuList.Items.Count; i++)
            //{
            //    var item = (ListViewItem)MenuList.Items[i];
            //    item.Tapped += OnMenuItemTapped;
            //}

            Navigated += (s, e) =>
            {
                VisibilityState = GetIsVisible(Content as DependencyObject);
                PageTitle = GetTitle(Content as DependencyObject);
                UpdateTopBarButtons(GetTopBarButtons(Content as DependencyObject));
                UpdateOneCommandBar(GetCommandBar(Content as DependencyObject));
            };
            MenuList.ItemInvoked += (s, e) =>
            {
                var item = (NavMenuItem)((NavMenuListView)s).ItemFromContainer(e);

                if (item != null)
                {
                    if (String.IsNullOrEmpty(item.DestPage))
                        navigationService.Navigate("ErrorView", null);
                    else
                        navigationService.Navigate(item.DestPage, item.Arguments);

                    CommonState = CommonStates.Normal;
                }
            };

            Messenger.Default.Register<PopupMessage>(this, m => ShowPopup(m));
            
            Loaded += ChromeFrame_Loaded;
        }

        /// <summary>
        /// Показать уведомление на экране.
        /// </summary>
        /// <param name="notification">Данные уведомления.</param>
        public async void ShowNotification(AppNotification notification)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (NotificationsPanel == null)
                {
                    WaitingNotifications.Add(notification);
                    return;
                }

                var anc = new AppNotificationControl() { Message = notification };
                anc.Loaded += (s, e) => anc.Show();
                anc.Tapped += (s, e) =>
                {
                    anc.Hide();

                    var pageState = GetIsVisible((DependencyObject)Content);

                    if (!String.IsNullOrWhiteSpace(anc.Message.DestinationView) &&
                    pageState != VisibilityStates.Hided && pageState != VisibilityStates.Tool)
                        navigationService.Navigate(anc.Message.DestinationView, anc.Message.NavigationParameter);

                    if (anc.Message.ActionToDo != null)
                        anc.Message.ActionToDo();
                };
                NotificationsPanel.Children.Insert(0, anc);
            });
        }

        private void ChromeFrame_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ChromeFrame_Loaded;
            VisibilityState = GetIsVisible(Content as DependencyObject);
        }

        /// <summary>
        /// Завершает трансформацию панели меню.
        /// </summary>
        private void CompleteTransformMenuPanel()
        {
            var transform = MenuPanel.RenderTransform as CompositeTransform;
            if (CommonState == CommonStates.Normal)
            {
                if (transform.TranslateX >= -180) CommonState = CommonStates.MenuOpened;
                else
                {
                    FinalizeTranslate.Stop();
                    var anim = FinalizeTranslate.Children[0] as DoubleAnimation;
                    anim.To = -281;
                    FinalizeTranslate.Begin();
                    UpdateOneCommandBar(GetCommandBar(Content as DependencyObject));
                }
            }
            else
            {
                if (transform.TranslateX <= -140) CommonState = CommonStates.Normal;
                else
                {
                    FinalizeTranslate.Stop();
                    var anim = FinalizeTranslate.Children[0] as DoubleAnimation;
                    anim.To = 0;
                    FinalizeTranslate.Begin();
                    UpdateOneCommandBar(null);
                }
            }            
        }

        /// <summary>
        /// Выполняет перемещение панели меню.
        /// </summary>
        /// <param name="delta">Смещение.</param>
        private void TransformMenuPanel(double delta)
        {
            UpdateOneCommandBar(null);

            var transform = MenuPanel.RenderTransform as CompositeTransform;

            if (transform.TranslateX + delta > 0)
                transform.TranslateX = 0;
            else if (transform.TranslateX + delta < -281)
                transform.TranslateX = -281;
            else transform.TranslateX += delta;
        }

        #region Перечисления
        /// <summary>
        /// Перечисление базовых состояний элемента управления.
        /// </summary>
        public enum CommonStates
        {
            /// <summary>
            /// Основное состояние.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Состояние с открытым меню.
            /// </summary>
            MenuOpened = 1
        }

        /// <summary>
        /// Перечисление состояний видимости верхней панели.
        /// </summary>
        public enum VisibilityStates
        {
            /// <summary>
            /// Панель видима.
            /// </summary>
            Visible = 0,
            /// <summary>
            /// Панель скрыта.
            /// </summary>
            Hided = 1,
            /// <summary>
            /// Полускрытое состояние.
            /// </summary>
            Intermediate = 2,
            /// <summary>
            /// Полускрытое состояние с заголовком.
            /// </summary>
            IntermediateFull = 3,
            /// <summary>
            /// Состояние для служебных страниц.
            /// </summary>
            Tool = 4
        }        
        
        #endregion
    }
}
