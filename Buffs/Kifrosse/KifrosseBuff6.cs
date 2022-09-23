using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
    public class KifrosseBuff6 : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Kifrosse (6 Tails)");
			Description.SetDefault("Mystic energy chills the air around you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.Kifrosse6>()] > 0) {
				modPlayer.Kifrosse6 = true;
			}
			if (!modPlayer.Kifrosse6) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}
			player.GetDamage(DamageClass.Summon) += 0.30f;
		}
	}
}