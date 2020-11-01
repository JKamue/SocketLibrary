using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SharedCode;
using SocketLibrary.Encryption;
using SocketLibrary.SocketConnection;

namespace Server
{
    class Program
    {
        private static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        private static string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

        static void Main(string[] args)
        {
            var server = new TcpEncryptionEstablisher(52000);
            Console.WriteLine("hi");
            var key = TcpEncryptionEstablisher.EstablishEncryption(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 52000));
            Console.ReadKey();
            return;
            var provider = new RsaEncryptionProvider();
            var mes = StringToByteArray("test test test 123");
            var publicKey = provider.PublicKey;
            var encrypted = RsaEncryptionProvider.Encrypt(mes, publicKey);
            var decrypted = provider.Decrypt(encrypted);
            Console.WriteLine(ByteArrayToString(decrypted));
            Console.ReadKey();
            return;
            var encryption = AesEncryptionProvider.Create();
            var test = "testetsetws12412";
            var enc =encryption.Encrypt(StringToByteArray(test));
            //var decryption = AesEncryptionProvider.Create(encryption.ExportKey(), encryption.ExportIv());
            var dec = ByteArrayToString(encryption.Decrypt(enc));
            Console.WriteLine(dec);
            Console.ReadKey();
            /**
            var connection = new SecuredUdpConnection(51000, encryption,true);
            connection.OnMessageReceived += Test;
            var serverAddress = IPAddress.Parse("127.0.0.1");
            var recipient = new IPEndPoint(serverAddress, 51000);
            connection.Send(new Text("test"), recipient);
            Console.ReadKey();
    **/
        }

        static void Test(object sender, SocketEventArgs e)
        {
            Handle(e.Packet);
        }

        static void Handle(Text text)
        {
            Console.WriteLine("Received: " + text.Content);
        }

        static void Handle(OtherType other)
        {
            if (other.Status)
            {
                Console.WriteLine("Status received ok");
            }
            else
            {
                Console.WriteLine("Status received not okay");
            }
        }
    }
}
