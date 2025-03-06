using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.IO;

namespace TestDynamicConfig
{
  // 用特性标记配置项的UI展示方式
  [AttributeUsage(AttributeTargets.Property)]
  public class ConfigUIAttribute : Attribute
  {
    public string Label { get; }        // 显示名称
    public string ControlType { get; }  // 控件类型
    public string Description { get; }  // 描述信息

    public ConfigUIAttribute(string label, string controlType, string description = "")
    {
      Label = label;
      ControlType = controlType;
      Description = description;
    }
  }

  // 数据库配置类
  public class DatabaseConfig
  {
    [ConfigUI("服务器地址", "TextBox", "数据库服务器的IP地址或域名")]
    public string Server { get; set; } = "localhost";

    [ConfigUI("端口号", "NumberBox", "数据库服务端口")]
    public int Port { get; set; } = 3306;

    [ConfigUI("启用SSL", "CheckBox", "是否使用SSL加密连接")]
    public bool UseSSL { get; set; } = false;

    [ConfigUI("连接超时", "Slider", "连接超时时间（秒）")]
    public int Timeout { get; set; } = 30;
  }

  public class DynamicConfigDemo
  {
    // 模拟生成配置界面
    public static void GenerateConfigUI(object config)
    {
      Console.WriteLine("=== 动态生成配置界面 ===\n");

      foreach (PropertyInfo prop in config.GetType().GetProperties())
      {
        var uiAttr = prop.GetCustomAttribute<ConfigUIAttribute>();
        if (uiAttr != null)
        {
          // 获取当前值
          object value = prop.GetValue(config);

          // 模拟显示配置项
          Console.WriteLine($"【{uiAttr.Label}】({uiAttr.ControlType})");
          Console.WriteLine($"说明: {uiAttr.Description}");
          Console.WriteLine($"当前值: {value}\n");
        }
      }
    }

    // 保存配置到文件
    public static void SaveConfig(object config, string fileName)
    {
      string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions
      {
        WriteIndented = true
      });
      File.WriteAllText(fileName, jsonString);
      Console.WriteLine($"配置已保存到: {fileName}");
    }

    // 从文件加载配置
    public static T LoadConfig<T>(string fileName) where T : new()
    {
      if (File.Exists(fileName))
      {
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<T>(jsonString);
      }
      Console.WriteLine($"配置文件不存在: {fileName}");
      return new T();
    }

    public static void Test()
    {
      Console.WriteLine("动态配置界面示例\n");

      string configFile = "database.json";

      // 1. 创建默认配置并保存
      Console.WriteLine("1. 创建默认配置文件");
      var defaultConfig = new DatabaseConfig
      {
        Server = "db.example.com",
        Port = 5432,
        UseSSL = true,
        Timeout = 60
      };
      SaveConfig(defaultConfig, configFile);

      // 2. 显示保存的JSON内容
      Console.WriteLine("\n2. 配置文件内容:");
      if (File.Exists(configFile))
      {
        Console.WriteLine(File.ReadAllText(configFile));
      }

      // 3. 加载配置并显示界面
      Console.WriteLine("\n3. 加载配置并生成界面:");
      var loadedConfig = LoadConfig<DatabaseConfig>(configFile);
      GenerateConfigUI(loadedConfig);

      // 4. 修改配置并保存
      Console.WriteLine("4. 修改配置...");
      loadedConfig.Server = "new-server.example.com";
      loadedConfig.Port = 3306;
      SaveConfig(loadedConfig, configFile);

      // 5. 显示更新后的配置界面
      Console.WriteLine("\n5. 更新后的配置界面:");
      GenerateConfigUI(loadedConfig);
    }
  }
}