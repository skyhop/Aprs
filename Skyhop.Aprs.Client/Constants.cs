using System.Collections.Generic;
using System.Text.RegularExpressions;
using Skyhop.Aprs.Client.Enums;

namespace Skyhop.Aprs.Client
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
                { '!', Symbol.Police },
                { '"', Symbol.Reserved },
                { '#', Symbol.Digi },
                { '$', Symbol.Phone },
                { '%', Symbol.DxCluster },
                { '&', Symbol.HfGateway },
                { '\'', Symbol.SmallAircraft },
                { '(', Symbol.MobileSatelliteGroundstation },
                { ')', Symbol.Empty },
                { '*', Symbol.Snowmobile },
                { '+', Symbol.RedCross },
                { ',', Symbol.BoyScouts },
                { '-', Symbol.HouseQth },
                { '.', Symbol.X },
                { '/', Symbol.Dot },
                { '0', Symbol.Zero },
                { '1', Symbol.One },
                { '2', Symbol.Two },
                { '3', Symbol.Three },
                { '4', Symbol.Four },
                { '5', Symbol.Five },
                { '6', Symbol.Six },
                { '7', Symbol.Seven },
                { '8', Symbol.Eight },
                { '9', Symbol.Nine },
                { ':', Symbol.Fire },
                { ';', Symbol.Campground },
                { '<', Symbol.Motorcycle },
                { '=', Symbol.RailroadEnigne },
                { '>', Symbol.Car },
                { '?', Symbol.FileServer },
                { '@', Symbol.HurricaneFuturePrediction },
                { 'A', Symbol.AidStation },
                { 'B', Symbol.BBS },
                { 'C', Symbol.Canoe },
                { 'D', Symbol.Empty },
                { 'E', Symbol.Eyeball },
                { 'F', Symbol.Empty },
                { 'G', Symbol.GridSquare },
                { 'H', Symbol.Hotel },
                { 'I', Symbol.TcpIp },
                { 'J', Symbol.Empty },
                { 'K', Symbol.School },
                { 'L', Symbol.Empty },
                { 'M', Symbol.MacAprs },
                { 'N', Symbol.NtsStation },
                { 'O', Symbol.Balloon },
                { 'P', Symbol.Police },
                { 'Q', Symbol.Empty },
                { 'R', Symbol.RecreationalVehicle },
                { 'S', Symbol.SpaceShuttle },
                { 'T', Symbol.Sstv },
                { 'U', Symbol.Bus },
                { 'V', Symbol.Atv },
                { 'W', Symbol.NationalWeatherServiceSite },
                { 'X', Symbol.Helicopter },
                { 'Y', Symbol.Yacht },
                { 'Z', Symbol.WinAprs },
                { '[', Symbol.Jogger },
                { '\\', Symbol.Triangle },
                { ']', Symbol.Pbbs },
                { '^', Symbol.LargeAircraft },
                { '_', Symbol.WeatherStation },
                { '`', Symbol.DishAntenna },
                { 'a', Symbol.Ambulance },
                { 'b', Symbol.Bicycle },
                { 'c', Symbol.Empty },
                { 'd', Symbol.DualGarage },
                { 'e', Symbol.Horse },
                { 'f', Symbol.FireTruck },
                { 'g', Symbol.Glider },
                { 'h', Symbol.Hospital },
                { 'i', Symbol.Iota },
                { 'j', Symbol.Jeep },
                { 'k', Symbol.Truck },
                { 'l', Symbol.Empty },
                { 'm', Symbol.MicRepeater },
                { 'n', Symbol.Node },
                { 'o', Symbol.EmergencyOperationsCenter },
                { 'p', Symbol.Rover },
                { 'q', Symbol.GridSquareAbove128Meters },
                { 'r', Symbol.Antenna },
                { 's', Symbol.Ship },
                { 't', Symbol.TruckStop },
                { 'u', Symbol.EighteenWheeler },
                { 'v', Symbol.Van },
                { 'w', Symbol.WaterStation },
                { 'x', Symbol.XAprs },
                { 'y', Symbol.Yagi },
                { 'z', Symbol.Empty },
                { '{', Symbol.Empty },
                { '|', Symbol.Reserved },
                { '}', Symbol.Empty },
                { '~', Symbol.Reserved }
            };

            internal static readonly Dictionary<char, Symbol> SecondarySymbolTableSymbolMap = new Dictionary
                <char, Symbol>
            {
                { '!', Symbol.Emergency },
                { '"', Symbol.Reserved },
                { '#', Symbol.Digi },
                { '$', Symbol.Atm },
                { '%', Symbol.Empty },
                { '&', Symbol.HfGateway },
                { '\'', Symbol.CrashSite },
                { '(', Symbol.Cloudy },
                { ')', Symbol.Empty },
                { '*', Symbol.Snow },
                { '+', Symbol.Church},
                { ',', Symbol.GirlScouts },
                { '-', Symbol.HouseQth },
                { '.', Symbol.Unknown },
                { '/', Symbol.Empty },
                { '0', Symbol.Circle },
                { '1', Symbol.Empty },
                { '2', Symbol.Empty },
                { '3', Symbol.Empty },
                { '4', Symbol.Empty },
                { '5', Symbol.Empty },
                { '6', Symbol.Empty },
                { '7', Symbol.Empty },
                { '8', Symbol.Empty },
                { '9', Symbol.GasStation },
                { ':', Symbol.Hail },
                { ';', Symbol.Park },
                { '<', Symbol.NwsAdvisory },
                { '=', Symbol.Empty },
                { '>', Symbol.Car },
                { '?', Symbol.InformationKiosk },
                { '@', Symbol.Hurricane },
                { 'A', Symbol.Box },
                { 'B', Symbol.BlowingSnow },
                { 'C', Symbol.Coastguard },
                { 'D', Symbol.Drizzle },
                { 'E', Symbol.Smoke },
                { 'F', Symbol.FreezingRain },
                { 'G', Symbol.SnowShower},
                { 'H', Symbol.Haze },
                { 'I', Symbol.RainShower},
                { 'J', Symbol.Lightning },
                { 'K', Symbol.Kenwood },
                { 'L', Symbol.Lighthouse },
                { 'M', Symbol.Empty },
                { 'N', Symbol.NavigationBuoy },
                { 'O', Symbol.Empty },
                { 'P', Symbol.Parking },
                { 'Q', Symbol.Earthquake },
                { 'R', Symbol.Restaurant },
                { 'S', Symbol.Sattelite },
                { 'T', Symbol.Thunderstorm },
                { 'U', Symbol.Sunny },
                { 'V', Symbol.Vortac },
                { 'W', Symbol.NwsSite },
                { 'X', Symbol.Pharmacy },
                { 'Y', Symbol.Empty },
                { 'Z', Symbol.Empty },
                { '[', Symbol.WallCloud },
                { '\\', Symbol.Empty },
                { ']', Symbol.Empty },
                { '^', Symbol.Aircraft },
                { '_', Symbol.Wx },
                { '`', Symbol.Rain },
                { 'a', Symbol.Empty },
                { 'b', Symbol.BlowingDust },
                { 'c', Symbol.CivilDefence },
                { 'd', Symbol.DxSpot },
                { 'e', Symbol.Sleet },
                { 'f', Symbol.FunnelCloud },
                { 'g', Symbol.GaleFlags },
                { 'h', Symbol.HamStore },
                { 'i', Symbol.IndoorShortRange },
                { 'j', Symbol.WorkZone },
                { 'k', Symbol.Empty },
                { 'l', Symbol.AreaSymbols },
                { 'm', Symbol.ValueSignpost },
                { 'n', Symbol.Triangle },
                { 'o', Symbol.SmallSircle },
                { 'p', Symbol.ParlyCloudy },
                { 'q', Symbol.Empty },
                { 'r', Symbol.Restrooms },
                { 's', Symbol.Ship },
                { 't', Symbol.Tornado },
                { 'u', Symbol.Truck },
                { 'v', Symbol.Van  },
                { 'w', Symbol.Flooding },
                { 'x', Symbol.Empty },
                { 'y', Symbol.Empty  },
                { 'z', Symbol.Empty },
                { '{', Symbol.Fog },
                { '|', Symbol.Reserved },
                { '}', Symbol.Empty },
                { '~', Symbol.Reserved }
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
