using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.SparkblossomBeacon
{
    public class FleetingSparkBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Fleeting Spark");
			Description.SetDefault("Static electricity arcs from your minions");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.SparkblossomBeacon.FleetingSparkMinion>()] > 0) {
				modPlayer.FleetingSparkMinion = true;
			}
			if (!modPlayer.FleetingSparkMinion) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}


		}
	}
}