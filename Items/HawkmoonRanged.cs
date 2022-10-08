using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class HawkmoonRanged : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Hawkmoon");
			Tooltip.SetDefault("Right click to reload the gun with 12 rounds" +
				"\nTime the reload perfectly to gain [c/0059FF:Paracausal Anomaly] until all bullets are spent" +
				"\n[c/0059FF:Paracausal Anomaly] increases damage while making all attacks critical" +
				"\nThis weapon does not use ammo" +
				"\nThis weapon can be re-crafted to swap between Magic and Ranged damage" +
				"\n'What you call Darkness is the end of your evolution'" +
				$"");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 42;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;
			Item.shootSpeed = 36f;
			Item.crit = 10;
			Item.shoot = ProjectileType<HawkmoonRoundWeak>();
			//item.UseSound = SoundID.Item11;
			Item.scale = 0.7f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int rounds;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled == false && player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds <= 0)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryReload, player.Center);
					player.GetModPlayer<StarsAbovePlayer>().hawkmoonPerfectReload = false;
	
					player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer = 0;
					player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled = true;

				}
				else
				{
					if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer >= 85 && player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer < 100 && player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled == true)//Success!
					{
						//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RadGunSuccess"), 0.5f);
						for (int d = 0; d < 22; d++)
						{
							Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}
						for (int d = 0; d < 12; d++)
						{
							Dust.NewDust(player.Center, 0, 0, 135, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}
						for (int d = 0; d < 88; d++)
						{
							Dust.NewDust(player.Center, 0, 0, 135, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}
						player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled = false;
						player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds = 12;
						player.GetModPlayer<StarsAbovePlayer>().hawkmoonPerfectReload = true;
						return true;
					}
					else
					{
						if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer > 20 && player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer < 85 && player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled == true)//Failure
						{
							player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled = false;
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RadGunFail"), 0.5f);
							player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds = 12;
							player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer = 0;
							
							return true;
						}
						else
						{
							return false;
						}
					}
				}
				
			}
			else
			{
				if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds > 0)
				{
					player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds--;
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;

		}
		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer >= 100 && player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled)//fail
			{
				player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled = false;
				//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/HuckleberryReload"), 1f);
				player.GetModPlayer<StarsAbovePlayer>().hawkmoonRounds = 12;
				player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer = 0;
				

			}
			if (player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimerEnabled)
			{
				player.GetModPlayer<StarsAbovePlayer>().hawkmoonReloadTimer += 3;
			}
			float launchSpeed = 2f + (int)Math.Round(player.GetModPlayer<StarsAbovePlayer>().bowCharge / 10);
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;

			if (player.altFunctionUse == 2)
			{

			}
			else
			{


			}
			//item.shootSpeed = 8f + (int)Math.Round(player.GetModPlayer<StarsAbovePlayer>().bowCharge / 10);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			else
			{
				if(player.GetModPlayer<StarsAbovePlayer>().hawkmoonPerfectReload == true)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y,ProjectileType<HawkmoonRound>(), damage + 8, knockback, player.whoAmI, 1f);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryShoot, player.Center);

				}
				else
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryShoot, player.Center);

					return true;
				}
				

			}

			return false;



		}


		public virtual void OnHitNPCWithProj(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
			

		}
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

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		//public override Vector2? HoldoutOffset()
		//{
		//	return new Vector2(-15, 5);
		//}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			if (player.statMana > 5)
			{

				Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WhisperShoot"));

			}
			else
			{
				Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/WhisperReload"));
			}

			return true;
		}*/

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
				.AddIngredient(ItemType<HawkmoonMagic>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfTheHawkmoon>())
				.AddIngredient(ItemType<EnigmaticDust>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}
