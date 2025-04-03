using System;
using System.Globalization;
using System.Windows.Data;

namespace DXTestCase.Converters
{
  public class BooleanToOnOffConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is bool b && b ? "ON" : "OFF";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is string s && s.Equals("ON", StringComparison.OrdinalIgnoreCase);
    }
  }
}