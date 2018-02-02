using Ether.Network.Packets;
using Ether.Network.Photon.Common;

namespace FLPH.Network.Common
{
    internal sealed class PhotonProxyConnection : PhotonConnection
    {
        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);
        }
    }
}