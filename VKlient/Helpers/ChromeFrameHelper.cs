using GalaSoft.MvvmLight;
using OneVK.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace OneVK.Helpers
{
    /// <summary>
    /// Помощник работы с фрэймом.
    /// </summary>
    public class ChromeFrameHelper
    {
        /// <summary>
        /// Текущий фрэйм в окне приложения.
        /// </summary>
        public ChromeFrame CurrentFrame
        {
            get
            {
#if DEBUG
                if (ViewModelBase.IsInDesignModeStatic)
                    return new ChromeFrame();
#endif
                if (Window.Current.Content == null ||
                    !(Window.Current.Content is ChromeFrame))
                    return null;
                return (ChromeFrame)Window.Current.Content;
            }
        }
    }
}
