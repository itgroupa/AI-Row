namespace Itgroupa.Udp
{
    public static class UdpServerBuilder
    {
        public static IUdpStarter Build(int port)
        {
            var result = new UdpStarter(port);

            return result;
        }
    }
}