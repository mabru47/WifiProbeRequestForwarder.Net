using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WifiMeasurement.Data.Models
{
    public class TestSeries
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestSeriesId { get; set; }

        public int Distance { get; set; }

        public Device Device { get; set; }

        public int DeviceId { get; set; }

        public DateTimeOffset? FinishedAt { get; set; }

        public DateTimeOffset? StartedAt { get; set; }

        public IEnumerable<Measurement> Measurements { get; set; }
    }
}
