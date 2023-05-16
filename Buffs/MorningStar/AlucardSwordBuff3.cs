using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.MorningStar
{
    public class AlucardSwordBuff3 : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Alucard's Sword");
			// Description.SetDefault("A powerful blade is aiding you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.MorningStar.AlucardSword3>()] > 0) {
				modPlayer.AlucardSwordMinion3 = true;
			}
			if (!modPlayer.AlucardSwordMinion3) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}


			



		}
	}
}