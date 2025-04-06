
**DXTestCase**

WPF项目，用于测试 Prism.Unity/DevExpress.Wpf(24.1.5)/CommunityToolkit.Mvvm(8.3.2)

其中 DevExpress.Wpf是本地破解版



**架构**

Prism架构中，独立的Service，用interface来定义。interface类似C++的抽象类，不能实例化。或者可以理解为头文件


获取RecentProject：放在可执行程序的 Data/WorkStation1/Config.cfg下

配置文件是放在固定的地方，（如可执行程序文件夹下），然后去更新这个配置文件。

xyz中是放在注册表中。

这里应该还不完善，Directory.GetCurrentDirectory()在不同的启动方式下，会有问题。

```cs
        public ConfigService(IMessageService messageService)
        {
            _messageService = messageService;
            ConfigFilePath = $"{Directory.GetCurrentDirectory()}\\Data\\{WorkStationName}\\Config.cfg";
            WorkStationPath = $"{Directory.GetCurrentDirectory()}\\Data\\{WorkStationName}\\";
            InitializeRecentFiles();
        }

        private void InitializeRecentFiles()
        {
            if (File.Exists(ConfigFilePath))
            {
                using (StreamReader sr = new StreamReader(ConfigFilePath, Encoding.Default))
                {
                    var content = sr.ReadToEnd();
                    RecentProjects = JsonConvert.DeserializeObject<List<RecentProject>>(content);
                }
            }
        }
```


ConfigService.cs中

项目另存为有问题：无法更新上位机软件的标题栏，即

```cs
            Project project = new Project
            {
                Title = title,
                Jobs = Project.Jobs
            };
```
bug原因是少了：

Project = project

- 表单验证
  - ValidateAllProperties()
- Control中新建自己的ListView，含图标和选择

- UI


MainWindow.xaml


重点：RibbonView/DockPanelView

- <ContentControl" />: RibbonRegion： RibbonView
  - <dxr:RibbonControl.ApplicationMenu>
    - <dxr:BackstageViewControl x:Name="backstage">
      - <dxr:BackstageTabItem Content="打开"> BackstageOpenView
      - <dxr:BackstageButtonItem Content="新建"> 
      - <dxr:BackstageButtonItem Content="保存"> 
      - <dxr:BackstageButtonItem Content="另存为">
      - <dxr:BackstageTabItem Content="关于"> BackstageAboutView
- <ContentControl" />: DockRegion： DockPanelView
  - <DockPanel>
    - <dxdo:LayoutGroup> 默认应该是horizontal
      - <dxdo:LayoutGroup Orientation="Vertical>
        - <dxdo:LayoutGroup>
          - <dxdo:LayoutGroup>
            - <dxdo:LayoutPanel Caption="工作站">
              - <ContentControl/>: WorkStationView
            - <dxdo:DocumentGroup>
              - <dxdo:DocumentPanel Caption="起始页">: StartPageView

        - <dxdo:TabbedGroup>
          - <dxdo:LayoutPanel  Caption="输出">
            - <ContentControl" />: OutputView
          - <dxdo:LayoutPanel Caption="查找结果" />
          - <dxdo:LayoutPanel Caption="变量监控" />
          - <dxdo:LayoutPanel Caption="历史报警" />
          - <dxdo:LayoutPanel Caption="操作日志">
            - <ContentControl" />: OperationLogView
      - <dxdo:TabbedGroup>
        - <dxdo:LayoutPanel Caption="控制面板">
- <ContentControl" />: StatusBarRegion: StatusBarView