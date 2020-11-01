using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocketLibrary.Encryption
{
    public class EncryptionEstablishedArgs
    {
        public IPEndPoint EndPoint;
        public IEncryptionProvider EncryptionProvider;

        public EncryptionEstablishedArgs(IPEndPoint endPoint, IEncryptionProvider encryptionProvider)
        {
            EndPoint = endPoint;
            EncryptionProvider = encryptionProvider;
        }
    }
}
