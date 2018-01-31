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
            data = AppContext.Instance.HookManager.CallClientToGameServerHooks(data);
            using (var response = new PhotonPacket())
            {
                response.Write(data, 0, data.Length);
                AppContext.Instance.ProxyClient.Send(response);
            }
        }
    }
}