using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.MorningStar
{
    public class AlucardSwordBuff1 : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Alucard's Sword");
			// Description.SetDefault("A powerful blade is aiding you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.MorningStar.AlucardSword1>()] > 0) {
				modPlayer.AlucardSwordMinion1 = true;
			}
			if (!modPlayer.AlucardSwordMinion1) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}


			



		}
	}
}