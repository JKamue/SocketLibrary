using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    public class Text : Packet
    {
        public string Content;

        public Text(string content)
        {
            Content = content;
        }
    }
}
