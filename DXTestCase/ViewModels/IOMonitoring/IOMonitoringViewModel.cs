using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DXTestCase.Models.IO;
using Prism.Regions;
using DXTestCase.Core.Mvvm;

namespace DXTestCase.ViewModels.IOMonitoring
{
    public partial class IOMonitoringViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private IOMonitorTreeItemBase _selectedItem;

        public ObservableCollection<IOMonitorTreeItemBase> IOTreeItems { get; private set; }


        public IOMonitoringViewModel(IRegionManager regionManager) : base(regionManager)
        {
            IOTreeItems = new ObservableCollection<IOMonitorTreeItemBase>();
            Initialize();
        }

        private void Initialize()
        {
            var userIO = new IOGroupItem() { Name = "用户IO" };
            userIO.Children.Add(new IOTypeItem() { Name = "用户IO输入", DeviceType = IODeviceType.UI });
            userIO.Children.Add(new IOTypeItem() { Name = "用户IO输出", DeviceType = IODeviceType.UO });

            var systemIO = new IOGroupItem() { Name = "系统IO" };
            systemIO.Children.Add(new IOTypeItem() { Name = "系统IO输入", DeviceType = IODeviceType.SysIn });
            systemIO.Children.Add(new IOTypeItem() { Name = "系统IO输出", DeviceType = IODeviceType.SysOut });

            //var guiIO = new IOGroupItem() { Name = "用户组IO" };
            //guiIO.Children.Add(new IOTypeItem() { Name = "用户组IO输入", DeviceType = IODeviceType.GUI });
            //guiIO.Children.Add(new IOTypeItem() { Name = "用户组IO输出", DeviceType = IODeviceType.GUO });

            IOTreeItems.Add(userIO);
            IOTreeItems.Add(systemIO);
            //IOTreeItems.Add(guiIO);
        }

        [RelayCommand]
        public void IOItemSelected(IOMonitorTreeItemBase item)
        {
            if (item is IOTypeItem && item.IsLeaf)
            {
                NavigationParameters param = new NavigationParameters();
                RegionManager.RequestNavigate("IOMonitorDetailRegion", GetViewNameForDeviceType((item as IOTypeItem).DeviceType));
            }
        }

        private string GetViewNameForDeviceType(IODeviceType deviceType)
        {
            switch (deviceType)
            {
                case IODeviceType.UI:
                    return "UIMonitoringView";
                case IODeviceType.UO:
                    return "UOMonitoringView";
                case IODeviceType.SysIn:
                    return "UIMonitoringView";
                case IODeviceType.SysOut:
                    return "UOMonitoringView";
                default:
                    return "UIMonitoringView";
            }
        }



    }


}