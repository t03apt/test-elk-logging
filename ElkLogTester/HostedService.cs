using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElkLogTester
{
    internal class HostedService : IHostedService
    {
        private readonly ILogger<HostedService> _logger;

        public HostedService(ILogger<HostedService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Out);

            _logger.LogInformation("Data logged");
            _logger.LogInformation("Text: {TextField}", "text");
            await Task.Delay(5000);
            _logger.LogInformation("Int: {TextField}", 0);
            await Task.Delay(5000);
            _logger.LogInformation("Date: {TextField}", DateTime.Now);
            await Task.Delay(5000);
            _logger.LogInformation("Boolean: {TextField}", true);
            await Task.Delay(5000);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}