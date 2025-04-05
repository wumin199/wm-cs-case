
**DXTestCase**

WPF项目，用于测试 Prism.Unity/DevExpress.Wpf(24.1.5)/CommunityToolkit.Mvvm(8.3.2)

其中 DevExpress.Wpf是本地破解版



**架构**

Prism架构中，独立的Service，用interface来定义。interface类似C++的抽象类，不能实例化。或者可以理解为头文件


获取RecentProject

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