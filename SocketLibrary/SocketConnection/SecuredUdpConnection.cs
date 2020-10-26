using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketLibrary.Encryption;
using SocketLibrary.Messages;

namespace SocketLibrary.SocketConnection
{
    public sealed class SecuredUdpConnection
    {
        private readonly UdpClient _udpClient;
        private readonly AesEncryptionProvider _encryptionProvider;
        private readonly bool _debug;

        public SecuredUdpConnection(int myPort, AesEncryptionProvider encryptionProvider, bool debug = false)
        {
            _udpClient = new UdpClient(myPort);
            _encryptionProvider = encryptionProvider;
            _debug = debug;
            BeginReceive();
        }

        public event EventHandler<SocketEventArgs> OnMessageReceived;
        public void Send(Packet obj, IPEndPoint recipient)
        {
            var message = Message.Create(obj);
            var content = Serializer.Serialize(message);
            var enc = _encryptionProvider.Encrypt(content);
            _udpClient.Send(enc, enc.Length, recipient);
        }

        private void BeginReceive()
        {
            try
            {
                _udpClient.BeginReceive(DataReceived, _udpClient);
            }
            catch (SocketException e)
            {
                Log($"Could not start listening to packets");
            }
        }

        private void DataReceived(IAsyncResult ar)
        {
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                var receivedBytes = _udpClient.EndReceive(ar, ref receivedIpEndPoint);
                var dec = _encryptionProvider.Decrypt(receivedBytes);

                var message = Serializer.Deserialize<Message>(dec);

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
            catch (FileNotFoundException e)
            {
                Log($"Received from {receivedIpEndPoint}: \tUnknown, not deserializable packet");
                BeginReceive();
            }

            BeginReceive();
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
