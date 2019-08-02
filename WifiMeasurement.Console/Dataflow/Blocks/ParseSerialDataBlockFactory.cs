using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WifiMeasurement.Console.Dataflow.Models;

namespace WifiMeasurement.Console.Dataflow.Blocks
{
    internal interface IParseSerialDataBlockFactory
    {
        TransformBlock<string, MeasurementDto> CreateBlock(ExecutionDataflowBlockOptions blockOptions);
    }

    internal class ParseSerialDataBlockFactory : IParseSerialDataBlockFactory
    {
        public TransformBlock<string, MeasurementDto> CreateBlock(ExecutionDataflowBlockOptions blockOptions)
        {
            return new TransformBlock<string, MeasurementDto>(RunAsync, blockOptions);
        }

        private async Task<MeasurementDto> RunAsync(string  rawLine)
        {
            var parts = rawLine
                .Trim()
                .Split(';');

            var measurementDto = new MeasurementDto()
            {
                MAC = parts[0],
                RSSI = int.Parse(parts[1]),
                Channel = int.Parse(parts[2])
            };

            return measurementDto;
        }
    }
}
