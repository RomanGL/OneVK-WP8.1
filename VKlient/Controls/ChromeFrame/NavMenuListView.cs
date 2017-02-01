using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace OneVK.Controls
{
    public class NavMenuListView : ListView
    {
        private ChromeFrame chromeFrameHost;

        public NavMenuListView()
        {
            this.SelectionMode = ListViewSelectionMode.Single;
            this.IsItemClickEnabled = true;
            this.ItemClick += ItemClickedHandler;

            // Locate the hosting SplitView control
            this.Loaded += (s, a) =>
            {
                var parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is ChromeFrame))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent != null)
                {
                    this.chromeFrameHost = parent as ChromeFrame;
                }
            };
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Remove the entrance animation on the item containers.
            for (int i = 0; i < this.ItemContainerTransitions.Count; i++)
            {
                if (this.ItemContainerTransitions[i] is EntranceThemeTransition)
                {
                    this.ItemContainerTransitions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Mark the <paramref name="item"/> as selected and ensures everything else is not.
        /// If the <paramref name="item"/> is null then everything is unselected.
        /// </summary>
        /// <param name="item"></param>
        public void SetSelectedItem(ListViewItem item)
        {
            int index = -1;
            if (item != null)
            {
                index = this.IndexFromContainer(item);
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                var lvi = (ListViewItem)this.ContainerFromIndex(i);
                if (lvi == null) continue;

                if (i != index)
                {
                    lvi.IsSelected = false;
                }
                else if (i == index)
                {
                    lvi.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Occurs when an item has been selected
        /// </summary>
        public event EventHandler<ListViewItem> ItemInvoked;

        /// <summary>
        /// Custom keyboarding logic to enable movement via the arrow keys without triggering selection 
        /// until a 'Space' or 'Enter' key is pressed. 
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnKeyDown(KeyRoutedEventArgs e)
        //{
        //var focusedItem = FocusManager.GetFocusedElement();

        //    switch (e.Key)
        //    {
        //        case VirtualKey.Up:
        //            this.TryMoveFocus(FocusNavigationDirection.Up);
        //            e.Handled = true;
        //            break;

        //        case VirtualKey.Down:
        //            this.TryMoveFocus(FocusNavigationDirection.Down);
        //            e.Handled = true;
        //            break;

        //        case VirtualKey.Tab:
        //            var shiftKeyState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Shift);
        //            var shiftKeyDown = (shiftKeyState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;

        //            // If we're on the header item then this will be null and we'll still get the default behavior.
        //            if (focusedItem is ListViewItem)
        //            {
        //                var currentItem = (ListViewItem)focusedItem;
        //                bool onlastitem = currentItem != null && this.IndexFromContainer(currentItem) == this.Items.Count - 1;
        //                bool onfirstitem = currentItem != null && this.IndexFromContainer(currentItem) == 0;

        //                if (!shiftKeyDown)
        //                {
        //                    if (onlastitem)
        //                    {
        //                        this.TryMoveFocus(FocusNavigationDirection.Next);
        //                    }
        //                    else
        //                    {
        //                        this.TryMoveFocus(FocusNavigationDirection.Down);
        //                    }
        //                }
        //                else // Shift + Tab
        //                {
        //                    if (onfirstitem)
        //                    {
        //                        this.TryMoveFocus(FocusNavigationDirection.Previous);
        //                    }
        //                    else
        //                    {
        //                        this.TryMoveFocus(FocusNavigationDirection.Up);
        //                    }
        //                }
        //            }
        //            else if (focusedItem is Control)
        //            {
        //                if (!shiftKeyDown)
        //                {
        //                    this.TryMoveFocus(FocusNavigationDirection.Down);
        //                }
        //                else // Shift + Tab
        //                {
        //                    this.TryMoveFocus(FocusNavigationDirection.Up);
        //                }
        //            }

        //            e.Handled = true;
        //            break;

        //        case VirtualKey.Space:
        //        case VirtualKey.Enter:
        //            // Fire our event using the item with current keyboard focus
        //            this.InvokeItem(focusedItem);
        //            e.Handled = true;
        //            break;

        //        default:
        //            base.OnKeyDown(e);
        //            break;
        //    }
        //}

        /// <summary>
        /// This method is a work-around until the bug in FocusManager.TryMoveFocus is fixed.
        /// </summary>
        /// <param name="direction"></param>
        //private void TryMoveFocus(FocusNavigationDirection direction)
        //{
        //    if (direction == FocusNavigationDirection.Next || direction == FocusNavigationDirection.Previous)
        //    {
        //        FocusManager.TryMoveFocus(direction);
        //    }
        //    else
        //    {
        //        var control = FocusManager.FindNextFocusableElement(direction) as Control;
        //        if (control != null)
        //        {
        //            control.Focus(FocusState.Programmatic);
        //        }
        //    }
        //}

        private void ItemClickedHandler(object sender, ItemClickEventArgs e)
        {
            // Triggered when the item is selected using something other than a keyboard
            var item = this.ContainerFromItem(e.ClickedItem);
            this.InvokeItem(item);
        }

        private void InvokeItem(object focusedItem)
        {
            this.SetSelectedItem(focusedItem as ListViewItem);
            this.ItemInvoked(this, focusedItem as ListViewItem);

            if (focusedItem is ListViewItem)
            {
                ((ListViewItem)focusedItem).Focus(FocusState.Programmatic);
            }
        }
    }
}
