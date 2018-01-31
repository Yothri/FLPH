using System;
using System.Net.Sockets;
using Ether.Network.Packets;
using Ether.Network.Photon.Client;
using Ether.Network.Photon.Common;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Client
{
    internal class PhotonProxyClient : PhotonClient
    {
        internal PhotonProxyClient() : base(AppContext.Instance.Configuration.GenuineGameServerIp, AppContext.Instance.Configuration.GenuineGameServerPort, 9)
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
            data = AppContext.Instance.HookManager.CallGameServerToClientHooks(data);
            using (var response = new PhotonPacket())
            {
                response.Write(data, 0, data.Length);
                AppContext.Instance.ProxyServer.SendTo(AppContext.Instance.ProxyServer.Clients, response);
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