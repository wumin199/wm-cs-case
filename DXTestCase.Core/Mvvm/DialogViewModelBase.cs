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

    public virtual void OnConfirmed(IDialogParameters dialogParameters)
    {
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