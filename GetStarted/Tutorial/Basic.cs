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
      Thread.Sleep(5000);
      Console.WriteLine("Starting async operation..."); // 这一句是同步的，await才是异步的
      int result = await PerformTaskWithResultAsync();
      Console.WriteLine($"Async operation completed with result: {result}");
    }

    public void TestAsyncMethod()
    {
      Button_Click(this, EventArgs.Empty);
      Console.WriteLine("Button_Click method has been called.");
      Task.Delay(10000).Wait();
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

  public class BaseC { }

  public class DerivedC : BaseC { }

  public class TestType
  {
    public void Test1()
    {
      object b = new BaseC();
      Console.WriteLine(b is BaseC);  // output: True
      Console.WriteLine(b is DerivedC);  // output: False

      object d = new DerivedC();
      Console.WriteLine(d is BaseC);  // output: True
      Console.WriteLine(d is DerivedC); // output: True

      int i = 27;
      Console.WriteLine(i is System.IFormattable);  // output: True

      object iBoxed = i;
      Console.WriteLine(iBoxed is int);  // output: True
      Console.WriteLine(iBoxed is long);  // output: False

      int ii = 23;
      object iiBoxed = ii;
      int? jNullable = 7;
      if (iiBoxed is int aa && jNullable is int bb)
      {
        Console.WriteLine(aa + bb);  // output 30
      }
    }

  }

  public class Publisher
  {
    public delegate void PropertyChangedHandler(string message);
    public event PropertyChangedHandler PropertyChanged;

    public void Notify(string message)
    {
      PropertyChanged?.Invoke(message);
    }
  }

  public class Subscriber
  {
    public void OnChanged(string message)
    {
      Console.WriteLine("Event received: " + message);
    }
  }

  public class TestEvent
  {
    public void Test1()
    {
      var publisher = new Publisher();
      var subscriber = new Subscriber();
      publisher.PropertyChanged += subscriber.OnChanged;
      publisher.Notify("Hello, event!");
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


namespace TestTask
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using System.Linq;

  // 1. Task基础示例类
  public class TaskExample
  {
    public void BasicTask()
    {
      Task task = Task.Run(() =>
      {
        Console.WriteLine("Task正在运行...");
        Thread.Sleep(5000);
        Console.WriteLine("Task完成");
      });
      task.Wait();
    }

    public async Task<int> TaskWithResult()
    {
      return await Task.Run(() =>
      {
        Thread.Sleep(3000);
        return 42;
      });
    }
  }

  // 2. async/await示例类
  public class AsyncAwaitExample
  {
    public async Task DoWorkAsync()
    {
      Console.WriteLine("开始工作");
      await Task.Delay(3000);
      Console.WriteLine("工作完成");
    }

    public async Task CallAsyncMethods()
    {
      Console.WriteLine("方法调用开始");
      await DoWorkAsync();
      Console.WriteLine("方法调用结束");
    }
  }

  // 3. Task状态和异常示例类
  public class TaskStatusExample
  {
    public async Task TaskStatusAndException()
    {
      Task successTask = Task.Run(() =>
      {
        Console.WriteLine("成功Task执行中");
        Thread.Sleep(3000);
      });
      await successTask;
      Console.WriteLine($"Task状态: {successTask.Status}");

      Task failedTask = Task.Run(() =>
      {
        throw new Exception("Task失败");
      });
      try
      {
        await failedTask;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Task异常: {ex.Message}");
        Console.WriteLine($"Task状态: {failedTask.Status}");
      }
    }
  }

  // 4. Task组合示例类
  public class TaskCombinationExample
  {
    public async Task CombineTasks()
    {
      Task task1 = Task.Run(() =>
      {
        Thread.Sleep(1000);
        Console.WriteLine("Task 1完成");
      });

      Task task2 = Task.Run(() =>
      {
        Thread.Sleep(4000);
        Console.WriteLine("Task 2完成");
      });

      await Task.WhenAll(task1, task2);
      Console.WriteLine("所有Task完成");

      Task task3 = Task.Run(() =>
      {
        Thread.Sleep(3000);
        Console.WriteLine("Task 3完成");
      });

      Task task4 = Task.Run(() =>
      {
        Thread.Sleep(1000);
        Console.WriteLine("Task 4完成");
      });

      await Task.WhenAny(task3, task4);
      Console.WriteLine("有一个Task完成");
    }
  }

  // 5. Task取消示例类
  public class TaskCancellationExample
  {
    public async Task CancellableTask()
    {
      using var cts = new CancellationTokenSource();

      Task task = Task.Run(() =>
      {
        for (int i = 0; i < 10; i++)
        {
          if (cts.Token.IsCancellationRequested)
          {
            Console.WriteLine("Task被取消");
            return;
          }
          Thread.Sleep(500);
          Console.WriteLine($"执行进度: {i + 1}");
        }
      }, cts.Token);

      await Task.Delay(3000);
      cts.Cancel();

      try
      {
        await task;
      }
      catch (OperationCanceledException)
      {
        Console.WriteLine("捕获到取消异常");
      }
    }
  }

  // 6. Task超时示例类
  public class TaskTimeoutExample
  {
    public async Task TaskWithTimeout()
    {
      using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

      try
      {
        await Task.Run(() =>
        {
          for (int i = 0; i < 10; i++)
          {
            cts.Token.ThrowIfCancellationRequested();
            Thread.Sleep(1000);
            Console.WriteLine($"执行进度: {i + 1}");
          }
        }, cts.Token);
      }
      catch (OperationCanceledException)
      {
        Console.WriteLine("Task超时");
      }
    }
  }

  // 7. 异步数据服务示例类
  public class AsyncDataService
  {
    private readonly Random _random = new Random();

    public async Task<List<int>> GetDataAsync(int count)
    {
      return await Task.Run(() =>
      {
        var data = new List<int>();
        for (int i = 0; i < count; i++)
        {
          Thread.Sleep(1000);
          data.Add(_random.Next(100));
        }
        return data;
      });
    }

    public async Task ProcessDataAsync(List<int> data)
    {
      await Task.Run(() =>
      {
        foreach (var item in data)
        {
          Thread.Sleep(100);
          Console.WriteLine($"处理数据: {item}");
        }
      });
    }
  }



  public class Test
  {
    public async Task Test1()
    {
      Console.WriteLine("=== 异步编程完整示例 ===");

      // 1. 基础Task示例
      // Console.WriteLine("\n1. 基础Task示例:");
      // var taskExample = new TaskExample();
      // taskExample.BasicTask();
      // int result = await taskExample.TaskWithResult();
      // Console.WriteLine("done");
      // Console.WriteLine($"返回值: {result}");

      // // 2. async/await示例
      // Console.WriteLine("\n2. async/await示例:");
      // var asyncExample = new AsyncAwaitExample();
      // await asyncExample.CallAsyncMethods();

      // // 3. Task状态和异常示例
      // Console.WriteLine("\n3. Task状态和异常示例:");
      // var statusExample = new TaskStatusExample();
      // await statusExample.TaskStatusAndException();

      // // 4. Task组合示例
      // Console.WriteLine("\n4. Task组合示例:");
      // var combinationExample = new TaskCombinationExample();
      // await combinationExample.CombineTasks();

      // // 5. Task取消示例
      // Console.WriteLine("\n5. Task取消示例:");
      // var cancellationExample = new TaskCancellationExample();
      // await cancellationExample.CancellableTask();

      // // 6. Task超时示例
      // Console.WriteLine("\n6. Task超时示例:");
      // var timeoutExample = new TaskTimeoutExample();
      // await timeoutExample.TaskWithTimeout();

      // // 7. 异步数据服务示例
      Console.WriteLine("\n7. 异步数据服务示例:");
      var dataService = new AsyncDataService();
      Console.WriteLine("开始获取");
      var data = await dataService.GetDataAsync(5);
      Console.WriteLine("获取数据ing");
      await dataService.ProcessDataAsync(data);

      // Console.WriteLine("\n所有示例执行完成");
    }
  }
}