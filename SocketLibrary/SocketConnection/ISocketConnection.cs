using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SocketLibrary.Messages;

namespace SocketLibrary.SocketConnection
{
    public interface ISocketConnection
    {
        event EventHandler<SocketEventArgs> OnMessageReceived;
        void Send(Packet obj, IPEndPoint recipient);
        void Close();
    }
}
