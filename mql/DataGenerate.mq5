#property copyright "Turin"
#property link      "https://github.com/itgroupa"
#property version   "1.00"
#include "socket.h"
//+------------------------------------------------------------------+
input string Host="127.0.0.1";
input ushort Port=5002;

SOCKET64 client=INVALID_SOCKET64;
ref_sockaddr srvaddr={0};
int OnInit()
   {
      char ch[]; StringToCharArray(Host,ch);
      sockaddr_in addrin;
      addrin.sin_family=AF_INET;
      addrin.sin_addr.u.S_addr=inet_addr(ch);
      addrin.sin_port=htons(Port);
      srvaddr.in=addrin;
      
      SendData();
      SendData();
      
      return INIT_SUCCEEDED;
   }
void OnDeinit(const int reason) { CloseClean(); }
void OnTick()
   {
      
   }
void SendData()
{
   if(client!=INVALID_SOCKET64)
        {
         uchar data[]; BuildText(data);
         if(sendto(client,data,ArraySize(data),0,srvaddr.ref,ArraySize(srvaddr.ref))==SOCKET_ERROR)
           {
            int err=WSAGetLastError();
            if(err!=WSAEWOULDBLOCK) { Print("-Send failed error: "+WSAErrorDescript(err)); CloseClean(); }
           }
         else
            Print("send "+Symbol()+" tick to server");
        }
      else
        {
         int res=0;
         char wsaData[]; ArrayResize(wsaData,sizeof(WSAData));
         res=WSAStartup(MAKEWORD(2,2),wsaData);
         if(res!=0) { Print("-WSAStartup failed error: "+string(res)); return; }
      
         client=socket(AF_INET,SOCK_DGRAM,IPPROTO_UDP);
         if(client==INVALID_SOCKET64) { Print("-Create failed error: "+WSAErrorDescript(WSAGetLastError())); CloseClean(); return; }
      
         int non_block=1;
         res=ioctlsocket(client,(int)FIONBIO,non_block);
         if(res!=NO_ERROR) { Print("ioctlsocket failed error: "+string(res)); CloseClean(); return; }
      
         Print("create socket OK");
        }
}
void BuildText(uchar &data[])
   {
      StringToCharArray(Symbol()+" "+DoubleToString(SymbolInfoDouble(Symbol(),SYMBOL_BID),Digits()),data);
   }
void CloseClean()
   {
      if(client!=INVALID_SOCKET64)
        {
         if(shutdown(client,SD_BOTH)==SOCKET_ERROR) Print("-Shutdown failed error: "+WSAErrorDescript(WSAGetLastError()));
         closesocket(client); client=INVALID_SOCKET64;
        }
      
      WSACleanup();
      Print("close socket");
   }