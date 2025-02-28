using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstWPF.Tutorial.DependencyPropertity
{
  /// <summary>
  /// NewTextBox.xaml 的交互逻辑
  /// </summary>
  public partial class NewTextBox : UserControl
    {
        public NewTextBox()
        {
            InitializeComponent();
        }

    public static readonly DependencyProperty SetTextProperty = DependencyProperty.Register("SetText", typeof(string), typeof(NewTextBox), new PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));

    public string SetText
    {
      get { return (string)GetValue(SetTextProperty); }
      set { SetValue(SetTextProperty, value); }
    }

    private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      NewTextBox control = d as NewTextBox;
      control.OnSetTextChanged(e);
    }

    private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
    {
      tbText.Text = e.NewValue.ToString();
    }
  }
}
