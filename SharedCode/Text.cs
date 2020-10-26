using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    [PacketDescription("Text Packet", "Simple Packet transferring text")]
    public class Text : Packet
    {
        public string Content;

        public Text(string content)
        {
            Content = content;
        }
    }
}
