
using System.Windows;
using DXTestCase.Views.ComunityToolKit;
using DXTestCase.Views.IOMonitoring;
using Prism.Regions;


namespace DXTestCase.Views
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("TestCommunityToolKitValidatorRegion", typeof(ValidatorView));

    }
  }
}