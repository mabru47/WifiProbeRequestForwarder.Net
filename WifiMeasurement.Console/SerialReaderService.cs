using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow;
using WifiMeasurement.Console.Services;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console
{
    internal interface ISerialReaderService
    {
        Task ReadAsync(string port, CancellationToken cancellationToken);
    }

    internal class SerialReaderService : ISerialReaderService
    {
        private readonly IDataflowFactory _dataflowFactory;
        private readonly ILogger<SerialReaderService> _logger;
        private readonly IMeasurementRepository _repository;
        private readonly IContextService _context;

        public SerialReaderService(
            IDataflowFactory dataflowFactory,
            ILogger<SerialReaderService> logger,
            IMeasurementRepository repository,
            IContextService context)
        {
            _dataflowFactory = dataflowFactory;
            _logger = logger;
            _repository = repository;
            _context = context;
        }

        public async Task ReadAsync(string port, CancellationToken cancellationToken)
        {
            var dataflow = _dataflowFactory.Create();
            try
            {
                using (var _serialPort = new SerialPort())
                {
                    _serialPort.PortName = port;
                    _serialPort.BaudRate = 19200;
                    _serialPort.Parity = Parity.None;
                    _serialPort.DataBits = 8;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.Handshake = Handshake.None;

                    _serialPort.Open();

                    while (false == cancellationToken.IsCancellationRequested)
                    {
                        string message = _serialPort.ReadLine();
                        dataflow.Start.Post(message);
                    }

                    _serialPort.Close();
                }
            }
            finally
            {
                await Task.WhenAll(dataflow.Completions);
                await _repository.FinishTestSeriesAsync(_context.TestSeriesId);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
