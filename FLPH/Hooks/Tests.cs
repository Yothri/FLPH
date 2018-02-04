using FLPH;
using Newtonsoft.Json.Linq;
using WebCommon;

public class Tests : Hook
{
    public override string GameToClientOverride(string cmd, string data)
    {
        var jData = JObject.Parse(data);
        if (cmd == "AccountHeroPacketInfo")
        {            
            WPDAccountHeroInfo heroInfo = new WPDAccountHeroInfo();
            heroInfo.DeserializeFromBase64String(jData["accountHeroInfo"].ToObject<string>());
            
            // Modifications here!


            jData["accountHeroInfo"] = heroInfo.SerializeBase64String();
        }

        return jData.ToString();
    }

    public override byte[] GameServerToClientOverride(byte[] data)
    {
        return base.GameServerToClientOverride(data);
    }
}