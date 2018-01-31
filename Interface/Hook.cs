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

        public virtual byte[] ClientToGameServerOverride(byte[] data) { return data; }
        public virtual byte[] GameServerToClientOverride(byte[] data) { return data; }
    }
}