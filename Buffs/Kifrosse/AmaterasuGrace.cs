using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
	public class AmaterasuGrace : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Amaterasu's Grace");
			Description.SetDefault("Mystic energy grants effective damage against foes inflicted with frostburn");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex) {
			
		}
	}
}