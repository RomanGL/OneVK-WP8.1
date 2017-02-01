using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Базовый класс для кнопок, выполняющих действия, отражающие
    /// отношение пользователя к тому или иному контенту.
    /// </summary>
    [TemplateVisualState(GroupName = CommonStatesGroupName, Name = InactiveStateName)]
    [TemplateVisualState(GroupName = CommonStatesGroupName, Name = ActiveStateName)]
    public class BaseSocActionButton : Control
    {
        protected const string CommonStatesGroupName = "CommonStates";
        protected const string ActiveStateName = "Active";
        protected const string InactiveStateName = "Inactive";
        
        /// <summary>
        /// Задано ли активное состояние социальной кнопки.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), 
            typeof(BaseSocActionButton), new PropertyMetadata(default(bool), OnIsActiveChanged));

        /// <summary>
        /// Вызывается при изменении состояния активности социальной кнопки.
        /// </summary>
        private static void OnIsActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseSocActionButton)obj).UpdateVisualState();
        }

        /// <summary>
        /// Обновляет визуальное состояния элемента управления.
        /// </summary>
        private void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, IsActive ? ActiveStateName : InactiveStateName, false);
        }

        /// <summary>
        /// Вызывается при построении шаблона элемента управления.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
        }
    }
}
