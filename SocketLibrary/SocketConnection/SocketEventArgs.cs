using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocketLibrary.SocketConnection
{
    public class SocketEventArgs
    {
        public IPEndPoint SenderEndPoint;
        public dynamic Packet;

        public SocketEventArgs(IPEndPoint senderEndPoint, dynamic packet)
        {
            SenderEndPoint = senderEndPoint;
            Packet = packet;
        }
    }
}
