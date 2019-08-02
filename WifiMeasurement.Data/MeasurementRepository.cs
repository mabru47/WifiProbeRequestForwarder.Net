using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WifiMeasurement.Data.Models;

namespace WifiMeasurement.Data
{
    public interface IMeasurementRepository : IRepository
    {
        void AddMeasurement(int testSeriesId, string MAC, int RSSI, int channel);

        Task<int> AddTestAsync(int deviceId, int distance);

        Task FinishTestSeriesAsync(int testSeriesId);

        Task<List<Device>> GetDevicesAsync();

        Task<int> CreateDeviceAsync(string name, string mac);
    }

    internal class MeasurementRepository : Repository, IMeasurementRepository
    {
        private readonly MeasurementContext _context;

        public MeasurementRepository(MeasurementContext context, ILogger<Repository> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public void AddMeasurement(int testSeriesId, string mac, int rssi, int channel)
        {
            _context.Measurements.Add(new Measurement
            {
                MAC = mac,
                RSSI = rssi,
                Channel = channel,
                TestSeriesId = testSeriesId,
                DateTime = DateTimeOffset.UtcNow
            });
        }

        public async Task<int> AddTestAsync(int deviceId, int distance)
        {
            var test = new TestSeries
            {
                DeviceId = deviceId,
                Distance = distance,
                StartedAt = DateTimeOffset.UtcNow
            };

            _context.TestSeries.Add(test);

            await SaveChangesAsync();

            return test.TestSeriesId;
        }

        public async Task FinishTestSeriesAsync(int testSeriesId)
        {
            var testSeries = _context.TestSeries.Single(x => x.TestSeriesId == testSeriesId);
            testSeries.FinishedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
        }

        public Task<List<Device>> GetDevicesAsync()
        {
            return _context.Devices.ToListAsync();
        }

        public async Task<int> CreateDeviceAsync(string name, string mac)
        {
            var device = new Device
            {
                Name = name,
                MAC = mac
            };
            _context.Devices.Add(device);

            await SaveChangesAsync();

            return device.DeviceId;
        }
    }
}
