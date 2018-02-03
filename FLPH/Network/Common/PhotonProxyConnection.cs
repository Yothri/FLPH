﻿using System;
using AdvancedConsole;
using SlightNet.Common.Interface;
using SlightNet.Photon.Server;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Common
{
    internal sealed class PhotonProxyConnection : PhotonUser
    {
        public override void HandlePacket(IPacketStream packet)
        {
            base.HandlePacket(packet);

            var data = packet.Read<byte>(packet.Size);

            Console.WriteLine("Client -> Server | Size: {0:X8}", packet.Size);
            AConsole.WriteHexView(data);
            Console.WriteLine();

            AppContext.Instance.ProxyClient.SendRaw(data);
        }
    }
}