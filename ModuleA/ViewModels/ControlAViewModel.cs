using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// 默认应该用Views文件夹，现在改成了Controls文件夹
// 需要修改App.xaml.cs中的代码
namespace ModuleA.ViewModels
{
  public class ControlAViewModel : BindableBase
  {
    private string _title = "hello from ControlAViewModel";
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    private bool _canExecute = false;
    public bool CanExecute
    {
      get { return _canExecute; }
      set
      {
        SetProperty(ref _canExecute, value);

        // 刷新Command方法1：
        // 促使前端Button重新刷新Command事件，也就是会刷新CanClick()
        //ClickCommand.RaiseCanExecuteChanged();
      }
    }

    public DelegateCommand ClickCommand { get; private set; }
    public ControlAViewModel()
    {
      // 方法1：配合ClickCommand.RaiseCanExecuteChanged();
      //ClickCommand = new DelegateCommand(Click, CanClick);

      // 方法2：直接定义CanExecute，此时CanClick()方法不再需要
      //ClickCommand = new DelegateCommand(Click).ObservesCanExecute(() => CanExecute);

      // 方法3：监视CanExecute属性，当CanExecute属性变化时，刷新Command，此时CanClick()方法不再需要
      ClickCommand = new DelegateCommand(Click, CanClick).ObservesProperty(() => CanExecute);
    }

    private bool CanClick()
    {
      //return true;
      return CanExecute;
    }

    private void Click()
    {
      Title = "You Click Me";
    }
  }
}
