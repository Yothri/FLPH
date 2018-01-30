using FLPH;
using Newtonsoft.Json.Linq;

public sealed class WPDGameServerRewrite : Hook
{
    public override string AuthToClientOverride(string cmd, string data)
    {
        var obj = JObject.Parse(data);
        if (cmd == "GameServers")
        {
            var gsg = new JObject();
            gsg["groupId"] = 1;
            gsg["name"] = "Deutsche";
            gsg["recommendGameServerId"] = 65003;

            var gs = new JObject();
            gs["virtualGameServerId"] = 65003;
            gs["gameServerId"] = 65003;
            gs["groupId"] = 1;
            gs["displayNo"] = 1;
            gs["name"] = "Skorpion";
            gs["apiUrl"] = "http://192.168.0.122:8010/game/";
            gs["proxyGameServerIp"] = "192.168.0.122";
            gs["proxyGameServerPort"] = 22703;
            gs["isMaintenance"] = false;
            gs["pkEnabled"] = false;
            gs["timeZone"] = "DE";
            gs["stateCode"] = 1;
            gs["isRecommend"] = true;

            obj["gameServers"] = new JArray(gs);
            obj["gameServerGroups"] = new JArray(gsg);
        }
        return obj.ToString();
    }
}