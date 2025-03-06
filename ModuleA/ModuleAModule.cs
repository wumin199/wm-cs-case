using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using ModuleA.Views;
using System.Diagnostics;
using System.Windows.Controls;
using ModuleA.ViewModels;
using ModuleA.Controls;
using System.Windows.Navigation;

namespace ModuleA
{
  public class ModuleAModule : IModule
  {
    private readonly IRegionManager _regionManager;
    public ModuleAModule(IRegionManager regionManager)
    {
      _regionManager = regionManager;
    }
    public void OnInitialized(IContainerProvider containerProvider)
    {
      // method1: defualt convension or RegisterTypes
      //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
      _regionManager.RegisterViewWithRegion("ContentRegion1", typeof(ControlA));
      _regionManager.RegisterViewWithRegion("ContentRegion2", typeof(TabView));

      // method2
      //IRegion region = _regionManager.Regions["ContentRegion"];
      //var view1 = containerProvider.Resolve<ViewA>();
      //region.Add(view1);
      //region.Activate(view1);


      // method: break the convension
      //IRegion region = _regionManager.Regions["ContentRegion"];
      //var controla = containerProvider.Resolve<ControlA>();
      //region.Add(controla);
      //region.Activate(controla);




      //var view2 = containerProvider.Resolve<ViewA>();
      //// view2.Content是个Grid， 参考ViewA.xaml
      //Debug.WriteLine("-----------------------");

      //if (view2.Content is Grid grid)
      //{
      //  foreach (var child in grid.Children)
      //  {
      //    if (child is TextBlock textBlock)
      //    {
      //      Debug.WriteLine(textBlock.Text);
      //    }
      //  }
      //}

      //view2.Content = new TextBlock
      //{
      //  Text = "Hello, World, this is view2 !",
      //  HorizontalAlignment = HorizontalAlignment.Center,
      //  VerticalAlignment = VerticalAlignment.Center,
      //  FontSize = 48
      //};

      //region.Add(view2);


      //region.Activate(view1);

      // 一次只能激活一个view
      //region.Activate(view1);

      //region.Add(view2);

      //region.Activate(view2);

      //region.Activate(view1);
      //region.Deactivate(view1);

      //region.Activate(view2);
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
      // _regionManager.RegisterViewWithRegion("ContentRegion", typeof(ControlA));
      //ViewModelLocationProvider.Register<ControlA, ControlAViewModel>();

      ViewModelLocationProvider.Register<ControlA>(() =>
      {
        return new ControlAViewModel() { Title= "Hello from factory"};
      });

      ViewModelLocationProvider.Register<TabView>(() =>
      {
        return new TabViewModel();
      });

    }
  }
}
