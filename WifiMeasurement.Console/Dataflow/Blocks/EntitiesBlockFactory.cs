using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow.Models;
using WifiMeasurement.Console.Services;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console.Dataflow.Blocks
{
    internal interface IEntitiesBlockFactory
    {
        TransformBlock<MeasurementDto, Guid> CreateBlock(ExecutionDataflowBlockOptions blockOptions);
    }

    internal class EntitiesBlockFactory : IEntitiesBlockFactory
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IContextService _context;

        public EntitiesBlockFactory(
            IMeasurementRepository measurementRepository,
            IContextService context)
        {
            _measurementRepository = measurementRepository;
            _context = context;
        }

        public TransformBlock<MeasurementDto, Guid> CreateBlock(ExecutionDataflowBlockOptions blockOptions)
        {
            return new TransformBlock<MeasurementDto, Guid>(RunAsync, blockOptions);
        }

        private Task<Guid> RunAsync(MeasurementDto measurementDto)
        {
            _measurementRepository.AddMeasurement(
                testSeriesId:_context.TestSeriesId,
                MAC: measurementDto.MAC,
                RSSI: measurementDto.RSSI,
                channel: measurementDto.Channel);

            return Task.FromResult(Guid.NewGuid());
        }
    }
}
