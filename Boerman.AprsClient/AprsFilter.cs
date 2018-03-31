using System;
using Boerman.Core.Extensions;

namespace Boerman.AprsClient
{
    public abstract class AprsFilter
    {
        /// <summary>
        /// Pass posits and objects within dist km from lat/lon. lat and lon are 
        /// signed decimal degrees, i.e.negative for West/South and positive for 
        /// East/North.Up to 9 range filters can be defined at the same time to 
        /// allow better coverage.Messages addressed to stations within the 
        /// range are also passed.
        /// </summary>
        public class Range : AprsFilter
        {
            public Range(double latitude, double longitude, int range)
            {
                Result = $"r/{latitude}/{longitude}/{range}";
            }
        }

        /// <summary>
        /// Pass traffic with fromCall that start with aa or bb or cc...
        /// </summary>
        public class Prefix : AprsFilter
        {
            public Prefix(params string[] prefix)
            {
                if (prefix == null) return;

                Result = "p";
                prefix.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// Pass all traffic from exact call: call1, call2, ... (* wild card 
        /// allowed)
        /// </summary>
        public class Budlist : AprsFilter
        {
            public Budlist(params string[] buds)
            {
                if (buds == null) return;

                Result = "b";
                buds.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// Pass all objects with the exact name of obj1, obj2, ... (* wild card 
        /// allowed)(spaces not allowed) (| => / and ~ => *)
        /// </summary>
        public class Object : AprsFilter
        {
            public Object(params string[] objects)
            {
                if (objects == null) return;

                Result = "os";
                objects.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// Pass all traffic based on packet type.
        /// One or more types can be defined at the same time, t/otq is a valid definition.
        /// 
        /// p = Position packets
        /// o = Objects
        /// i = Items
        /// m = Message
        /// q = Query
        /// s = Status
        /// t = Telemetry
        /// u = User-defined
        /// n = NWS format messages and objects
        /// w = Weather
        /// 
        /// Note: The weather type filter also passes positions packets for positionless weather packets.
        /// </summary>
        public class Type : AprsFilter
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:Boerman.AprsClient.AprsFilter.Type"/> class.
            /// </summary>
            /// <param name="types">Types.</param>
            public Type(Types types)
            {
                if (types == 0) return;

                Result = $"t/{BuildFlags(types)}";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Boerman.AprsClient.AprsFilter.Type"/> class.
            /// 
            /// This format allows putting a radius limit around "call" (station callsign-SSID or object name) for the requested station types.
            /// </summary>
            /// <param name="types">Types.</param>
            /// <param name="callsign">Callsign.</param>
            /// <param name="radius">Radius.</param>
            public Type(Types types, string callsign, int radius)
            {
                if (types == 0 || callsign == null || radius <= 0) return;

                Result = $"t/{BuildFlags(types)}/{callsign}/{radius}";
            }

            private string BuildFlags(Types types)
            {
                string result = "";

                if (types.HasFlag(Types.PositionPackets)) result += "p";
                if (types.HasFlag(Types.Objects)) result += "o";
                if (types.HasFlag(Types.Items)) result += "i";
                if (types.HasFlag(Types.Message)) result += "m";
                if (types.HasFlag(Types.Query)) result += "q";
                if (types.HasFlag(Types.Status)) result += "s";
                if (types.HasFlag(Types.Telemetry)) result += "t";
                if (types.HasFlag(Types.UserDefined)) result += "u";
                if (types.HasFlag(Types.NWS)) result += "n";
                if (types.HasFlag(Types.Weather)) result += "w";

                return result;
            }
        }

        /// <summary>
        /// pri = symbols in primary table (| => /)
        /// alt = symbols in alternate table(| => /)
        /// over = overlay character(case sensitive)
        /// For example:
        /// s/->     This will pass all House and Car symbols(primary table)
        /// s//#     This will pass all Digi with or without overlay
        /// s//#/T   This will pass all Digi with overlay of capital "T"
        /// </summary>
        public class Symbol : AprsFilter
        {
            public Symbol(string primary, string alternate, string overlay)
            {
                if (String.IsNullOrWhiteSpace(primary)
                    || String.IsNullOrWhiteSpace(alternate)
                    || String.IsNullOrWhiteSpace(overlay)) return;

                Result = $"s/{primary}/{alternate}/{overlay}";
            }
        }

        /// <summary>
        /// The digipeater filter will pass all packets that have been 
        /// digipeated by a particular station(s) (the station's call is in the 
        /// path). This filter allows the * wildcard.
        /// </summary>
        public class Digipeater : AprsFilter
        {
            public Digipeater(params string[] stations)
            {
                if (stations == null) return;

                Result = "d";
                stations.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// The area filter works the same as rang filter but the filter is 
        /// defined as a box of coordinates. The coordinates can also been seen 
        /// as upper left coordinate and lower right. Lat/lon are decimal 
        /// degrees. South and west are negative. Up to 9 area filters can be 
        /// defined at the same time.
        /// </summary>
        public class Area : AprsFilter
        {
            public Area(double latitudeNorth,
                        double longitudeWest,
                        double latitudeSouth,
                        double longitudeEast)
            {
                if (latitudeNorth == 0
                    || longitudeWest == 0
                    || latitudeSouth == 0
                    || longitudeEast == 0) return;

                Result = $"a/{latitudeNorth}/{longitudeWest}/{latitudeSouth}/{longitudeEast}";
            }
        }

        /// <summary>
        /// This filter passes all packets with the specified callsign-SSID(s) 
        /// immediately following the q construct. This allows filtering based 
        /// on receiving IGate, etc. Supports * wildcard.
        /// </summary>
        public class EntryStation : AprsFilter
        {
            public EntryStation(params string[] callsigns)
            {
                if (callsigns == null) return;

                Result = "e";
                callsigns.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// This filter passes all message packets with the specified 
        /// callsign-SSID(s) as the addressee of the message. Supports * wildcard.
        /// </summary>
        public class GroupMessage : AprsFilter
        {
            public GroupMessage(params string[] callsigns)
            {
                if (callsigns == null) return;

                Result = "g";
                callsigns.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// This filter passes all packets with the specified destination 
        /// callsign-SSID(s) (also known as the To call or unproto call). 
        /// Supports * wildcard.
        /// </summary>
        public class Unproto : AprsFilter
        {
            public Unproto(params string[] callsigns)
            {
                if (callsigns == null) return;

                Result = "u";
                callsigns.ForEach(q => Result += $"/{q}");
            }
        }

        /// <summary>
        /// This is the same as the range filter except that the center is 
        /// defined as the last known position of the logged in client.
        /// </summary>
        public class MyRange : AprsFilter
        {
            public MyRange(int distance)
            {
                if (distance == 0) return;

                Result = $"m/{distance}";
            }
        }

        /// <summary>
        /// This is the same as the range filter except that the center is 
        /// defined as the last known position of call. Up to 9 friend filters 
        /// can be defined at the same time.
        /// </summary>
        public class FriendRange : AprsFilter
        {
            public FriendRange(string callsign, int distance)
            {
                if (String.IsNullOrWhiteSpace(callsign) || distance == 0) return;

                Result = $"f/{callsign}/{distance}";
            }
        }

        [Flags]
        public enum Types
        {
            None = 0,
            PositionPackets = 1,
            Objects = 2,
            Items = 4,
            Message = 8,
            Query = 16,
            Status = 32,
            Telemetry = 64,
            UserDefined = 128,
            NWS = 256,
            Weather = 512,

            All = 1023
        }

        internal string Result { get; set; }
    }
}
