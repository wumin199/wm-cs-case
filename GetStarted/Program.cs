using System;
using System.Threading;
using GetStarted;
using TestLib;
using TestNuGet;
using DIBasics;

class Program
{
  static void Main()
  {
    // Console.WriteLine("Hello World, That's Get Started!");

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
    // TestAsyncHelper.CheckRunningTasks();


    // var test = new TestBasic();
    // test.Test1();

    // var test = new TestJson();
    // test.Test1();

    // var test = new TestType();
    // test.Test1();

    var test = new DIBasics.Example.Test1();
    test.Run();
  }


  public void QuickTest()
  {
    var limit = 3;
    int[] source = { 0, 1, 2, 3, 4, 5 };
    var query = from item in source
                where item <= limit
                select item;
  }
}