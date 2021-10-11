﻿using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Boerman.Core.Spatial;
using Skyhop.Aprs.Client.Enums;

namespace Skyhop.Aprs.Client.Models
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
        public Symbol? Symbol { get; internal set; }
        public char SymbolOverlay { get; internal set; }
        public MicEMessageType MicEMessageType { get; internal set; }
        public DateTime ReceivedDate { get; internal set; }

        public string DeviceId { get; internal set; }

        public AircraftType AircraftType { get; internal set; }

        public AddressType AddressType { get; internal set; }

        public bool StealthMode { get; internal set; }

        public bool NoTrackingFlag { get; internal set; }

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
            int padding = 17;
            var sb = new StringBuilder();

            sb.AppendLine($"Packet Information Received {ReceivedDate} UTC");
            sb.AppendLine($"{(nameof(Callsign).SplitCamelCase().PadLeft(padding))}: {Callsign}");
            sb.AppendLine($"{(nameof(StationRoute).SplitCamelCase().PadLeft(padding))}: {string.Join(", ", StationRoute)}");
            sb.AppendLine($"{(nameof(DataType).SplitCamelCase().PadLeft(padding))}: {DataType}");
            sb.AppendLine($"{(nameof(Latitude).SplitCamelCase().PadLeft(padding))}: {Latitude}");
            sb.AppendLine($"{(nameof(Longitude).SplitCamelCase().PadLeft(padding))}: {Longitude}");
            sb.AppendLine($"{(nameof(Altitude).SplitCamelCase().PadLeft(padding))}: {Altitude}");
            sb.AppendLine($"{(nameof(Direction).SplitCamelCase().PadLeft(padding))}: {Direction}");
            sb.AppendLine($"{(nameof(Speed).SplitCamelCase().PadLeft(padding))}: {Speed}");
            sb.AppendLine($"{(nameof(SymbolTable).SplitCamelCase().PadLeft(padding))}: {SymbolTable}");
            sb.AppendLine($"{(nameof(Symbol).SplitCamelCase().PadLeft(padding))}: {Symbol}");
            sb.AppendLine($"{(nameof(DeviceId).SplitCamelCase().PadLeft(padding))}: {DeviceId}");
            sb.AppendLine($"{(nameof(AircraftType).SplitCamelCase().PadLeft(padding))}: {AircraftType}");
            sb.AppendLine($"{(nameof(AddressType).SplitCamelCase().PadLeft(padding))}: {AddressType}");

            if (DataType == DataType.CurrentMicE)
                sb.AppendLine($"{(nameof(MicEMessageType).SplitCamelCase().PadLeft(padding))}: {MicEMessageType}");

            return sb.ToString();
        }
    }

    static class SplitCamelCaseExtension
    {
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }
    }
}
