using GalaSoft.MvvmLight;
using OneVK.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using Windows.Phone.UI.Input;

namespace OneVK.Controls
{    
    [TemplateVisualState(GroupName = CommonStatesGroupName, Name = NormalStateName)]
    [TemplateVisualState(GroupName = CommonStatesGroupName, Name = MenuOpenedStateName)]
    [TemplateVisualState(GroupName = ButtonStatesGroupName, Name = NormalButtonStateName)]
    [TemplateVisualState(GroupName = ButtonStatesGroupName, Name = PressedButtonStateName)]
    [TemplateVisualState(GroupName = ButtonVisibilityStatesGroupName, Name = VisibleButtonStateName)]
    [TemplateVisualState(GroupName = ButtonVisibilityStatesGroupName, Name = HidedButtonStateName)]
    public abstract class BaseOneCommandBar : ContentControl
    {
        protected const string MenuButtonBorderName = "MenuButtonBorder";
        protected const string DimmingRectName = "DimmingRect";
        protected const string OpenMenuStoryboardName = "OpenMenuStoryboard";
        protected const string MenuPanelName = "MenuPanel";
        protected const string ButtonsControlName = "ButtonsControl";
        protected const string NormalPanelName = "NormalPanel";
        protected const string RootCanvasName = "RootCanvas";

        protected const string CommonStatesGroupName = "CommonStates";
        protected const string ButtonStatesGroupName = "ButtonStates";
        protected const string ButtonVisibilityStatesGroupName = "ButtonVisibilityStates";
        protected const string NormalStateName = "Normal";
        protected const string MenuOpenedStateName = "MenuOpened";
        protected const string NormalButtonStateName = "NormalButton";
        protected const string PressedButtonStateName = "PressedButton";
        protected const string VisibleButtonStateName = "VisibleButton";
        protected const string HidedButtonStateName = "HidedButton";

        protected BaseOneCommandBar()
        {
            this.DefaultStyleKey = typeof(BaseOneCommandBar);
            PrimaryCommands = new OneCommandBarElementsCollection();
        }

        protected FrameworkElement menuButtonBorder;
        protected FrameworkElement dimmingRect;
        protected FrameworkElement menuPanel;
        protected FrameworkElement buttonsControl;
        protected FrameworkElement normalPanel;
        protected FrameworkElement rootCanvas;
        protected Storyboard openMenuStoryboard;

        #region DependencyProperties
        /// <summary>
        /// Коллекция основных элементов.
        /// </summary>
        public OneCommandBarElementsCollection PrimaryCommands
        {
            get { return (OneCommandBarElementsCollection)GetValue(PrimaryCommandsProperty); }
            set { SetValue(PrimaryCommandsProperty, value); }
        }

        public static readonly DependencyProperty PrimaryCommandsProperty =
            DependencyProperty.Register("PrimaryCommands", typeof(OneCommandBarElementsCollection),
                typeof(BaseOneCommandBar), new PropertyMetadata(default(OneCommandBarElementsCollection)));

        /// <summary>
        /// Коллекция второстепенных команд.
        /// </summary>
        public OneCommandBarElementsCollection SecondaryCommands
        {
            get { return (OneCommandBarElementsCollection)GetValue(SecondaryCommandsProperty); }
            set { SetValue(SecondaryCommandsProperty, value); }
        }
        
        public static readonly DependencyProperty SecondaryCommandsProperty =
            DependencyProperty.Register("SecondaryCommands", typeof(OneCommandBarElementsCollection), 
                typeof(BaseOneCommandBar), new PropertyMetadata(default(OneCommandBarElementsCollection)));

        /// <summary>
        /// Базовое состояние панели.
        /// </summary>
        public bool IsMenuOpened
        {
            get { return (bool)GetValue(IsMenuOpenedProperty); }
            set { SetValue(IsMenuOpenedProperty, value); }
        }
        
        public static readonly DependencyProperty IsMenuOpenedProperty =
            DependencyProperty.Register("IsMenuOpened", typeof(bool), typeof(BaseOneCommandBar), 
                new PropertyMetadata(default(bool), OnIsMenuOpenedChanged));

        /// <summary>
        /// Вызывается при изменении базового состояния.
        /// </summary>
        private static void OnIsMenuOpenedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseOneCommandBar)obj).ChangeCommonState((bool)e.NewValue ? CommonStates.MenuOpened : CommonStates.Normal);
        }        

        public bool IsButtonHided
        {
            get { return (bool)GetValue(IsButtonHidedProperty); }
            set { SetValue(IsButtonHidedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsButtonHidedProperty =
            DependencyProperty.Register("IsButtonHided", typeof(bool), typeof(BaseOneCommandBar), 
                new PropertyMetadata(default(bool), OnIsButtonHidedChanged));

        /// <summary>
        /// Вызывается при изменении базового состояния.
        /// </summary>
        private static void OnIsButtonHidedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseOneCommandBar)obj).ChangeButtonVisibilityState(
                (bool)e.NewValue ? ButtonVisibilityStates.Hided : ButtonVisibilityStates.Visible);
        }

        #endregion
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.menuButtonBorder = GetTemplateChild(MenuButtonBorderName) as FrameworkElement;
            this.dimmingRect = GetTemplateChild(DimmingRectName) as FrameworkElement;
            this.menuPanel = GetTemplateChild(MenuPanelName) as FrameworkElement;
            this.buttonsControl = GetTemplateChild(ButtonsControlName) as FrameworkElement;
            this.normalPanel = GetTemplateChild(NormalPanelName) as FrameworkElement;
            this.rootCanvas = GetTemplateChild(RootCanvasName) as FrameworkElement;
            this.openMenuStoryboard = GetTemplateChild(OpenMenuStoryboardName) as Storyboard;

#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic) this.Width = 400;
            else this.Width = ((FrameworkElement)Window.Current.Content).ActualWidth;
#else
            this.Width = ((FrameworkElement)Window.Current.Content).ActualWidth;
#endif


            ChangeButtonVisibilityState(IsButtonHided ? ButtonVisibilityStates.Hided : ButtonVisibilityStates.Visible);
            ChangeCommonState(IsMenuOpened ? CommonStates.MenuOpened : CommonStates.Normal);

            try
            {
                SubscribeToEvents();
            }
            catch (Exception) { }
            
        }
        
        protected virtual double GetMenuPanelTranslate()
        {
            return -this.menuPanel.ActualHeight - 25;
        }

        protected virtual void ShowMenu()
        {     
            if (SecondaryCommands == null || SecondaryCommands.Count == 0) return;
            this.openMenuStoryboard.Stop();
            var anim = this.openMenuStoryboard.Children[0] as DoubleAnimation;
            anim.To = GetMenuPanelTranslate();
            this.openMenuStoryboard.Begin();
        }

        protected virtual void SubscribeToEvents()
        {
            if (ViewModelBase.IsInDesignModeStatic) return;

            this.menuButtonBorder.Tapped += (s, e) => 
            {
                IsMenuOpened = !IsMenuOpened;
                if (IsMenuOpened)
                    ShowMenu();
                e.Handled = true;
            };
            this.menuPanel.Tapped += (s, e) =>
            {
                if (IsMenuOpened)
                    IsMenuOpened = false;
                e.Handled = true;
            };
            this.buttonsControl.Tapped += (s, e) =>
            {
                if (IsMenuOpened)
                    IsMenuOpened = false;
                e.Handled = true;
            };
            this.normalPanel.Tapped += (s, e) => { e.Handled = true; };
            this.menuButtonBorder.PointerEntered += delegate { VisualStateManager.GoToState(this, PressedButtonStateName, true); };
            this.menuButtonBorder.PointerExited += delegate { VisualStateManager.GoToState(this, NormalButtonStateName, true); };
            this.dimmingRect.Tapped += (s, e) => 
            {
                IsMenuOpened = false;
                e.Handled = true;
            };

            ((FrameworkElement)Window.Current.Content).SizeChanged += (s, e) =>
            {
                this.Width = e.NewSize.Width;
            };

            HardwareButtons.BackPressed += (s, e) =>
            {
                if (IsMenuOpened)
                {
                    e.Handled = true;
                    IsMenuOpened = false;                    
                }
            };
        }

        /// <summary>
        /// Изменить базовое состояние.
        /// </summary>
        private void ChangeCommonState(CommonStates state)
        {
            string stateName = null;
            switch (state)
            {
                case CommonStates.Normal:
                    stateName = NormalStateName;
                    App.CancelGoBack = false;
                    break;
                case CommonStates.MenuOpened:
                    stateName = MenuOpenedStateName;
                    App.CancelGoBack = true;
                    break;
            }
            VisualStateManager.GoToState(this, stateName, true);
        }

        /// <summary>
        /// Изменить состояние кнопки.
        /// </summary>
        private void ChangeButtonVisibilityState(ButtonVisibilityStates state)
        {
            string stateName = null;
            switch (state)
            {
                case ButtonVisibilityStates.Visible:
                    stateName = VisibleButtonStateName;
                    break;
                case ButtonVisibilityStates.Hided:
                    stateName = HidedButtonStateName;
                    break;
            }
            VisualStateManager.GoToState(this, stateName, true);
        }

        /// <summary>
        /// Перечисление обычных состояний.
        /// </summary>
        private enum CommonStates : byte
        {
            /// <summary>
            /// Обычное состояние.
            /// </summary>
            Normal,
            /// <summary>
            /// Меню открыто.
            /// </summary>
            MenuOpened
        }

        /// <summary>
        /// Перечисление состояний видимости кнопки меню.
        /// </summary>
        private enum ButtonVisibilityStates : byte
        {
            Visible,
            Hided
        }
    }
}
