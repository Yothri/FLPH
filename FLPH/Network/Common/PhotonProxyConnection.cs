using System;
using Ether.Network.Photon.Common;
using SlightNet.Common.Interface;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Common
{
    internal sealed class PhotonProxyConnection : PhotonConnection
    {
        public override void HandlePacket(IPacketStream packet)
        {
            base.HandlePacket(packet);

            var data = packet.Read<byte>(packet.Size);
            
            Console.WriteLine($"Packet with Size: {data.Length} from Game Client received.");

            using (var response = new PhotonPacket())
            {
                response.Write(data, 0, data.Length);
                AppContext.Instance.ProxyClient.Send(response);
            }
        }
    }
}