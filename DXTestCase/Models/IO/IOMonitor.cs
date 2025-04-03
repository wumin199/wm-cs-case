using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace DXTestCase.Models.IO
{
    //public class IOMonitor
    //{
    //    public string Name { get; set; }
    //    public IODeviceType DeviceType { get; set; }

    //}

    public enum IODeviceType
    {
        UI,
        UO,
        SysIn,
        SysOut
    }
    public static class IODeviceTypeExtensions
    {
        private static readonly Dictionary<IODeviceType, string> DeviceTypeNames = new Dictionary<IODeviceType, string>
        {
            { IODeviceType.UI, "用户IO输入" },
            { IODeviceType.UO, "用户IO输出" },
            { IODeviceType.SysIn, "系统IO输入" },
            { IODeviceType.SysOut, "系统IO输出" }
        };

        public static string GetDisplayName(this IODeviceType deviceType)
        {
            return DeviceTypeNames.TryGetValue(deviceType, out string name) ? name : deviceType.ToString();
        }

        public static IODeviceType? GetDeviceTypeFromName(string name)
        {
            foreach (var pair in DeviceTypeNames)
            {
                if (pair.Value.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }

    public class IOMonitorTreeItemBase : BindableBase
    {
        public string Name { get; set; }
        public ObservableCollection<IOMonitorTreeItemBase> Children { get; set; }
        public bool IsLeaf { get; set; }
    }

    public class IOGroupItem : IOMonitorTreeItemBase
    {
        public IOGroupItem()
        {
            Children = new ObservableCollection<IOMonitorTreeItemBase>();
            IsLeaf = false;
        }
    }

    public class IOTypeItem : IOMonitorTreeItemBase
    {
        public IODeviceType DeviceType { get; set; }

        public IOTypeItem()
        {
            Children = new ObservableCollection<IOMonitorTreeItemBase>();
            IsLeaf = true;
        }
    }


}