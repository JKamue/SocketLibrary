using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    public class OtherType : Packet
    {
        public bool Status;

        public OtherType(bool status)
        {
            Status = status;
        }
    }
}
