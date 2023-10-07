using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Summon.KroniicPrincipality;

namespace StarsAbove.Items.Weapons.Summon
{
    public class KroniicAccelerator : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Kroniic Principality");
			/* Tooltip.SetDefault("Use this weapon to throw a piercing projectile" +
				"\nHolding this weapon additionally summons 2 [c/0C65A7:Temporal Timepieces] to orbit you" +
				"\nWhen a [c/0C65A7:Temporal Timepiece] comes into contact with an enemy, it accrues [c/97BCBB:Chronal Heat] displayed within the [c/EFCE38:Temporal Gauge]" +
				"\nAbove 80% [c/97BCBB:Chronal Heat], both the [c/0C65A7:Temporal Timepieces] and the weapon will deal 60 bonus damage" +
				"\n[c/97BCBB:Chronal Heat] will naturally deplete over time" +
				"\nRight click to expend 50 mana and 80 [c/97BCBB:Chronal Heat], preparing a [c/6764F9:Timeframe]" +
				"\nAfter 3 seconds, the [c/EFCE38:Temporal Gauge] will light up, indicating the [c/6764F9:Timeframe] is ready" +
				"\nRight click with a [c/6764F9:Timeframe] ready to instantly return to your position upon cast of the [c/6764F9:Timeframe]" +
				"\nThis restores Health and Mana to the values present when the [c/6764F9:Timeframe] was prepared" +
				"\nThe [c/6764F9:Timeframe] lasts for 6 seconds before it expires, and it has a cooldown of 1 minute" +
				"\n'The time.. has come'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 88;
			Item.DamageType = DamageClass.Summon;
			Item.width = 52;
			Item.height = 20;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Red;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TemporalTimepiece>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int heatDrain;
		int blastingChargeTimer;
		Vector2 savedPosition;
		int savedHP;
		int savedMana;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().powderGaugeIndicatorOn == false && blastingChargeTimer < 0 && !Main.LocalPlayer.HasBuff(BuffType<Buffs.KroniicPrincipalityCooldown>()))
				{
					if (player.statMana >= 50 && player.GetModPlayer<WeaponPlayer>().powderGauge >= 80)
					{
						player.GetModPlayer<WeaponPlayer>().powderGauge -= 80;
						player.statMana -= 50;
						blastingChargeTimer = 180;
						player.GetModPlayer<WeaponPlayer>().kroniicSavedPosition = player.position;
						player.GetModPlayer<WeaponPlayer>().kroniicSavedHP = player.statLife;
						player.GetModPlayer<WeaponPlayer>().kroniicSavedMP = player.statMana;
						player.GetModPlayer<WeaponPlayer>().kroniicTeleport = true;
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					if(player.GetModPlayer<WeaponPlayer>().powderGaugeIndicatorOn == true)
					{
						return true;
					}

					return false;
				}
			}
			else
			{

			}
			return player.ownedProjectileCounts[Item.shoot] < 1;//Important (only 1 spear)
		}

		public override void HoldItem(Player player)
		{
			
			player.GetModPlayer<WeaponPlayer>().kroniicHeld = 10;
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.KroniicPrincipality.TemporalTimepiece2>()] < 1)
			{
				
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Summon.KroniicPrincipality.TemporalTimepiece2>(), Item.damage, 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;

			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.KroniicPrincipality.TemporalTimepiece3>()] < 1)
			{
				

				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Summon.KroniicPrincipality.TemporalTimepiece3>(), Item.damage, 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;
			}
			blastingChargeTimer--;
			if (blastingChargeTimer == 0)
			{
				player.GetModPlayer<WeaponPlayer>().kroniicTimer = 360;
				player.GetModPlayer<WeaponPlayer>().powderGaugeIndicatorOn = true;
			}
			if (player.GetModPlayer<WeaponPlayer>().powderGauge > 0)
			{
				heatDrain++;
				if (heatDrain >= 10)
				{
					player.GetModPlayer<WeaponPlayer>().powderGauge--;
					heatDrain = 0;
				}
			}
			Vector2 playerCenterFixed = new Vector2(player.Center.X, player.Center.Y + 15);
			for (int i = 0; i < 30; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 217);
				offset.Y += (float)(Math.Cos(angle) * 217);

			
				Dust d = Dust.NewDustPerfect(playerCenterFixed + offset, 159, Vector2.Zero, 200, default(Color), 0.7f);
				d.fadeIn = 0.0000001f;
				d.noGravity = true;
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
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().powderGaugeIndicatorOn == true)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_TimeEffect, player.Center);
					player.AddBuff(BuffType<Buffs.KroniicPrincipalityCooldown>(), 3600);//7200 is 2 minutes
					player.Teleport(player.GetModPlayer<WeaponPlayer>().kroniicSavedPosition, 1, 0);
					player.statLife = player.GetModPlayer<WeaponPlayer>().kroniicSavedHP;
					player.statMana = player.GetModPlayer<WeaponPlayer>().kroniicSavedMP;
					player.GetModPlayer<WeaponPlayer>().kroniicTeleport = false;
					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, savedPosition.X, savedPosition.Y, 1, 0, 0);
					player.GetModPlayer<WeaponPlayer>().powderGaugeIndicatorOn = false;

					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(player.position, 0, 0, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(player.position, 0, 0, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(player.position, 0, 0, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(player.position, 0, 0, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(player.position, 0, 0, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
					}
				}
				return false;
				
			}
			else
			{
				if(player.GetModPlayer<WeaponPlayer>().powderGauge >= 80)
                {
					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<TemporalTimepiece>(), damage += 60, knockback, player.whoAmI, 0f);


					Main.projectile[index].originalDamage = Item.damage;
					
				}
				else
                {
					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<TemporalTimepiece>(), damage, knockback, player.whoAmI, 0f);


					Main.projectile[index].originalDamage = Item.damage;
					
				}
				
			}
			return false;
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
										.AddIngredient(ItemType<EssenceOfTime>())
				.AddIngredient(ItemType<PrismaticCore>(), 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
