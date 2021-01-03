using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Itgroupa.Common;
using Itgroupa.Dto;

namespace Itgroupa.Udp
{
    internal class UdpStarter : IUdpStarter
    {
        private const string EndCommand = "end";
        
        private readonly int _port;

        private readonly DataDump _dataDump;
        private readonly object _sync = 1;

        public UdpStarter(int port)
        {
            _port = port;
            _dataDump = new DataDump
            {
                Data = new Dictionary<DateTime, Price>()
            };
        }
        public void Start()
        {
            var thread = new Thread(Receive);
            thread.Start();
        }

        public DataDump Stop()
        {
            var client = new UdpClient();
            var command = BinarySerialization.ToByteArray(EndCommand);
            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port);
            
            client.Send(command, command.Length, endPoint);
            client.Close();

            lock (_sync)
            {
                return _dataDump;
            }
        }

        private void Receive()
        {
            var client = new UdpClient(_port);
            var data = string.Empty;
            IPEndPoint remoteIpEndPoint = null;
            
            while (!data.Equals(EndCommand))
            {
                try
                {
                    var bytesData = client.Receive(ref remoteIpEndPoint);
                    data = BinarySerialization.GetString(bytesData);
                
                    if (data.Equals(EndCommand))
                        break;

                    var dataPrice = JsonSerialization.GetObject<DataPrice>(data);
                    lock (_sync)
                    {
                        _dataDump.Data.Add(dataPrice.DateTime, dataPrice);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            client.Close();
        }
    }
}