using System;
using Ether.Network.Photon.Server;
using FLPH.Network.Common;
using AppContext = FLPH.Core.AppContext;

namespace FLPH.Network.Server
{
    internal sealed class PhotonProxyServer : PhotonServer<PhotonProxyConnection>
    {
        public PhotonProxyServer()
        {
            Configuration.Backlog = 1;
            Configuration.Host = AppContext.Instance.Configuration.ProxyServerInterface;
            Configuration.Port = AppContext.Instance.Configuration.ProxyServerPort;
            Configuration.BufferSize = 9;
        }

        protected override void Initialize()
        {
            Console.WriteLine("ProxyServer has been initialized.");
        }

        protected override void OnClientConnected(PhotonProxyConnection connection)
        {
            Console.WriteLine("Game Client connected to Proxy Server.");
        }
        
        protected override void OnClientDisconnected(PhotonProxyConnection connection)
        {
            Console.WriteLine("Game Client disconnected from Proxy Server.");

            AppContext.Instance.ProxyClient.Disconnect();
            AppContext.Instance.ProxyClient.Connect();
        }

        protected override void OnError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}