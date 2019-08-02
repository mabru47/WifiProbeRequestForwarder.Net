using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace WifiMeasurement.Console.Services
{
    public interface ISelectPortService
    {
        string SelectPort();
    }

    internal class SelectPortService : ISelectPortService
    {
        public string SelectPort()
        {
            System.Console.WriteLine("Select a serial port number: ");
            var portNames = SerialPort.GetPortNames();
            if (portNames.Length == 2 && portNames.Contains("COM1"))
            {
                var autoSelectedPort = portNames.Single(x => x != "COM1");
                System.Console.WriteLine($"Auto slected Port: {autoSelectedPort}");
                return autoSelectedPort;
            }

            foreach (var portName in SerialPort.GetPortNames())
            {
                System.Console.WriteLine(portName);
            }
            var port = "COM" + System.Console.ReadKey().KeyChar;
            System.Console.WriteLine();
            return port;
        }
    }
}
