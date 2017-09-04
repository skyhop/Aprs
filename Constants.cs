using System.Collections.Generic;
using System.Text.RegularExpressions;
using Boerman.Aeronautics.AprsClient.Enums;

namespace Boerman.Aeronautics.AprsClient
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

            internal static readonly Dictionary<char, Symbol> PrimarySymbolTableSymbolMap = new Dictionary<char, Symbol>
            {
                { '\'', Symbol.Aircraft },
                {'^', Symbol.Aircraft }
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
        }
    }
}
