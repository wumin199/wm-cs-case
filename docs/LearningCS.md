
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