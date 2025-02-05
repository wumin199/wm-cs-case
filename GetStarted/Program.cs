
// System 命名空间中的 Console 类的 WriteLine 方法生成程序的输出。 此类由标准类库提供，默认情况下，每个 C# 程序中会自动引用这些库。

Console.WriteLine("Project: GetStarted");
Console.WriteLine("Hello, World!");


// WorkWithNumbers();

void WorkWithNumbers()
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


// WorkWithCondition();
void WorkWithCondition()
{
  int a = 5;
  int b = 3;
  if (a + b > 10)
    Console.WriteLine("The answer is greater than 10");
  else
    Console.WriteLine("The answer is not greater than 10");

  for (int index = 0; index < 10; index++)
  {
    Console.WriteLine($"Hello World! The index is {index}");
  }
}


// StringFormating();
void StringFormating()
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


TestList();

void TestList()
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