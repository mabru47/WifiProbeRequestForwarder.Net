using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console.Services
{
    internal interface ITestSeriesService
    {
        Task CreateTestSeriesAsync();
    }

    internal class TestSeriesService : ITestSeriesService
    {
        private readonly IMeasurementRepository _repository;
        private readonly IContextService _context;
        public TestSeriesService(
            IMeasurementRepository repository,
            IContextService context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task CreateTestSeriesAsync()
        {
            System.Console.Write("Distance in cm: ");
            var distance = int.Parse(System.Console.ReadLine().Trim());

            var testId = await _repository.AddTestAsync(
                deviceId: _context.DeviceId,
                distance: distance);

            _context.TestSeriesId = testId;
        }
    }
}
