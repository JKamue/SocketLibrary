using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    public class OtherType : IPacket
    {
        public bool Status;

        public OtherType(bool status)
        {
            Status = status;
        }
    }
}
