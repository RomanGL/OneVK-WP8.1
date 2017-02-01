using GalaSoft.MvvmLight;
using Windows.UI.Xaml;

namespace OneVK.Core.Xaml
{
    public class SizeProvider : ObservableObject
    {
        private static double _screenWidth = 200;

        /// <summary>
        /// Возвращает ширину экрана.
        /// </summary>
        public double ScreenWidth
        {
            get { return _screenWidth; }
        }

        public void Update()
        {
            _screenWidth = Window.Current.Bounds.Width;
            RaisePropertyChanged(() => ScreenWidth);
        }
    }
}
