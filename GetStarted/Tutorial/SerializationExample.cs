using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace TestSerialization
{
  // 1. 定义要序列化的类
  public class UserProfile
  {
    [JsonPropertyName("user_name")]
    public string Name { get; set; }

    [JsonPropertyName("user_age")]
    public int Age { get; set; }

    [JsonIgnore]
    public string TempData { get; set; }

    // 用于XML序列化的默认构造函数
    public UserProfile() { }

    public UserProfile(string name, int age)
    {
      Name = name;
      Age = age;
    }
  }

  public class SerializationDemo
  {
    public static void JsonSerializationExample()
    {
      Console.WriteLine("=== JSON序列化示例 ===\n");

      var user = new UserProfile("张三", 25) { TempData = "临时数据" };

      // 序列化
      string jsonString = JsonSerializer.Serialize(user, new JsonSerializerOptions
      {
        WriteIndented = true
      });
      Console.WriteLine("序列化结果:");
      Console.WriteLine(jsonString);

      // 反序列化
      var deserializedUser = JsonSerializer.Deserialize<UserProfile>(jsonString);
      Console.WriteLine("\n反序列化后的对象:");
      Console.WriteLine($"姓名: {deserializedUser.Name}, 年龄: {deserializedUser.Age}");
    }

    public static void FileSerializationExample()
    {
      Console.WriteLine("\n=== 文件序列化示例 ===\n");

      var user = new UserProfile("李四", 30);
      string fileName = "user.json";

      // 序列化到文件
      File.WriteAllText(fileName, JsonSerializer.Serialize(user));
      Console.WriteLine($"已将用户数据保存到文件: {fileName}");

      // 从文件反序列化
      if (File.Exists(fileName))
      {
        string jsonString = File.ReadAllText(fileName);
        var loadedUser = JsonSerializer.Deserialize<UserProfile>(jsonString);
        Console.WriteLine($"从文件加载的用户: {loadedUser.Name}, {loadedUser.Age}岁");
      }
    }

    public static void Test()
    {
      Console.WriteLine("序列化和反序列化示例\n");
      // JsonSerializationExample();
      FileSerializationExample();
    }
  }
}