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
            base.OnConnect +=
                (sender, args) =>
                {
                    Send($"user {AprsConfig.Callsign} pass {AprsConfig.Password} vers experimenting 0.1 filter {AprsConfig.Filter}\n");
                };

            base.OnReceive += OnReceive;
            
            Open();
        }

        private void OnReceive(object sender, OnReceiveEventArgs<string> onReceiveEventArgs)
        {
            if (String.IsNullOrEmpty(onReceiveEventArgs.Data)) return;

            DataReceived?.Invoke(this, new AprsDataReceivedEventArgs(onReceiveEventArgs.Data));

            if (PacketReceived == null) return;

            AprsMessage packetInfo;

            try
            {
                packetInfo = PacketInfo.Parse(onReceiveEventArgs.Data);
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
