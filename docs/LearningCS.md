
基础数据类型及转化、事件、委托、异常、异步编程、日志、配置

单元测试、CICD(依赖包最好放在特定地方，然后编译完删除，否则会越来越大）、格式检查、创建类库、自动格式化、混合编程、类型转换、内置数据类型（值类型、引用类型），通用设计原则

特殊修饰符(in/out/new/ref/readonly)

https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/reference-types

linq

参考资料：

- [C# 语言介绍](https://learn.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/overview)（特别适合用来写教程）
- [Python 开发者学习 C# 的路线图](https://learn.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/tips-for-python-developers)(特别是和类比学习)

1. 无需在控制台应用程序项目中显式包含 Main 方法。 相反，可以使用顶级语句功能最大程度地减少必须编写的代码。
   1. [顶级语句 - 不使用 Main 方法的程序](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/program-structure/top-level-statements)
   2. 只有一个文件可以有顶级语句
2. dynamic	== System.Object
3. 在新式计算机上，使用双精度数字比使用单精度数字更为常见
4. decimal 类型的范围较小，但精度高于 double
5. 强制类型转换：is/as/typeof, Giraffe g2 = (Giraffe)a
6. struct是值类型，string/class/interface是引用类型
7. readonly相当于C++的const
   1. private readonly可以和一般的属性进行区分。防止被修改
   2. public readonly struct Coord;   var a = Coord(1,2); a.x = 5 (不可以)， a = b（不可以）  
8. class和struct
   **struct**：值类型，存储在栈(stack)上，不支持继承，适用于小型数据结构。 简单理解就是类似int。 如果侧重于类型的行为，请考虑定义一个类。结构具有类类型的大部分功能。 结构类型不能从其他类或结构类型继承，也不能作为类的基础类型。 但是，结构类型可以实现接口。
   **class**：引用类型，存储在堆(heap)上，支持继承，适用于大型数据结构。

9.  internal 不想被其他程序集使用，sealed不想被集成（重写）
10. 快捷键：ctor，prop/propp，override， 有时候tab一次，有时候tab2次
11. 默认的类属性一般是internal，需要跨程序集的话，修改为public
12. Func是委托，常用于lamda表达式。没有返回值的lamdab表达式，可以用Action表示。
    1.  委托是一个类型安全的函数指针，用于引用方法。委托可以有参数和返回值。你可以定义自己的委托类型，也可以使用预定义的委托类型（如 Func 和 Action）
    2.  •	委托是一个更广泛的概念，用于引用方法。
        •	Func 是一个特定类型的委托，用于表示具有返回值的方法。
        •	Action 是另一个特定类型的委托，用于表示没有返回值的方法。

13. class引用类型
    
    •	默认引用传递：类类型参数默认是通过引用传递的，方法内部可以修改对象的属性。
    •	in 关键字：明确表示参数是只读的，方法内部不能修改对象的属性。这有助于代码可读性、维护性和防止意外修改。
    •	使用 in 关键字可以提高代码的可读性和安全性，特别是在你希望明确表示方法不会修改传入对象的情况下。

    使用 out 关键字的主要原因是为了明确表示参数是用于输出的，并且方法内部必须为该参数赋值。虽然类类型参数默认是通过引用传递的，但 out 关键字有其特定的用途和好处。


    ref class数据类型，用的比较少，主要用在强调这个class也会被修改（如class =  ()）的情况。还要就是用于ref struct的场景。


14. Interface/Abstract/override
    
    我来通过一个实际开发中的例子来说明如何选择Interface和Abstract Class。假设我们要开发一个日志系统，需要支持不同的日志输出方式（文件、数据库、控制台等）。

    首先，让我们看看两种实现方式：

    1. 使用接口的方式：

        ```cs
        // 定义日志接口
        public interface ILogger
        {
            void Log(string message, LogLevel level);
            void LogError(string message);
            void LogWarning(string message);
            void LogInfo(string message);
        }

        // 文件日志实现
        public class FileLogger : ILogger
        {
            private readonly string _filePath;
            
            public FileLogger(string filePath)
            {
                _filePath = filePath;
            }

            public void Log(string message, LogLevel level)
            {
                // 实现文件写入逻辑
            }

            public void LogError(string message)
            {
                Log(message, LogLevel.Error);
            }

            public void LogWarning(string message)
            {
                Log(message, LogLevel.Warning);
            }

            public void LogInfo(string message)
            {
                Log(message, LogLevel.Info);
            }
        }

        // 数据库日志实现
        public class DatabaseLogger : ILogger
        {
            private readonly string _connectionString;
            
            public DatabaseLogger(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Log(string message, LogLevel level)
            {
                // 实现数据库写入逻辑
            }

            public void LogError(string message)
            {
                Log(message, LogLevel.Error);
            }

            public void LogWarning(string message)
            {
                Log(message, LogLevel.Warning);
            }

            public void LogInfo(string message)
            {
                Log(message, LogLevel.Info);
            }
        }
        ```

    2. 使用抽象类的方式:

        ```cs
              // 定义日志抽象类
        public abstract class LoggerBase
        {
            protected readonly string _source;
            
            protected LoggerBase(string source)
            {
                _source = source;
            }

            // 抽象方法，必须由子类实现
            public abstract void Log(string message, LogLevel level);

            // 共享实现的方法
            public virtual void LogError(string message)
            {
                Log(message, LogLevel.Error);
            }

            public virtual void LogWarning(string message)
            {
                Log(message, LogLevel.Warning);
            }

            public virtual void LogInfo(string message)
            {
                Log(message, LogLevel.Info);
            }

            // 共享的辅助方法
            protected string FormatMessage(string message, LogLevel level)
            {
                return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            }
        }

        // 文件日志实现
        public class FileLogger : LoggerBase
        {
            private readonly string _filePath;
            
            public FileLogger(string filePath) : base("File")
            {
                _filePath = filePath;
            }

            public override void Log(string message, LogLevel level)
            {
                var formattedMessage = FormatMessage(message, level);
                // 实现文件写入逻辑
            }
        }

        // 数据库日志实现
        public class DatabaseLogger : LoggerBase
        {
            private readonly string _connectionString;
            
            public DatabaseLogger(string connectionString) : base("Database")
            {
                _connectionString = connectionString;
            }

            public override void Log(string message, LogLevel level)
            {
                var formattedMessage = FormatMessage(message, level);
                // 实现数据库写入逻辑
            }
        }
        ```


    在这个例子中，我建议使用抽象类，原因如下：

    1. 共享实现
        - 所有日志实现都需要相同的格式化逻辑
        - 所有日志实现都需要相同的错误、警告、信息处理方法
        - 抽象类可以包含这些共享实现，避免代码重复
    2. 类型层次
        - 所有日志实现都是"Logger"的一种
        - 它们共享相同的基本属性和行为
        - 抽象类更好地表达了这种"是什么"的关系
    3. 构造函数和字段
        - 所有日志实现都需要源信息（source）
        - 抽象类可以包含构造函数和字段来管理这些共享状态

    但是，如果我们改变需求，比如：
    1. 需要支持日志轮转（Log Rotation）
    2. 需要支持异步日志
    3. 需要支持日志过滤
    那么使用接口可能更合适：

    ```cs
      // 定义多个接口
      public interface ILogger
      {
          void Log(string message, LogLevel level);
      }

      public interface ILogRotatable
      {
          void Rotate();
      }

      public interface IAsyncLogger
      {
          Task LogAsync(string message, LogLevel level);
      }

      public interface ILogFilterable
      {
          void SetFilter(Func<string, LogLevel, bool> filter);
      }

      // 实现类可以组合多个接口
      public class FileLogger : ILogger, ILogRotatable, IAsyncLogger
      {
          // 实现所有接口方法
      }

      public class DatabaseLogger : ILogger, IAsyncLogger, ILogFilterable
      {
          // 实现所有接口方法
      }
    ```

    选择建议：
    1. 使用抽象类当：
      - 多个类共享大量代码
      - 需要提供共享实现
      - 需要构造函数或字段
      - 表达"是什么"的关系
    2. 使用接口当：
      - 需要多重继承
      - 只需要定义契约
      - 需要更灵活的组合
      - 表达"能做什么"的关系
    3. 两者结合使用：
      - 可以用抽象类提供基础实现
      - 用接口定义扩展功能
      - 实现类继承抽象类并实现接口
    这样的设计既保持了代码的复用性，又提供了足够的灵活性。