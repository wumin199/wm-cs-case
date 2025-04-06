using Prism.Services.Dialogs;
using System;

namespace DXTestCase.Core.Mvvm
{
  public class DialogViewModelBase : ViewModelBase, IDialogAware
  {
    // https://www.cnblogs.com/zh7791/p/14140920.html
    public string Title { get; set; }

    public event Action<IDialogResult> RequestClose;

    public virtual bool CanCloseDialog()
    {
      return true;
    }

    // // 当用户点击对话框的"确定"按钮时调用
    // 简单理解：
    // 这是处理对话框"确定"按钮点击的方法
    // 调用这个方法会关闭对话框
    // 可以带一些参数返回给打开对话框的页面
    public virtual void OnConfirmed(IDialogParameters dialogParameters)
    {
      // 用户点击确认按钮 -> 
      // 调用 OnConfirmed -> 
      // 触发 RequestClose 事件->
      // 对话框关闭并返回结果
      RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
    }

    public virtual void OnDialogClosed()
    {
    }

    public virtual void OnDialogOpened(IDialogParameters parameters)
    {
    }
  }
}