using System;
using System.Threading.Tasks;
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

            var data = AppContext.Instance.HookManager.CallClientToGameServerHooks(packet.Read<byte>(packet.Size));

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Client -> Server | Size: {0:X8}", packet.Size);
                AConsole.WriteHexView(data);
                Console.WriteLine();
            });
            
            AppContext.Instance.ProxyClient.SendRaw(data);
        }
    }
}