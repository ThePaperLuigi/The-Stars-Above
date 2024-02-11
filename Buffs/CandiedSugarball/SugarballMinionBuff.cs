using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.CandiedSugarball
{
    public class SugarballMinionBuff : ModBuff
	{
		public override void SetStaticDefaults() {

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.CandiedSugarball.Charsugar>()] > 0 || player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.CandiedSugarball.Sugartle>()] > 0 || player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.CandiedSugarball.Bulbasugar>()] > 0) {
				modPlayer.sugarballMinions = true;
			}
			if (!modPlayer.sugarballMinions) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}


		}
	}
}