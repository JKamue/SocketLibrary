using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketLibrary.Messages;

namespace SocketLibrary.SocketConnection
{
    public class UnsecuredUdpConnection : ISocketConnection
    {
        private readonly UdpClient _udpClient;
        private readonly bool _debug;

        public UnsecuredUdpConnection(int myPort, bool debug = false)
        {
            _udpClient = new UdpClient(myPort);
            _debug = debug;
            _udpClient.BeginReceive(DataReceived, _udpClient);
        }

        public event EventHandler<SocketEventArgs> OnMessageReceived;
        public void Send(Packet obj, IPEndPoint recipient)
        {
            var message = Message.Create(obj);
            var content = Serializer.Serialize(message);
            _udpClient.Send(content, content.Length, recipient);
        }

        private void DataReceived(IAsyncResult ar)
        {
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                var receivedBytes = _udpClient.EndReceive(ar, ref receivedIpEndPoint);
                var message = Serializer.Deserialize<Message>(receivedBytes);
                var content = Serializer.DeserializeType(message.content, message.ContentType);

                if (_debug)
                {
                    var packet = (Packet)content;
                    Console.WriteLine($"Received from {receivedIpEndPoint}: \t{packet.Title} - {packet.Description}");
                }

                OnMessageReceived?.Invoke(this, new SocketEventArgs(receivedIpEndPoint, content));
            }
            catch (SocketException e)
            {
                Log("Receive error: " + e.Message);
            }
            catch (ObjectDisposedException e)
            {
                Log("Receive Error: " + e.Message);
                return;
            }

            try
            {
                _udpClient.BeginReceive(DataReceived, _udpClient);
            }
            catch (SocketException e)
            {
                Log($"Could not restart listening to packets");
            }
        }

        private void Log(string mes)
        {
            if (_debug)
                Console.WriteLine(mes);
        }

        public void Close()
        {
            _udpClient.Client?.Shutdown(SocketShutdown.Both);
            _udpClient.Client?.Close();
            _udpClient.Close();
            _udpClient.Dispose();
        }
    }
}
