
基础数据类型及转化、事件、委托、异常、异步编程、日志、配置

单元测试、CICD(依赖包最好放在特定地方，然后编译完删除，否则会越来越大）、格式检查、创建类库、自动格式化、混合编程、类型转换、内置数据类型（值类型、引用类型），通用设计原则

特殊修饰符(in/out/new/ref/readonly)

https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/ref

https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/builtin-types/struct

https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/types/

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
8. class和struct
   **struct**：值类型，存储在栈(stack)上，不支持继承，适用于小型数据结构。 简单理解就是类似int
   **class**：引用类型，存储在堆(heap)上，支持继承，适用于大型数据结构。

9.  internal 不想被其他程序集使用，sealed不想被集成（重写）