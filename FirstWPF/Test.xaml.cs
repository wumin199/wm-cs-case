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


    #region 基础控件
    private void basicBtn_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("You have clicked the button");
    }

    private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
      var calender = sender as Calendar;
      if (calender?.SelectedDate.HasValue == true)
      {
        DateTime date = calender.SelectedDate.Value;
        if (basicLbl != null)
          basicLbl.Content = date.ToShortDateString();
      }
    }
    private void HandleThirdState(object sender, RoutedEventArgs e)
    {
      if (sender is CheckBox cb)
      {
        basicTb2.Text = "3 state CheckBox is in indeterminate state";
      }
    }

    private void HandleCheck(object sender, RoutedEventArgs e)
    {
      if (sender is CheckBox cb)
      {
        if (cb.Name == "basicCb1") basicTb1.Text = "2 state CheckBox is checked";
        else basicTb2.Text = "3 state CheckBox is checked";
      }
    }



    private void HandleUnchecked(object sender, RoutedEventArgs e)
    {
      if (sender is CheckBox cb)
      {
        if (cb.Name == "basicCb1") basicTb1.Text = "2 state CheckBox is unchecked";
        else basicTb2.Text = "3 state CheckBox is unchecked";
      }

    }
    private void Combo3_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var cb = sender as ComboBox;
      if (cb?.SelectedItem is ComboBoxItem selectedItem)
      {
        basicTb3.Text = "You have selected: " + selectedItem.Content.ToString();
      }
    }

    private void Combo4_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var cb = sender as ComboBox;
      if (cb?.SelectedItem is ComboBoxItem selectedItem)
      {
        basicTb4.Text = "You have selected: " + selectedItem.Content.ToString();
      }
    }
    private void Bold_Checked(object sender, RoutedEventArgs e)
    {
      basicTb5.FontWeight = FontWeights.Bold;
    }

    private void Bold_Unchecked(object sender, RoutedEventArgs e)
    {
      basicTb5.FontWeight = FontWeights.Normal;
    }

    private void Italic_Checked(object sender, RoutedEventArgs e)
    {
      basicTb5.FontStyle = FontStyles.Italic;
    }

    private void Italic_Unchecked(object sender, RoutedEventArgs e)
    {
      basicTb5.FontStyle = FontStyles.Normal;
    }

    private void IncreaseFont_Click(object sender, RoutedEventArgs e)
    {
      if(basicTb5.FontSize < 18)
      {
        basicTb5.FontSize += 2;
      }
    }

    private void DecreaseFont_Click(object sender, RoutedEventArgs e)
    {
      if (basicTb5.FontSize > 10)
      {
        basicTb5.FontSize -= 2;
      }
    }
    #endregion


  }
}
