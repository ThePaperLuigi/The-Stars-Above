using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.EverlastingPickaxe
{
    public class EverlastingGunpowderLoaded : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Everlasting Gunpowder Loaded");
			Description.SetDefault("The Everlasting Pickaxe has gained new attacking abilities" +
                "\nDamage is increased by 80%");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			player.GetDamage(DamageClass.Melee) += 0.8f;

		}
	}
}