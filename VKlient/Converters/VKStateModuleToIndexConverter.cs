using System;
using Windows.UI.Xaml.Data;
using OneVK.Core.VK.Models.Groups;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует состояние модуля сообщества ВКонтакте в индекс элемента.
    /// </summary>
    public sealed class VKStateModuleToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is VKFourStateModule)
                return (int)(VKFourStateModule)value;
            else if (value is VKThreeStateModule)
                return (int)(VKThreeStateModule)value;
            else if (value is VKTwoStateModule)
                return (int)(VKTwoStateModule)value;
            else if (value is VKGroupAccess)
                return (int)(VKGroupAccess)value;
            else
                return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) return null;

            int index = (int)value;
            switch (parameter.ToString())
            {
                case "2":
                    return (VKTwoStateModule)index;
                case "3":
                    return (VKThreeStateModule)index;
                case "4":
                    return (VKFourStateModule)index;
                case "a":
                    return (VKGroupAccess)index;
                default:
                    return null;
            }
        }
    }
}
