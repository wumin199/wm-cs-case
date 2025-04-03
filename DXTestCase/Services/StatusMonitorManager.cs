using System;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace DXTestCase.Services
{
  public partial class StatusMonitorManager : ObservableObject
  {
    private readonly System.Timers.Timer _timer;
    private readonly Random _random = new Random();
    private bool _isPaused = true; // 默认暂停状态

    [ObservableProperty]
    private float _speed;

    [ObservableProperty]
    private bool _isConnected;

    // 速度变更事件
    public event EventHandler<float> SpeedChanged;
    // 连接状态变更事件
    public event EventHandler<bool> IsConnectedChanged;

    public StatusMonitorManager()
    {
      _timer = new System.Timers.Timer(100); // 100ms更新一次
      _timer.Elapsed += Timer_Elapsed;
      _timer.AutoReset = true;  // 重复触发
      _timer.Start(); // 启动定时器，但默认是暂停状态
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (!_isPaused && IsConnected)
      {
        // 在-100到100之间随机变化
        Speed = (float)(_random.NextDouble() * 200 - 100);
      }
    }

    public void Start()
    {
      _isPaused = false;
    }

    public void Stop()
    {
      _isPaused = true;
    }

    // 模拟通讯连接
    public void Connect()
    {
      IsConnected = true;
    }

    // 模拟通讯断开
    public void Disconnect()
    {
      IsConnected = false;  // 最后更新连接状态
    }

    partial void OnSpeedChanged(float value)
    {
      SpeedChanged?.Invoke(this, value);
    }

    partial void OnIsConnectedChanged(bool value)
    {
      IsConnectedChanged?.Invoke(this, value);
    }
  }
}