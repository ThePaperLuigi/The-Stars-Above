using Microsoft.Xna.Framework;
using StarsAbove.Buffs.TrickspinTwoStep;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.TrickspinTwoStep;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class TrickspinTwoStep : ModItem
	{
		public override void SetStaticDefaults()
		{
			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 24; // The width of the item's hitbox.
			Item.height = 24; // The height of the item's hitbox.

			Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
			Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
			Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
			Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
			Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).

			Item.damage = 17; // The amount of damage the item does to an enemy or player.
			Item.DamageType = DamageClass.Summon; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
			Item.knockBack = 2.5f; // The amount of knockback the item inflicts.
			Item.crit = 8; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
			Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
			Item.rare = ItemRarityID.Green; // The item's rarity. This changes the color of the item's name.
			Item.value = Item.buyPrice(gold: 1); // The amount of money that the item is can be bought for.

			Item.shoot = ModContent.ProjectileType<TrickspinTwoStepProjectile>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
			Item.shootSpeed = 16f; // The velocity of the shot projectile.			
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<MeAndMyKillingMachineCooldown>()))
                {
					SoundEngine.PlaySound(SoundID.Item77, player.Center);

					float dustAmount = 12f;
					float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + randomConstant);
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 1.5f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(45));
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 1.5f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 1.5f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(45));
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 1.5f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(135));
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 1.5f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}

					player.AddBuff(BuffType<MeAndMyKillingMachineBuff>(), 60 * 8);
					player.AddBuff(BuffType<MeAndMyKillingMachineCooldown>(), 60 * 30);
				}
				else
                {
					return false;
                }
				
				

			}

			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			if(StarsAbove.weaponActionKey.JustPressed)
            {
				float launchSpeed = 18f;
				Vector2 pos = player.GetModPlayer<WeaponPlayer>().TrickspinCenter;
				Vector2 direction = Vector2.Normalize(pos - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;

				//If player does NOT have a yoyo
				if (player.ownedProjectileCounts[ProjectileType<TrickspinSuspendYoyo>()] < 1)
				{
					SoundEngine.PlaySound(SoundID.Item1, player.Center);

					Vector2 directionMouse = Vector2.Normalize(Main.MouseWorld - player.Center);
					Vector2 arrowVelocityMouse = directionMouse * launchSpeed;
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y,arrowVelocityMouse.X, arrowVelocityMouse.Y, ProjectileType<TrickspinSuspendYoyo>(), 0, 0, player.whoAmI, 0f);
					float rotation = (float)Math.Atan2(player.Center.Y - Main.MouseWorld.Y, player.Center.X - Main.MouseWorld.X);

					for (int d = 0; d < 35; d++)
					{
						float Speed2 = Main.rand.NextFloat(0, 18);  //projectile speed
						Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
						int dustIndex = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;
					}

				}
				else
				{
					if (player.GetModPlayer<WeaponPlayer>().TrickspinReady && !player.HasBuff(BuffType<TrickspinLeapCooldown>()))
					{
						player.AddBuff(BuffType<KickStartBuff>(), 180);
						player.AddBuff(BuffType<TrickspinLeapCooldown>(), 30);
						player.velocity += new Vector2(arrowVelocity.X, arrowVelocity.Y);

						SoundEngine.PlaySound(SoundID.DoubleJump, player.Center);


						float rotation = (float)Math.Atan2(player.Center.Y - pos.Y, player.Center.X - pos.X);
						// Large Smoke Gore spawn
						for (int g = 0; g < 4; g++)
						{
							int goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[goreIndex].scale = 1f;
							Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
							Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
							goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[goreIndex].scale = 1f;
							Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
							Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
							goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[goreIndex].scale = 1f;
							Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
							Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
							goreIndex = Gore.NewGore(null, new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[goreIndex].scale = 1f;
							Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
							Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
						}
						for (int d = 0; d < 35; d++)
						{
							float Speed2 = Main.rand.NextFloat(0, -18);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
							int dustIndex = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
							Main.dust[dustIndex].noGravity = true;
						}
					}
				}
				
			}

            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.altFunctionUse == 2)
			{
				

				return false;
            }
			else
            {
				SoundEngine.PlaySound(SoundID.Item1, player.Center);

			}

			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.WoodYoyo)
				.AddIngredient(ItemID.FlinxFur)
				.AddIngredient(ItemID.WhiteString)
				.AddIngredient(ItemID.FlintlockPistol)
				.AddIngredient(ItemType<EssenceOfSpinning>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}