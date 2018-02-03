using System;
using System.Linq;
using System.Threading.Tasks;
using AdvancedConsole;
using SlightNet.Common.Interface;
using SlightNet.Photon.Client;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Client
{
    internal class PhotonProxyClient : PhotonClient
    {
        internal PhotonProxyClient()
        {
            Configuration.Host = AppContext.Instance.Configuration.GenuineGameServerIp;
            Configuration.Port = AppContext.Instance.Configuration.GenuineGameServerPort;
            Configuration.BufferSize = 1024;
        }

        protected override void OnConnected()
        {
            Console.WriteLine("Connection to Genuine Game Server established.");
        }

        public override void HandlePacket(IPacketStream packet)
        {
            base.HandlePacket(packet);

            var data = packet.Read<byte>(packet.Size);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Server -> Client | Size: {0:X8}", packet.Size);
                AConsole.WriteHexView(data);
                Console.WriteLine();
            });

            AppContext.Instance.ProxyServer.Clients.ToList().ForEach(c => c.SendRaw(data));
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine("Connection to Genuine Game Server reset.");
        }
    }
}