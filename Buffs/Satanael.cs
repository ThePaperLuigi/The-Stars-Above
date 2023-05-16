using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class Satanael : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Satanael");
			// Description.SetDefault("'You would slay gods to protect the justice you believe in...'");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Satanael>()] > 0) {
				modPlayer.SatanaelMinion = true;
			}
			if (!modPlayer.SatanaelMinion) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}
		}
	}
}