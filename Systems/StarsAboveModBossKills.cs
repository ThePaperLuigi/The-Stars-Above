using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarsAbove
{
    public class DownedBossSystem : ModSystem
	{
		public static bool downedWarrior = false;
		public static bool downedVagrant = false;
		public static bool downedPenth = false;
		public static bool downedArbiter = false;
		public static bool downedNalhaun = false;
		public static bool downedTsuki = false;
		public static bool downedDioskouroi = false;

		public override void OnWorldLoad()
		{
			downedWarrior = false;
			downedVagrant = false;
			downedPenth = false;
			downedArbiter = false;
			downedNalhaun = false;
			downedTsuki = false;
			downedDioskouroi = false;

		}

		public override void OnWorldUnload()
		{
			downedWarrior = false;
			downedVagrant = false;
			downedPenth = false;
			downedArbiter = false;
			downedNalhaun = false;
			downedTsuki = false;
			downedDioskouroi = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{
			if (downedWarrior)
			{
				tag["downedWarrior"] = true;
			}
			if (downedVagrant)
			{
				tag["downedVagrant"] = true;
			}
			if (downedNalhaun)
			{
				tag["downedNalhaun"] = true;
			}
			if (downedPenth)
			{
				tag["downedPenth"] = true;
			}
			if (downedArbiter)
			{
				tag["downedArbiter"] = true;
			}
			if (downedTsuki)
			{
				tag["downedTsuki"] = true;
			}
			if (downedDioskouroi)
			{
				tag["downedDioskouroi"] = true;
			}
			// if (downedOtherBoss) {
			//	tag["downedOtherBoss"] = true;
			// }
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedWarrior = tag.ContainsKey("downedWarrior");
			downedVagrant = tag.ContainsKey("downedVagrant");
			downedNalhaun = tag.ContainsKey("downedNalhaun");
			downedPenth = tag.ContainsKey("downedPenth");
			downedArbiter = tag.ContainsKey("downedArbiter");
			downedTsuki = tag.ContainsKey("downedTsuki");
			downedDioskouroi = tag.ContainsKey("downedDioskouroi");
		}

		public override void NetSend(BinaryWriter writer)
		{
			//Order of operations is important and has to match that of NetReceive
			var flags = new BitsByte();
			flags[0] = downedWarrior;
			flags[1] = downedVagrant;
			flags[2] = downedNalhaun;
			flags[3] = downedPenth;
			flags[4] = downedArbiter;
			flags[5] = downedTsuki;
			flags[6] = downedDioskouroi;

			writer.Write(flags);

		}

		public override void NetReceive(BinaryReader reader)
		{

			BitsByte flags = reader.ReadByte();
			downedWarrior = flags[0];
			downedVagrant = flags[1];
			downedNalhaun = flags[2];
			downedPenth = flags[3];
			downedArbiter = flags[4];
			downedTsuki = flags[5];
			downedDioskouroi = flags[6];
		}
	}
}

