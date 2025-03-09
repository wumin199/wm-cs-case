using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace DXDemo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : ThemedWindow, INotifyPropertyChanged
  {
    private int selectedOption;
    public int SelectedOption
    {
      get => selectedOption;
      set
      {
        if (selectedOption != value)
        {
          selectedOption = value;
          OnPropertyChanged(nameof(SelectedOption));
          MessageBox.Show($"选中了选项: {value}");
        }
      }
    }

    public List<string> Items { get; } = new List<string> { "项目1", "项目2", "项目3" };

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindow()
    {
      InitializeComponent();
      DataContext = this;
    }
  }
}
