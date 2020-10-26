using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketLibrary.Messages;

namespace Client
{
    [Serializable]
    class BadPacket : Packet
    {
        public int Test;

        public BadPacket(int test)
        {
            Test = test;
        }
    }
}
