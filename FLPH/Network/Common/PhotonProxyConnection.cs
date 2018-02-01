using System.Linq;
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
            var hookData = data.Skip(1).ToArray();
            if (data[0] != 240)
                hookData = AppContext.Instance.HookManager.CallClientToGameServerHooks(data.Skip(8).ToArray());

            using (var response = new PhotonPacket(data[0] == 240))
            {
                response.Write(hookData, 0, hookData.Length);
                AppContext.Instance.ProxyClient.Send(response);
            }
        }
    }
}