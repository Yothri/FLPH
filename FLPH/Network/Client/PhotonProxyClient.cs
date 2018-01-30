using System;
using Ether.Network.Photon.Client;
using Ether.Network.Photon.Common;
using SlightNet.Common.Interface;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Client
{
    internal class PhotonProxyClient : PhotonClient
    {
        internal PhotonProxyClient()
        {
            Configuration.Host = AppContext.Instance.Configuration.GenuineGameServerIp;
            Configuration.Port = AppContext.Instance.Configuration.GenuineGameServerPort;
            Configuration.BufferSize = 8;
        }

        protected override void OnConnected()
        {
            Console.WriteLine("Connection to Genuine Game Server established.");
        }

        public override void HandlePacket(IPacketStream packet)
        {
            base.HandlePacket(packet);

            var data = packet.Read<byte>(packet.Size);

            Console.WriteLine($"Packet with Size: {data.Length} from Genuine Game Server received.");

            using (var response = new PhotonPacket())
            {
                response.Write(data, 0, data.Length);
                AppContext.Instance.HookManager.CallGameServerToClientHooks(response);
                AppContext.Instance.ProxyServer.SendTo(AppContext.Instance.ProxyServer.Clients, response);
            }
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine("Connection to Genuine Game Server reset.");
        }
    }
}