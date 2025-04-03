using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DXTestCase.Models.IO;
using Prism.Regions;
using DXTestCase.Core.Mvvm;

namespace DXTestCase.ViewModels.IOMonitoring
{
    public partial class GlobalDataMonitorViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private GlobalDataMonitorEntry _selectedEntry;

        public ObservableCollection<GlobalDataMonitorCategory> MonitorRoot { get; set; }

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _userIntData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _userRealData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _userByteData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _userUShortData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _pRData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _sysIntData;

        [ObservableProperty]
        public ObservableCollection<GlobalMonitorData> _sysRealData;

        public GlobalDataMonitorViewModel(IRegionManager regionManager) : base(regionManager)
        {
            InitializeData();
            MonitorRoot = GetGlobaDataMonitorRoot();

            if (MonitorRoot.Any() && MonitorRoot[0].MonitorEntries.Any())
            {
                SelectedEntry = MonitorRoot[0].MonitorEntries[0];
            }
        }

        private void InitializeData()
        {
            UserIntData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_USER_INT_COUNT).Select(i => new GlobalMonitorData { Name = $"I{i}", Value = $"{i}", Description = $"用户整型{i}的描述" }));
            UserRealData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_USER_REAL_COUNT).Select(i => new GlobalMonitorData { Name = $"R{i}", Value = $"{i}", Description = $"用户实型{i}的描述" }));
            UserByteData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_USER_BYTE_COUNT).Select(i => new GlobalMonitorData { Name = $"B{i}", Value = $"{i}", Description = $"用户字节{i}的描述" }));
            UserUShortData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_USER_USHORT_COUNT).Select(i => new GlobalMonitorData { Name = $"H{i}", Value = $"{i}", Description = $"用户短整型{i}的描述" }));
            PRData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_PR_COUNT).Select(i => new GlobalMonitorData { Name = $"位置变量{i}", Value = $"{i}", Description = $"位置变量{i}的描述" }));
            SysIntData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_SYS_INT_COUNT).Select(i => new GlobalMonitorData { Name = $"整形变量:{i}", Value = $"{i}", Description = $"系统整型{i}的描述" }));
            SysRealData = new ObservableCollection<GlobalMonitorData>(Enumerable.Range(0, GlobalDataTypeCount.MAX_SYS_REAL_COUNT).Select(i => new GlobalMonitorData { Name = $"实型变量:{i}", Value = $"{i}", Description = $"系统实型{i}的描述" }));
        }

        public class GlobalDataMonitorEntry
        {
            public string Name { get; set; }
            public GlobalDataType DataType { get; set; }
        }

        public class GlobalMonitorData
        {
            public string Name { set; get; }
            public string Value { set; get; }
            public string Description { set; get; }
        }

        //public class PRData
        //{
        //    public string Name { set; get; }

        //    public string PoseIndex { set; get; }

        //    public bool Locked { set; get; }

        //    public string X { set; get; }
        //    public string Y { set; get; }
        //    public string Z { set; get; }
        //    public string Rx { set; get; }
        //    public string Ry { set; get; }
        //    public string Rz { set; get; }


        //    public string Value { set; get; }
        //    public string Description { set; get; }
        //}


        public class GlobalDataMonitorCategory
        {
            public string Name { set; get; }
            public ObservableCollection<GlobalDataMonitorEntry> MonitorEntries { get; set; }
        }

        private ObservableCollection<GlobalDataMonitorCategory> GetGlobaDataMonitorRoot()
        {
            var usr_in = new GlobalDataMonitorEntry { Name = GlobalDataType.UserInt.GetDisplayName(), DataType = GlobalDataType.UserInt };
            var usr_real = new GlobalDataMonitorEntry { Name = GlobalDataType.UserReal.GetDisplayName(), DataType = GlobalDataType.UserReal };
            var usr_byte = new GlobalDataMonitorEntry { Name = GlobalDataType.UserByte.GetDisplayName(), DataType = GlobalDataType.UserByte };
            var usr_ushort = new GlobalDataMonitorEntry { Name = GlobalDataType.UserUShort.GetDisplayName(), DataType = GlobalDataType.UserUShort };

            var pr = new GlobalDataMonitorEntry { Name = GlobalDataType.PR.GetDisplayName(), DataType = GlobalDataType.PR };

            var sys_int = new GlobalDataMonitorEntry { Name = GlobalDataType.SysInt.GetDisplayName(), DataType = GlobalDataType.SysInt };
            var sys_real = new GlobalDataMonitorEntry { Name = GlobalDataType.SysReal.GetDisplayName(), DataType = GlobalDataType.SysReal };

            var monitor_root = new ObservableCollection<GlobalDataMonitorCategory>
            {
                new GlobalDataMonitorCategory { Name = "用户变量", MonitorEntries = new ObservableCollection<GlobalDataMonitorEntry> { usr_in, usr_real, usr_byte, usr_ushort } },
                new GlobalDataMonitorCategory { Name = "系统变量", MonitorEntries = new ObservableCollection<GlobalDataMonitorEntry> { sys_int, sys_real } },
                new GlobalDataMonitorCategory { Name = "位置变量", MonitorEntries = new ObservableCollection<GlobalDataMonitorEntry> { pr } },
            };

            return monitor_root;
        }



    }
}