using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SocketLibrary.Messages;

namespace SocketLibrary.Encryption
{
    public sealed class RsaEncryptionProvider
    {
        private readonly RSACryptoServiceProvider _csp;
        public RsaEncryptionProvider()
        {
            _csp = new RSACryptoServiceProvider(2048);
        }

        public byte[] Decrypt(byte[] data)
        {
            return _csp.Decrypt(data, false);
        }

        public RSAParameters PublicKey => _csp.ExportParameters(false);

        public static byte[] Encrypt(byte[] data, RSAParameters key)
        {
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(key);
            return csp.Encrypt(data, false);
        }
    }
}
