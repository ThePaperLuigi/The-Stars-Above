using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Pets
{
    public class BladeWolfPetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Blade Wolf");
			Description.SetDefault("The blade wolf attends you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<WeaponPlayer>().BladeWolfPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.BladeWolfPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.BladeWolfPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			
			
		}
	}
}