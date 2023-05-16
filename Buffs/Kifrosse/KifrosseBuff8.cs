using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
    public class KifrosseBuff8 : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Kifrosse (8 Tails)");
			// Description.SetDefault("Mystic energy chills the air around you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.Kifrosse8>()] > 0) {
				modPlayer.Kifrosse8 = true;
			}
			if (!modPlayer.Kifrosse8) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}
			player.GetDamage(DamageClass.Summon) += 0.40f;
		}
	}
}