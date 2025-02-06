using System;
using System.Threading;
using GetStarted;

class Program
{
  static void Main()
  {
    Console.WriteLine("Hello World, That's Get Started!");

    // GetStarted.ThreadStaticExample.Run();

    var threadExample = new ThreadExample();
    threadExample.Run();
  }
}

