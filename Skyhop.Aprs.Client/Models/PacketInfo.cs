/*
 * ToDo: Split up logic in this file so that the parsing logic becomes a little bit more readable/testable
 */

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Boerman.Core.Spatial;
using Skyhop.Aprs.Client.Enums;

namespace Skyhop.Aprs.Client.Models
{
    public static class PacketInfo
    {
        public static AprsMessage Parse(string rawData)
        {
            try
            {
                var match = Constants.Regexes.CallsignRegex.Match(rawData);

                var aprsMessage = new AprsMessage
                {
                    RawData = rawData,
                    ReceivedDate = DateTime.UtcNow,
                    Callsign = match.Groups[1].Value
                };

                rawData = match.Groups[2].Value;

                match = Regex.Match(rawData, @"^([^\:]*)\:(.*)$");

                aprsMessage.StationRoute = new ReadOnlyCollection<string>(match.Groups[1].Value.Split(','));

                rawData = match.Groups[2].Value;

                if (string.IsNullOrEmpty(rawData)) return null;

                // -- id bits and mask ----------------------------------------------------------------
                // according: http://wiki.glidernet.org/wiki:subscribe-to-ogn-data
                //       and: http://www.ediatec.ch/pdf/FLARM%20Data%20Port%20Specification%20v7.00.pdf
                // idXXYYYYYY => XX encoding, YY address

                try
                {
                    var matchAircraft = Regex.Match(rawData, @"(?:\sid)([a-fA-F0-9]{8})(?:\s)");

                    if (matchAircraft.Success)
                    {
                        aprsMessage.DeviceId = matchAircraft.Groups[1].Value.Substring(2);
                        var aircraftId = ulong.Parse(matchAircraft.Groups[1].Value.Trim(), NumberStyles.HexNumber);
                        byte addressTypeAndFlagsByte = (byte)((aircraftId & 0xFF000000) >> 24);
                        aprsMessage.AddressType = (AddressType)(addressTypeAndFlagsByte & 0x03);
                        aprsMessage.AircraftType = (AircraftType)((addressTypeAndFlagsByte & 0x3C) >> 2);
                        aprsMessage.StealthMode = (addressTypeAndFlagsByte & 0x80) > 0;
                        aprsMessage.NoTrackingFlag = (addressTypeAndFlagsByte & 0x40) > 0;
                        //uint aircraftAddress = (uint)(aircraftId & 0x00FFFFFF);
                    }
                }
                catch (Exception ex)
                {
                        
                }

                DataType dataType;

                if (!Constants.Maps.DataTypeMap.TryGetValue(Convert.ToByte(rawData[0]), out dataType))
                    throw new ArgumentException("Unsupported data type in raw data", nameof(rawData));

                aprsMessage.DataType = dataType;

                Symbol symbol;

                switch (aprsMessage.DataType)
                {
                    case DataType.CurrentMicE:
                        Latitude latitude;
                        short longitudeOffset;
                        LongitudeHemisphere longitudeHemisphere;
                        MicEMessageType micEMessageType;

                        DecodeMicEDestinationAddress(
                            aprsMessage.StationRoute[0],
                            out latitude,
                            out longitudeOffset,
                            out longitudeHemisphere,
                            out micEMessageType);

                        aprsMessage.Latitude = latitude;
                        aprsMessage.MicEMessageType = micEMessageType;

                        var longitudeDegrees = (short)(rawData[1] - 28 + longitudeOffset);
                        if (180 <= longitudeDegrees && longitudeDegrees <= 189)
                            longitudeDegrees -= 80;
                        else if (190 <= longitudeDegrees && longitudeDegrees <= 199)
                            longitudeDegrees -= 190;

                        aprsMessage.Longitude = new Longitude(
                            longitudeDegrees,
                            (short)((rawData[2] - 28) % 60),
                            (rawData[3] - 28) * 0.6,
                            longitudeHemisphere,
                            latitude.Ambiguity);

                        var speedCourseSharedByte = rawData[5] - 28;
                        aprsMessage.Speed =
                            Speed.FromKnots(((rawData[4] - 28) * 10 + (int)Math.Floor(speedCourseSharedByte / 10.0)) %
                                            800);
                        aprsMessage.Direction =
                            new Heading((short)((speedCourseSharedByte % 10 * 100 + (rawData[6] - 28)) % 400), 0, 0);

                        {
                            var symbolTableSelector = rawData[8];

                            if (symbolTableSelector != '/'
                                && symbolTableSelector != '\\')
                            {
                                aprsMessage.SymbolOverlay = symbolTableSelector;
                                aprsMessage.SymbolTable = Constants.Maps.SymbolTableMap['\\']; //Take the secondary table
                            }
                            else
                            {
                                aprsMessage.SymbolTable = Constants.Maps.SymbolTableMap[symbolTableSelector];
                            }
                        }

                        if ((aprsMessage.SymbolTable == SymbolTable.Primary
                            ? Constants.Maps.PrimarySymbolTableSymbolMap
                            : Constants.Maps.SecondarySymbolTableSymbolMap).TryGetValue(rawData[7], out symbol))
                        {
                            aprsMessage.Symbol = symbol;
                        }
                        else
                        {
                            aprsMessage.Symbol = null;
                        }

                        if (rawData.Length > 12 && rawData[12] == '}')
                        {
                            aprsMessage.Altitude =
                                Altitude.FromMetersAboveBaseline(ConvertFromBase91(rawData.Substring(9, 3)));
                        }

                        break;
                    case DataType.PositionWithoutTimestampWithAprsMessaging:
                    case DataType.PositionWithoutTimestampNoAprsMessaging:
                        aprsMessage.Latitude = new Latitude(Convert.ToInt16(rawData.Substring(1, 2)),
                            Convert.ToDouble(rawData.Substring(3, 5)),
                            rawData[8] == 'N' ? LatitudeHemisphere.North : LatitudeHemisphere.South);

                        {
                            var symbolTableSelector = rawData[9];

                            if (symbolTableSelector != '/'
                                && symbolTableSelector != '\\')
                            {
                                aprsMessage.SymbolOverlay = symbolTableSelector;
                                aprsMessage.SymbolTable = Constants.Maps.SymbolTableMap['\\']; //Take the secondary table
                            }
                            else
                            {
                                aprsMessage.SymbolTable = Constants.Maps.SymbolTableMap[symbolTableSelector];
                            }
                        }

                        aprsMessage.Longitude = new Longitude(Convert.ToInt16(rawData.Substring(10, 3)),
                            Convert.ToDouble(rawData.Substring(13, 5)),
                            rawData[18] == 'E' ? LongitudeHemisphere.East : LongitudeHemisphere.West);

                        if ((aprsMessage.SymbolTable == SymbolTable.Primary
                            ? Constants.Maps.PrimarySymbolTableSymbolMap
                            : Constants.Maps.SecondarySymbolTableSymbolMap).TryGetValue(rawData[19], out symbol))
                        {
                            aprsMessage.Symbol = symbol;
                        }
                        else
                        {
                            aprsMessage.Symbol = null;
                        }

                        rawData = rawData.Substring(20);
                        if (Regex.IsMatch(rawData, @"^\d\d\d\\\d\d\d"))
                        {
                            aprsMessage.Direction = new Heading(Convert.ToInt16(rawData.Substring(0, 3)), 0, 0);
                            aprsMessage.Speed = Speed.FromKnots(Convert.ToDouble(rawData.Substring(4, 3)));
                            rawData = rawData.Substring(7);
                        }
                        if (Constants.Regexes.AltitudeRegex.IsMatch(rawData))
                            aprsMessage.Altitude =
                                Altitude.FromFeetAboveSeaLevel(
                                    Convert.ToInt32(Constants.Regexes.AltitudeRegex.Match(rawData).Groups[1].Value));

                        // OGN FLAVORED STUFF
                        if (Regex.IsMatch(rawData, @"([\+\-]\d\d\d)fpm"))
                        {
                            aprsMessage.ClimbRate =
                                Convert.ToInt16(Regex.Match(rawData, @"([\+\-]\d\d\d)fpm").Groups[1].Value);
                        }

                        if (Regex.IsMatch(rawData, @"([\+\-]\d.\d)rot"))
                        {
                            aprsMessage.TurnRate =
                                Convert.ToDouble(Regex.Match(rawData, @"([\+\-]\d.\d)rot").Groups[1].Value);
                        }


                        // ToDo: In case OGN flavored aprs is enabled add the precision enhancement, clibrate and turnrate.
                        break;
                    case DataType.PositionWithTimestampWithAprsMessaging:
                    case DataType.PositionWithTimestampNoAprsMessaging:
                        if (Regex.IsMatch(rawData.Substring(1), @"^(\d\d\d\d\d\d)h")) rawData = rawData.Substring(8);
                        else return null;

                        aprsMessage.Latitude = new Latitude(Convert.ToInt16(rawData.Substring(0, 2)),
                            Convert.ToDouble(rawData.Substring(2, 5)),
                            rawData[7] == 'N' ? LatitudeHemisphere.North : LatitudeHemisphere.South);

                        SymbolTable symbolTable;
                        if (!Constants.Maps.SymbolTableMap.TryGetValue(rawData[8], out symbolTable)) return null;
                        aprsMessage.SymbolTable = symbolTable;

                        aprsMessage.Longitude = new Longitude(Convert.ToInt16(rawData.Substring(9, 3)),
                            Convert.ToDouble(rawData.Substring(12, 5)),
                            rawData[17] == 'E' ? LongitudeHemisphere.East : LongitudeHemisphere.West);

                        if ((aprsMessage.SymbolTable == SymbolTable.Primary
                            ? Constants.Maps.PrimarySymbolTableSymbolMap
                            : Constants.Maps.SecondarySymbolTableSymbolMap).TryGetValue(rawData[18], out symbol))
                        {
                            aprsMessage.Symbol = symbol;
                        } else
                        {
                            aprsMessage.Symbol = null;
                        }

                        short direction;
                        if (!Int16.TryParse(rawData.Substring(19, 3), out direction)) return null;

                        aprsMessage.Direction = new Heading(direction, 0, 0);


                        aprsMessage.Speed = Speed.FromKnots(Convert.ToDouble(rawData.Substring(23, 3)));
                        rawData = rawData.Substring(26);
                        if (Constants.Regexes.AltitudeRegex.IsMatch(rawData))
                            aprsMessage.Altitude =
                                Altitude.FromFeetAboveSeaLevel(
                                    Convert.ToInt32(Constants.Regexes.AltitudeRegex.Match(rawData).Groups[1].Value));

                        // OGN FLAVORED STUFF
                        if (Regex.IsMatch(rawData, @"([\+\-]\d\d\d)fpm"))
                        {
                            aprsMessage.ClimbRate =
                                Convert.ToInt16(Regex.Match(rawData, @"([\+\-]\d\d\d)fpm").Groups[1].Value);
                        }

                        if (Regex.IsMatch(rawData, @"([\+\-]\d.\d)rot"))
                        {
                            aprsMessage.TurnRate =
                                Convert.ToDouble(Regex.Match(rawData, @"([\+\-]\d.\d)rot").Groups[1].Value);
                        }
                        break;
                }

                return aprsMessage;
            }
            catch
            {
                return null;
            }
        }

        private static int ConvertFromBase91(string data)
        {
            return data.Select((t, i) => Convert.ToInt32(Math.Pow(91, data.Length - i - 1))*(t - 33)).Sum();
        }

        private static void DecodeMicEDestinationAddress(
            string data,
            out Latitude latitude,
            out short longitudeOffset,
            out LongitudeHemisphere longitudeHemisphere,
            out MicEMessageType micEMessageType)
        {
            if (string.IsNullOrEmpty(data) || data.Length != 6)
                throw new ArgumentException("Data must be a six character string", nameof(data));

            var sbLatitude = new StringBuilder();
            var sbMicEMessageCode = new StringBuilder();

            var isStandardMessage = false;
            var isCustomMessage = false;
            var latitudeHemisphere = default(LatitudeHemisphere);
            //latitude = null;
            longitudeOffset = 0;
            longitudeHemisphere = default;
            //micEMessageType = MicEMessageType.Unknown;

            for (var p = 0; p < 6; p++)
            {
                var c = data[p];
                if (c >= '0' && c <= '9' || c == 'L')
                {
                    if (c == 'L')
                        sbLatitude.Append(" ");
                    else
                        sbLatitude.Append(c - '0');

                    if (p < 3)
                        sbMicEMessageCode.Append('0');
                    else
                        switch (p)
                        {
                            case 3:
                                latitudeHemisphere = LatitudeHemisphere.South;
                                break;
                            case 4:
                                longitudeOffset = 0;
                                break;
                            case 5:
                                longitudeHemisphere = LongitudeHemisphere.East;
                                break;
                        }
                }
                else if (c >= 'A' && c <= 'K')
                {
                    if (c == 'K')
                        sbLatitude.Append(" ");
                    else
                        sbLatitude.Append(c - 'A');

                    if (p < 3)
                    {
                        sbMicEMessageCode.Append(1);
                        isCustomMessage = true;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid data: " + data);
                    }
                }
                else if (c >= 'P' && c <= 'Z')
                {
                    if (c == 'Z')
                        sbLatitude.Append(" ");
                    else
                        sbLatitude.Append(c - 'P');

                    if (p < 3)
                    {
                        sbMicEMessageCode.Append(1);
                        isStandardMessage = true;
                    }
                    else
                        switch (p)
                        {
                            case 3:
                                latitudeHemisphere = LatitudeHemisphere.North;
                                break;
                            case 4:
                                longitudeOffset = 100;
                                break;
                            case 5:
                                longitudeHemisphere = LongitudeHemisphere.West;
                                break;
                        }
                }
                else
                {
                    throw new ArgumentException("Invalid data: " + data);
                }
            }

            short degrees, minutes;
            double seconds;
            short latitudeAmbiguity;
            ParseLatitudeValue(sbLatitude.ToString(), out degrees, out minutes, out seconds, out latitudeAmbiguity);

            latitude = new Latitude(degrees, minutes, seconds, latitudeHemisphere, latitudeAmbiguity);

            if (isStandardMessage && isCustomMessage)
            {
                micEMessageType = MicEMessageType.Unknown;
            }
            else if (isStandardMessage)
            {
                if (!Constants.Maps.StandardMicEMessageTypeMap.TryGetValue(sbMicEMessageCode.ToString(), out micEMessageType))
                    throw new InvalidOperationException("Invalid MicE Message Code: " + sbMicEMessageCode);
            }
            else if (isCustomMessage)
            {
                if (!Constants.Maps.CustomMicEMessageTypeMap.TryGetValue(sbMicEMessageCode.ToString(), out micEMessageType))
                    throw new InvalidOperationException("Invalid MicE Message Code: " + sbMicEMessageCode);
            }
            else
            {
                micEMessageType = MicEMessageType.Emergency;
            }
        }

        private static void ParseLatitudeValue(string data, out short degrees, out short minutes, out double seconds, out short ambiguity)
        {
            ambiguity = 0;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                if (data[i] != ' ')
                    break;
                ambiguity++;
            }

            degrees = Convert.ToInt16(data.Substring(0, 2).Replace(' ', '0'));
            minutes = Convert.ToInt16(data.Substring(2, 2).Replace(' ', '0'));
            seconds = Convert.ToDouble("0." + data.Substring(4).Replace(' ', '0'))*60;
        }
    }
}
