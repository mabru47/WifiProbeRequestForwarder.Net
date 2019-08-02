using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow.Blocks;
using WifiMeasurement.Console.Dataflow.Models;

namespace WifiMeasurement.Console.Dataflow
{
    internal interface IDataflowFactory
    {
        DataflowResult<string> Create();
    }

    internal class DataflowFactory : IDataflowFactory
    {
        private readonly IParseSerialDataBlockFactory _parseSerialDataBlockFactory;
        private readonly IMacFilterBlockFactory _macFilterBlockFactory;
        private readonly IEntitiesBlockFactory _entitiesBlockFactory;
        private readonly IFlushDatabaseCacheBlockFactory _flushDatabaseCacheBlockFactory;

        public DataflowFactory(
            IParseSerialDataBlockFactory parseSerialDataBlockFactory,
            IMacFilterBlockFactory macFilterBlockFactory,
            IEntitiesBlockFactory entitiesBlockFactory,
            IFlushDatabaseCacheBlockFactory flushDatabaseCacheBlockFactory)
        {
            _parseSerialDataBlockFactory = parseSerialDataBlockFactory;
            _macFilterBlockFactory = macFilterBlockFactory;
            _entitiesBlockFactory = entitiesBlockFactory;
            _flushDatabaseCacheBlockFactory = flushDatabaseCacheBlockFactory;
        }

        public DataflowResult<string> Create()
        {
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var blockOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 };

            var parseSerialDataBlock = _parseSerialDataBlockFactory.CreateBlock(blockOptions);

            var macFilterBlock = _macFilterBlockFactory.CreateBlock(blockOptions);
            parseSerialDataBlock.LinkTo(macFilterBlock, linkOptions);

            var createEntitiesBlock = _entitiesBlockFactory.CreateBlock(blockOptions);
            macFilterBlock.LinkTo(createEntitiesBlock, linkOptions, m => m != null);
            macFilterBlock.LinkTo(DataflowBlock.NullTarget<MeasurementDto>());

            var batchBlock = new BatchBlock<Guid>(5);
            createEntitiesBlock.LinkTo(batchBlock, linkOptions);

            var flushDatabaseCacheBlock = _flushDatabaseCacheBlockFactory.CreateBlock(blockOptions);
            batchBlock.LinkTo(flushDatabaseCacheBlock, linkOptions);

            return new DataflowResult<string>
            {
                Start = parseSerialDataBlock,
                Leaves = new List<IDataflowBlock>
                {
                    flushDatabaseCacheBlock
                }
            };
        }
    }
}
