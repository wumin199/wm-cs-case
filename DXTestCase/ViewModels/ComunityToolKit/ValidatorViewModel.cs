using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DXTestCase.Core.Mvvm;
using Prism.Regions;

namespace DXTestCase.ViewModels.ComunityToolKit
{
  partial class ValidatorViewModel : RegionViewModelBase
  {

    [ObservableProperty]
    private double _velocity = 30.3;
    public ValidatorViewModel(IRegionManager regionManager) : base(regionManager)
    {
    }

    [RelayCommand]
    private void Setting()
    {
      Velocity = 50.5f;
    }
  }
}