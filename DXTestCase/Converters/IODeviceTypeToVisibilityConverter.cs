using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DXTestCase.Models.IO;
using DXTestCase.ViewModels.IOMonitoring;

namespace DXTestCase.Converters
{
  public class IODeviceTypeToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is IOMonitorViewModel.IOMonitorEntry entry && parameter is string typeStr)
      {
        return entry.IODeviceType.ToString() == typeStr
            ? Visibility.Visible
            : Visibility.Collapsed;
      }
      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return DependencyProperty.UnsetValue;
    }
  }
}