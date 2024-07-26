using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Magic.VenerationOfButterflies;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Other
{
    public class Suistrume : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("Use this item to expend mana, filling your [c/c2f4ff:Performance Gauge] and beginning your [c/40b9ff:Stellar Performance]" +
				"\n[c/40b9ff:Stellar Performance] will increase all outgoing damage by 15%" +
				"\n[c/40b9ff:Stellar Performance] will increase movement speed by 40%" +
				"\n[c/40b9ff:Stellar Performance] will grant all nearby team members (including the user) a 5% increase to outgoing damage along with bonus health and mana regeneration" +
				"\nThe [c/c2f4ff:Performance Gauge] will nautrally deplete over time" +
				"\nMoving during [c/40b9ff:Stellar Performance] will increase the [c/c2f4ff:Performance Gauge]" +
				"\nThe [c/c2f4ff:Performance Gauge] will deplete further according to damage taken (maximum 30% of the gauge lost per hit)" +
				"\nIf the [c/c2f4ff:Performance Gauge] reaches 0, the [c/40b9ff:Stellar Performance] will end and will have a 2 minute cooldown" +
				"\nIf the [c/40b9ff:Stellar Performance] is completed in its entirety, you will restore all HP and MP and [c/40b9ff:Stellar Performance] will have its cooldown halved to 1 minute" +
				"\n'Whatever you're singing, it's cute as always'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 150;
			Item.width = 36;
			Item.height = 38;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			//item.UseSound = SoundID.Item26;
			Item.autoReuse = false;
			Item.shoot = ProjectileType<ButterflyProjectile>();
			Item.shootSpeed = 15f;
			
		}
		int upkeepTimer;

		public override bool CanUseItem(Player player)
		{
			

			if (player.GetModPlayer<WeaponPlayer>().stellarPerformanceCooldown == true)
			{
				player.GetModPlayer<WeaponPlayer>().stellarPerformanceStart = false;
				return false;
			}
			else
			{
				if (player.GetModPlayer<WeaponPlayer>().stellarPerformanceActive == false)
				{

					player.GetModPlayer<WeaponPlayer>().stellarPerformanceStart = true;

					return true;
				}
			}

				
			
			return false;
		}

		public override void HoldItem(Player player)
		{

			

			base.HoldItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			
			
			
			return false; // return false because we don't want tmodloader to shoot projectile
		}

		// What if I wanted an inaccurate gun? (Chain Gun)
		// Inaccurate Gun style: Single Projectile, Random spread 
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			return true;
		}*/

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}*/


		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?

		// How can I get a "Clockwork Assault Rifle" effect?
		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
		/*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override void OnConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

		// How can I shoot 2 different projectiles at the same time?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

		// How can I choose between several projectiles randomly?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.LunarBar, 20)
				.AddIngredient(ItemID.FragmentStardust, 20)
				.AddIngredient(ItemType<EssenceOfStarsong>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
