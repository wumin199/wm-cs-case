# wm-cs-case

## Qucick Search 

zh-cn/en-us

## feature1
1. 依赖注入和通用设计原则
  [.NET 依赖项注入](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection)， 

  参考：

  - [了解 .NET 中的依赖关系注入基础知识](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection-basics)
  - [体系结构原则](https://learn.microsoft.com/zh-cn/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)


  依赖关系注入是一种设计模式，可用于删除硬编码的依赖关系，并使应用程序更易于维护和测试。 DI 是一种在类与其依赖关系之间实现控制反转 (IoC) 的技术

  C++/python中依赖注入相对用的少，C#中用的多。依赖注入主要是在调用/构造时，申明函数变量用的是接口/基类。

  在C#中，使用“Service”这样的名字来命名依赖注入的类是一种常见的约定。


  

  .NET 中的依赖关系注入是框架的内置部分，与配置、日志记录和选项模式一样。

  see: `DIP.cs`

  用了依赖注入，就用依赖注入来获得单例服务，不用手动用静态类来常见单例模式了。
  
  
  多个服务，怎么注册使用。

  DI的使用。类的数据成员变量，封装成DI注入方式。当然不是每个数据成员都要封装，而是类的重要数据成员！

  AddTransit: 临时变量
  AddScoped：区域变量，类似 {}同一个作用域就是同一个类
  AddSingleton: 全局变量，单例模式

  用法可以参考：DIBasics.Example.Test3(); Test1和2的案例不是很好。 [C#依赖注入3种实现方法](https://blog.csdn.net/blu_e__heart/article/details/135725766)

  // 只有建構式時會給值，所以可以順手加上 readonly 防止被變動
    private readonly ISpell _spell;
  加readonly 可以和一般的属性区分

  不懂的地方，如果我需要输入一般的参数，怎么混合使用？