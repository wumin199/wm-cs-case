using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

    // WPF的data Binding只能绑定属性，不能绑定字段
    // 所以必须是有{get;set}
    // 这样 Text = "{Binding person.Name, Mode=TwoWay} 才可以
    // 如果没有{get; set}，以上绑定失败
    // 这个Person没有实现INotifyPropertyChanged
    // INotifyPropertyChanged 是在属性值发生变化时通知绑定的控件
    // 但是在 WPF 中，数据绑定的默认行为是将控件的值更新到数据源对象中，即使数据源对象没有实现 INotifyPropertyChanged 接口。INotifyPropertyChanged 主要用于在数据源对象的属性值发生变化时通知绑定的控件更新 UI。
    public Person person { get; set; } = new Person { Name = "John", ID = "1", Age = 42 };



    public Test()
    {
      InitializeComponent();
      dataGrid.ItemsSource = Employee.GetEmployees();
      MenList.Items.Add(new Person() { Name = "John", ID = "1", Age = 42 });
      MenList.Items.Add(new Person() { Name = "Paul", ID = "2", Age = 39 });
      MenList.Items.Add(new Person() { Name = "George", ID = "3", Age = 29 });

      //CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewExecuted, CanNew));
      //CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenExecuted, CanOpen));
      //CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveExecuted, CanSave));

      string[] args = App.Args;
      if (args != null && args.Length > 0)
      {
        try
        {
          using (StreamReader sr = new StreamReader(args[0]))
          {
            string line = sr.ReadToEnd();
            basicTb8.AppendText(line);
            basicTb8.AppendText("\n");
          }
        }
        catch (Exception ex)
        {
          basicTb8.AppendText("The file could not be read: ");
          basicTb8.AppendText("\n");
          basicTb8.AppendText(ex.Message);
        }
      }

      //this.DataContext = person;
      this.DataContext = this; // Binding person.Name 相当于 WPF的 Name的Get/Set和 this.person的get/Set一致

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


    #region 命令

    private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("New command is executed");
    }

    private void CanNew(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Open command is executed");
    }

    private void CanOpen(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Save command is executed");
    }

    private void CanSave(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
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
      MessageBox.Show("OnStackPane");
      if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
      {
        MessageBox.Show("KeyDown: Do you want to open a file?");
        //e.Handled = true;
      }
    }

    private void OnInputBtnClick(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("BtnClick: Do you want to open a file?");
      //e.Handled = true;
    }


    private void showBtn_Click(object sender, RoutedEventArgs e)
    {
      string msg = person.Name + " ID: " + person.ID + " Age: " + person.Age;
      MessageBox.Show(msg);
    }

    private void changeResourceButton_Click(object sender, RoutedEventArgs e)
    {
      //if (this.Resources["brushResource"] is SolidColorBrush brush)
      //{
      //  // 颜色都会变
      //  //brush.Color = brush.Color == Colors.Blue ? Colors.Red : Colors.Blue;

      //  // static不会变
      //  this.Resources["brushResource"] = new SolidColorBrush(Colors.Red);
      //}

      if (Application.Current.Resources["brushResource"] is SolidColorBrush brush)
      {
        // 颜色都会变
        //brush.Color = brush.Color == Colors.Blue ? Colors.Red : Colors.Blue;
        // static不会变
        Application.Current.Resources["brushResource"] = new SolidColorBrush(Colors.Red);

      }
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
    // •	实现了 INotifyPropertyChanged 接口，以便在属性值发生变化时通知绑定的控件。
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
    /*
     ObservableCollection<T> 
    是 .NET 中的一个集合类，位于 System.Collections.ObjectModel 命名空间中。
    它继承自 Collection<T>，
    并实现了 INotifyCollectionChanged 和 INotifyPropertyChanged 接口。
    这意味着当集合中的项发生变化（如添加、删除或修改项）时，
    它会通知绑定到该集合的 UI 控件，从而自动更新 UI。
 */
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
    /**
      虽然 INotifyPropertyChanged 不是必需的，但它在以下情况下非常有用：
      1.	当你希望在数据源对象的属性值发生变化时自动更新绑定的控件。
      2.	当你希望在多个控件之间共享数据源对象，并希望所有控件在数据源对象的属性值发生变化时自动更新。
      在你的示例中，由于你只是在按钮点击时读取 person.Name 的当前值，因此不需要 INotifyPropertyChanged 也能正常工作。但如果你希望在 person.Name 发生变化时自动更新 UI，则需要实现 INotifyPropertyChanged。
     */
    public string Name { get; set; }
    public string ID { get; set; }
    public int Age { get; set; }
  }

  #endregion
}
