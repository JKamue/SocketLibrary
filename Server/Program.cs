using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode;
using SocketLibrary.SocketConnection;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new UnsecuredUdpConnection(51000, true);
            connection.OnMessageReceived += Test;
            Console.ReadKey();
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
