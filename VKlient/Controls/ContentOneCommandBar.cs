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

namespace OneVK.Controls
{
    public class ContentOneCommandBar : BaseOneCommandBar
    {
        protected const string PrimaryButtonsPanelName = "PrimaryButtonsPanel";
         
        public ContentOneCommandBar()
        {
            DefaultStyleKey = typeof(ContentOneCommandBar);
        }

        protected FrameworkElement primaryButtonsPanel;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.primaryButtonsPanel = GetTemplateChild(PrimaryButtonsPanelName) as FrameworkElement;

            this.rootCanvas.SizeChanged += (s, e) =>
            {
                this.primaryButtonsPanel.Width = e.NewSize.Width;
            };
        }

        protected override void ShowMenu()
        {
            this.openMenuStoryboard.Stop();
            var secondaryAnim = this.openMenuStoryboard.Children[0] as DoubleAnimation;
            var primaryAnim = this.openMenuStoryboard.Children[1] as DoubleAnimation;
            if (PrimaryCommands == null || PrimaryCommands.Count == 0)
            {
                primaryAnim.To = 60;
                if (SecondaryCommands == null || SecondaryCommands.Count == 0)
                    secondaryAnim.To = 60;
                else
                    secondaryAnim.To = -this.menuPanel.ActualHeight;
            }
            else
            {
                primaryAnim.To = -79;
                if (SecondaryCommands == null || SecondaryCommands.Count == 0)
                    secondaryAnim.To = 60;
                else
                    secondaryAnim.To = -this.menuPanel.ActualHeight - this.primaryButtonsPanel.ActualHeight + 4;
            }
            this.openMenuStoryboard.Begin();
        }
    }
}
