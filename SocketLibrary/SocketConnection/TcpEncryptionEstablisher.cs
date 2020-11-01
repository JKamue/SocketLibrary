using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SocketLibrary.Encryption;
using SocketLibrary.Messages;

namespace SocketLibrary.SocketConnection
{
    public class TcpEncryptionEstablisher
    {
        private readonly TcpListener _server = null;
        private bool Open = true;
        Byte[] bytes = new Byte[8192];

        public event EventHandler<EncryptionEstablishedArgs> OnEncryptionEstablished;

        public TcpEncryptionEstablisher(int port)
        {
            _server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            StartAsync();
        }

        public async Task StartAsync()
        {
            _server.Start();

            while (Open)
            {
                TcpClient client =  await _server.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                var endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
                var aes = AesEncryptionProvider.Create();
                Console.WriteLine("Server is receiving client");
                while ((_ = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var rsaKey = Serializer.Deserialize<RSAParameters>(bytes);
                    var aesKey = aes.Export();
                    var serializedKey = Serializer.Serialize(aesKey);
                    var encryptedKey = RsaEncryptionProvider.Encrypt(serializedKey, rsaKey);

                    Console.WriteLine("Got key, sending AES key");
                    stream.Write(encryptedKey, 0, encryptedKey.Length);
                    Console.WriteLine("Key sent");
                }
                OnEncryptionEstablished?.Invoke(this, new EncryptionEstablishedArgs(endpoint, aes));
            }
        }

        public static AesEncryptionProvider EstablishEncryption(IPEndPoint target)
        {
            var rsa = new RsaEncryptionProvider();
            var publicKey = rsa.PublicKey;
            var serializedKey = Serializer.Serialize(publicKey);

            TcpClient client = new TcpClient("127.0.0.1", 52000);
            NetworkStream stream = client.GetStream();
            stream.Write(serializedKey, 0, serializedKey.Length);
            Console.WriteLine((new System.Text.ASCIIEncoding()).GetString(serializedKey));


            string str;
            using (stream = client.GetStream())
            {
                byte[] data = new byte[1024];
                using (MemoryStream ms = new MemoryStream())
                {

                    int numBytesRead;
                    while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                    {
                        ms.Write(data, 0, numBytesRead);
                    }
                    str = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
                }
            }

            var aesKey = Serializer.Deserialize<AesKeyPair>((new System.Text.ASCIIEncoding()).GetBytes(str));

            Console.WriteLine(str);

            return AesEncryptionProvider.Create(aesKey);
        }

        public void Close()
        {
            Open = false;
        }
    }
}
