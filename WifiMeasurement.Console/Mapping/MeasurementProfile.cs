using AutoMapper;
using WifiMeasurement.Console.Dataflow.Models;
using WifiMeasurement.Data.Models;

namespace WifiMeasurement.Console.Mapping
{
    internal class MeasurementProfile : Profile
    {
        public MeasurementProfile()
        {
            base.CreateMap<MeasurementDto, Measurement>()
                .ForMember(m => m.RSSI, c => c.MapFrom(d => d.RSSI));
        }
    }
}
