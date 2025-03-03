using Prism.DryIoc;
using Prism.Ioc;
using PrismDemo.Views;
using System.Windows;

namespace PrismDemo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
  protected override Window CreateShell()
  {
    // 返回 MainWindow
    return Container.Resolve<ShellWindow>();
  }


  protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Register any types here
    }
}

