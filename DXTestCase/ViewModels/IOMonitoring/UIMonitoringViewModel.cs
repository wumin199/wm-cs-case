using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Regions;
using DXTestCase.Core.Mvvm;
using DevExpress.Mvvm;
using System.Windows.Threading;
using System.Windows;

namespace DXTestCase.ViewModels.IOMonitoring
{
    public partial class UIMonitoringViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private float _velocity;

        private readonly System.Timers.Timer _timer;
        private readonly Random _random;
        private readonly object _lockObject = new object();

        public UIMonitoringViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Velocity = 100.01f;
            _random = new Random();

            // 初始化定时器
            _timer = new System.Timers.Timer(100); // 100ms
            _timer.Elapsed += Timer_Elapsed;
            _timer.AutoReset = true; // 确保定时器可以重复触发
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 在后台线程处理数据
            float newVelocity = ProcessData();

            // 安全地更新 UI
            var current = System.Windows.Application.Current;
            if (current != null)
            {
                current.Dispatcher.Invoke(() =>
                {
                    Velocity = newVelocity;
                });
            }
        }

        private float ProcessData()
        {
            // lock (_lockObject)
            // 必要的时候再加Lock
            {
                return (float)(_random.NextDouble() * 100);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _timer.Start();
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            _timer.Stop();
        }
    }
}