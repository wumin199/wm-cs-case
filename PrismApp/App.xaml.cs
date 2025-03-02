using System.Configuration;
using System.Data;
using System.Windows;
using Prism.DryIoc;
using PrismApp.ViewModels;

namespace PrismApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
  protected override Window CreateShell()
  {
    //  启动页，配置默认启动页
    // 返回应用程序的主窗口
    return Container.Resolve<MainWindow>();
  }

  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    //throw new NotImplementedException();
    // 该方法用于在Prism初始化过程中, 我们定义自身需要的一些注册类型, 以便于在Prism中可以使用。
    //containerRegistry.RegisterForNavigation<ViewA>();
  }
}

