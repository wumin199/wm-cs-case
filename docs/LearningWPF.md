

## 关键词

> 
> 快速了解：概念背景、概念含义/定义(是什么)，目的(设计/概念目的)、代码案例(非常重要)
> 
> 如果不能快速了解，则说明材料不合适
> 

数据绑定、MVVM、IOC注入、DevExpress、依赖属性、控件的docker、类型转换器、Resource资源、Template

详见notion中的笔记


- [桌面指南 (WPF .NET)](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/overview/?view=netdesktop-8.0)
- [WPF 窗口概述 (WPF .NET)](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/windows/?view=netdesktop-9.0)(很多基础概念)


## 重要概念

### 依赖属性


[WPF - Dependency Properties](https://www.tutorialspoint.com/wpf/wpf_dependency_properties.htm)

[依赖项属性概述 （WPF .NET）](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/properties/dependency-properties-overview?view=netdesktop-8.0)

[DependencyProperty 类](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.dependencyproperty?view=windowsdesktop-9.0)

依赖属性：表示可通过方法（如样式、数据绑定、动画和继承）设置的属性。 C#中不是所有属性都是依赖属性，但有一部分已经是了。

依赖属性的目的是提供一种方法，以便根据其他输入的值计算属性的值，例如：

- 系统属性，如主题和用户首选项。
- 即时属性确定机制，如数据绑定和动画/情节剧本。
- 多用途模板，如资源和样式。
- 通过父子关系与元素树中的其他元素已知的值。


此外，依赖属性还可以提供：

- 独立验证。
- 默认值。
- 用于监控其他属性更改的回调。
- 一个可以根据运行时信息强制属性值的系统。


## Quick Start

```bash
cd wm-cs-case
dotnet new list
dotnet new wpf -o FirstWPF
dotnet build
dotnet run
```

教程：

- [教程：使用 C 创建 WPF 应用程序](https://learn.microsoft.com/zh-cn/visualstudio/get-started/csharp/tutorial-wpf)(拖拽控件)
- [教程：使用 .NET 创建新的 WPF 应用](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/get-started/create-app-visual-studio?view=netdesktop-8.0)(直接编写xaml)
- [WPF-教程](https://www.tutorialspoint.com/wpf/index.htm)
