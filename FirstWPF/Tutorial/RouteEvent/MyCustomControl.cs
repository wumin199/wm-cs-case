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

namespace FirstWPF.Tutorial.RouteEvent
{
  /// <summary>
  /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
  ///
  /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
  /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
  /// 元素中:
  ///
  ///     xmlns:MyNamespace="clr-namespace:FirstWPF.Tutorial.RouteEvent"
  ///
  ///
  /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
  /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
  /// 元素中:
  ///
  ///     xmlns:MyNamespace="clr-namespace:FirstWPF.Tutorial.RouteEvent;assembly=FirstWPF.Tutorial.RouteEvent"
  ///
  /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
  /// 并重新生成以避免编译错误:
  ///
  ///     在解决方案资源管理器中右击目标项目，然后依次单击
  ///     “添加引用”->“项目”->[浏览查找并选择此项目]
  ///
  ///
  /// 步骤 2)
  /// 继续操作并在 XAML 文件中使用控件。
  ///
  ///     <MyNamespace:MyCustomControl/>
  ///
  /// </summary>
  public class MyCustomControl : Control
  {
    static MyCustomControl()
    {
      // 通过覆盖 DefaultStyleKeyProperty，为控件设置默认样式。(资源字典中的样式）
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomControl), new FrameworkPropertyMetadata(typeof(MyCustomControl)));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      //demo purpose only, check for previous instances and remove the handler first 
      var button = GetTemplateChild("PART_Button") as Button;
      if (button != null)
      {
        button.Click += Button_Click;
      }
    }
    void Button_Click(object sender, RoutedEventArgs e)
    {
      // 调用 RaiseClickEvent 方法，触发自定义的路由事件。
      RaiseMyClickEvent();
    }

    // 自定义路由事件
    // "Click"，可以在xaml或代码中使用这个名字订阅
    public static readonly RoutedEvent MyClickEvent = EventManager.RegisterRoutedEvent("MyClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyCustomControl));

    public event RoutedEventHandler MyClick
    {
      //定义一个事件，允许外部订阅和取消订阅 MyClickEvent。
      add { AddHandler(MyClickEvent, value); }
      remove { RemoveHandler(MyClickEvent, value); }
    }

    protected virtual void RaiseMyClickEvent()
    {
      // 触发 ClickEvent 路由事件
      RoutedEventArgs newEventArgs = new RoutedEventArgs(MyCustomControl.MyClickEvent);
      //  触发ClientEvent路由事件
      // 订阅了 Click 事件的外部处理程序 MyCustomControl_Click 会被调用
      RaiseEvent(newEventArgs);
    }
  }
}
