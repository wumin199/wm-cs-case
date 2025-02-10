# wm-cs-case

## Qucick Search 

zh-cn/en-us

## 20250210
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


  



