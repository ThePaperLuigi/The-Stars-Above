using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class VitchPetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Fox Vixen");
			// Description.SetDefault("She'll kill you and charge extra for disposal");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<WeaponPlayer>().VitchPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.VitchPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.VitchPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			if (player.controlDown && player.releaseDown) {
				if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15) {
					for (int j = 0; j < 1000; j++) {
						if (Main.projectile[j].active && Main.projectile[j].type == ProjectileType<Projectiles.Pets.VitchPet>() && Main.projectile[j].owner == player.whoAmI) {
							Projectile lightpet = Main.projectile[j];
							Vector2 vectorToMouse = Main.MouseWorld - lightpet.Center;
							lightpet.velocity += 5f * Vector2.Normalize(vectorToMouse);
						}
					}
				}
			}
		}
	}
}