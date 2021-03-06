﻿using System;
using ClientCommon.PacketData;

namespace ClientCommon.CommandBody
{
	public class BossAnnihilationInfoResponseBody : ResponseBody
	{
		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.isEnterableTime);
			writer.Write(this.isOpened);
			writer.Write(this.isFinished);
			writer.Write(this.memberCount);
			writer.Write(this.myBestRecord);
			writer.Write(this.bestRecord);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);
			this.isEnterableTime = reader.ReadBoolean();
			this.isOpened = reader.ReadBoolean();
			this.isFinished = reader.ReadBoolean();
			this.memberCount = reader.ReadInt32();
			this.myBestRecord = reader.ReadPDPacketData<PDBossAnnihilationBestRecord>();
			this.bestRecord = reader.ReadPDPacketData<PDBossAnnihilationBestRecord>();
		}

		public BossAnnihilationInfoResponseBody()
		{
		}

		public bool isEnterableTime;

		public bool isOpened;

		public bool isFinished;

		public int memberCount;

		public PDBossAnnihilationBestRecord myBestRecord;

		public PDBossAnnihilationBestRecord bestRecord;
	}
}
