using System;
using System.Net.Sockets;
using AdvancedConsole;
using Ether.Network.Packets;
using Ether.Network.Photon.Client;
using Ether.Network.Photon.Common;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Client
{
    internal class PhotonProxyClient : PhotonClient
    {
        internal PhotonProxyClient() : base(AppContext.Instance.Configuration.GenuineGameServerIp, AppContext.Instance.Configuration.GenuineGameServerPort, 8)
        {

        }

        protected override void OnConnected()
        {
            Console.WriteLine("Connection to Genuine Game Server established.");
        }

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
                AppContext.Instance.ProxyServer.SendToAll(p);
            }
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine("Connection to Genuine Game Server reset.");
        }

        protected override void OnSocketError(SocketError socketError)
        {
            throw new NotImplementedException();
        }
    }
}