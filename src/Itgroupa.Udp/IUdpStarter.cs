using Itgroupa.Dto;

namespace Itgroupa.Udp
{
    public interface IUdpStarter
    {
        void Start();
        DataDump Stop();
    }
}