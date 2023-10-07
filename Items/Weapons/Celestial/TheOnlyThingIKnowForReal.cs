using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Buffs.TheOnlyThingIKnowForReal;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Celestial.TheOnlyThingIKnowForReal;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class TheOnlyThingIKnowForReal : ModItem //MASTER MODE EXCLUSIVE
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("" +
				"[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
                "\n[c/F7D76A:Hold left click to charge the weapon; the longer the weapon is charged, the stronger the resulting attack will become]" +
				"\nUncharged attacks with this weapon swing in the cursor's direction" +
				"\nStriking foes with the tip of the blade deals damage twice" +
				"\nCharged attacks will execute [c/8D0404:Guntrigger Execution], teleporting to your cursor (in range), granting Invincibility for 2 seconds, and slashing foes around you" +
                "\nWhen charging the weapon, movement is disabled" +
				"\nAfter use of [c/8D0404:Guntrigger Execution], you are inflicted with the debuff [c/B96B6B:Impact Recoil] for 12 seconds" +
				"\n[c/B96B6B:Impact Recoil] halves the damage of [c/8D0404:Guntrigger Execution] and removes Invincibility upon activation" +
				"\nTiming the charged attack perfectly will deal 50% increased damage" +
				"\nRight click to activate [c/FF7B7B:Guntrigger Parry] for 1 second (12 second cooldown)" +
                "\nDuring this time, all damage sustained is negated and reflected to foes" +
				"\nSuccessfully parrying an attack immediately removes [c/B96B6B:Impact Recoil] and grants the buff [c/EC2B65:Jetstream Bloodshed]" +
				"\n[c/EC2B65:Jetstream Bloodshed] grants 30% increased damage and lasts until damage is taken or the weapon is no longer held" +
				"\nAt 100 HP or below, [c/FF7B7B:Guntrigger Parry] cooldown is doubled, but successful parries remove the cooldown" +
				"\nAdditionally, [c/EC2B65:Jetstream Bloodshed]'s damage buff is increased to 50% and natural life regeneration is disabled" +
				"\n'Let's dance'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 310;
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 64;
			Item.height = 32;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;

			Item.rare = ItemRarityID.Purple;
			Item.master = false;
			Item.masterOnly = false;
			//item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.channel = true;//Important for all "bows"
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<TheOnlyThingIKnowForRealSlash>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

		}
		 
		bool altSwing;
		bool meleeStance = false;
		int meleeStanceTimer = 0;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<GuntriggerParryCooldown>()) && !player.HasBuff(BuffType<GuntriggerParry>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<GuntriggerParryProjectile>(), 0, 3, player.whoAmI, 0f);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_GuntriggerParryPrep, player.Center);

					player.AddBuff(BuffType<GuntriggerParry>(), 60);
					if(player.statLife > 100)
                    {
						player.AddBuff(BuffType<GuntriggerParryCooldown>(), 720);

					}
					else
                    {
						player.AddBuff(BuffType<GuntriggerParryCooldown>(), 1440);

					}

				}
				return false;

			}
			if (Main.myPlayer == player.whoAmI)
			{



			}
			return true;

		}
		public override void HoldItem(Player player)
		{
			//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{MathHelper.ToDegrees((float)Math.Atan2(player.Center.Y, Main.MouseWorld.X))}"), 241, 255, 180);}\
			if(player.HasBuff<JetstreamBloodshed>())
            {
				player.AddBuff(BuffType<JetstreamBloodshed>(), 10);
            }
			if(player.HasBuff<GuntriggerParry>() || player.HasBuff<JetstreamBloodshed>())
            {
				Vector2 vector = new Vector2(
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
				Dust d = Main.dust[Dust.NewDust(
					player.MountedCenter + vector, 1, 1,
					235, 0, 0, 255,
					new Color(0.8f, 0.4f, 1f), 0.8f)];
				d.velocity = -vector / 12;
				d.velocity -= player.velocity / 8;
				d.noLight = true;
				d.noGravity = true;
			}

			if (Main.myPlayer == player.whoAmI)
			{
				float launchSpeed = 4f + (int)Math.Round(player.GetModPlayer<WeaponPlayer>().bowCharge / 30);
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;

				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * (600));
					offset.Y += (float)(Math.Cos(angle) * (600));

					Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 235, player.velocity, 200, default(Color), 0.5f);
					d2.fadeIn = 0.1f;
					d2.noGravity = true;
				}


				if (player.channel)
				{
					Item.useTime = 2;
					Item.useAnimation = 2;
					player.GetModPlayer<WeaponPlayer>().bowChargeActive = true;
					player.GetModPlayer<WeaponPlayer>().bowCharge += 2;
					if (player.GetModPlayer<WeaponPlayer>().bowCharge == 1)
					{
						//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
					}
					if (player.GetModPlayer<WeaponPlayer>().bowCharge == 98)
					{
						for (int d = 0; d < 32; d++)
						{
							Dust.NewDust(player.Center, 0, 0, 235, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}
						player.AddBuff(BuffType<PerfectGuntrigger>(), 14);
						player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
						SoundEngine.PlaySound(SoundID.Item113, player.position);
					}
					if (player.GetModPlayer<WeaponPlayer>().bowCharge == 16)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSheathe>(), 0, 3, player.whoAmI, 0f);

						SoundEngine.PlaySound(SoundID.Item1, player.position);
					}
					if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 16)
					{
						player.velocity = Vector2.Zero;
					}
					if (player.GetModPlayer<WeaponPlayer>().bowCharge == 40 || player.GetModPlayer<WeaponPlayer>().bowCharge == 64)
					{

						SoundEngine.PlaySound(SoundID.Item1, player.position);
					}
					if (player.GetModPlayer<WeaponPlayer>().bowCharge < 100)
					{
						for (int i = 0; i < 30; i++)
						{//Circle
							Vector2 offset = new Vector2();
							double angle = Main.rand.NextDouble() * 2d * Math.PI;
							offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
							offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

							Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 235, player.velocity, 200, default(Color), 0.5f);
							d2.fadeIn = 0.1f;
							d2.noGravity = true;
						}
						//Charge dust
						Vector2 vector = new Vector2(
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
						Dust d = Main.dust[Dust.NewDust(
							player.MountedCenter + vector, 1, 1,
							235, 0, 0, 255,
							new Color(0.8f, 0.4f, 1f), 0.8f)];
						d.velocity = -vector / 12;
						d.velocity -= player.velocity / 8;
						d.noLight = true;
						d.noGravity = true;

					}
					else
					{
						Dust.NewDust(player.Center, 0, 0, 235, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
					}
				}
				else
				{
					Item.useTime = 25;
					Item.useAnimation = 25;

					if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 98)
					{
						//SoundEngine.PlaySound(SoundID.Item11, player.position);
						//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<BigShot>(), player.GetWeaponDamage(Item) + 30, 5, player.whoAmI, 0f);
						player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
						player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
						
						if (player.HasBuff(BuffType<ImpactRecoil>()))
						{

						}
						else
						{
							player.AddBuff(BuffType<Invincibility>(), 120);

						}
						SoundEngine.PlaySound(SoundID.Item1, player.position);
						if(Vector2.Distance(Main.MouseWorld, player.Center) <= 600f)
                        {
							for (int i = 0; i < 100; i++)
							{
								Vector2 position = Vector2.Lerp(player.Center, Main.MouseWorld, (float)i / 100);
								Dust d = Dust.NewDustPerfect(position, 235, null, 240, default(Color), 0.9f);
								d.fadeIn = 0.3f;
								d.noLight = true;
								d.noGravity = true;
								
							}
							player.Teleport(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 10), 1, 0);
							NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y-10, 1, 0, 0);
						}
						
						if(player.HasBuff(BuffType<ImpactRecoil>()))
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSpin>(), (player.GetWeaponDamage(Item)/2)/2, 3, player.whoAmI, 0f);

						}
						else
                        {
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSpin>(), (player.GetWeaponDamage(Item) / 2), 3, player.whoAmI, 0f);
							//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSpin2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						}
						player.AddBuff(BuffType<ImpactRecoil>(), 720);

					}
					else
					{
						if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0 && player.GetModPlayer<WeaponPlayer>().bowCharge <= 30)
						{//
						 //SoundEngine.PlaySound(SoundID.Item11, player.position);

							player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
							player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
							SoundEngine.PlaySound(SoundID.Item1, player.position);

							if (player.direction == 1)
                            {
								if(altSwing)
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlashVFX2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

									altSwing = false;
                                }
								else
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlash>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlashVFX>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

									altSwing = true;
                                }

							}
							else
                            {
								if (altSwing)
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlash>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlashVFX>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

									altSwing = false;
								}
								else
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSlashVFX2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

									altSwing = true;
								}
							}

						}
						else
                        {
							player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
							player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
						}
					}
				}


			}
		}



		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 


			if (player.channel)
			{

			}
			else
			{

			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 22)
				.AddIngredient(ItemID.SoulofMight, 15)
				.AddIngredient(ItemID.RedPhasesaber, 1)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemType<EssenceOfBloodshed>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
