using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
	public class AmaterasuWinter : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Amaterasu's Winter");
			Description.SetDefault("Mystic energy empowers the Blizzard Foxfires");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex) {
			
		}
	}
}