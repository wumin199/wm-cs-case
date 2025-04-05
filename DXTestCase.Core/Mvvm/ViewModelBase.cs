using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Navigation;

namespace DXTestCase.Core.Mvvm
{
  //   ObservableObject 是 CommunityToolkit.Mvvm 中的一个基础类，它实现了 INotifyPropertyChanged 接口，主要用于属性变更通知。当属性值发生变化时，它会通知 UI 更新。
  // ObservableValidator 是 CommunityToolkit.Mvvm 中的一个更高级的类，它继承自 ObservableObject，并添加了数据验证功能。它集成了 System.ComponentModel.DataAnnotations 的验证特性，可以：
  // 支持属性验证（通过特性如[Required], [StringLength] 等）
  // 提供验证状态跟踪
  // 支持验证错误消息的显示
  // 可以验证整个对象或特定属性


  public abstract class ViewModelBase : ObservableValidator, IDestructible
  {
    protected ViewModelBase()
    {
    }

    public virtual void Destroy()
    {
    }
  }
}