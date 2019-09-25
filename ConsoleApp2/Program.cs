using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application started");

            var url = "http://elasticsearch-master.elk.svc.cluster.local:9200";
            //var url = "http://localhost:9200";
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug,
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                });

            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            using (var logger = loggerConfig.CreateLogger())
            {
                logger.Information("Data logged");
            }

            Console.WriteLine("Waiting 5 minutes before exit");
            Thread.Sleep(TimeSpan.FromMinutes(5));
        }
    }
}
