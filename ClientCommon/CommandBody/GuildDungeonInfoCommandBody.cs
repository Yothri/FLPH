﻿using System;

namespace ClientCommon.CommandBody
{
	public class GuildDungeonInfoCommandBody : CommandBody
	{
		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.dungeonId);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);
			this.dungeonId = reader.ReadInt32();
		}

		public GuildDungeonInfoCommandBody()
		{
		}

		public int dungeonId;
	}
}
