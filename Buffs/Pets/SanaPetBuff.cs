using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Pets
{
    public class SanaPetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Astrogirl");
			// Description.SetDefault("Without the limiter, she'd cover the whole screen");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<WeaponPlayer>().SanaPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.SanaPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.SanaPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			
			
		}
	}
}