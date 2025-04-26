using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using DXTestCase.Views;
using DXTestCase.Views.IOMonitoring;
using Prism.Ioc;
using Prism.Unity;
using DXTestCase.Services;

namespace DXTestCase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {


    private int _testCase = 0;

    protected override Window CreateShell()
    {

      _testCase = 1;

      switch (_testCase)
      {
        case 0:
          return Container.Resolve<MainWindow>();
        case 1:
          return Container.Resolve<TestWindow>();
        default:

          return Container.Resolve<MainWindow>();
      }
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      switch (_testCase)
      {
        case 0:
          containerRegistry.RegisterForNavigation<UIMonitoringView>();
          containerRegistry.RegisterForNavigation<UOMonitoringView>();
          containerRegistry.RegisterSingleton<StatusMonitorManager>();
          break;
        case 1:
          break;
        default:
          break;
      }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      // 注册全局异常处理
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      System.Windows.Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      try
      {
        System.Windows.MessageBox.Show(
            "发生未处理的异常，程序将关闭。\n" +
            $"错误信息：{(e.ExceptionObject as Exception)?.Message}",
            "错误",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
      }
      catch
      {
        // 如果显示消息框失败，直接退出程序
        Environment.Exit(1);
      }
    }

    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      try
      {
        System.Windows.MessageBox.Show(
            "发生UI线程异常。\n" +
            $"错误信息：{e.Exception.Message}",
            "错误",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        e.Handled = true;
      }
      catch
      {
        Environment.Exit(1);
      }
    }
  }
}