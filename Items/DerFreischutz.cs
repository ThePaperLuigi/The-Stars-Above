using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class DerFreischutz : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes at 70% efficiency, but inherits 150% from critical strike chance modifiers]" +
                "\nBullets will fire from your cursor" +
				"\nLine of sight is not required, but bullets will inherit direction" +
				"\nCritical rate and damage will increase with the amount of bullets fired, but the seventh shot will have recoil damage and damage allies" +
				"\nIf the recoil damage would kill you, the gun will misfire" +
				"\n'This magical bullet can truly hit anyone- just like you say'" +
				$""); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 29;
			Item.DamageType = ModContent.GetInstance<Systems.PsychomentDamageClass>();
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 999f;
			Item.useAmmo = AmmoID.Bullet;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int curse = 0;

		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			position = Main.MouseWorld;
			base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position = Main.MouseWorld;
			if (player.statLife < 26)
			{
				return false;
			}
			else
			{


				curse += 1;
				if (curse >= 7)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld.X, Main.MouseWorld.Y,velocity.X, velocity.Y,302, damage, knockback, player.whoAmI, 0f);
					curse = 0;
					player.statLife -= 10;

					for (int d = 0; d < 70; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 60, 0f, 0f, 150, default(Color), 1.5f);
						SoundEngine.PlaySound(StarsAboveAudio.SFX_Death, player.Center);
					}

				}
				if (curse < 7)
				{
					Item.crit = curse * 10;//No += for variables, just modifier
					Item.damage = 12 + curse * 5;
				}

				for (int l = 0; l < Main.projectile.Length; l++)
				{
					Projectile proj = Main.projectile[l];
					if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
					{
						proj.active = false;
					}
				}
				Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
				if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				{

				}


				player.AddBuff(Item.buffType, 2);

				position = Main.MouseWorld;
				return true;
			}
		}
		public override void HoldItem(Player player)
		{
			
		}
		//public override void HoldItem(Player player)
		//{
		//	Dust.NewDust(Mouse.position, player.width, player.height, DustType<Crosshair>());

		//}

		/*
		 * Feel free to uncomment any of the examples below to see what they do
		 */

		// What if I wanted this gun to have a 38% chance not to consume ammo?
		/*public override void OnConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .38f;
		}*/

		// What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
		// Uzi/Molten Fury style: Replace normal Bullets with Highvelocity
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/

		// What if I wanted it to shoot like a shotgun?
		// Shotgun style: Multiple Projectiles, Random spread 
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}*/

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
				.AddIngredient(ItemID.Handgun, 1)
				.AddIngredient(ItemType<EssenceOfTheFreeshooter>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

}
