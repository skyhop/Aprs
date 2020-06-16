using Skyhop.AprsClient.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Skyhop.AprsClient
{
    public static class Constants
    {
        internal static class Regexes
        {
            internal static readonly Regex AltitudeRegex = new Regex(@"A\=(\d\d\d\d\d\d)", RegexOptions.Compiled);
            internal static readonly Regex CallsignRegex = new Regex(@"^([^\>]*)\>(.*)$", RegexOptions.Compiled);
        }

        internal static class Maps
        {

            internal static readonly Dictionary<int, DataType> DataTypeMap = new Dictionary<int, DataType>
            {
                {0x1c, DataType.CurrentMicERev0},
                {0x1d, DataType.OldMicERev0},
                {0x21, DataType.PositionWithoutTimestampNoAprsMessaging},       // 0x21 == '!'
                {0x23, DataType.PeetBrosUiiWxStation},                          // 0x23 == '#'
                {0x24, DataType.RawGpsDataOrUltimeter2000},                     // 0x24 == '$'
                {0x25, DataType.AgreloDfJrMicroFinder},                         // 0x25 == '%'
                {0x27, DataType.OldMicE},                                       // 0x27 == '\'
                {0x29, DataType.Item},                                          // 0x29 == ')'
                {0x2a, DataType.PeetBrosUiiWxStation},                          // 0x2a == '*'
                {0x2b, DataType.ShelterDataWithTime},                           // 0x2b == '+'
                {0x2c, DataType.InvalidOrTestData},                             // 0x2c == ','
                {0x2e, DataType.SpaceWeather},                                  // 0x2e == '.'
                {0x2f, DataType.PositionWithTimestampNoAprsMessaging},          // 0x2f == '/'
                {0x3a, DataType.Message},                                       // 0x3a == ':'
                {0x3b, DataType.Object},                                        // 0x3b == ';'
                {0x3c, DataType.StationCapabilities},                           // 0x3c == '<'
                {0x3d, DataType.PositionWithoutTimestampWithAprsMessaging},     // 0x3d == '='
                {0x3e, DataType.Status},                                        // 0x3e == '>'
                {0x3f, DataType.Query},                                         // 0x3f == '?'
                {0x40, DataType.PositionWithTimestampWithAprsMessaging},        // 0x40 == '@'
                {0x54, DataType.TelemetryData},                                 // 0x54 == 'T'
                {0x5b, DataType.MaidenheadGridLocatorBeacon},                   // 0x5b == '['
                {0x5f, DataType.WeatherReportWithoutPosition},                  // 0x5f == '_'
                {0x60, DataType.CurrentMicE},                                   // 0x60 == '`'
                {0x7b, DataType.UserDefinedAprsPacketFormat},                   // 0x7b == '{'
                {0x7d, DataType.ThirdPartyTraffic}                              // 0x7d == '}'
            };

            internal static readonly Dictionary<string, MicEMessageType> CustomMicEMessageTypeMap = new Dictionary
                <string, MicEMessageType>
            {
                {"111", MicEMessageType.Custom0},
                {"110", MicEMessageType.Custom1},
                {"101", MicEMessageType.Custom2},
                {"100", MicEMessageType.Custom3},
                {"011", MicEMessageType.Custom4},
                {"010", MicEMessageType.Custom5},
                {"001", MicEMessageType.Custom6}
            };

            private static readonly Dictionary<char, Symbol> primarySymbolTableSymbolMap = new Dictionary<char, Symbol>
            {
                { '!', Symbol.Police },
                { '"', Symbol.Reserved },
                { '#', Symbol.DIGI },
                { '$', Symbol.Phone },
                { '%', Symbol.DxCluster },
                { '&', Symbol.HfGateway },
                { '\'', Symbol.Aircraft },
                { '(', Symbol.Cloud },
                { ')', Symbol.None },
                { '*', Symbol.Snow },
                { '+', Symbol.RedCross },
                { ',', Symbol.ReverseLShape },
                { '-', Symbol.HouseQth },
                { '.', Symbol.X },
                { '/', Symbol.Dot },
                { '0', Symbol.NumericalCircle },
                { '1', Symbol.NumericalCircle },
                { '2', Symbol.NumericalCircle },
                { '3', Symbol.NumericalCircle },
                { '4', Symbol.NumericalCircle },
                { '5', Symbol.NumericalCircle },
                { '6', Symbol.NumericalCircle },
                { '7', Symbol.NumericalCircle },
                { '8', Symbol.NumericalCircle },
                { '9', Symbol.NumericalCircle },
                { ':', Symbol.Fire },
                { ';', Symbol.Campground },
                { '<', Symbol.Motorcycle },
                { '=', Symbol.RailroadEngine },
                { '>', Symbol.Car },
                { '?', Symbol.Server },
                { '@', Symbol.Hurricane },
                { 'A', Symbol.AidStation },
                { 'B', Symbol.BBS },
                { 'C', Symbol.Canoe },
                { 'D', Symbol.None },
                { 'E', Symbol.None },
                { 'F', Symbol.None },
                { 'G', Symbol.GridSquare },
                { 'H', Symbol.Hotel },
                { 'I', Symbol.TcpIp },
                { 'J', Symbol.None },
                { 'K', Symbol.School },
                { 'L', Symbol.Avail },
                { 'M', Symbol.MacAprs },
                { 'N', Symbol.NtsStation },
                { 'O', Symbol.Balloon },
                { 'P', Symbol.Police },
                { 'Q', Symbol.Tbd },
                { 'R', Symbol.RecreationalVehicle },
                { 'S', Symbol.Shuttle },
                { 'T', Symbol.Thunderstorm },
                { 'U', Symbol.Bus },
                { 'V', Symbol.Tbd },
                { 'W', Symbol.WxServiceSite },
                { 'X', Symbol.Helo },
                { 'Y', Symbol.Yacht },
                { 'Z', Symbol.WinAprs },
                { '[', Symbol.Runner },
                { '\\', Symbol.Triangle },
                { ']', Symbol.Pbbs },
                { '^', Symbol.LargeAircraft },
                { '_', Symbol.WeatherStation },
                { '`', Symbol.DishAntenna },
                { 'a', Symbol.Ambulance },
                { 'b', Symbol.Bike },
                { 'c', Symbol.Tbd },
                { 'd', Symbol.DualGarage },
                { 'e', Symbol.Horse },
                { 'f', Symbol.FireTruck },
                { 'g', Symbol.Glider },
                { 'h', Symbol.Hospital },
                { 'i', Symbol.Iota },
                { 'j', Symbol.Jeep },
                { 'k', Symbol.Truck },
                { 'l', Symbol.AreaLocations },
                { 'm', Symbol.MicRepeater },
                { 'n', Symbol.Node },
                { 'o', Symbol.Eoc },
                { 'p', Symbol.Rover },
                { 'q', Symbol.GridSq },
                { 'r', Symbol.Antenna },
                { 's', Symbol.Ship },
                { 't', Symbol.TruckStop },
                { 'u', Symbol.Truck },
                { 'v', Symbol.Van },
                { 'w', Symbol.Water },
                { 'x', Symbol.XAprs },
                { 'y', Symbol.Yagi },
                { 'z', Symbol.None },
                { '{', Symbol.None },
                { '|', Symbol.Reserved },
                { '}', Symbol.Diamond },
                { '~', Symbol.Reserved }
            };

            internal static readonly Dictionary<char, Symbol> SecondarySymbolTableSymbolMap = new Dictionary
                <char, Symbol>
            {
                {'`', Symbol.Aircraft},
                {'^', Symbol.Aircraft }
            };

            internal static readonly Dictionary<string, MicEMessageType> StandardMicEMessageTypeMap = new Dictionary
                <string, MicEMessageType>
            {
                {"111", MicEMessageType.OffDuty},
                {"110", MicEMessageType.EnRoute},
                {"101", MicEMessageType.InService},
                {"100", MicEMessageType.Returning},
                {"011", MicEMessageType.Committed},
                {"010", MicEMessageType.Special},
                {"001", MicEMessageType.Priority}
            };

            internal static readonly Dictionary<char, SymbolTable> SymbolTableMap = new Dictionary<char, SymbolTable>
            {
                {'/', SymbolTable.Primary},
                {'\\', SymbolTable.Secondary}
            };

            internal static Dictionary<char, Symbol> PrimarySymbolTableSymbolMap => primarySymbolTableSymbolMap;
        }
    }
}
