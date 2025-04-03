using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Reflection;

namespace DXTestCase.Converters
{
  public class EnumTypeToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is string typeStr)
      {
        // 如果value本身就是枚举
        if (value is Enum enumValue)
        {
          return enumValue.ToString() == typeStr
              ? Visibility.Visible
              : Visibility.Collapsed;
        }

        // 如果value是对象，尝试获取指定的枚举属性
        if (value != null)
        {
          // 参数格式: "EnumPropertyName:EnumValue"
          var parts = typeStr.Split(':');
          if (parts.Length == 2)
          {
            var propertyName = parts[0];
            var enum_Value = parts[1];

            var property = value.GetType().GetProperty(propertyName);
            if (property != null && property.PropertyType.IsEnum)
            {
              var propertyValue = property.GetValue(value);
              if (propertyValue != null)
              {
                return propertyValue.ToString() == enum_Value
                    ? Visibility.Visible
                    : Visibility.Collapsed;
              }
            }
          }
        }
      }
      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}