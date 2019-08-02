using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow.Models;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console.Dataflow.Blocks
{
    internal interface IFlushDatabaseCacheBlockFactory
    {
        ActionBlock<IEnumerable<Guid>> CreateBlock(ExecutionDataflowBlockOptions blockOptions);
    }
    internal class FlushDatabaseCacheBlockFactory : IFlushDatabaseCacheBlockFactory
    {
        private readonly IMeasurementRepository _measurementRepository;

        public FlushDatabaseCacheBlockFactory(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public ActionBlock<IEnumerable<Guid>> CreateBlock(ExecutionDataflowBlockOptions blockOptions)
        {
            return new ActionBlock<IEnumerable<Guid>>(RunAsync, blockOptions);
        }

        private async Task RunAsync(IEnumerable<Guid> placeholder)
        {
            await _measurementRepository.SaveChangesAsync();
        }
    }
}
