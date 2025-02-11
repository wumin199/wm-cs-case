namespace DIBasics.Example;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;



#region test1

// ClassA -> IClassB -> ClassC

// 接口
public interface IConsole
{
  void WriteLine(string message);
}


// 默认接口实现
// 服务实现可以是 sealed 来防止继承，可以是 internal 来限制对程序集的访问
internal sealed class DefaultConsole : IConsole
{
  public bool IsEnabled { get; set; } = true;

  // 重写接口方法
  void IConsole.WriteLine(string message)
  {
    if (IsEnabled is false)
    {
      return;
    }

    Console.WriteLine(message);
  }
}


// 接口
public interface IGreetingService
{
  string Greet(string name);
}

// 默认接口实现
internal sealed class DefaultGreetingService(
    IConsole console) : IGreetingService
{
  public string Greet(string name)
  {
    var greeting = $"Hello, {name}!";

    console.WriteLine(greeting);

    return greeting;
  }
}

// 这是具体实现案例
public class FarewellService(IConsole console)
{
  public string SayGoodbye(string name)
  {
    var farewell = $"Goodbye, {name}!";

    console.WriteLine(farewell);

    return farewell;
  }
}

public class Test1
{
  public void Run()
  {
    // 1. Create the service collection.
    var services = new ServiceCollection();

    // 2. Register (add and configure) the services.
    services.AddSingleton<IConsole>(
        implementationFactory: static _ => new DefaultConsole
        {
          IsEnabled = true
        });
    services.AddSingleton<IGreetingService, DefaultGreetingService>();
    services.AddSingleton<FarewellService>();


    // or 
    // services.Add(ServiceDescriptor.Describe(
    //   serviceType: typeof(IConsole),
    //   implementationFactory: static _ => new DefaultConsole
    //   {
    //     IsEnabled = true
    //   },
    //   lifetime: ServiceLifetime.Singleton));

    // services.Add(ServiceDescriptor.Describe(
    //     serviceType: typeof(IGreetingService),
    //     implementationType: typeof(DefaultGreetingService),
    //     lifetime: ServiceLifetime.Singleton));

    // services.Add(ServiceDescriptor.Describe(
    //     serviceType: typeof(FarewellService),
    //     implementationType: typeof(FarewellService),
    //     lifetime: ServiceLifetime.Singleton));

    // 3. Build the service provider from the service collection.
    var serviceProvider = services.BuildServiceProvider();

    // 4. Resolve the services that you need.
    var greetingService = serviceProvider.GetRequiredService<IGreetingService>();
    var farewellService = serviceProvider.GetRequiredService<FarewellService>();

    // 5. Use the services
    var greeting = greetingService.Greet("David");
    var farewell = farewellService.SayGoodbye("David");
  }
}

#endregion

#region test2

public interface IReportServiceLifetime
{
  Guid Id { get; }

  ServiceLifetime Lifetime { get; }
}

public interface IExampleTransientService : IReportServiceLifetime
{
  ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Transient;
}

internal sealed class ExampleTransientService : IExampleTransientService
{
  Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}

public interface IExampleScopedService : IReportServiceLifetime
{
  ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Scoped;
}

internal sealed class ExampleScopedService : IExampleScopedService
{
  Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}

public interface IExampleSingletonService : IReportServiceLifetime
{
  ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Singleton;
}

internal sealed class ExampleSingletonService : IExampleSingletonService
{
  Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}


internal sealed class ServiceLifetimeReporter(
    IExampleTransientService transientService,
    IExampleScopedService scopedService,
    IExampleSingletonService singletonService)
{
  public void ReportServiceLifetimeDetails(string lifetimeDetails)
  {
    Console.WriteLine(lifetimeDetails);

    LogService(transientService, "Always different");
    LogService(scopedService, "Changes only with lifetime");
    LogService(singletonService, "Always the same");
  }

  private static void LogService<T>(T service, string message)
      where T : IReportServiceLifetime =>
      Console.WriteLine(
          $"    {typeof(T).Name}: {service.Id} ({message})");
}


public class Test2
{
  static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
  {
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    ServiceLifetimeReporter logger = provider.GetRequiredService<ServiceLifetimeReporter>();
    logger.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine("...");

    logger = provider.GetRequiredService<ServiceLifetimeReporter>();
    logger.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine();
  }

  public async Task Run(string[] args)
  {
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
    builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
    builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
    builder.Services.AddTransient<ServiceLifetimeReporter>();

    using IHost host = builder.Build();

    ExemplifyServiceLifetime(host.Services, "Lifetime 1");
    ExemplifyServiceLifetime(host.Services, "Lifetime 2");

    await host.RunAsync();
  }

}

#endregion

#region test3

// 测试生命周期
public class TestService
{
  public string Name { get; set; }
  public void SayHi()
  {
    Console.WriteLine(Name);
  }
}

public class Test3
{
  public void test1()
  {

    ServiceCollection services = new ServiceCollection();
    services.AddTransient<TestService>();
    using (var sp = services.BuildServiceProvider())
    {
      TestService t1 = sp.GetService<TestService>();
      t1.Name = "SimonWu";
      t1.SayHi();
      TestService t2 = sp.GetService<TestService>();
      t2.Name = "SimonWu";
      t2.SayHi();
      Console.WriteLine(Object.ReferenceEquals(t1, t2));//False
    }
  }

  public void test2()
  { // 单例模式
    ServiceCollection services = new ServiceCollection();
    services.AddSingleton<TestService>();
    using (var sp = services.BuildServiceProvider())
    {
      TestService t1 = sp.GetService<TestService>();
      t1.Name = "SimonWu";
      t1.SayHi();
      TestService t2 = sp.GetService<TestService>();
      t2.Name = "Hillo";
      t2.SayHi();
      Console.WriteLine(Object.ReferenceEquals(t1, t2));//True
    }
  }

  public void test3()
  {
    ServiceCollection services = new ServiceCollection();
    services.AddScoped<TestService>();
    using (var sp = services.BuildServiceProvider())
    {
      TestService t1, t2, t3;
      using (IServiceScope scope = sp.CreateScope())
      {
        t1 = scope.ServiceProvider.GetService<TestService>();
        t1.Name = "SimonWu";
        t1.SayHi();

        t2 = scope.ServiceProvider.GetService<TestService>();
        t2.Name = "Hillo";
        t2.SayHi();
        Console.WriteLine(Object.ReferenceEquals(t1, t2));//True
      }

      using (IServiceScope scope = sp.CreateScope())
      {
        t3 = scope.ServiceProvider.GetService<TestService>();
        t3.Name = "Hillo";
        t3.SayHi();
        Console.WriteLine(Object.ReferenceEquals(t1, t3));//False
      }
    }
  }
}


#endregion


#region test4

class Controller
{
  private readonly ILog log;
  private readonly IStorage storage;
  public Controller(ILog log, IStorage storage)//构造函数注入
  {
    this.log = log;
    this.storage = storage;
  }

  public void Test()
  {
    log.Log("开始上传");
    storage.Save("asdkks", "1.txt");
    log.Log("上传完毕");
  }
}

interface ILog
{
  public void Log(string msg);
}

class LogImpl : ILog
{
  public void Log(string msg)
  {
    Console.WriteLine("日志：" + msg);
  }
}

interface IConfig
{
  public string GetValue(string name);
}

class ConfigImpl : IConfig
{
  public string GetValue(string name)
  {
    return "hello";
  }
}

interface IStorage
{
  public void Save(string content, string name);
}

class StorageImpl : IStorage
{
  private readonly IConfig _config;
  public StorageImpl(IConfig config)//构造函数注入,当DI创建StorageImpl时候，框架自动创建IConfig服务
  {
    _config = config;
  }

  public void Save(string content, string name)
  {
    string server = _config.GetValue("server");
    Console.WriteLine($"向服务器{server}的文件名{name}上传{content}");
  }
}

public class Test4
{
  public void Run()
  {
    ServiceCollection services = new ServiceCollection();
    services.AddScoped<ILog, LogImpl>();
    services.AddScoped<IConfig, ConfigImpl>();
    services.AddScoped<IStorage, StorageImpl>();
    services.AddScoped<Controller>();

    using (var sp = services.BuildServiceProvider())
    {
      var controller = sp.GetService<Controller>();
      controller.Test();
    }
    Console.ReadKey();
  }
}

#endregion