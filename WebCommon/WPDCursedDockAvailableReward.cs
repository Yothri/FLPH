﻿using System;

namespace WebCommon
{
	public class WPDCursedDockAvailableReward : WPDPacketData
	{
		public override void Serialize(WPacketWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.rewardNo);
			writer.Write(this.itemId);
		}

		public override void Deserialize(WPacketReader reader)
		{
			base.Deserialize(reader);
			this.rewardNo = reader.ReadInt32();
			this.itemId = reader.ReadInt32();
		}

		public WPDCursedDockAvailableReward()
		{
		}

		public int rewardNo;

		public int itemId;
	}
}
