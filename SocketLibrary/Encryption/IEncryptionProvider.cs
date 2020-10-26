using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLibrary.Encryption
{
    public interface IEncryptionProvider
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
