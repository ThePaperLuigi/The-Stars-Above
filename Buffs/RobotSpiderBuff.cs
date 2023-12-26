using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class RobotSpiderBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Mechanical Arachnids");
			// Description.SetDefault("The mechanical arachnids aid you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.ArachnidNeedlepoint.RobotSpider>()] > 0) {
				modPlayer.RobotSpiderMinion = true;
			}
			if (!modPlayer.RobotSpiderMinion) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}

			if (player.whoAmI == Main.myPlayer && player.controlDown && player.releaseDown
				&& player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
			{
				player.AddBuff(BuffType<RobotSpiderBuff>(), 3600, true);
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("RobotSpiderStationary").Type, 0, 0, player.whoAmI);
				Main.projectile[index].originalDamage = 0;
			}

			



		}
	}
}