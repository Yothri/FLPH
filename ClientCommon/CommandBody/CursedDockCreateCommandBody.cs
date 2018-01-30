﻿using System;

namespace ClientCommon.CommandBody
{
	public class CursedDockCreateCommandBody : CommandBody
	{
		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.requiredBattlePower);
			writer.Write(this.autoStart);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);
			this.requiredBattlePower = reader.ReadInt32();
			this.autoStart = reader.ReadBoolean();
		}

		public CursedDockCreateCommandBody()
		{
		}

		public int requiredBattlePower;

		public bool autoStart;
	}
}
