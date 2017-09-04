using System;
using System.Collections.ObjectModel;
using System.Text;
using Boerman.Aeronautics.AprsClient.Enums;

namespace Boerman.Aeronautics.AprsClient.Models
{
    public class AprsMessage
    {
        public static AprsMessage Parse(string message)
        {
            return PacketInfo.Parse(message);
        }

        public string Callsign { get; internal set; }
        public ReadOnlyCollection<string> StationRoute { get; internal set; }
        public DataType DataType { get; internal set; }
        public Latitude Latitude { get; internal set; }
        public Longitude Longitude { get; internal set; }
        public Altitude Altitude { get; internal set; }
        public Heading Direction { get; internal set; }
        public Speed Speed { get; internal set; }
        public SymbolTable SymbolTable { get; internal set; }
        public Symbol Symbol { get; internal set; }
        public MicEMessageType MicEMessageType { get; internal set; }
        public DateTime ReceivedDate { get; internal set; }

        // This is specifically for OGN flavored APRS
        public int ClimbRate { get; internal set; }
        public double TurnRate { get; internal set; }

        public string RawData { get; internal set; }

        /*
         * In case you're wondering why your CPU goes haywire when editing this function, it's due to some
         * bug in codelens. Even editing this comment may set it off </3
         */
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Packet Information Received {ReceivedDate} UTC\n");
            sb.Append($"        Callsign: {Callsign}\n");
            sb.Append($"   Station Route: {string.Join(", ", StationRoute)}\n");
            sb.Append($"        DataType: {DataType}\n");
            sb.Append($"        Latitude: {Latitude}\n");
            sb.Append($"       Longitude: {Longitude}\n");
            sb.Append($"        Altitude: {Altitude}\n");
            sb.Append($"          Course: {Direction}\n");
            sb.Append($"           Speed: {Speed}\n");
            sb.Append($"    Symbol Table: {SymbolTable}\n");
            sb.Append($"          Symbol: {Symbol}\n");

            if (DataType == DataType.CurrentMicE)
                sb.Append($"   Mic E Message: {MicEMessageType}\n");


            return sb.ToString();
        }
    }
}
