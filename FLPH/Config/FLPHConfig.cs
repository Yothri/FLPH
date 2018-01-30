namespace FLPH.Config
{
    internal sealed class FLPHConfig
    {
        public FLPHConfig()
        {
            GenuineGateServerAddress = "http://gate-en.flyff-legacy.com/Api.ashx";
            GenuineAuthServerAddress = "http://auth-useu.flyff-legacy.com/Api.ashx";
            GenuineGameServerAddress = "http://s5003-useu.flyff-legacy.com/Api.ashx";
            GenuineGameServerIp = "s5003-useu.flyff-legacy.com";
            GenuineGameServerPort = 22703;

            ProxyServerInterface = "0.0.0.0";
            ProxyServerPort = 22703;
        }

        public string GenuineGateServerAddress { get; set; }
        public string GenuineAuthServerAddress { get; set; }
        public string GenuineGameServerAddress { get; set; }
        public string GenuineGameServerIp { get; set; }
        public int GenuineGameServerPort { get; set; }
        public string ProxyServerInterface { get; set; }
        public int ProxyServerPort { get; set; }
    }
}