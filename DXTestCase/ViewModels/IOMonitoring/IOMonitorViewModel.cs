using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DXTestCase.Models.IO;
using Prism.Regions;
using DXTestCase.Core.Mvvm;

namespace DXTestCase.ViewModels.IOMonitoring
{
    public partial class IOMonitorViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private IOMonitorEntry _selectedMonitorEntry;

        [ObservableProperty]
        private ObservableCollection<IOMonitorData> _uIData;
        [ObservableProperty]
        private ObservableCollection<IOMonitorData> _uOData;
        [ObservableProperty]
        private ObservableCollection<IOMonitorData> _sysIData;
        [ObservableProperty]
        private ObservableCollection<IOMonitorData> _sysOData;

        public ObservableCollection<IOMonitorCategory> IOMonitorRoot { get; set; }

        public IOMonitorViewModel(IRegionManager regionManager) : base(regionManager)
        {
            InitializeData();
            IOMonitorRoot = GetIOMonitorRoot();
            // 设置默认选中第一个类别的第一个项
            if (IOMonitorRoot.Any() && IOMonitorRoot[0].IOMonitorEntries.Any())
            {
                SelectedMonitorEntry = IOMonitorRoot[0].IOMonitorEntries[0];
            }
        }

        private void InitializeData()
        {
            // @TODO(min.wu): for test
            // @TODO(min.wu): index starting from 0 or 1
            UIData = new ObservableCollection<IOMonitorData>(
                Enumerable.Range(1, 2000).Select(i => new IOMonitorData
                {
                    Name = $"UI[{i - 1}]",
                    IOState = false,
                    Description = "ABC"
                })
            );
            UOData = new ObservableCollection<IOMonitorData>(
                Enumerable.Range(1, 2000).Select(i => new IOMonitorData
                {
                    Name = $"UO[{i - 1}]",
                    IOState = false,
                    Description = ""
                })
            );
            SysIData = new ObservableCollection<IOMonitorData>(
                Enumerable.Range(1, 2000).Select(i => new IOMonitorData
                {
                    Name = $"SysIn[{i - 1}]",
                    IOState = false,
                    Description = ""
                })
            );
            SysOData = new ObservableCollection<IOMonitorData>(
                Enumerable.Range(1, 2000).Select(i => new IOMonitorData
                {
                    Name = $"SysOut[{i - 1}]",
                    IOState = false,
                    Description = ""
                })
            );
        }

        public class IOMonitorData
        {
            public string Name { get; set; }
            public bool IOState { get; set; }
            public string Description { get; set; }
        }

        public class IOMonitorEntry
        {
            public IODeviceType IODeviceType { get; set; }
            public string Name { get; set; }
        }

        public class IOMonitorCategory
        {
            public string Name { get; set; }
            public ObservableCollection<IOMonitorEntry> IOMonitorEntries { get; set; }
        }

        private ObservableCollection<IOMonitorCategory> GetIOMonitorRoot()
        {
            var ui = new IOMonitorEntry { Name = IODeviceType.UI.GetDisplayName(), IODeviceType = IODeviceType.UI };
            var uo = new IOMonitorEntry { Name = IODeviceType.UO.GetDisplayName(), IODeviceType = IODeviceType.UO };
            var uio = new IOMonitorCategory { Name = "用户IO", IOMonitorEntries = new ObservableCollection<IOMonitorEntry> { ui, uo } };

            var sys_in = new IOMonitorEntry { Name = IODeviceType.SysIn.GetDisplayName(), IODeviceType = IODeviceType.SysIn };
            var sys_out = new IOMonitorEntry { Name = IODeviceType.SysOut.GetDisplayName(), IODeviceType = IODeviceType.SysOut };
            var sys_io = new IOMonitorCategory { Name = "系统IO", IOMonitorEntries = new ObservableCollection<IOMonitorEntry> { sys_in, sys_out } };

            var monitor_root = new ObservableCollection<IOMonitorCategory> { uio, sys_io };

            return monitor_root;
        }
    }
}