using System;
using System.Reflection;

namespace TestReflection
{
  // 1. 定义一个示例类
  public class Student
  {
    public string Name { get; set; }
    private int age;

    public Student()
    {
      Name = "Unknown";
      age = 0;
    }

    public Student(string name, int age)
    {
      this.Name = name;
      this.age = age;
    }

    private void Study(string subject)
    {
      Console.WriteLine($"{Name} is studying {subject}");
    }
  }

  // 2. 反射示例类
  public class ReflectionDemo
  {
    public static void ShowTypeInfo()
    {
      Console.WriteLine("\n=== 类型信息示例 ===");
      Type studentType = typeof(Student);
      Console.WriteLine($"类名: {studentType.Name}");
      Console.WriteLine($"完整类名: {studentType.FullName}");
      Console.WriteLine($"命名空间: {studentType.Namespace}");
    }

    public static void ShowMembers()
    {
      Console.WriteLine("\n=== 成员信息示例 ===");
      Type studentType = typeof(Student);

      // 显示公共属性
      Console.WriteLine("公共属性:");
      foreach (PropertyInfo property in studentType.GetProperties())
      {
        Console.WriteLine($"- {property.PropertyType.Name} {property.Name}");
      }

      // 显示所有字段（包括私有）
      Console.WriteLine("\n所有字段:");
      var fields = studentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
      foreach (FieldInfo field in fields)
      {
        Console.WriteLine($"- {field.FieldType.Name} {field.Name}");
      }
    }

    public static void DynamicObjectCreation()
    {
      Console.WriteLine("\n=== 动态创建对象示例 ===");
      Type studentType = typeof(Student);

      // 使用默认构造函数
      Console.WriteLine("使用默认构造函数:");
      object student1 = Activator.CreateInstance(studentType);
      PropertyInfo nameProperty = studentType.GetProperty("Name");
      nameProperty.SetValue(student1, "Alice");
      Console.WriteLine($"学生1名字: {nameProperty.GetValue(student1)}");

      // 使用带参数的构造函数
      Console.WriteLine("\n使用带参数的构造函数:");
      object student2 = Activator.CreateInstance(studentType, new object[] { "Bob", 20 });
      Console.WriteLine($"学生2名字: {nameProperty.GetValue(student2)}");
    }

    public static void AccessPrivateMembers()
    {
      Console.WriteLine("\n=== 访问私有成员示例 ===");
      Type studentType = typeof(Student);
      object student = Activator.CreateInstance(studentType, new object[] { "Charlie", 22 });

      // 访问私有字段
      FieldInfo ageField = studentType.GetField("age", BindingFlags.NonPublic | BindingFlags.Instance);
      Console.WriteLine($"私有字段age的值: {ageField.GetValue(student)}");

      // 调用私有方法
      MethodInfo studyMethod = studentType.GetMethod("Study", BindingFlags.NonPublic | BindingFlags.Instance);
      studyMethod.Invoke(student, new object[] { "反射编程" });
    }
  }

  // 3. 测试类
  public class Test
  {
    public static void RunAllExamples()
    {
      Console.WriteLine("========= 反射完整示例演示 =========");

      // 展示类型信息
      // ReflectionDemo.ShowTypeInfo();

      // // 展示成员信息
      // ReflectionDemo.ShowMembers();

      // // 展示动态创建对象
      // ReflectionDemo.DynamicObjectCreation();

      // // 展示访问私有成员
      ReflectionDemo.AccessPrivateMembers();

      Console.WriteLine("\n========= 示例演示结束 =========");
    }
  }
}