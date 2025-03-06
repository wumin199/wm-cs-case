using System;
using System.Reflection;

namespace TestSimpleReflection
{
  // 1. 简单的配置类
  public class AppConfig
  {
    public string ServerUrl { get; set; }
    public int Port { get; set; }
    public bool IsDebugMode { get; set; }

    public AppConfig()
    {
      // 默认值
      ServerUrl = "localhost";
      Port = 8080;
      IsDebugMode = false;
    }
  }

  // 2. 简单的用户界面类
  public class UserInfo
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
  }

  public class SimpleReflectionDemo
  {
    // 示例1：显示配置信息
    public static void ShowConfig(AppConfig config)
    {
      Console.WriteLine("=== 配置信息 ===");
      // 使用反射获取所有属性并显示
      foreach (PropertyInfo prop in config.GetType().GetProperties())
      {
        object value = prop.GetValue(config);
        Console.WriteLine($"{prop.Name} = {value}");
      }
    }

    // 示例2：从字符串更新配置
    public static void UpdateConfig(AppConfig config, string propertyName, string value)
    {
      PropertyInfo prop = config.GetType().GetProperty(propertyName);
      if (prop != null)
      {
        // 将字符串值转换为属性的实际类型
        object convertedValue = Convert.ChangeType(value, prop.PropertyType);
        prop.SetValue(config, convertedValue);
        Console.WriteLine($"已更新 {propertyName} = {value}");
      }
    }

    // 示例3：简单的界面数据显示
    public static void DisplayUserInfo(UserInfo user)
    {
      Console.WriteLine("\n=== 用户信息 ===");
      foreach (PropertyInfo prop in user.GetType().GetProperties())
      {
        Console.WriteLine($"{prop.Name}: {prop.GetValue(user)}");
      }
    }

    // 测试方法
    public static void RunDemo()
    {
      Console.WriteLine("反射在配置和界面中的应用示例\n");

      // 1. 配置文件示例
      var config = new AppConfig();
      ShowConfig(config);

      // 2. 更新配置示例
      Console.WriteLine("\n更新配置:");
      UpdateConfig(config, "ServerUrl", "example.com");
      UpdateConfig(config, "Port", "9090");
      ShowConfig(config);

      // 3. 界面显示示例
      var user = new UserInfo
      {
        Name = "张三",
        Email = "zhangsan@example.com",
        Age = 25
      };
      DisplayUserInfo(user);
    }
  }
}