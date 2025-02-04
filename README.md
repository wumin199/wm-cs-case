## wm-cs-case
wcc: csharp test


包含：C#，WPF

关键问题：

1. IOC注入和prisma框架
2. 基础语法（委托等）
3. 单元测试
4. 调试
5. 委托
6. NuGet
7. 打包
8. 格式化


## Quick Start

```shell
dotnet --version
dotnet --list-sdks
dotnet --info
dotnet -h

# first console project
dotnet new console -o sample1
cd sample1
dotnet run
```

VSCode中需要安装3个插件：

- `C# Dev Kit`
- `C#`
- `IntelliCode for C# Dev Kit`

VSCode中的一些常见设置：

左下角 `Manage` -> `Settings`，做一些设置，如Tab Size等，以及这个插件(C#...)的一些设置。

这里设置了Tab Size为2，同时设置了C#的部分[Inlay Hints](https://devblogs.microsoft.com/java-ch/java-on-visual-studio-code-2023-07/)。这部分参考了 [Installing C# Dev Kit](https://www.youtube.com/watch?v=S4Rks1L03LI)


使用VSCode的引导功能：

`Ctrl + Shift + P` -> `Welcome: Open Walkthrough` -> `Get Started with C# Dev Kit`: 然后选择对应的操作

直接创建.NET项目:

`Ctrl + Shift + P` -> `.NET:New Project` -> `Console app`

编译.NET项目：
<!--  -->
`Ctrl + Shift + P` -> `.NET:Build` ，这会编译Project下的所有代码。

Clean .NET项目：

`Ctrl + Shift + P` -> `.NET:Clean`


使用流程：

可以创建好几个project，比如 `ConsoleApp1`、`GetStarted`和 `get_started`

这时候可以在 `wm-cs-case.sln` 中看到有3个项目的信息。

使用`.NET:Build`编译所有这3个项目。然后cd到bin目录下执行程序。

如果编译失败，可以先 Clean 再编译。

如果是删除了其中的某个项目，重新build会失败，这时候可以手动更新 `wm-cs-case.sln` 中不存在的项目，或者是在VSCode的`EXPLORER` -> `SOLUTION EXPLORER`下删掉对应的项目，这回自动更新 `wm-cs-case.sln`




也可以把光标放到main文件下，如何按 `F5`，或者 `Run` -> `Start Debugging`

Clean .NET项目，包括删除多余项目后，重新更新 `.sln`等：







## 参考资料


- C# in Vscode（突出VSCode相关，重点不是C#学习）
  - [Introductory Videos for C# in VS Code](https://code.visualstudio.com/docs/csharp/introvideos-csharp)(左侧菜单栏有全部C# with VSCode)
  - [Getting Started with C# in VS Code](https://code.visualstudio.com/docs/csharp/get-started)（只教如何用Vscode编写C#，不教C#本身）
  - [Working with C#](https://code.visualstudio.com/docs/languages/csharp)
  - [教程：使用 Visual Studio Code 创建 .NET 控制台应用程序](https://learn.microsoft.com/zh-cn/dotnet/core/tutorials/with-visual-studio-code)

- [.NET 基础知识文档](https://learn.microsoft.com/zh-cn/dotnet/fundamentals/)
- [.NET 入门](https://learn.microsoft.com/zh-cn/dotnet/core/get-started)
- [.NET 分发打包](https://learn.microsoft.com/zh-cn/dotnet/core/distribution-packaging)
- [.NET CLI 概述](https://learn.microsoft.com/zh-cn/dotnet/core/tools/)
- [教程：使用 Visual Studio Code 创建 .NET 控制台应用程序](https://learn.microsoft.com/zh-cn/dotnet/core/tutorials/with-visual-studio-code)

- [Add .gitignore for C Sharp](https://github.com/github/gitignore/pull/4430/files)
