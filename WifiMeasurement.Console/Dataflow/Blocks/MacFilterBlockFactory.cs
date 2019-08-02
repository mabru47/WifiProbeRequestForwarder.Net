using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow.Models;
using WifiMeasurement.Console.Services;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console.Dataflow.Blocks
{
    internal interface IMacFilterBlockFactory
    {
        TransformBlock<MeasurementDto, MeasurementDto> CreateBlock(ExecutionDataflowBlockOptions blockOptions);
    }

    internal class MacFilterBlockFactory : IMacFilterBlockFactory
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IContextService _context;
        private readonly ILogger<MacFilterBlockFactory> _logger;

        public MacFilterBlockFactory(
            IMeasurementRepository measurementRepository,
            IContextService context,
            ILogger<MacFilterBlockFactory> logger)
        {
            _measurementRepository = measurementRepository;
            _context = context;
            _logger = logger;
        }

        public TransformBlock<MeasurementDto, MeasurementDto> CreateBlock(ExecutionDataflowBlockOptions blockOptions)
        {
            return new TransformBlock<MeasurementDto, MeasurementDto>(RunAsync, blockOptions);
        }

        private Task<MeasurementDto> RunAsync(MeasurementDto measurementDto)
        {
            if (_context.MACFilter != null && _context.MACFilter != measurementDto.MAC)
            {
                return Task.FromResult((MeasurementDto)null);
            }

            _logger.LogInformation(measurementDto.ToString());

            return Task.FromResult(measurementDto);
        }
    }
}
