using Ether.Network.Photon.Common;

namespace FLPH
{
    public abstract class Hook
    {
        public virtual string ClientToGateOverride(string data) { return data; }
        public virtual string GateToClientOverride(string data) { return data; }

        public virtual string ClientToAuthOverride(string data) { return data; }
        public virtual string AuthToClientOverride(string data) { return data; }

        public virtual string ClientToGameOverride(string data) { return data; }
        public virtual string GameToclientOverride(string data) { return data; }

        public virtual PhotonPacket ClientToGameOverride(PhotonPacket data) { return data; }
        public virtual PhotonPacket GameToClientOverride(PhotonPacket data) { return data; }
    }
}