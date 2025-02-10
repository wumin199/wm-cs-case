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