using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Xaml.Data
{
    /// <summary>
    /// Позволяет пробрасывать DataContext в ресурсы.
    /// </summary>
    public class DataContextProxy : FrameworkElement
    {
        public DataContextProxy()
        {
            this.Loaded += new RoutedEventHandler(DataContextProxyLoaded);
        }

        void DataContextProxyLoaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding();
            if (!String.IsNullOrEmpty(BindingPropertyName))
            {
                binding.Path = new PropertyPath(BindingPropertyName);
            }
            binding.Source = this.DataContext;
            binding.Mode = BindingMode;
            this.SetBinding(DataContextProxy.DataSourceProperty, binding);
        }

        public Object DataSource
        {
            get { return (Object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(Object), typeof(DataContextProxy), null);


        public string BindingPropertyName { get; set; }

        public BindingMode BindingMode { get; set; }

    }
}
