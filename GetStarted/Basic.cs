using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using UtilityLibraries; // 需要先引入 UtilityLibraries 项目
using Newtonsoft.Json;

namespace GetStarted
{
  public class Basic
  {
    public static void ReadLine()
    {
      Console.WriteLine("What is your name?");
      var name = Console.ReadLine();
      var currentDate = DateTime.Now;
      Console.WriteLine($"{Environment.NewLine}Hello, {name}, on {currentDate:d} at {currentDate:t}!");
      Console.Write($"{Environment.NewLine}Press Enter to exit...");
      // Console.Read();
      Console.ReadKey(true);
    }

    public string Greet(string name)
    {
      return $"Hello, {name}!";
    }

    public static void WorkWithNumbers()
    {

      // 如何使用 C# 中的整数和浮点数
      // https://learn.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/tutorials/numbers-in-csharp-local

      int max = int.MaxValue;
      int min = int.MinValue;
      Console.WriteLine($"The range of integers is {min} to {max}");


      double max_d = double.MaxValue;
      double min_d = double.MinValue;
      Console.WriteLine($"The range of double is {min_d} to {max_d}");

      decimal min_dec = decimal.MinValue;
      decimal max_dec = decimal.MaxValue;
      Console.WriteLine($"The range of the decimal type is {min_dec} to {max_dec}");

      double a = 1.0;
      double b = 3.0;
      Console.WriteLine(a / b);

      decimal c = 1.0M;
      decimal d = 3.0M;
      Console.WriteLine(c / d);
    }

    public static void StringFormating()
    {
      var name = "Mark";
      var date = DateTime.Now;
      // Composite formatting:
      Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
      // String interpolation:
      Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
      // Both calls produce the same output that is similar to:
      // Hello, Mark! Today is Wednesday, it's 19:40 now.
    }


    public static void TestList()
    {
      List<string> names = ["<name>", "Ana", "Felipe"];
      foreach (var name in names)
      {
        Console.WriteLine($"Hello {name.ToUpper()}!");
      }
      Console.WriteLine();
      names.Add("Maria");
      names.Add("Bill");
      names.Remove("Ana");
      foreach (var name in names)
      {
        Console.WriteLine($"Hello {name.ToUpper()}!");
      }
    }
  }

  public class ThreadStaticExample
  {
    static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

    public static void Run()
    {
      Thread workerThread = new Thread(Worker);
      workerThread.Start();

      Console.WriteLine("Main thread is waiting for the worker thread to signal...");
      autoResetEvent.WaitOne(); // 等待信号
      Console.WriteLine("Main thread received the signal.");

      workerThread.Join();
    }

    static void Worker()
    {
      Console.WriteLine("Worker thread is doing some work...");
      Thread.Sleep(2000); // 模拟工作
      Console.WriteLine("Worker thread is signaling the main thread.");
      autoResetEvent.Set(); // 发送信号
    }
  }

  public class ThreadExample
  {
    private AutoResetEvent autoResetEvent = new AutoResetEvent(false);

    public void Run()
    {
      Thread workerThread = new Thread(Worker);
      workerThread.Start();

      Console.WriteLine("Main thread is waiting for the worker thread to signal...");
      autoResetEvent.WaitOne(); // 等待信号
      Console.WriteLine("Main thread received the signal.");

      workerThread.Join();
    }

    private void Worker()
    {
      Console.WriteLine("Worker thread is doing some work...");
      Thread.Sleep(2000); // 模拟工作
      Console.WriteLine("Worker thread is signaling the main thread.");
      autoResetEvent.Set(); // 发送信号
    }
  }

  public class AsyncTaskExample
  {
    private async Task<int> PerformTaskWithResultAsync()
    {
      await Task.Delay(1000); // 模拟异步操作
      return 42; // 返回结果
    }


    // Action是delegate的意思，一般用作回调函数等
    public async void Button_Click(object sender, EventArgs e)
    {
      Console.WriteLine("Starting async operation...");
      int result = await PerformTaskWithResultAsync();
      Console.WriteLine($"Async operation completed with result: {result}");
    }

    public void TestAsyncMethod()
    {
      Button_Click(this, EventArgs.Empty);
      Console.WriteLine("Button_Click method has been called.");
    }
  }


  public class AsyncHelper
  {
    private static List<Task> TaskList = new List<Task>();
    public async static void ExecuteAsync(Action async, Action<Exception> finish)
    {
      var t = Task.Run(async);
      TaskList.Add(t);
      try
      {
        await t;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception in AsyncHelper: " + ex.Message);
      }
      finally
      {
        if (finish != null)
          finish.Invoke(t.Exception); // but in finish method
        TaskList.Remove(t);
      }
    }

    public static void ExecuteAsync(Action method)
    {
      ExecuteAsync(method, null);
    }

    public static bool HasRunningTasks()
    {
      return TaskList.Any(t => t.Status == TaskStatus.Running);
    }
  }

  public class TestAsyncHelper
  {
    public static void ExampleUsage()
    {
      Action asyncTask = () =>
      {
        Console.WriteLine("Async task started.");
        Thread.Sleep(1000);
        Console.WriteLine("Async task completed.");
      };

      Action<Exception> finishCallback = (ex) =>
      {
        if (ex != null)
        {
          Console.WriteLine("Exception in async task: " + ex.Message);
        }
        else
        {
          Console.WriteLine("Async task finished successfully.");
        }
      };

      AsyncHelper.ExecuteAsync(asyncTask, finishCallback);
      Thread.Sleep(2000);
      Console.WriteLine("done");
    }

    public static void ExampleUsageWithoutExceptionHandling()
    {
      Action asyncTask = () =>
      {
        Thread.Sleep(1000);
        Console.WriteLine("Async task completed.");
      };

      AsyncHelper.ExecuteAsync(asyncTask);
      Thread.Sleep(2000);
      Console.WriteLine("done");
    }

    public static void CheckRunningTasks()
    {
      Action asyncTask = () =>
      {
        Thread.Sleep(1000);
        Console.WriteLine("Async task completed.");
      };

      AsyncHelper.ExecuteAsync(asyncTask);
      while (AsyncHelper.HasRunningTasks())
      {
        Console.WriteLine("Waiting for async task to complete...");
        Thread.Sleep(100);
      }
      Console.WriteLine("All async tasks have been completed.");
    }
  }

  [Serializable]
  public class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }

  public class SerializationHelper
  {
    public static void SerializePerson(string filePath, Person person)
    {

      var options = new JsonSerializerOptions { WriteIndented = true };
      string jsonString = System.Text.Json.JsonSerializer.Serialize(person, options);
      File.WriteAllText(filePath, jsonString);
      ;
    }

    public static Person DeserializePerson(string filePath)
    {
      string jsonString = File.ReadAllText(filePath);
      return System.Text.Json.JsonSerializer.Deserialize<Person>(jsonString) ?? new Person();
    }
  }

  public class TestReflector
  {
    public static void Test1()
    {
      var person = new Person { Name = "Alice", Age = 30 };
      SerializationHelper.SerializePerson("person.json", person);
      Person newPerson = SerializationHelper.DeserializePerson("person.json");
      Console.WriteLine($"Name: {newPerson.Name}, Age: {newPerson.Age}");
    }
  }

  public class TestDelegate()
  {
    // 和 C++的std::function类似
    public delegate void MyDelegate(string message);
    public class MyClass
    {
      public void MyMethod(string message)
      {
        Console.WriteLine(message);
      }
    }

    public void Test1()
    {
      var my_class = new MyClass();
      MyDelegate my_delegate = my_class.MyMethod;
      my_delegate("Hello, delegate!");
    }
  }
}

namespace TestLib
{
  public class TestBasic
  {
    int row = 0;
    void ResetConsole()
    {
      if (row > 0)
      {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
      }
      Console.Clear();
      Console.WriteLine($"{Environment.NewLine}Press <Enter> only to exit; otherwise, enter a string and press <Enter>:{Environment.NewLine}");
      row = 3;
    }

    public void Test1()
    {
      do
      {
        if (row == 0 || row >= 25) ResetConsole();

        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) break;
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Begins with uppercase? " +
        $"{(input.StartsWithUpper() ? "Yes" : "No")}");
        Console.WriteLine();
        row += 3;
      } while (true);
    }

  }

}

namespace TestNuGet
{
  public class Account
  {
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime DOB { get; set; }
  }
  public class TestJson
  {
    public void Test1()
    {
      var account = new Account
      {
        Name = "John Doe",
        Email = "wmin199@126.com",
        DOB = new DateTime(1990, 5, 18, 0, 0, 0, DateTimeKind.Utc)
      };
      string json = JsonConvert.SerializeObject(account, Formatting.Indented);
      Console.WriteLine(json);
    }
  }
}