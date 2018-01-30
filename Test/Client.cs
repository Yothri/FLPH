using System;
using System.Linq;
using System.Net.Sockets;
using Ether.Network.Packets;
using Ether.Network.Photon.Client;
using Ether.Network.Photon.Packets;

namespace Test
{
    internal class Client : PhotonClient
    {
        public Client() : base("s5003-useu.flyff-legacy.com", 22703, 8192)
        {

        }

        protected override void OnConnected()
        {
            Console.WriteLine("Connected");

            using (var packet = new PhotonPacket())
            {
                var data = "fb000000300001f30001061e410106004c6f616442616c616e63696e6700000000000000000000000000000000000000".ToArray();
                packet.Write(data, 0, data.Length);
                Send(packet);
            }
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine("Disonnected");
        }

        protected override void OnSocketError(SocketError socketError)
        {
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var data = packet.Read<byte>(packet.Size);

            using (var p = new PhotonPacket())
            {
                var b = "fb000000720001f3060000010178000000605bc64a01ae3daf0a34bc84fe47a16f1bdf7a2744e9075317d236c483e807829908849fb713d3a8fd6f732066080634f8dcfb811d092b7fe489ffe71362c87df370c5eb47a4efa55dc833cfc0f746f1ed6013cc10b1967b1bd5451046f7bb96ac".ToArray();
                p.Write(b, 0, b.Length);
                Send(p);
            }
        }

        protected override IPacketProcessor PacketProcessor => new PhotonServerProcessor();
    }

    static class Util
    {
        public static byte[] ToArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}