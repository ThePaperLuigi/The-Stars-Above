using Microsoft.Xna.Framework;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using System.Reflection;
using System;
using IL.Terraria.Utilities;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
	public class IzanagiEdge : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Izanagi's Edge");
			Tooltip.SetDefault("Every fourth shot will always deal critical damage" +
				"\nAll critical damage from this weapon does quadruple damage instead of double" +
				"\nDamage will increase with every shot" +
				"\nAfter the fourth shot is taken, the gun will take time to reload" +
				"\nIf you land enough shots, your next reload will gain the strength of [c/66B2D1:Honed Edge]" +
				"\nOnce you reload with [c/66B2D1:Honed Edge], the next shot will deal 10x the normal amount critically, and will then reload" +
				"\nGain 100 mana if [c/66B2D1:Honed Edge]'s empowered shot hits an enemy" +
				"\nThis mana increase does not apply if this weapon is attuned to Melee, but attack speed is drastically increased" +
				"\nYou have permanent Swiftness when this weapon is in your hands" +
				"\n'Strike true! Ziodyne!'" +
				$"");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 140;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 106;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.mana = 25;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = false;
			Item.shootSpeed = 86f;
			Item.crit = 1;
			Item.reuseDelay = 40;
			Item.shoot = ProjectileType<IzanagiRound>();
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		int shotCount = 0;
		
		int edgeHonedPrep = 0;
		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			shotCount++;
			if (player.GetModPlayer<StarsAbovePlayer>().edgeHoned)
			{
				player.GetModPlayer<StarsAbovePlayer>().edgeHoned = false;
			}

			if (shotCount == 4)
			{
				if (edgeHonedPrep == 1)
				{
					

					SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiShootBuff, player.Center);
					player.GetModPlayer<StarsAbovePlayer>().edgeHoned = true;
					edgeHonedPrep = 0;
					Item.useTime = 70;
					Item.useAnimation = 70;
					Item.reuseDelay = 70;

				}

				Item.damage = 120 + shotCount * 5;
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 244, 0f, 0f, 150, default(Color), 1.5f);
					}
				
				shotCount = 0;
				Item.crit = 100;

				SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiShoot, player.Center);
				if (player.GetModPlayer<StarsAbovePlayer>().izanagiPerfect >= 3)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiReloadBuff, player.Center);
					edgeHonedPrep = 1;

					shotCount = 3;
					Item.useTime = 70;
					Item.useAnimation = 70;
					Item.reuseDelay = 70;
				}
				else
				{
					
					SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiReload, player.Center);
					Item.useTime = 20;
					Item.useAnimation = 20;
					Item.reuseDelay = 40;
				}
				
				
				
				player.GetModPlayer<StarsAbovePlayer>().izanagiPerfect = 0;

			}
			else
			{
				if (shotCount == 3)
				{
					
					Item.damage = 120 + shotCount * 20;
					Item.useTime = 70;
					Item.useAnimation = 70;
					Item.reuseDelay = 70;
					Item.shootSpeed = 86f;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiShoot, player.Center);
				}
				else
				{
					Item.damage =120 + shotCount * 20;
					Item.crit = 10;
					Item.useTime = 20;
					Item.useAnimation = 20;
					Item.reuseDelay = 40;
					Item.shootSpeed = 86f;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_izanagiShoot, player.Center);
				}
			}

			return true;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(BuffID.Swiftness, 1);
			Vector2 position = player.position;
			if (shotCount == 3)
			{
				
				Dust.NewDust(position, 20, 20, 206, 0f, 0f, 150, default(Color), 1.5f);
				if (edgeHonedPrep == 1)
				{
					Dust.NewDust(position, 20, 20, 206, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					Dust.NewDust(position, 20, 20, 159, 0f + Main.rand.Next(-50, 50), 0f + Main.rand.Next(-50, 50), 150, default(Color), 0.5f);
				}	
			}
			else
			{
				

			}
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

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}

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
				.AddIngredient(ItemType<EssenceOfIzanagi>())
				.AddIngredient(ItemType<EnigmaticDust>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}
