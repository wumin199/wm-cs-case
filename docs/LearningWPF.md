



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

style偏样式（如批量修改按钮的background和fontsize），template偏创建新控件（如自定义带圆弧的按钮）


- Styles can only change the appearance of your control with default properties of that control.

- With templates, you can access more parts of a control than in styles. You can also specify both existing and new behavior of a control.


- Button的基类是ButtonBase，ButtonBase的基类是ContentControl，ContentControl的基类是Control
  - [Control基类](https://www.wpfsoft.com/2023/08/22/1093.html)
  - [ContentControl类（内容控件）](https://www.wpfsoft.com/2023/08/22/1155.html)
  - ContentControl它有一个Content属性，关键是这个属性的类型是object

### 依赖属性(Dependency Property) 和 附加属性(Attached Property)

可以自定义，也可以使用框架。主要目的是，一般的属性(如自定义的Person)无法和前端进行绑定。需要定义成附加属性。


依赖属性是 WPF 中一个特殊的属性系统，它提供了比普通 CLR 属性更强大的功能。从代码中我们可以看到一些典型的依赖属性实现：

依赖属性的主要特点：

1. 数据绑定支持：支持双向数据绑定，可以自动更新 UI
2. 样式和模板支持：可以在样式中设置和修改
3. 属性值继承：可以从父元素继承属性值
4. 默认值支持：可以设置默认值
5. 属性变更通知：通过 PropertyChangedCallback 可以监听属性值变化
6. 内存优化：只在值改变时才分配内存


依赖属性：
- 自定义控件开发
- 需要数据绑定的属性
- 需要样式和模板支持的属性
- 需要属性变更通知的场景

```cs
// 自定义控件的依赖属性：

<UserControl>
    <Grid>
        <TextBox x:Name="tbText" Text="TextBox"/>
    </Grid>
</UserControl>



public static readonly DependencyProperty SetTextProperty = 
    DependencyProperty.Register("SetText",           // 属性名
        typeof(string),                              // 属性类型
        typeof(NewTextBox),                          // 拥有者类型
        new PropertyMetadata("",                     // 默认值
            new PropertyChangedCallback(OnSetTextChanged))); // 属性变更回调

private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    NewTextBox control = d as NewTextBox;
    control.OnSetTextChanged(e);
}

private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
{
    tbText.Text = e.NewValue.ToString();
}


// 使用方法：
// 通过依赖属性 SetText 来控制内部 TextBox 的文本内容
// 当 SetText 属性值改变时，会自动更新内部 TextBox 的显示内容

<views:NewTextBox SetText="Hello World"/>


```


**附加属性(Attached Property)：**

附加属性是一种特殊的依赖属性，它允许在一个元素上设置一个不属于该元素的属性。从代码中我们可以看到一些典型的附加属性使用：


附加属性的主要特点：
- 跨元素属性设置：可以在一个元素上设置属于另一个元素的属性
- 布局系统支持：常用于布局系统，如 Grid.Row、Grid.Column
- 服务定位：可以用于服务定位和依赖注入
- 属性值继承：支持属性值继承
- 数据绑定支持：支持数据绑定

附加属性：
- 布局系统（Grid、DockPanel 等）
- 服务定位
- 跨元素属性设置
- 行为扩展


```xml
<Grid>
    <!-- Grid 定义了行和列 -->
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto"/>
        <ColumnDefinition Width="200"/>
    </Grid.ColumnDefinitions>

    <!-- 这里的 Button 不是 Grid 控件，但可以设置 Grid.Row 和 Grid.Column -->
    <Button Grid.Row="0" Grid.Column="0" Content="Button 1"/>
    
    <!-- TextBox 也不是 Grid 控件，但同样可以设置 Grid.Row 和 Grid.Column -->
    <TextBox Grid.Row="1" Grid.Column="1" Text="TextBox 1"/>
</Grid>
```

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

## 教程

- [教程：使用 C 创建 WPF 应用程序](https://learn.microsoft.com/zh-cn/visualstudio/get-started/csharp/tutorial-wpf)(拖拽控件)
- [教程：使用 .NET 创建新的 WPF 应用](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/get-started/create-app-visual-studio?view=netdesktop-8.0)(直接编写xaml)
- [WPF-教程](https://www.tutorialspoint.com/wpf/index.htm)
- [WPF中文网](https://www.wpfsoft.com/)
- [WPF从假入门到真的入门](https://www.zhihu.com/column/c_1397867519101755392)
- [WPF基础 常用控件学习](https://www.cnblogs.com/LtWf/p/17328051.html)
