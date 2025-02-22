using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
      dataGrid.ItemsSource = Employee.GetEmployees();
      MenList.Items.Add(new Person() { Name = "John", ID = "1", Age = 42 });
      MenList.Items.Add(new Person() { Name = "Paul", ID = "2", Age = 39 });
      MenList.Items.Add(new Person() { Name = "George", ID = "3", Age = 29 });
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
      if (basicTb5.FontSize < 18)
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

    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      var picker = sender as DatePicker;
      DateTime? date = picker?.SelectedDate;
      if (date.HasValue)
      {
        basicTb6.Text = date.Value.ToShortDateString();
      }
      else
      {
        basicTb6.Text = "No date selected";
      }
    }

    private void ShowMessageBox_Click(object sender, RoutedEventArgs e)
    {
      string msgtext = "Click any button";
      string txt = "My Title";
      MessageBoxButton btn = MessageBoxButton.YesNoCancel;
      MessageBoxResult result = MessageBox.Show(msgtext, txt, btn);
      switch (result)
      {
        case MessageBoxResult.Yes: basicTb7.Text = "Yes button is clicked"; break;
        case MessageBoxResult.No: basicTb7.Text = "No button is clicked"; break;
        case MessageBoxResult.Cancel: basicTb7.Text = "Cancel button is clicked"; break;
      }
    }
    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void MenuItem_Click1(object sender, RoutedEventArgs e)
    {

    }

    private void MenuItem_Click2(object sender, RoutedEventArgs e)
    {

    }

    private void HandleStatus(object sender, RoutedEventArgs e)
    {
      var rb = sender as RadioButton;
      if (rb?.IsChecked == true)
      {
        textBlock3.Text = "You have selected: " + rb.Content.ToString();
      }
    }

    private void HandleGender(object sender, RoutedEventArgs e)
    {
      var rb = sender as RadioButton;
      if (rb?.IsChecked == true)
      {
        textBlock2.Text = "You have selected: " + rb.Content.ToString();
      }

    }

    private void basicSlider1Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (sender is Slider slider)
      {
        int val = Convert.ToInt32(e.NewValue);
        string msg = String.Format("Current value: {0}", val);
        basicTblk.Text = msg;
      }

    }
    private void HandleUncheckBtn(object sender, RoutedEventArgs e)
    {
      basicTbl8.Text = "You have unchecked the button";
    }
    private void HandleCheckTbn(object sender, RoutedEventArgs e)
    {
      basicTbl8.Text = "You have checked the button";
    }

    private void DockerPanel_Click(object sender, RoutedEventArgs e)
    {
      var btn = sender as Button;
      if (btn != null)
      {
        MessageBox.Show("You have clicked the button: " + btn.Content.ToString());
      }
    }

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
      Rectangle source = e.Source as Rectangle;
      if (source != null)
      {
        source.Fill = Brushes.SlateGray;
      }
      basicTbl12.Text = "Mouse entered";
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
      Rectangle source = e.Source as Rectangle;
      if (source != null)
      {
        source.Fill = Brushes.LightGray;
      }
      basicTbl12.Text = "Mouse Leave";
      basicTbl13.Text = "";
      basicTbl14.Text = "";
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      Point pnt = e.GetPosition(mrRec);
      basicTbl13.Text = "Mouse Move: " + pnt.X + ", " + pnt.Y;
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
      Rectangle source = e.Source as Rectangle;
      Point pnt = e.GetPosition(mrRec);
      
      if (source != null)
      {
        source.Fill = Brushes.Beige;
      }
      basicTbl14.Text = "Mouse Down: " + pnt.X + ", " + pnt.Y;

    }
    private void OnStackPanelKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
      {
        MessageBox.Show("Do you want to open a file?");
        e.Handled = true;
      }
    }

    private void OnInputBtnClick(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Do you want to open a file?");
      e.Handled = true;
    }


    #endregion


  }


  #region 基础控件

  public enum Party
  {
    Indepentent,
    Federalist,
    DemocratRepublican,
  }

  public class Employee : INotifyPropertyChanged
  {
    private string name;
    private string title;
    private bool wasReElected;
    private Party affiliation;


    public string Name
    {
      get { return name; }
      set
      {
        name = value;
        RaisePropertyChanged();

      }
    }

    public string Title
    {
      get { return title; }
      set
      {
        title = value;
        RaisePropertyChanged();
      }
    }

    public bool WasReElected
    {
      get { return wasReElected; }
      set
      {
        wasReElected = value;
        RaisePropertyChanged();
      }
    }

    public Party Affiliation
    {
      get { return affiliation; }
      set
      {
        affiliation = value;
        RaisePropertyChanged();
      }
    }

    public static ObservableCollection<Employee> GetEmployees()
    {
      var employees = new ObservableCollection<Employee>();
      employees.Add(new Employee() { Name = "George Washington", Title = "Minister", WasReElected = true, Affiliation = Party.Indepentent });
      employees.Add(new Employee() { Name = "John Adams", Title = "CM", WasReElected = false, Affiliation = Party.Federalist });
      employees.Add(new Employee() { Name = "Thomas Jefferson", Title = "PM", WasReElected = false, Affiliation = Party.DemocratRepublican });
      employees.Add(new Employee() { Name = "James Madison", Title = "Minister", WasReElected = true, Affiliation = Party.Indepentent });
      employees.Add(new Employee() { Name = "James Monroe", Title = "Minister", WasReElected = false, Affiliation = Party.Federalist });
      employees.Add(new Employee() { Name = "John Quincy Adams", Title = "Minister", WasReElected = false, Affiliation = Party.DemocratRepublican });
      return employees;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string caller = "")
    {
      // 这个可以认为是被 xaml中的 {Binding Name} 所订阅
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));


    }


  }

  public class Person
  {
    public string Name { get; set; }
    public string ID { get; set; }
    public int Age { get; set; }
  }

  #endregion
}
