using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SocketLibrary.Messages
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct,
            AllowMultiple = true)  // Multiuse attribute.  
    ]
    public class PacketDescription : System.Attribute
    {
        public string Title;
        public string Description;

        public PacketDescription(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }

    [Serializable]
    [PacketDescription("Unspecified Packet", "No description has been set for this packet")]
    public abstract class Packet
    {
        public string Title => GetPacketDescription().Title;
        public string Description => GetPacketDescription().Description;

        private PacketDescription GetPacketDescription()
        {
            return (PacketDescription) System.Attribute.GetCustomAttributes(GetType()).First(a => a is PacketDescription);
        }
    }
}
