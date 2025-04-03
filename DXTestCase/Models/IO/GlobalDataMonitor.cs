using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXTestCase.Models.IO
{
    public enum GlobalDataType
    {
        UserInt,                // -2,147,483,648-2,147,483,647
        UserReal,               // -999999.999999-999999.999999
        UserByte,               // 0-255
        UserUShort,             // 0-65535
        PR,                     //  x,y,z,rx,ry,rz,mword_1, mword_2, mword_3
        SysInt,                 // 
        SysReal,                // 
    }

    public static class GlobalDataTypeExtensions
    {
        private static readonly Dictionary<GlobalDataType, string> Names = new Dictionary<GlobalDataType, string>
        {
            { GlobalDataType.UserInt, "用户整型" },
            {GlobalDataType.UserReal, "用户实型" },
            {GlobalDataType.UserByte, "用户字节" },
            {GlobalDataType.UserUShort, "用户短整型" },
            {GlobalDataType.PR, "位置变量" },
            {GlobalDataType.SysInt, "系统整型" },
            {GlobalDataType.SysReal, "系统实型" },
        };

        public static string GetDisplayName(this GlobalDataType dataType)
        {
            return Names.TryGetValue(dataType, out string name) ? name : dataType.ToString();
        }

        public static GlobalDataType? GetDataTypeFromName(string name)
        {
            foreach (var pair in Names)
            {
                if (pair.Value.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }


    public static class GlobalDataTypeCount
    {
        public const int MAX_USER_INT_COUNT = 512;
        public const int MAX_USER_REAL_COUNT = 512;
        public const int MAX_USER_BYTE_COUNT = 512;
        public const int MAX_USER_USHORT_COUNT = 512;
        public const int MAX_PR_COUNT = 100;
        public const int MAX_SYS_INT_COUNT = 100;
        public const int MAX_SYS_REAL_COUNT = 100;
    }


}