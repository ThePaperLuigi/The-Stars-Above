using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
    public class KifrosseBuff2 : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Kifrosse (2 Tails)");
			// Description.SetDefault("Mystic energy chills the air around you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.Kifrosse2>()] > 0) {
				modPlayer.Kifrosse2 = true;
			}
			if (!modPlayer.Kifrosse2) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}
			player.GetDamage(DamageClass.Summon) += 0.1f;
		}
	}
}