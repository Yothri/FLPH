﻿using System;

namespace ClientCommon.CommandBody
{
	public class UseHighRankFameSealCommandBody : CommandBody
	{
		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.slotNo);
			writer.Write(this.count);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);
			this.slotNo = reader.ReadInt32();
			this.count = reader.ReadInt32();
		}

		public UseHighRankFameSealCommandBody()
		{
		}

		public int slotNo;

		public int count;
	}
}
