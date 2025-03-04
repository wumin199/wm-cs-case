using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    public class TabViewModel: BindableBase
    {
    private string _title;
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    private bool _canUpdate;
    public bool CanUpdate
    {
      get { return _canUpdate; }
      set { SetProperty(ref _canUpdate, value); }
    }

    private string _updatedText;
    public string UpdateText
    {
      get { return _updatedText; }
      set { SetProperty(ref _updatedText, value); }
    }

    public DelegateCommand UpdateCommand { get; private set; }

    public TabViewModel()
    {
        UpdateCommand = new DelegateCommand(Update).ObservesCanExecute(() => CanUpdate);
    }

    private void Update()
    {
      UpdateText = $"Updated at {DateTime.Now}";
    }
  }

}
