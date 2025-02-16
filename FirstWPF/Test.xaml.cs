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
using System.Windows.Shapes;

namespace FirstWPF
{
  /// <summary>
  /// Test.xaml 的交互逻辑
  /// </summary>
  public partial class Test : Window
  {
    public Test()
    {
      InitializeComponent();
    }



    #region 路由事件

    // Window -> StackPanel -> Button
    private void RouteBtn_Click(object sender, RoutedEventArgs e)
    {
      txt1.Text = "Button is Clicked";

    }

    private void RouteStatckPanel_Click(object sender, RoutedEventArgs e)
    {
      txt2.Text = "Click event is bubbled to Stack Panel";
      e.Handled = true;
    }

    private void RouteWindow_Click(object sender, RoutedEventArgs e)
    {
      txt3.Text = "Click event is bubbled to Window";
    }

    private void MyCustomControl_MyClick(object sender, RoutedEventArgs e)
    {
      // 这个Click的路由事件是在MyCustomControl中定义的，Click是在Generic.xaml中联合定义的
      MessageBox.Show("It's the custom routed event of your custom control");
    }


    #endregion


  }
}
