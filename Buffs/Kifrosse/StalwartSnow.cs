using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
	public class StalwartSnow : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Stalwart Snow");
			Description.SetDefault("Proximity to the Foxfrost Mystic grants increased defense");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.statDefense += 16;
		}
	}
}