using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiMeasurement.Console.Services
{
    internal class HostedService : IHostedService
    {
        private readonly IDeviceService _deviceService;
        private readonly ITestSeriesService _testSeriesService;
        private readonly ISelectPortService _selectPortService;
        private readonly ISerialReaderService _serialReaderService;
        private readonly ILogger<HostedService> _logger;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public HostedService(
            IDeviceService deviceService,
            ITestSeriesService testSeriesService,
            ISelectPortService selectPortService,
            ISerialReaderService serialReaderService,
            ILogger<HostedService> logger)
        {
            _deviceService = deviceService;
            _testSeriesService = testSeriesService;
            _selectPortService = selectPortService;
            _serialReaderService = serialReaderService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _task = Task.Run(async () => await RunInternal(_cancellationTokenSource.Token));
            return Task.CompletedTask;
        }

        private async Task RunInternal(CancellationToken cancellationToken)
        {
            while (false == cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _deviceService.SelectDeviceAsync();

                    await _testSeriesService.CreateTestSeriesAsync();

                    var port = _selectPortService.SelectPort();

                    await _serialReaderService.ReadAsync(port, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.GetType().Name}: {ex.Message}");
                    await Task.Delay(500, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            await _task;
        }
    }
}
