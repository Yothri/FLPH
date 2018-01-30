using System;
using System.Linq;
using AppContext = FLPH.Core.AppContext;

namespace FLPH
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppContext.Instance)
            {
                AppContext.Instance.Initialize();
                AppContext.Instance.ProxyClient.Connect();
                AppContext.Instance.HttpProxyServer.Start();
                AppContext.Instance.ProxyServer.Start();

                while (true)
                {
                    var inp = Console.ReadLine();
                    if (inp.Equals("rldhooks"))
                        AppContext.Instance.HookManager.LoadHooks();
                    if (inp.Equals("dcclient"))
                        AppContext.Instance.ProxyServer.DisconnectClient(AppContext.Instance.ProxyServer.Clients.FirstOrDefault().Id);
                    if (inp.Equals("exit"))
                        break;
                }

                AppContext.Instance.ProxyClient.Disconnect();
                AppContext.Instance.ProxyServer.Stop();
            }
        }
    }
}