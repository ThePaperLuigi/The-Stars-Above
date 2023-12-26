using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.AshenAmbition;
using StarsAbove.Items.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles;
using StarsAbove.Buffs.BrilliantSpectrum;
using Terraria.Graphics.Shaders;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Melee.AshenAmbition;
using StarsAbove.Projectiles.Celestial.BrilliantSpectrum;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class BrilliantSpectrum : ModItem
	{
		public override void SetStaticDefaults()
		{

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 30;           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 12;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 12;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<AshenAmbitionSpear>();
			Item.shootSpeed = 20f;
			Item.noUseGraphic = true;
			Item.noMelee = true;

		}
		bool altAttack = false;
		int lifeDrainTimer = 0;
		int lifeDrainTimeMax = 50;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void UpdateInventory(Player player)
		{
			if (NPC.downedSlimeKing)
			{
				Item.damage = 8;
			}
			if (NPC.downedBoss1)
			{
				Item.damage = 12;
			}
			if (NPC.downedBoss2)
			{
				Item.damage = 14;
			}
			if (NPC.downedQueenBee)
			{
				Item.damage = 19;
			}
			if (NPC.downedBoss3)
			{
				Item.damage = 22;
			}
			if (Main.hardMode)
			{
				Item.damage = 26;
			}
			if (NPC.downedMechBossAny)
			{
				Item.damage = 35;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				Item.damage = 44;
			}
			if (NPC.downedPlantBoss)
			{
				Item.damage = 68;
			}
			if (NPC.downedGolemBoss)
			{
				Item.damage = 80;
			}
			if (NPC.downedFishron)
			{
				Item.damage = 96;
			}
			if (NPC.downedAncientCultist)
			{
				Item.damage = 110;
			}
			if (NPC.downedMoonlord)
			{
				Item.damage = 125;
			}
			base.UpdateInventory(player);
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<SpectrumDashCooldown>()))
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
			var modPlayer = player.GetModPlayer<WeaponPlayer>();
			modPlayer.BrilliantSpectrumHeld = true;
			//player.AddBuff(BuffType<BrilliantSpectrumBuff>(), 10);
			if (modPlayer.refractionGauge >= 1 && modPlayer.refractionGauge < 20)
			{
				if (player.ownedProjectileCounts[ProjectileType<SpectrumVFX1>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SpectrumVFX1>(), 0, 0, player.whoAmI, 0f);
				}
			}
			if (modPlayer.refractionGauge >= 20 && modPlayer.refractionGauge < 90)
			{
				if (player.ownedProjectileCounts[ProjectileType<SpectrumVFX2>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SpectrumVFX2>(), 0, 0, player.whoAmI, 0f);
				}
			}
			if (modPlayer.refractionGauge >= 90)
			{
				if (player.ownedProjectileCounts[ProjectileType<SpectrumVFX3>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0, 0, ProjectileType<SpectrumVFX3>(), 0, 0, player.whoAmI, 0f);
				}
			}
			if (modPlayer.refractionGauge >= 80)
            {
				player.AddBuff(BuffType<Alacrity>(), 10);
            }
			if (player.HasBuff(BuffID.OnFire) || player.HasBuff(BuffID.OnFire3) || player.HasBuff(BuffID.Burning) || player.HasBuff(BuffID.ShadowFlame))
			{
				player.ClearBuff(BuffID.OnFire);
				player.ClearBuff(BuffID.OnFire3);
				player.ClearBuff(BuffID.Burning);
				player.ClearBuff(BuffID.ShadowFlame);

				player.AddBuff(BuffType<SpectrumBlazeAffinity>(), 60 * 10);
			}
			if (Main.myPlayer == player.whoAmI)
			{
				if(StarsAbove.weaponActionKey.JustPressed)
                {
					if(player.statLife > 50)
                    {
						player.statLife -= 10;

					}
				}
				if (StarsAbove.weaponActionKey.Old && player.itemTime <= 0)
				{
					player.lifeRegenTime = 0;
					if (player.statLife > 50)
					{
						if (modPlayer.refractionGauge < 20)
						{
							Vector2 vector = new Vector2(
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
							Dust d = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.GemTopaz, 0, 0, 255,
								Color.White, 0.8f)];
							d.velocity = -vector / 12;
							d.velocity -= player.velocity / 8;
							d.noGravity = true;
						}
						if (modPlayer.refractionGauge >= 20 && modPlayer.refractionGauge < 90)
						{
							Vector2 vector = new Vector2(
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
							Dust d = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.GemDiamond, 0, 0, 255,
								new Color(51, 255,147), 0.8f)];
							d.velocity = -vector / 12;
							d.velocity -= player.velocity / 8;
							d.noGravity = true;
						}
						if (modPlayer.refractionGauge >= 90)
						{
							Vector2 vector = new Vector2(
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
					   Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
							Dust d = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.GemSapphire, 0, 0, 255,
								Color.White, 0.8f)];
							d.velocity = -vector / 12;
							d.velocity -= player.velocity / 8;
							d.noGravity = true;
						}
						

						player.AddBuff(BuffType<SpectrumAbsorption>(), 2);

						

					}
					

					lifeDrainTimer++;
					if (lifeDrainTimer > lifeDrainTimeMax)
					{
						if(lifeDrainTimeMax > 2)
                        {
							lifeDrainTimeMax--;
                        }
						lifeDrainTimer = 0;
						if (player.statLife > 50)
						{
							player.statLife -= 1;
							player.lifeRegen = 0;
						}
					}
				}
				else
				{
					lifeDrainTimeMax = 15;
					lifeDrainTimer = 0;
				}

			}

		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			var modPlayer = player.GetModPlayer<WeaponPlayer>();

			float launchSpeed = 12f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			if (player.altFunctionUse == 2)
			{
				
				float rotation = (float)Math.Atan2(player.Center.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), player.Center.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));
				SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, player.Center);

				float dustAmount = 36f;
				if (modPlayer.refractionGauge < 20)
				{
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 2f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
				}
				if (modPlayer.refractionGauge >= 20 && modPlayer.refractionGauge < 90)
				{
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemDiamond);
						Main.dust[dust].color = new Color(51, 255, 147);
						Main.dust[dust].scale = 2f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
				}
				if (modPlayer.refractionGauge >= 90)
				{
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
						int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemSapphire);
						Main.dust[dust].scale = 2f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = player.Center + spinningpoint5;
						Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
					}
				}
				
				player.velocity += new Vector2(arrowVelocity.X, arrowVelocity.Y);
				player.AddBuff(BuffType<Invincibility>(), 2);

				player.AddBuff(BuffType<SpectrumDashCooldown>(), 60);
			}
			else
			{
				if(!player.HasBuff(BuffType<SpectrumAbsorption>()))
				{
					
					Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(12));
					if(player.HasBuff(BuffType<SpectrumFrostAffinity>()))
                    {
						Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.NorthPoleSnowflake, damage, knockback, player.whoAmI);
					}

					if (!altAttack)
					{
						Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<BrilliantSpectrumPunch>(), damage, knockback, player.whoAmI);

						altAttack = true;
					}
					else
					{
						Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<BrilliantSpectrumPunchAlt>(), damage, knockback, player.whoAmI);

						altAttack = false;
					}
				}
				
				

			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.Banana)
				.AddIngredient(ItemID.SunStone)
				.AddIngredient(ItemType<EssenceOfKinetics>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
