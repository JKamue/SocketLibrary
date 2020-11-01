using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLibrary.Encryption
{
    [Serializable]
    public class AesKeyPair
    {
        public byte[] Key;
        public byte[] Iv;

        public AesKeyPair(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}
