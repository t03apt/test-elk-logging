using Itron.Platform.Hosting;
using Itron.Platform.Hosting.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ElkLogTester
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                //var url = "http://localhost:9200";
                //var loggerConfig = new LoggerConfiguration()
                //    .MinimumLevel.Verbose()
                //    .WriteTo.Console()
                //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url))
                //    {
                //        AutoRegisterTemplate = true,
                //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                //        IndexFormat = "itron-{0:yyyy.MM.dd}",
                //        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug,
                //        Period = TimeSpan.FromMilliseconds(10)
                //    });
                var builder = new HostBuilder()
                    .UsePlatform(new DependencyInjectionContext(typeof(Program))
                    {
                        Args = args,
                        LoadModules = true,
                        AddFileHealthReporter = true,
                        ConfigureConfiguration = (configBuilder) =>
                        {
                            configBuilder.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false);
                            configBuilder.AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: false);
                        },
                        ConfigureDependencies = (services, configuration, logger) =>
                        {
                            services.AddHostedService<HostedService>();
                        }
                    });

                var host = builder.UseConsoleLifetime().Build();
                host.InitializePlatform();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }

            return 0;
        }
    }
}
