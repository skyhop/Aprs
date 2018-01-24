using System;

namespace Boerman.AprsClient.Models
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public PacketReceivedEventArgs(AprsMessage pi)
        {
            AprsMessage = pi;
        }

        public AprsMessage AprsMessage { get; private set; }
    }
}