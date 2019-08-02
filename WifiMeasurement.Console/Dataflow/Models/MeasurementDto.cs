using Newtonsoft.Json;

namespace WifiMeasurement.Console.Dataflow.Models
{
    internal class MeasurementDto
    {
        public string MAC { get; set; }

        public int RSSI { get; set; }

        public int Channel { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
