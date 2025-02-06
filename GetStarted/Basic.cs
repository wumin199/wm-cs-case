using System;
using System.Threading;

namespace GetStarted
{
  public class Basic
  {
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
}

