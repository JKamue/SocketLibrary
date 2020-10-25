using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SocketLibrary.Messages
{
    internal static class Serializer
    {
        internal static byte[] Serialize<T>(T obj)
        {
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        internal static T Deserialize<T>(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        internal static dynamic DeserializeType(byte[] bytes, Type type)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter br = new BinaryFormatter();
                return Convert.ChangeType(br.Deserialize(ms), type);
            }
        }
    }
}
