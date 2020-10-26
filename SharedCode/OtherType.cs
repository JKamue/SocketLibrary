using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    // Does not have a PacketDescription Attribute for demonstration purposes
    public class OtherType : Packet
    {
        public bool Status;

        public OtherType(bool status)
        {
            Status = status;
        }
    }
}
