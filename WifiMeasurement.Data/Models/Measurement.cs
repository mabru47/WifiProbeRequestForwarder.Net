using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WifiMeasurement.Data.Models
{
    public class Measurement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementId { get; set; }

        public int RSSI { get; set; }

        public int Channel { get; set; }

        public string MAC { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public TestSeries TestSeries { get; set; }

        public int TestSeriesId { get; set; }
    }
}
