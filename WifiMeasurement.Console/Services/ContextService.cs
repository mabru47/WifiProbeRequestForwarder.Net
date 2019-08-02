using System;
using System.Collections.Generic;
using System.Text;

namespace WifiMeasurement.Console.Services
{
    public interface IContextService
    {
        int TestSeriesId { get; set; }

        int DeviceId { get; set; }

        string MACFilter { get; set; }
    }

    internal class ContextService : IContextService
    {
        public int TestSeriesId { get; set; }

        public int DeviceId { get; set; }

        public string MACFilter { get; set; }
    }
}
