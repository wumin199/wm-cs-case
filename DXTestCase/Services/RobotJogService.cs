using System;
using System.Timers;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DXTestCase.Services
{
  // 仅作测试用
  public class JogErrorEventArgs : EventArgs
  {
    public int Axis { get; }
    public string ErrorMessage { get; }
    public JogErrorType ErrorType { get; }

    public JogErrorEventArgs(int axis, string errorMessage, JogErrorType errorType)
    {
      Axis = axis;
      ErrorMessage = errorMessage;
      ErrorType = errorType;
    }
  }

  // 点动错误类型，仅作测试
  public enum JogErrorType
  {
    LimitReached,    // 限位
    SpeedError,      // 速度异常
    ConnectionError, // 连接异常
    Other           // 其他异常
  }

  public class RobotJogService : ObservableObject, IDisposable
  {
    private readonly System.Timers.Timer _jogTimer;
    private readonly Dictionary<int, bool> _axisJogStatus = new Dictionary<int, bool>();
    private readonly IRobotApi _robotApi; // 机器人API接口
    private int _currentJoggingAxis = 0; // 0表示没有轴在点动

    public event EventHandler<int> JogBlocked; // 当点动被阻止时触发的事件
    // 点动异常事件，也可能可以直接打印日志取代
    public event EventHandler<JogErrorEventArgs> JogError;

    public RobotJogService(IRobotApi robotApi)
    {
      _robotApi = robotApi;
      _jogTimer = new System.Timers.Timer(100); // 100ms间隔
      _jogTimer.Elapsed += Timer_Elapsed;
      _jogTimer.AutoReset = true;

      // 初始化4个轴的状态
      for (int i = 1; i <= 4; i++)
      {
        _axisJogStatus[i] = false;
      }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      // 只检查当前点动轴
      if (_currentJoggingAxis > 0 && _axisJogStatus[_currentJoggingAxis])
      {
        try
        {
          _robotApi.Jog(_currentJoggingAxis);
        }
        catch (RobotLimitException ex)
        {
          // 处理限位异常
          OnJogError(_currentJoggingAxis, ex.Message, JogErrorType.LimitReached);
          StopJog(_currentJoggingAxis); // 自动停止点动
        }
        catch (RobotSpeedException ex)
        {
          // 处理速度异常
          OnJogError(_currentJoggingAxis, ex.Message, JogErrorType.SpeedError);
          StopJog(_currentJoggingAxis);
        }
        catch (RobotConnectionException ex)
        {
          // 处理连接异常
          OnJogError(_currentJoggingAxis, ex.Message, JogErrorType.ConnectionError);
          StopJog(_currentJoggingAxis);
        }
        catch (Exception ex)
        {
          // 处理其他未知异常
          OnJogError(_currentJoggingAxis, ex.Message, JogErrorType.Other);
          StopJog(_currentJoggingAxis);
        }
      }
      else
      {
        // 如果没有轴在点动，停止定时器
        StopTimer();
      }
    }

    private void OnJogError(int axis, string message, JogErrorType errorType)
    {
      JogError?.Invoke(this, new JogErrorEventArgs(axis, message, errorType));
    }

    // 开始某个轴的点动
    public bool StartJog(int axis)
    {
      if (axis < 1 || axis > 4)
        return false;

      // 如果当前没有轴在点动，或者就是这个轴在点动
      if (_currentJoggingAxis == 0 || _currentJoggingAxis == axis)
      {
        _axisJogStatus[axis] = true;
        _currentJoggingAxis = axis;
        StartTimer(); // 启动定时器
        return true;
      }
      else
      {
        // 通知UI层点动被阻止
        JogBlocked?.Invoke(this, axis);
        return false;
      }
    }

    // 停止某个轴的点动
    public void StopJog(int axis)
    {
      if (axis >= 1 && axis <= 4)
      {
        _axisJogStatus[axis] = false;
        if (_currentJoggingAxis == axis)
        {
          _currentJoggingAxis = 0; // 清除当前点动轴
          StopTimer(); // 停止定时器
        }
      }
    }

    // 获取当前正在点动的轴
    public int GetCurrentJoggingAxis()
    {
      return _currentJoggingAxis;
    }

    // 启动定时器
    private void StartTimer()
    {
      if (!_jogTimer.Enabled)
      {
        _jogTimer.Start();
      }
    }

    // 停止定时器
    private void StopTimer()
    {
      if (_jogTimer.Enabled)
      {
        _jogTimer.Stop();
      }
    }

    public void Dispose()
    {
      StopTimer();
      _jogTimer?.Dispose();
    }
  }

  // 机器人API接口定义
  public interface IRobotApi
  {
    /// <summary>
    /// 执行点动操作
    /// </summary>
    /// <param name="axis">轴号</param>
    /// <exception cref="RobotLimitException">当机器人达到限位时抛出</exception>
    /// <exception cref="RobotSpeedException">当点动速度异常时抛出</exception>
    /// <exception cref="RobotConnectionException">当与机器人连接异常时抛出</exception>
    /// <exception cref="Exception">其他未知异常</exception>
    void Jog(int axis);
  }

  // 机器人异常类定义
  public class RobotLimitException : Exception
  {
    public RobotLimitException(string message) : base(message) { }
  }

  public class RobotSpeedException : Exception
  {
    public RobotSpeedException(string message) : base(message) { }
  }

  public class RobotConnectionException : Exception
  {
    public RobotConnectionException(string message) : base(message) { }
  }
}