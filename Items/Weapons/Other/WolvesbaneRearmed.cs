using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Other.Hawkmoon;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Other.Wolvesbane;
using StarsAbove.Buffs.Other.Wolvesbane;
using Mono.Cecil;
using StarsAbove.Projectiles.Melee.RebellionBloodArthur;
using StarsAbove.Projectiles.Ranged.KissOfDeath;
using StarsAbove.Buffs;

namespace StarsAbove.Items.Weapons.Other
{
    public class WolvesbaneRearmed : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Lime;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;
			Item.shootSpeed = 36f;
			Item.crit = 10;
			Item.shoot = ProjectileType<WolvesbaneRound>();
			//item.UseSound = SoundID.Item11;
			Item.scale = 0.7f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
			Item.autoReuse = true;
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
				if(!player.HasBuff(BuffType<WolvesbaneBlastCooldown>()))
				{
                    Item.useTime = 40;
                    Item.useAnimation = 40;
                    return true;

                }
                else
				{
					return false;

				}

            }
            else
			{
                Item.useTime = 15;
                Item.useAnimation = 15;
                return true;

            }

        }
		public override void HoldItem(Player player)
        {
            player.AddBuff(BuffID.Wrath, 2);

            player.GetModPlayer<WeaponPlayer>().wolvesbaneGauge = MathHelper.Clamp(player.GetModPlayer<WeaponPlayer>().wolvesbaneGauge, 0, 100);
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.Current && player.itemTime <= 0)
            {
                if (player.GetModPlayer<WeaponPlayer>().wolvesbaneGauge >= 50)
                {
					if (Main.MouseWorld.X < player.Center.X)
					{
						player.direction = -1;
					}
					else
					{
						player.direction = 1;
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_skofnungSwing, player.Center);

                    player.SetDummyItemTime(20);
                    player.GetModPlayer<WeaponPlayer>().wolvesbaneGauge -= 50;
                    Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
                    Vector2 Velocity = direction * (18);
                    Vector2 direction2 = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y + 10) - player.Center) ;
					Vector2 Velocity2 = direction2 * 20;
                    player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;


                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<WolvesbaneRearmedSword>(), 0, 0, player.whoAmI, 0, 0, player.direction);
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Velocity, ProjectileType<WolvesbaneRearmedShockwave>(), player.GetWeaponDamage(Item)*3, 0, player.whoAmI, 0, 0, player.direction);
					//Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, direction, ProjectileType<WolvesbaneSlash>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0, 0, player.direction);
                    player.AddBuff(BuffType<Invincibility>(), 10);

                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);
                        Dust.NewDust(player.Center, 0, 0, DustID.GemRuby, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

                    }
                }
            }
            

            player.GetModPlayer<WeaponPlayer>().wolvesbaneHeld = true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
                
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 60f;
                position = new Vector2(position.X, position.Y);
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<WolvesbaneGunEmpowered>(), 0, knockback, player.whoAmI);

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<WolvesbaneBlastRound>(), damage * 2, knockback, player.whoAmI, 1f);
                SoundEngine.PlaySound(SoundID.Item125, player.Center);
				player.AddBuff(BuffType<WolvesbaneBlastCooldown>(), 4 * 60);

                for (int d = 0; d < 30; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(21));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemRuby, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 30; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(11));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(position, 0, 0, DustID.LifeDrain, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 30; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(33));
                    float scale = 0.3f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemRuby, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 30; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                    float scale = 0.4f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(position, 0, 0, DustID.LifeDrain, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
                position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 20f;
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(velocity.ToRotation());
                    int dust = Dust.NewDust(position, 0, 0, DustID.GemRuby);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = position + spinningpoint5;
                    Main.dust[dust].velocity = velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                }
                position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 10f;

                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(velocity.ToRotation());
                    int dust = Dust.NewDust(position, 0, 0, DustID.GemRuby);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = position + spinningpoint5;
                    Main.dust[dust].velocity = velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
                }
                return false;
			}
			else
			{
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 60f;
                position = new Vector2(position.X, position.Y);
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<WolvesbaneRearmedGun>(), 0, knockback, player.whoAmI);

                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<WolvesbaneRound>(), damage, knockback, player.whoAmI, 1f);
                SoundEngine.PlaySound(SoundID.Item11, player.Center);
            }

            return false;



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
                .AddIngredient(ItemID.Nanites, 20)
                .AddIngredient(ItemType<NeonTelemetry>(), 5)
                .AddIngredient(ItemType<Wolvesbane>())
                .AddTile(TileID.Anvils)
                .Register();
        }
	}
}
