/*
 * ToDo: log some usable information on exceptions
 */

using System;
using System.Diagnostics;
using System.Net;
using Boerman.Aeronautics.AprsClient.Models;
using Boerman.TcpLib.Client;
using Boerman.TcpLib.Shared;

namespace Boerman.Aeronautics.AprsClient
{
    public class Listener : TcpClient
    {
        public Listener() : base(new ClientSettings()
        {
            EndPoint = new DnsEndPoint(
                    AprsConfig.Uri,
                    AprsConfig.Port),
            Splitter = "\r\n",
            Timeout = 1020000, // 17 minutes afaik,
            ReconnectOnDisconnect = true
        })
        {
            base.Connected += (sender, args) => 
                {
                    Send($"user {AprsConfig.Callsign} pass {AprsConfig.Password} vers experimenting 0.1 filter {AprsConfig.Filter}\n");
                };

            base.DataReceived += OnDataReceived;            
            Open();
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs<string> dataReceivedEventArgs)
        {
            if (String.IsNullOrEmpty(dataReceivedEventArgs.Data)) return;

            DataReceived?.Invoke(this, new AprsDataReceivedEventArgs(dataReceivedEventArgs.Data));

            if (PacketReceived == null) return;

            AprsMessage packetInfo;

            try
            {
                packetInfo = PacketInfo.Parse(dataReceivedEventArgs.Data);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.ToString());
                return;
            }

            // Usually the case when the packet is corrupt or something like that.
            if (packetInfo == null) return;

            PacketReceived.Invoke(this, new PacketReceivedEventArgs(packetInfo));
        }

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        public event EventHandler<AprsDataReceivedEventArgs> DataReceived;
    }
}
