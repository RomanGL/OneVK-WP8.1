using System;
using OneVK.Helpers;
using OneVK.ViewModel;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Xaml.Data
{
    /// <summary>
    /// Позволяет представлению динамически получать модель представления.
    /// </summary>
    public class MultipleViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string viewModelKey;
            BaseKeyedViewModelOld vm;

            switch ((string)parameter)
            {
                case "Groups":
                    viewModelKey = "GroupsUser" + value;
                    CoreHelper.MultipleLocator.RegisterByKey<GroupsViewModel>(viewModelKey, value);
                    vm = CoreHelper.MultipleLocator.GetByKey<GroupsViewModel>(viewModelKey);
                    break;
                default:
                    return null;
            }
            
            vm.Activate();
            return vm;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
