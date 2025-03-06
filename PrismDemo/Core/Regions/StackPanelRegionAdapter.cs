using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismDemo.Core.Regions
{
  /// <summary>
  /// 适配器类，用于将 StackPanel 适配到 Prism 的 Region
  /// 默认是不支持StackPanel的
  /// 支持的，用<ContentControl prism:RegionManager.RegionName="ContentRegion" /> 就行了
  /// </summary>
  public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
  {
    public StackPanelRegionAdapter(RegionBehaviorFactory behaviorFactory): base(behaviorFactory)
    {
      // 将接口传递给 base class
    }

    protected override void Adapt(IRegion region, StackPanel regionTarget)
    {
      // s == sender, e== event args
      region.Views.CollectionChanged += (s, e) =>
      {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
          foreach (FrameworkElement item in e.NewItems)
          {
            regionTarget.Children.Add(item as UIElement);
          }
        }
        else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
        {
          foreach (FrameworkElement item in e.OldItems)
          {
            regionTarget.Children.Remove(item as UIElement);
          }
        }
      };
    }

    protected override IRegion CreateRegion()
    {
      //return new AllActiveRegion();
      return new Region();
    }
  }
}
