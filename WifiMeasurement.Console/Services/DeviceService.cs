using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WifiMeasurement.Data;

namespace WifiMeasurement.Console.Services
{
    internal interface IDeviceService
    {
        Task SelectDeviceAsync();
    }

    internal class DeviceService : IDeviceService
    {
        private readonly IMeasurementRepository _repository;
        private readonly IContextService _context;
        public DeviceService(
            IMeasurementRepository repository,
            IContextService context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task SelectDeviceAsync()
        {
            var devices = await _repository.GetDevicesAsync();
            System.Console.WriteLine("Please select a device: ");
            System.Console.WriteLine("(0) New device");

            var i = 1;
            foreach (var item in devices)
            {
                System.Console.WriteLine($"({i++}) {item.Name}");
            }

            var selectedId = int.Parse((System.Console.ReadLine() ?? "-1").Trim());
            if (selectedId < 0)
            {
                throw new TaskCanceledException();
            }
            else if (selectedId == 0)
            {
                System.Console.Write("Device name: ");
                var name = System.Console.ReadLine();

                System.Console.Write("MAC: ");

                var mac = System.Console.ReadLine().ToUpperInvariant();
                if (mac == "")
                    mac = null;
                else
                    PhysicalAddress.Parse(mac);

                _context.DeviceId = await _repository.CreateDeviceAsync(name, mac);
                _context.MACFilter = mac;
            }
            else
            {
                var device = devices[selectedId - 1];
                _context.DeviceId = device.DeviceId;
                _context.MACFilter = device.MAC;

                System.Console.WriteLine($"Selected device: {device.Name} ({device.DeviceId})");
            }
        }
    }
}
