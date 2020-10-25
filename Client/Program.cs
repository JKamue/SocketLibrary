﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharedCode;
using SocketLibrary.SocketConnection;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new UnsecuredUdpConnection(51001);
            var serverAddress = IPAddress.Parse("127.0.0.1");
            var recipient = new IPEndPoint(serverAddress, 51000);

            connection.Send(new Text("Test test test 123"), recipient);
            connection.Send(new OtherType(true), recipient);
            connection.Send(new Text("Second text"), recipient);
            connection.Send(new OtherType(false), recipient);
            Console.ReadKey();
        }
    }
}
