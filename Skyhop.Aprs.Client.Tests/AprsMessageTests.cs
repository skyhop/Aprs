using System;
using Boerman.Core.Spatial;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyhop.Aprs.Client.Enums;
using Skyhop.Aprs.Client.Models;

namespace Boerman.Aprs.Client.Tests
{
    [TestClass]
    public class AprsMessageTests
    {
        [TestMethod]
        public void TestFanetMessageParsing()
        {
            var message = "FNT110C70>OGNFNT,qAS,Letzi:/151029h4703.62N/00827.16Eg347/011/A=004364 !W13! id1E110C70 +354fpm FNT11 18.3dB -10.6kHz";

            var result = PacketInfo.Parse(message);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Altitude.FeetAboveSeaLevel == 4364);
            Assert.IsTrue(result.Callsign == "FNT110C70");
            Assert.IsTrue(result.ClimbRate == 354);
            Assert.IsTrue(result.DataType == Skyhop.Aprs.Client.Enums.DataType.PositionWithTimestampNoAprsMessaging);
            Assert.IsTrue(result.Direction.Degrees == 347);
            Assert.IsTrue(result.Latitude.AbsoluteValue == 47.060333333333332);
            Assert.IsTrue(result.Longitude.AbsoluteValue == 8.4526666666666657);
            Assert.IsTrue(result.MicEMessageType == Skyhop.Aprs.Client.Enums.MicEMessageType.OffDuty);
            Assert.IsTrue(result.Speed.Knots == 11);
            Assert.IsTrue(result.StationRoute[0] == "OGNFNT");
            Assert.IsTrue(result.StationRoute[1] == "qAS");
            Assert.IsTrue(result.StationRoute[2] == "Letzi");
            Assert.IsTrue(result.Symbol == Skyhop.Aprs.Client.Enums.Symbol.Glider);
            Assert.IsTrue(result.TurnRate == 0);
            Assert.AreEqual(AircraftType.Paraglider, result.AircraftType);
        }

        [TestMethod]
        public void TestOGNGliderHB1669LszxMessageParsing()
        {
            var message = "ICA4B4B2C>APRS,qAS,LSZX:/090803h4710.41N/00902.63E'152/051/A=002129 !W18! id054B4B2C -138fpm -1.6rot 24.0dB 0e -1.8kHz gps2x2";

            var result = PacketInfo.Parse(message);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Altitude.FeetAboveSeaLevel == 2129);
            Assert.IsTrue(result.Callsign == "ICA4B4B2C");
            Assert.IsTrue(result.ClimbRate == -138);
            Assert.IsTrue(result.DataType == Skyhop.Aprs.Client.Enums.DataType.PositionWithTimestampNoAprsMessaging);
            Assert.IsTrue(result.Direction.Degrees == 152);
            Assert.IsTrue(result.Latitude.AbsoluteValue == 47.1735);
            Assert.IsTrue(result.Longitude.AbsoluteValue == 9.0438333333333336);
            Assert.IsTrue(result.MicEMessageType == Skyhop.Aprs.Client.Enums.MicEMessageType.OffDuty);
            Assert.IsTrue(result.Speed.Knots == 51);
            Assert.IsTrue(result.StationRoute[0] == "APRS");
            Assert.IsTrue(result.StationRoute[1] == "qAS");
            Assert.IsTrue(result.StationRoute[2] == "LSZX");
            Assert.IsTrue(result.Symbol == Skyhop.Aprs.Client.Enums.Symbol.Aircraft);
            Assert.IsTrue(result.TurnRate == -1.6);
            Assert.AreEqual(AircraftType.Glider, result.AircraftType);
        }

        [TestMethod]
        public void TestOGNTowAircraftHBEXPLszxMessageParsing()
        {
            var message = "ICA4B0CF5>APRS,qAS,LSZX:/091131h4710.19N/00902.42E'149/007/A=001355 !W74! id094B0CF5 +020fpm +0.4rot 42.2dB 0e -3.4kHz gps3x3";

            var result = PacketInfo.Parse(message);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Altitude.FeetAboveSeaLevel == 1355);
            Assert.IsTrue(result.Callsign == "ICA4B0CF5");
            Assert.IsTrue(result.ClimbRate == 20);
            Assert.IsTrue(result.DataType == Skyhop.Aprs.Client.Enums.DataType.PositionWithTimestampNoAprsMessaging);
            Assert.IsTrue(result.Direction.Degrees == 149);
            Assert.IsTrue(result.Latitude.AbsoluteValue == 47.16983333333333);
            Assert.IsTrue(result.Longitude.AbsoluteValue == 9.0403333333333329);
            Assert.IsTrue(result.MicEMessageType == Skyhop.Aprs.Client.Enums.MicEMessageType.OffDuty);
            Assert.IsTrue(result.Speed.Knots == 7);
            Assert.IsTrue(result.StationRoute[0] == "APRS");
            Assert.IsTrue(result.StationRoute[1] == "qAS");
            Assert.IsTrue(result.StationRoute[2] == "LSZX");
            Assert.IsTrue(result.Symbol == Skyhop.Aprs.Client.Enums.Symbol.Aircraft);
            Assert.IsTrue(result.TurnRate == 0.4);
            Assert.AreEqual(AircraftType.TowPlane, result.AircraftType);
        }

        [TestMethod]
        public void TestOGNFLRMessageParsing()
        {
            var message = "ICA4B3CA5>APRS,qAS,LSZX:/165008h4711.09N/00847.94E'054/079/A=004402 !W59! id054B3CA5 -157fpm +0.0rot 13.8dB 0e -1.9kHz gps1x2 +5.3dBm";

            var result = PacketInfo.Parse(message);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Altitude.FeetAboveSeaLevel == 4402);
            Assert.IsTrue(result.Callsign == "ICA4B3CA5");
            Assert.IsTrue(result.ClimbRate == -157);
            Assert.IsTrue(result.DataType == Skyhop.Aprs.Client.Enums.DataType.PositionWithTimestampNoAprsMessaging);
            Assert.IsTrue(result.Direction.Degrees == 54);
            Assert.IsTrue(result.Latitude.AbsoluteValue == 47.18483333333333);
            Assert.IsTrue(result.Longitude.AbsoluteValue == 8.799);
            Assert.IsTrue(result.MicEMessageType == Skyhop.Aprs.Client.Enums.MicEMessageType.OffDuty);
            Assert.IsTrue(result.Speed.Knots == 79);
            Assert.IsTrue(result.StationRoute[0] == "APRS");
            Assert.IsTrue(result.StationRoute[1] == "qAS");
            Assert.IsTrue(result.StationRoute[2] == "LSZX");
            Assert.IsTrue(result.Symbol == Skyhop.Aprs.Client.Enums.Symbol.Aircraft);
            Assert.IsTrue(result.TurnRate == 0);
        }

        [TestMethod]
        public void TestMultipleMessages()
        {
            string messages = @"# aprsc 2.1.2-gc90ee9c
# No user-specified filters on this port
# logresp SHCOGN unverified, server GLIDERN1
ICA400EBD>APRS,qAS,UKUPW:/103446h5237.32N\00026.43W^186/051/A=000630 !W23! id21400EBD -355fpm +1.0rot 3.5dB 5e -4.9kHz gps2x3
ICA400ED8>APRS,qAS,UKUPW:/103446h5241.52N\00025.75W^113/110/A=002352 !W57! id21400ED8 -1029fpm +0.3rot 11.0dB 0e -8.8kHz gps1x2
ICA400EAD>APRS,qAS,Stoke:/103453h5231.95N\00046.59W^155/098/A=000869 !W28! id21400EAD -157fpm -4.2rot 22.2dB 0e -9.5kHz gps2x3
ICA400ED8>APRS,qAS,Stoke:/103453h5241.42N\00025.45W^124/104/A=002214 !W57! id21400ED8 -1266fpm +0.1rot 5.0dB 0e -9.4kHz gps1x1
ICA405761>APRS,qAS,Burn:/103453h5345.04N/00104.93W'000/000/A=000020 !W77! id05405761 -019fpm +0.0rot 14.2dB 0e +16.3kHz gps1x1 +10.0dBm
FLRDD91F2>APRS,qAS,LOLH:/103453h4743.22N/01404.11EX273/038/A=008233 !W06! id0EDD91F2 -098fpm +3.4rot 5.5dB 0e -0.7kHz gps3x5
ICA3D1062>APRS,qAS,EDLE:/103453h5051.87N/00617.97E'015/128/A=004582 !W66! id053D1062 -395fpm -0.4rot 4.2dB 1e +1.5kHz gps1x1
ICA4B2FCB>OGFLR,qAS,LSZF:/103453h4726.72N\00812.93E^006/070/A=001752 !W84! id214B2FCB +871fpm +1.2rot 41.2dB 0e -10.6kHz gps1x2
ICA4403CA>APRS,qAS,LOIJ:/103453h4731.20N/01226.98E'167/005/A=002348 !W34! id094403CA -019fpm +4.1rot 40.2dB 0e -5.9kHz gps1x2
FNT11006A>APRS,qAS,FNB11006A:/103459h4720.10N/00935.36Eg161/000/A=001199 !W24! id1E11006A -01fpm
OGN395F39>OGNTRK,qAS,Barton:/103452h5145.94N/00111.50W'000/001/A=000315 !W88! id07395F39 -058fpm +0.0rot FL000.00 48.0dB 0e +3.7kHz gps3x5
FLRDDDBC3>APRS,qAS,UKTIB:/103452h5227.49N/00109.31E'000/000/A=000167 !W08! id06DDDBC3 +020fpm +0.0rot 26.5dB 0e +0.1kHz gps2x3
ICA400EB6>APRS,qAS,Topcliffe:/103453h5403.98N\00114.13W^208/052/A=000515 !W10! id21400EB6 -613fpm +0.3rot 2.0dB 6e -8.6kHz gps1x2
ICA400EDC>APRS,qAS,Topcliffe:/103453h5404.98N\00113.07W^219/096/A=000748 !W98! id21400EDC -197fpm -0.2rot 5.0dB 0e -7.1kHz gps1x2
ICA400EB6>APRS,qAS,UKRUF:/103453h5403.98N\00114.12W^208/052/A=000518 !W19! id21400EB6 -613fpm +0.3rot 4.5dB 2e -8.8kHz gps1x2 s6.40 h44 rDF0D8B
ICA400EDC>APRS,qAS,UKRUF:/103453h5404.98N\00113.07W^219/096/A=000751 !W97! id21400EDC -197fpm -0.2rot 17.0dB 0e -7.2kHz gps1x2
FLRDF12CB>APRS,qAS,FATP:/103453h2902.35S/02610.18E'000/000/A=004536 !W57! id06DF12CB -019fpm +0.0rot 44.0dB 0e -14.1kHz gps3x6 +11.1dBm
OGNCFEF59>APRS,qAS,LHTL:/103453h4719.10N/01901.86E'126/065/A=002831 !W51! id07CFEF59 -098fpm -0.3rot FL033.99 3.2dB 5e -7.4kHz gps4x6
ICA4B2FCB>OGFLR,qAS,Letzi:/103453h4726.72N\00812.93E^006/070/A=001752 !W84! id214B2FCB +871fpm +1.2rot 15.2dB 0e -11.4kHz gps1x2
ICA400EAA>APRS,qAS,Crowland:/103453h5246.35N\00017.18W^077/056/A=003772 !W47! id21400EAA -9285fpm -3.1rot 17.5dB 0e -7.5kHz gps4x5
ICA400ED8>APRS,qAS,Crowland:/103453h5241.42N\00025.45W^124/104/A=002207 !W47! id21400ED8 -1266fpm +0.1rot 4.5dB 1e -9.4kHz gps1x1
PAW406D47>APRS,qAS,PWOrwell:/103500h5208.11N/00000.64W'000/000/A=000137 !W48! id07406D47 +000fpm +0.0rot 20.0dB 0e -6.0kHz gps1x1
PAW406D47>APRS,qAS,PWOrwell2:/103512h5208.11N/00000.64W'000/000/A=000137 !W48! id07406D47 +000fpm +0.0rot 20.0dB 0e -6.0kHz gps1x1
EDCI>APRS,TCPIP*,qAC,GLIDERN2:/103500h5116.55NI01430.82E&/A=000485
EDCI>APRS,TCPIP*,qAC,GLIDERN2:>103500h v0.2.6.ARM CPU:0.4 RAM:691.0/972.2MB NTP:0.9ms/-1.2ppm +49.2C 0/0Acfts[1h] RF:+0+0.1ppm/-1.59dB/+1.7dB@10km[100221]/+3.5dB@10km[1/1]
FLRDDFAD6>APRS,qAS,PoloCL:/103453h3322.80S/07035.00W'000/000/A=002266 !W64! id06DDFAD6 +020fpm +0.0rot 22.2dB 0e -2.5kHz gps4x7
FLRDDD5F3>APRS,qAS,LECI1:/103453h4239.03N/00041.29W'202/132/A=006517 !W80! id0ADDD5F3 -791fpm -0.2rot 4.2dB 2e -5.9kHz gps1x2
ICA4053C6>APRS,qAS,Bicester:/103454h5154.82N/00108.38W'000/000/A=000259 !W11! id054053C6 +000fpm +0.0rot 15.2dB 0e -4.4kHz gps2x3
FLRDD91F2>APRS,qAS,LOLH:/103454h4743.22N/01404.10EX281/039/A=008233 !W40! id0EDD91F2 +020fpm +2.6rot 6.5dB 0e -0.8kHz gps3x5
OGN295B24>APRS,qAS,Keszeg1:/103450h4750.27N/01914.99EO147/000/A=000840 !W77! id2F295B24 +000fpm +0.0rot 36.0dB 0e +5.2kHz gps4x6
ICA400EAD>APRS,qAS,Stoke:/103454h5231.93N\00046.56W^144/103/A=000866 !W08! id21400EAD -157fpm -3.0rot 20.0dB 0e -9.5kHz gps2x3
ICA400EDC>APRS,qAS,Topcliffe:/103454h5404.96N\00113.10W^217/095/A=000738 !W87! id21400EDC -355fpm -0.4rot 7.0dB 0e -7.1kHz gps1x2
FLRDF07D3>APRS,qAS,LSZX:/103454h4710.17N/00902.39Ez000/000/A=001355 !W58! id02DF07D3 -019fpm +0.0rot 52.8dB 0e -5.0kHz gps1x1
OGN7D0229>APRS,qAS,LHPP:/103454h4559.26N/01828.56E'174/062/A=003460 !W17! id077D0229 -078fpm +0.1rot 14.8dB 0e -4.6kHz gps4x6
FLRDDEEE3>APRS,qAS,EHGR:/103451h5133.19N/00457.22E'189/033/A=000958 !W66! id06DDEEE3 -296fpm -0.5rot 13.2dB 0e +11.6kHz gps1x1
ICA4403CA>APRS,qAS,LOIJ:/103454h4731.20N/01226.98E'180/005/A=002348 !W14! id094403CA -019fpm +3.6rot 41.8dB 0e -6.7kHz gps1x2
ICA4053C6>APRS,qAS,UKBIC:/103454h5154.82N/00108.38W'000/000/A=000262 !W21! id054053C6 +000fpm +0.0rot 39.5dB 0e -5.3kHz gps2x3
ICA405612>APRS,qAS,Burn:/103455h5345.05N/00104.90W'000/000/A=000016 !W73! id05405612 +000fpm +0.0rot 9.5dB 0e +14.1kHz gps0x1 +9.3dBm
ICA400EAA>APRS,qAS,Crowland:/103454h5246.36N\00017.14W^064/092/A=003615 !W36! id21400EAA -9899fpm -6.1rot 19.5dB 0e -7.5kHz gps4x5
ICA400EBD>APRS,qAS,Crowland:/103454h5237.22N\00026.49W^214/043/A=000587 !W35! id21400EBD -454fpm +1.5rot 4.0dB 4e -5.5kHz gps2x3
ICA3D1062>APRS,qAS,EDLE:/103455h5051.94N/00618.00E'016/128/A=004576 !W62! id053D1062 -078fpm +0.1rot 4.0dB 3e +1.4kHz gps1x1 +12.8dBm
ICA3FF435>APRS,qAS,EDLB:/103455h5146.82N/00717.42E'274/009/A=000164 !W34! id053FF435 -019fpm +1.8rot 42.5dB 0e +2.6kHz gps1x1
ICA400EAA>APRS,qAS,TippsEnd:/103455h5246.38N\00017.11W^052/136/A=003470 !W60! id21400EAA -7998fpm -1.7rot 3.8dB 0e -7.3kHz gps4x5
FLRDD9B1A>APRS,qAS,EBKH:/103455h5110.67N/00512.93E'000/000/A=000105 !W22! id06DD9B1A +000fpm +0.0rot 31.0dB 0e +1.3kHz gps6x9
LOLT>APRS,TCPIP*,qAC,GLIDERN3:/103501h4803.09NI01439.67E&/A=001076
LOLT>APRS,TCPIP*,qAC,GLIDERN3:>103501h v0.2.6.ARM CPU:0.4 RAM:765.6/972.2MB NTP:0.6ms/-14.4ppm +28.2C 1/1Acfts[1h] RF:+0-1.2ppm/-1.27dB/+13.9dB@10km[67168]/+24.2dB@10km[1/1]
ICA4403CA>APRS,qAS,LOIJ:/103455h4731.20N/01226.98E'183/005/A=002348 !W03! id094403CA -019fpm +2.5rot 42.8dB 0e -5.8kHz gps1x2
EPRU>APRS,TCPIP*,qAC,GLIDERN1:/103501h5053.35NI01912.22E&/A=000869
EPRU>APRS,TCPIP*,qAC,GLIDERN1:>103501h v0.2.6.RPI-GPU CPU:0.7 RAM:225.9/456.4MB NTP:1.2ms/-7.7ppm +44.4C 0/0Acfts[1h] RF:+58-1.7ppm/+5.17dB/-0.3dB@10km[1925]
FLRDDACA6>APRS,qAS,OMB:/103455h4937.85N/01059.52E'194/082/A=002988 !W57! id0ADDACA6 -197fpm +0.0rot 8.0dB 0e -3.0kHz gps3x5
ICA3DE5C9>APRS,qAS,EDTD:/103454h4757.88N/00830.94E'146/069/A=002490 !W06! id053DE5C9 -078fpm -0.8rot 34.2dB 0e -6.1kHz gps1x3
ICA400EDC>APRS,qAS,UKRUF:/103454h5404.96N\00113.10W^217/095/A=000741 !W86! id21400EDC -355fpm -0.4rot 15.8dB 0e -7.2kHz gps1x2
ICA4B43C7>APRS,qAS,LSTZ:/103455h4636.71N/00722.29EX188/120/A=004353 !W61! id0D4B43C7 +554fpm -0.5rot 10.0dB 0e +10.6kHz gps4x5
OGNCFEF59>APRS,qAS,LHTL:/103455h4719.08N/01901.90E'123/064/A=002824 !W54! id07CFEF59 -098fpm -0.5rot FL033.96 4.8dB 3e -7.4kHz gps4x6
ICA400EAA>APRS,qAS,Crowland:/103455h5246.38N\00017.11W^052/136/A=003464 !W61! id21400EAA -7998fpm -1.7rot 20.0dB 0e -7.5kHz gps4x5
OGN2A4B49>APRS,qAS,Keszeg1:/103455h4750.28N/01915.01E'313/000/A=000781 !W60! id072A4B49 +000fpm +0.0rot 29.0dB 0e +8.2kHz gps3x5
ICA400EAD>APRS,qAS,UKGRL2:/103451h5232.00N\00046.62W^181/090/A=000869 !W59! id21400EAD +396fpm -4.9rot 11.2dB 4e -8.9kHz gps1x3
FNT11006A>APRS,qAS,FNB11006A:/103501h4720.10N/00935.36Eg161/000/A=001199 !W24! id1E11006A +07fpm
SoloStdby>APRS,TCPIP*,qAC,GLIDERN3:/103502h4712.67NI00731.89E&/A=001509
SoloStdby>APRS,TCPIP*,qAC,GLIDERN3:>103502h v0.2.6.ARM CPU:0.2 RAM:766.2/970.5MB NTP:0.8ms/-10.2ppm +34.9C 0/0Acfts[1h] RF:-5+0.3ppm/+0.50dB/+13.3dB@10km[114144]/+15.6dB@10km[7/13]
Lahr>APRS,TCPIP*,qAC,GLIDERN3:/103502h4817.34NI00749.53E&/A=000676
Lahr>APRS,TCPIP*,qAC,GLIDERN3:>103502h v0.2.6.RPI-GPU CPU:0.7 RAM:659.4/972.4MB NTP:0.3ms/-7.7ppm +45.1C 0/0Acfts[1h] RF:+50-0.6ppm/+0.26dB/+5.5dB@10km[91042]
UKDUN2>APRS,TCPIP*,qAC,GLIDERN2:/103502h5152.29NI00032.81W&/A=000535
UKDUN2>APRS,TCPIP*,qAC,GLIDERN2:>103502h v0.2.6.ARM CPU:0.3 RAM:770.3/972.2MB NTP:1.0ms/-3.6ppm +39.0C 1/1Acfts[1h] RF:+53+0.1ppm/-0.20dB/+17.8dB@10km[282006]/+19.1dB@10km[8/16]
PAW48AC45>APRS,qAS,PWWilmcot:/103502h5213.04N\00145.49W^000/001/A=000209 !W45! id2348AC45 +000fpm +0.0rot 20.0dB 0e -6.0kHz gps1x1
EDPK>APRS,TCPIP*,qAC,GLIDERN3:/103502h4802.89NI01230.17E&/A=001788
EDPK>APRS,TCPIP*,qAC,GLIDERN3:>103502h v0.2.6.ARM CPU:0.5 RAM:742.1/970.5MB NTP:0.4ms/-4.9ppm +25.0C 2/2Acfts[1h] RF:-1+0.4ppm/+1.36dB/+22.5dB@10km[684]/+25.0dB@10km[2/3]
Scandido>APRS,TCPIP*,qAC,GLIDERN3:/103502h4644.00NI01216.53E&/A=003890
Scandido>APRS,TCPIP*,qAC,GLIDERN3:>103502h v0.2.6.RPI-GPU CPU:1.3 RAM:241.0/456.4MB NTP:1.0ms/-7.2ppm +44.4C 0/0Acfts[1h] RF:+49-0.2ppm/+8.87dB
FAWC>APRS,TCPIP*,qAC,GLIDERN3:/103502h3339.85SI01925.14E&/A=000689
FAWC>APRS,TCPIP*,qAC,GLIDERN3:>103502h v0.2.6.RPI-GPU CPU:0.4 RAM:553.0/972.4MB NTP:0.6ms/-0.9ppm +60.1C 2/2Acfts[1h] RF:+24-0.9ppm/+5.01dB/+12.4dB@10km[1852]/+12.5dB@10km[1/2]
OGN295B24>APRS,RELAY*,qAS,Keszeg2:/103452h4750.27N/01914.99EO147/000/A=000840 !W77! id2F295B24 +000fpm +0.0rot gps3x5
ICA4B43D2>APRS,qAS,Malvaglia:/103455h4628.13N/00854.21EX156/149/A=007032 !W96! id0D4B43D2 -870fpm +0.1rot 9.0dB 0e -2.7kHz gps3x3
ICA400EDC>APRS,qAS,UKPOC:/103456h5404.92N\00113.15W^215/094/A=000735 !W58! id21400EDC -435fpm -0.5rot 4.5dB 1e -7.0kHz gps1x2
ICA400EAD>APRS,qAS,Stoke:/103456h5231.88N\00046.49W^132/108/A=000859 !W96! id21400EAD -078fpm -1.1rot 22.8dB 0e -9.6kHz gps2x3
ICA3FF435>APRS,qAS,EDLB:/103456h5146.82N/00717.42E'282/008/A=000164 !W50! id053FF435 +020fpm +2.6rot 44.2dB 0e +2.6kHz gps1x1
FLRDD9B1A>APRS,qAS,EBKH:/103456h5110.67N/00512.93E'000/000/A=000089 !W22! id06DD9B1A -019fpm +0.0rot 31.5dB 0e +0.1kHz gps5x8
ICA4403CA>APRS,qAS,LOIJ:/103456h4731.19N/01226.98E'197/005/A=002348 !W83! id094403CA +000fpm +2.8rot 40.5dB 0e -5.9kHz gps1x2
FLRDDAA12>APRS,qAS,edbk:/103456h5254.99N/01226.02E'000/000/A=000187 !W33! id06DDAA12 +020fpm +0.0rot 54.5dB 0e -2.7kHz gps6x6
ICA400EAA>APRS,qAS,TippsEnd:/103456h5246.41N\00017.05W^048/163/A=003375 !W59! id21400EAA -4197fpm -0.4rot 5.0dB 7e -7.3kHz gps4x5
ICA400ED8>APRS,qAS,UKGRL:/103456h5241.37N\00025.33W^124/105/A=002148 !W58! id21400ED8 -1464fpm +0.0rot 2.8dB 1e -8.6kHz gps1x1
FLRDD4F25>APRS,qAS,LFRI:/103456h4642.08N/00122.50W'281/050/A=000295 !W97! id0ADD4F25 -019fpm +0.1rot 36.0dB 0e +0.1kHz gps4x5
ICA400EAD>APRS,qAS,UKGRL2:/103453h5231.95N\00046.59W^155/098/A=000872 !W27! id21400EAD -157fpm -4.2rot 9.2dB 0e -8.7kHz gps2x3
ICA400EBD>APRS,qAS,UKGRL2:/103453h5237.23N\00026.48W^210/043/A=000604 !W33! id21400EBD -454fpm +1.3rot 11.0dB 0e -4.6kHz gps2x3
ICA400ED8>APRS,qAS,UKGRL2:/103453h5241.42N\00025.45W^124/104/A=002217 !W56! id21400ED8 -1266fpm +0.1rot 23.2dB 0e -8.5kHz gps1x1
FLRDDD591>APRS,qAS,LECI1:/103456h4239.58N/00042.37W'173/054/A=011598 !W85! id06DDD591 +317fpm -0.1rot 3.2dB 4e -0.8kHz gps1x1
FLRDDD5F3>APRS,qAS,LECI1:/103456h4238.93N/00041.34W'202/132/A=006471 !W52! id0ADDD5F3 -1108fpm +0.0rot 2.2dB 2e -5.9kHz gps1x2
ZwolleN>APRS,TCPIP*,qAC,GLIDERN2:/103503h5231.97NI00606.08E&/A=000092
ZwolleN>APRS,TCPIP*,qAC,GLIDERN2:>103503h v0.2.6.ARM CPU:0.3 RAM:803.3/1018.1MB NTP:0.7ms/-9.4ppm +33.8C 0/0Acfts[1h] RF:+0-0.4ppm/+3.46dB
PAW401FF2>APRS,qAS,PWRedhill:/103503h5113.13N\00008.53W^000/001/A=000193 !W00! id23401FF2 +000fpm +0.0rot 20.0dB 0e -6.0kHz gps1x1
ICA400EAA>APRS,qAS,Crowland:/103456h5246.41N\00017.05W^048/163/A=003369 !W49! id21400EAA -4197fpm -0.4rot 21.5dB 0e -7.5kHz gps4x5
PAW72C3AA>APRS,qAS,PWEGBW:/103503h5211.46N\00137.10Wn000/000/A=000170 !W00! id3F72C3AA +000fpm +0.0rot 20.0dB 0e -6.0kHz gps1x1
VITACURA2>OGNSDR,TCPIP*,qAC,GLIDERN4:/103503h3322.79SI07034.80W&/A=002329 Contact: achanes@manquehue.net, brito.felipe@gmail.com 
VITACURA2>OGNSDR,TCPIP*,qAC,GLIDERN4:>103503h v0.2.7.RPI-GPU CPU:0.6 RAM:766.6/970.5MB NTP:0.2ms/-1.4ppm +51.5C 2/2Acfts[1h] RF:+0-0.8ppm/-1.48dB/-0.6dB@10km[26722]/-0.2dB@10km[9/17]
FLRDDACA6>APRS,qAS,OMB:/103456h4937.83N/01059.51E'195/083/A=002982 !W39! id0ADDACA6 -197fpm -0.1rot 4.0dB 3e -3.0kHz gps3x5
FLRDDF944>OGFLR,qAS,VITACURA2:/103456h3322.78S/07034.83W'000/000/A=002227 !W85! id06DDF944 -019fpm +0.0rot 23.5dB 0e -3.5kHz gps3x5 s6.42 h05
ICA3D1062>APRS,qAS,EDLE:/103457h5052.01N/00618.03E'017/128/A=004569 !W53! id053D1062 -712fpm +0.0rot 2.8dB 4e +1.4kHz gps1x1";

            foreach (var line in messages.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                var result = PacketInfo.Parse(line);
                Console.WriteLine(result);
            }

            // At least it didn't crash:wq
        }

        [TestMethod]
        public void TestSouthernHemisphereMessage()
        {
            var data = "FLRDDFAD6>APRS,qAS,PoloCL:/103453h3322.80S/07035.00W'000/000/A=002266 !W64! id06DDFAD6 +020fpm +0.0rot 22.2dB 0e -2.5kHz gps4x7";

            var result = PacketInfo.Parse(data);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ParseMessageWithAlternativeSymbols()
        {
            var data = @"KB9VBR-N>APDG03,TCPIP*,qAC,T2INDIANA:!4456.70ND08937.08W&/A=000000440 MMDVM Voice 431.50000MHz +0.0000MHz, KB9VBR_Pi-Star
KD9GCX-S>APDG01,TCPIP*,qAC,KD9GCX-GS:;KD9GCX B 160126z4415.63ND08817.60Wa/A=000010RNG0001 440 Voice 433.00000MHz +0.0000MHz
WE9C0M-8>APN391,qAR,K9FR:!4541.60NS09120.85W#PHG6560 W3, In honor to Bill W9NNS
KD9GCX-B>APDG02,TCPIP,qAC,KD9GCX-BS:!4415.63ND08817.60W&/A=000010RNG0001 440 Voice 433.00000MHz +0.0000MHz";

            var messages = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var first = PacketInfo.Parse(messages[0]);

            Assert.AreEqual("KB9VBR-N", first.Callsign);
            Assert.AreEqual(Skyhop.Aprs.Client.Enums.DataType.PositionWithoutTimestampNoAprsMessaging, first.DataType);
            Assert.AreEqual(new Latitude(44, 56, 42, LatitudeHemisphere.North).ToString(), first.Latitude.ToString());
            Assert.AreEqual(new Longitude(89, 37, 4.8, LongitudeHemisphere.West).ToString(), first.Longitude.ToString());
            //Assert.AreEqual(Skyhop.Aprs.Client.Enums.Symbol.HfGateway, first.Symbol);

            var second = PacketInfo.Parse(messages[1]);

            Assert.IsNotNull(second);

            var third = PacketInfo.Parse(messages[2]);

            Assert.IsNotNull(third);

            var fourth = PacketInfo.Parse(messages[3]);

            Assert.IsNotNull(fourth);
        }
    }
}
