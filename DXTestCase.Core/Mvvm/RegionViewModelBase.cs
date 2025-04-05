using System;
using Prism.Regions;

namespace DXTestCase.Core.Mvvm
{
  public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
  {
    // https://www.cnblogs.com/zh7791/p/14140905.html
    // 应该只要IConfirmNavigationRequest就行了
    protected IRegionManager RegionManager { get; private set; }

    public RegionViewModelBase(IRegionManager regionManager)
    {
      RegionManager = regionManager;
    }

    // // 导航前确认，是否可以导航
    public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
    {
      //  Action<bool>，表示一个接受布尔参数并且不返回值的委托
      //  ConfirmNavigationRequest应该是系统内部自己调用，内部也定义了自己的Action<bool> continuationCallback并传递给ConfirmNavigationRequest
      // 效果就是，如果true，则继续导航，如果false，则取消导航
      continuationCallback(true);
    }

    // // 导航目标
    // 主要作用：
    // 决定当前视图是否可以作为导航目标
    // 控制视图的复用或重新创建
    // 优化导航性能
    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
      // 返回值 true 表示可以复用当前视图
      // 返回值 false 表示需要创建新视图
      return true;
    }

    // 导航离开时
    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    // 导航到达时
    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {

    }
  }
}