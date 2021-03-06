﻿using System;
using AdvancedConsole;
using Ether.Network.Packets;
using Ether.Network.Photon.Common;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Common
{
    internal sealed class PhotonProxyConnection : PhotonConnection
    {
        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var data = packet.Read<byte>(packet.Size);

            Console.WriteLine("Client -> Server | Size: {0}", data.Length);
            AConsole.WriteHexView(data);
            Console.WriteLine();

            using (var p = new PhotonPacket())
            {
                p.Write(data, 0, data.Length);
                AppContext.Instance.ProxyClient.Send(p);
            }
        }
    }
}