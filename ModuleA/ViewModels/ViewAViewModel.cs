using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ModuleA.ViewModels
{
  // BindableBase实现了INotifyPropertyChanged接口，可以实现数据绑定（Dependency  Property）
  // 一般的属性是clr属性


  /** 不使用Prism，自己实现INotifyPropertyChanged接口案例
   using System.ComponentModel;
    using System.Runtime.CompilerServices;

    namespace ModuleA.ViewModels
    {
        public class ViewAViewModel : INotifyPropertyChanged
        {
            private string _text;

            public string Text
            {
                get { return _text; }
                set
                {
                    if (_text != value)
                    {
                        _text = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   * */
  public class ViewAViewModel : BindableBase
  {

    private string _title = "hello from VeiwAViewModel";
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }
  }
}
