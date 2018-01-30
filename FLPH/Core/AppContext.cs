using System;
using System.IO;
using FLPH.Config;
using FLPH.Core.Hook;
using FLPH.Network.Client;
using FLPH.Network.Server;
using Newtonsoft.Json;

namespace FLPH.Core
{
    internal class AppContext : IDisposable
    {
        private const string APP_CONFIG = "Config/FLPHConfig.json";
        private static AppContext instance;
        public static AppContext Instance
        {
            get
            {
                return instance == null ? instance = new AppContext() : instance;
            }
        }

        public AppContext()
        {
            LoadConfiguration();
        }

        public void Initialize()
        {
            HttpProxyServer = new HttpProxyServer();
            ProxyServer = new PhotonProxyServer();
            ProxyClient = new PhotonProxyClient();
            HookManager = new HookManager();
            HookManager.LoadHooks();
        }

        public void LoadConfiguration()
        {
            if (!File.Exists(APP_CONFIG))
                SaveConfiguration();

            Configuration = JsonConvert.DeserializeObject<FLPHConfig>(File.ReadAllText(APP_CONFIG));
            Console.WriteLine("Configuration has been loaded.");
        }

        public void SaveConfiguration()
        {
            if (Configuration == null)
                Configuration = new FLPHConfig();

            File.WriteAllText(APP_CONFIG, JsonConvert.SerializeObject(Configuration, Formatting.Indented));
            Console.WriteLine("Configuration has been saved.");
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                HttpProxyServer.Dispose();
                ProxyServer.Dispose();
                ProxyClient.Dispose();
                SaveConfiguration();
            }
        }
        
        public FLPHConfig Configuration { get; private set; }
        public HttpProxyServer HttpProxyServer { get; private set; }
        public PhotonProxyServer ProxyServer { get; private set; }
        public PhotonProxyClient ProxyClient { get; private set; }
        public HookManager HookManager { get; private set; }
    }
}