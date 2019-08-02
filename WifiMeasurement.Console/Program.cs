using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WifiMeasurement.Console.Dataflow;
using WifiMeasurement.Console.Dataflow.Blocks;
using WifiMeasurement.Console.Services;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                await Run();
            }
        }
        static async Task Run()
        {
            var host = new HostBuilder()
                 .ConfigureServices((hostContext, services) =>
                 {
                     services
                        .AddDataContext("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=FuE-Projekt;Integrated Security=True;");

                     services
                        .AddHostedService<HostedService>();

                     services
                        .AddSingleton<IContextService, ContextService>()
                        .AddTransient<ISelectPortService, SelectPortService>()
                        .AddTransient<IDeviceService, DeviceService>()
                        .AddTransient<ITestSeriesService, TestSeriesService>()
                        .AddTransient<ISerialReaderService, SerialReaderService>();

                     services
                        .AddTransient<IDataflowFactory, DataflowFactory>()
                        .AddTransient<IParseSerialDataBlockFactory, ParseSerialDataBlockFactory>()
                        .AddTransient<IMacFilterBlockFactory, MacFilterBlockFactory>()
                        .AddTransient<IEntitiesBlockFactory, EntitiesBlockFactory>()
                        .AddTransient<IFlushDatabaseCacheBlockFactory, FlushDatabaseCacheBlockFactory>();

                     services.AddLogging(configure =>
                     {
                         configure.AddConsole();
                     });
                 })
                .Build();

            await host.Services.GetRequiredService<IMeasurementRepository>().MigrateAsync();

            await host.RunAsync();
        }
    }
}
