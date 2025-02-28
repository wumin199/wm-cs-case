using System.Configuration;
using System.Data;
using System.Windows;

namespace FirstWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

  App()
  {
    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
    //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
  }
  public static string[] Args;
  private void Application_Startup(object sender, StartupEventArgs e)
  {
    if (e.Args.Length == 0) return;
    if (e.Args.Length > 0)
    {
      Args = e.Args;
    }

    

  }
}

