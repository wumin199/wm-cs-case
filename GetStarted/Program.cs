using System;
using System.Threading;
using GetStarted;

class Program
{
  static void Main()
  {
    Console.WriteLine("Hello World, That's Get Started!");

    // GetStarted.Basic.ReadLine();

    // GetStarted.ThreadStaticExample.Run();

    // var threadExample = new ThreadExample();
    // threadExample.Run();

    // var asyncTaskExample = new AsyncTaskExample();
    // asyncTaskExample.TestAsyncMethod();

    // TestReflector.Test1();

    // var test_del = new TestDelegate();
    // test_del.Test1();


    // TestAsyncHelper.ExampleUsage();
    // TestAsyncHelper.ExampleUsageWithoutExceptionHandling();
    TestAsyncHelper.CheckRunningTasks();
  }
}

