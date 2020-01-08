using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Application started");
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"SelfLog: {msg}"));

            var url = "http://localhost:9200";
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = "itron-{0:yyyy.MM.dd}",
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug,
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    Period = TimeSpan.FromMilliseconds(10)
                });

            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            using (var logger = loggerConfig.CreateLogger())
            {
                logger.Information("Data logged");
                logger.Information("Text: {TextField}", "text");
                await Task.Delay(5000);
                logger.Information("Int: {TextField}", 0);
                await Task.Delay(5000);
                logger.Information("Date: {TextField}", DateTime.Now);
                await Task.Delay(5000);
                logger.Information("Boolean: {TextField}", true);
                await Task.Delay(5000);
            }

            Console.WriteLine("Waiting 5 minutes before exit");
            Thread.Sleep(TimeSpan.FromMinutes(5));
        }
    }
}
