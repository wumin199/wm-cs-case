using System;
using Prism.Regions;

namespace DXTestCase.Core.Mvvm
{
  public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
  {
    protected IRegionManager RegionManager { get; private set; }

    public RegionViewModelBase(IRegionManager regionManager)
    {
      RegionManager = regionManager;
    }

    public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
    {
      //  Action<bool>，表示一个接受布尔参数并且不返回值的委托
      //  ConfirmNavigationRequest应该是系统内部自己调用，内部也定义了自己的Action<bool> continuationCallback并传递给ConfirmNavigationRequest
      // 效果就是，如果true，则继续导航，如果false，则取消导航
      continuationCallback(true);
    }

    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
      return true;
    }

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {

    }
  }
}