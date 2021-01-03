using System;

namespace Itgroupa.Udp
{
    internal class UdpStarter : IUdpStarter
    {
        private readonly int _port;

        public UdpStarter(int port)
        {
            _port = port;
        }
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}