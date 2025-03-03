using Prism.DryIoc;
using Prism.Ioc;
using PrismDemo.Core.Regions;
using PrismDemo.Views;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using ModuleA;

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

  protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
  {
    // StackPanel默认是不adpater的，这里我们自定义一个适配器
    base.ConfigureRegionAdapterMappings(regionAdapterMappings);
    regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
  }

  protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
  {
    moduleCatalog.AddModule<ModuleAModule>();
  }



}

