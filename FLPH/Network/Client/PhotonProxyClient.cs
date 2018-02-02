using System;
using System.Net.Sockets;
using Ether.Network.Packets;
using Ether.Network.Photon.Client;
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