using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WifiMeasurement.Data.Models
{
    public class Device
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceId { get; set; }

        public string Name { get; set; }

        public string MAC { get; set; }

        public IEnumerable<TestSeries> TestSeries { get; set; }
    }
}
