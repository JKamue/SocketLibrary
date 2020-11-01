using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using SocketLibrary.Messages;

namespace SocketLibrary.Encryption
{
    public sealed class AesEncryptionProvider : IEncryptionProvider
    {
        private readonly Aes _aes;

        private AesEncryptionProvider(Aes aes)
        {
            _aes = aes;
        }

        public static AesEncryptionProvider Create()
        {
            return new AesEncryptionProvider(Aes.Create());
        }

        public static AesEncryptionProvider Create(AesKeyPair pair)
        {
            var aes = Aes.Create();
            aes.Key = pair.Key;
            aes.IV = pair.Iv;
            return new AesEncryptionProvider(aes);
        }

        public AesKeyPair Export() => new AesKeyPair(_aes.Key, _aes.IV);

        private byte[] PerformCryptography(ICryptoTransform cryptoTransform, byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            using (var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV))
            {
                return PerformCryptography(encryptor, data);
            }
        }


        public byte[] Decrypt(byte[] data)
        {
            using (var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV)) 
            {
                return PerformCryptography(decryptor, data);
            }
        }
    }
}
