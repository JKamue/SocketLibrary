using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLibrary.Messages
{
    [Serializable]
    class Message
    {
        public Type ContentType;
        public byte[] content;

        private Message(Type contentType, byte[] content)
        {
            ContentType = contentType;
            this.content = content;
        }

        internal static Message Create(Packet obj)
        {
            return new Message(obj.GetType(), Serializer.Serialize(obj));
        }
    }
}
