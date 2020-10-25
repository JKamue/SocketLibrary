using System;
using System.Collections.Generic;
using System.Text;
using SocketLibrary.Messages;

namespace SharedCode
{
    [Serializable]
    public class Text : IPacket
    {
        public string Content;

        public Text(string content)
        {
            Content = content;
        }
    }
}
