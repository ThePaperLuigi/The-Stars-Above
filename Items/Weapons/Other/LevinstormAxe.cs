using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;
using StarsAbove.Items.Pets;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.LevinstormAxe;
using StarsAbove.Buffs.LevinstormAxe;

namespace StarsAbove.Items.Weapons.Other
{
    public class LevinstormAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Boltstorm Axe");
			/* Tooltip.SetDefault("" +
				"[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty when aspected to Melee or Magic damage]" +
				"\nThe current [c/F592BF:Aspected Damage Type] influences the weapon's attacks (Can not be used if not Aspected to Melee or Magic)" +
				"\nRight click to activate [c/FDD059:Gathering Levinstorm], enhancing the weapon's attacks for 8 seconds (30 second cooldown)" +
				"\n[c/FFAB4D:Melee]: Attacks will throw the weapon at high velocity while ignoring terrain, piercing and causing lightning to radiate from struck foes" +
				"\nWhile [c/FDD059:Gathering Levinstorm] is active, gain Swiftness and 15% increased critical strike chance" +
				"\nAdditionally, during [c/FDD059:Gathering Levinstorm], critical strikes cause the target to explode, taking the damage again" + 
				"\n[c/EF4DFF:Magic]: Swings will emit powerful mid-ranged lightning towards foes, dealing half damage each" +
				"\nLightning ignores terrain and has no pierce limit, but split lightning deals 1/2th original damage" +
				"\nWhile [c/FDD059:Gathering Levinstorm] is active, gain 15% increased damage and critical strike chance" +
				"\n\nAdditionally, during [c/FDD059:Gathering Levinstorm], lightning critical strikes will cause lightning to strike from above, dealing half critical strike damage" +
                "\nFollow-up lightning can not trigger this effect" + //Umbra weapons
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 105;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 158;            //Weapon's texture's width
			Item.height = 158;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<Projectiles.LevinstormAxe.LevinstormAxe1>();
			Item.shootSpeed = 2;
		}

		public override bool AltFunctionUse(Player player)
		{
			
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
            {
                if (!player.HasBuff(BuffType<GatheringLevinstormCooldown>()))
                {
                    player.AddBuff(BuffType<GatheringLevinstormCooldown>(), 60 * 20);
                    player.AddBuff(BuffType<GatheringLevinstorm>(), 60 * 8);

                    SoundEngine.PlaySound(SoundID.Item92, player.Center);

                    for (int d = 0; d < 12; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
                        Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.8f);
                    }
                    return true;
                }
                else
                {

					return false;
                }
            }
            else
            {
				if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					return true;
				}
				else if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					return true;

				}
				else
				{
					return false;
				}
			}
			

			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2 && player.HasBuff(BuffType<GatheringLevinstorm>()))
			{
				player.GetCritChance(DamageClass.Melee) += 0.15f;
				player.AddBuff(BuffID.Swiftness, 10);

			}
			else if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 && player.HasBuff(BuffType<GatheringLevinstorm>()))
			{
				player.GetDamage(DamageClass.Magic) += 0.15f;
				player.GetCritChance(DamageClass.Magic) += 0.15f;
			}


			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<WeaponPlayer>().radiance++;
			
		}
		bool altSwing = false;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					SoundEngine.PlaySound(SoundID.Item71, player.Center);

					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
					velocity.X = perturbedSpeed.X;
					velocity.Y = perturbedSpeed.Y;
					for (int d = 0; d < 11; d++)
					{
						Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(24));
						float scale = 1f + (Main.rand.NextFloat() * 5.9f);
						perturbedSpeed1 = perturbedSpeed1 * scale;
						int dustIndex = Dust.NewDust(position, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed1.X, perturbedSpeed1.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;
					}
					velocity *= 2;
					Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<LevinstormAxeThrow>(), damage, 1f, Main.myPlayer, (Main.MouseWorld - player.Center).ToRotation(), Main.rand.Next(80));//

				}
				if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					if (player.direction == 1)
					{
						if (altSwing)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormAxe2>(), damage, knockback, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormVFX2>(), 0, knockback, player.whoAmI, 0f);

							altSwing = false;
						}
						else
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormAxe1>(), damage, knockback, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormVFX1>(), 0, knockback, player.whoAmI, 0f);

							altSwing = true;
						}

					}
					else
					{
						if (altSwing)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormAxe1>(), damage, knockback, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormVFX1>(), 0, knockback, player.whoAmI, 0f);

							altSwing = false;
						}
						else
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormAxe2>(), damage, knockback, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<LevinstormVFX2>(), 0, knockback, player.whoAmI, 0f);


							altSwing = true;
						}
					}

					Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 110f;
					position = new Vector2(position.X, position.Y + 7);
					if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
					{
						position += muzzleOffset;
					}
					SoundEngine.PlaySound(SoundID.Item71, player.Center);

					velocity *= 2;
					Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<LevinstormLightning>(), damage/2, 1f, Main.myPlayer, (Main.MouseWorld - player.Center).ToRotation(), Main.rand.Next(80));//

					for (int d = 0; d < 21; d++)
					{
						Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(24));
						float scale = 1f + (Main.rand.NextFloat() * 10.9f);
						perturbedSpeed1 = perturbedSpeed1 * scale;
						int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemTopaz, perturbedSpeed1.X, perturbedSpeed1.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;

					}

					//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<MiseryRound>(), 0, knockback, player.whoAmI);

					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
					velocity.X = perturbedSpeed.X;
					velocity.Y = perturbedSpeed.Y;
					for (int d = 0; d < 11; d++)
					{
						Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(24));
						float scale = 1f + (Main.rand.NextFloat() * 5.9f);
						perturbedSpeed1 = perturbedSpeed1 * scale;
						int dustIndex = Dust.NewDust(position, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed1.X, perturbedSpeed1.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;

					}
					/*for (int d = 0; d < 12; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);

						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.8f);
					}*/
					//



				}

				return false;

			}
			else
            {
				return false;

			}
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.ShroomiteBar, 8)
				.AddIngredient(ItemID.HallowedBar, 4)
				.AddIngredient(ItemID.PickaxeAxe, 1)
				.AddIngredient(ItemID.DD2LightningAuraT1Popper, 1)
				.AddIngredient(ItemType<EssenceOfLightning>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
