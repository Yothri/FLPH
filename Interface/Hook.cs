using Ether.Network.Photon.Common;

namespace FLPH
{
    public abstract class Hook
    {
        public virtual string ClientToGateOverride(string cmd, string data) { return data; }
        public virtual string GateToClientOverride(string cmd, string data) { return data; }

        public virtual string ClientToAuthOverride(string cmd, string data) { return data; }
        public virtual string AuthToClientOverride(string cmd, string data) { return data; }

        public virtual string ClientToGameOverride(string cmd, string data) { return data; }
        public virtual string GameToClientOverride(string cmd, string data) { return data; }

        public virtual void ClientToGameServerOverride(PhotonPacket data) { }
        public virtual void GameServerToClientOverride(PhotonPacket data) { }
    }
}