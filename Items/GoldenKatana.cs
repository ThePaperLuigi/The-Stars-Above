using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.BurningDesire;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.BurningDesire;
using StarsAbove.Projectiles.CatalystMemory;
using StarsAbove.Projectiles.GoldenKatana;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class GoldenKatana : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurum Edge");
			
				Tooltip.SetDefault("" +
                "[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
				"\n[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from both Melee and Magic damage]" +
				"\nAdditionally, this weapon can be [c/F7D76A:Overcharged], causing additional effects to occur" +
                "\nUncharged attacks with this weapon are disabled" +
				"\nCharged attacks with this weapon vary depending on the amount of [c/F7D76A:Overcharge]" +
				"\n[c/F7D76A:Overcharging] will additionally drain your HP upon reaching [c/F7D76A:Overcharge] thresholds (Will not reduce health below 1)" +
                "\nCharged attacks swing the blade in a colossal arc, inflicting Frostburn for 4 seconds" +
				"\nSecond stage charge attacks consume 20 HP, but deal 2x base damage and will inflict Shadowflame for 4 seconds" +
				"\nAt maximum [c/F7D76A:Overcharge], consume an additional 30 HP but guarantees a critical strike and inflict [c/FF4DF4:Hyperburn] for 4 seconds" +
				"\n[c/FF4DF4:Hyperburn] deals powerful damage over time and ignores most resistances" +
				"" +
				$"");  //The (English) text shown below your weapon's name
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 90;

			//The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.AuricDamageClass>();
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 4;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 4;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton

			Item.shoot = 337;
			Item.shootSpeed = 8f;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			
			
		}

		int currentSwing;
		int slashDuration;
		int comboTimer;
		public override bool AltFunctionUse(Player player)
		{

			return true;
		}
        public override void UpdateInventory(Player player)
        {

			
			base.UpdateInventory(player);
        }
        public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if(!player.HasBuff(BuffType<CatalystMemoryPrismicCooldown>()))
                {
					return true;
                }
				else
                {
					return false;
                }
			}
			else
			{

			}
				return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<StarsAbovePlayer>().GoldenKatanaHeld = true;
			player.GetModPlayer<StarsAbovePlayer>().CatalystMemoryProgress += 2;//Increase animation progress when the weapon is held.
			
			if (player.ownedProjectileCounts[ProjectileType<GoldenKatanaSheath>()] < 1)
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<GoldenKatanaSheath>(), 0, 0, player.whoAmI, 0f);
			}
			//Weapon Action Key
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed && player.itemTime <= 0)
			{
				
			}

			
			if (Main.myPlayer == player.whoAmI)
			{
				float launchSpeed = 100f;
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;

				
				if (player.altFunctionUse != 2)
				{
					if (player.channel)
					{
						Item.useTime = 10;
						Item.useAnimation = 10;
						player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = true;
						player.GetModPlayer<StarsAbovePlayer>().bowCharge += 1.5f;
						if (player.ownedProjectileCounts[ProjectileType<GoldenKatanaHeld>()] < 1)
						{//Equip animation.
							int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<GoldenKatanaHeld>(), 0, 0, player.whoAmI, 0f);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 1)
						{
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 97.5)//First gauge full
						{
							for (int d = 0; d < 32; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Blue, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							
							SoundEngine.PlaySound(SoundID.Item116, player.position);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().overCharge1 == 97.5)//Second gauge full
						{
							for (int d = 0; d < 12; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.PurpleCrystalShard, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							for (int d = 0; d < 32; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
							CombatText.NewText(textPos, new Color(255, 53, 53, 240), $"20", false, false);
							player.statLife -= 20;
							if (player.statLife < 1)
							{
								player.statLife = 1;
							}
							SoundEngine.PlaySound(SoundID.Item117, player.position);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().overCharge2 == 97.5)//Third gauge full
						{
							for (int d = 0; d < 12; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.PinkCrystalShard, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							for (int d = 0; d < 12; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.PinkFairy, 0f + Main.rand.Next(-22, 22), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							for (int d = 0; d < 32; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}
							Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
							CombatText.NewText(textPos, new Color(255, 53, 53, 240), $"30", false, false);
							player.statLife -= 30;
							if(player.statLife < 1)
                            {
								player.statLife = 1;
                            }
							SoundEngine.PlaySound(SoundID.Item105, player.position);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 16.5)
						{
							//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverVFX1>(), 0, 3, player.whoAmI, 0f);
							SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);
							//Weapon draw sound?
							//
						}
						
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge < 100)
						{
							for (int i = 0; i < 30; i++)
							{//Circle
								Vector2 offset = new Vector2();
								double angle = Main.rand.NextDouble() * 2d * Math.PI;
								offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
								offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
								
								if(player.GetModPlayer<StarsAbovePlayer>().overCharge2 > 0)
                                {
									Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.FireworkFountain_Pink, player.velocity, 200, default(Color), 0.8f);
									d2.fadeIn = 0.1f;
									d2.noGravity = true;
								}
								else
                                {
									if (player.GetModPlayer<StarsAbovePlayer>().overCharge1 > 0)
									{
										Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.5f);
										d2.fadeIn = 0.1f;
										d2.noGravity = true;
									}
									else
                                    {
										Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Blue, player.velocity, 200, default(Color), 0.3f);
										d2.fadeIn = 0.1f;
										d2.noGravity = true;
									}
								}

								
							}
							//Charge dust
							Vector2 vector = new Vector2(
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10));

							Dust d4 = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.Clentaminator_Blue, 0, 0, 255,
								new Color(0.8f, 0.4f, 1f), 0.8f)];


							d4.velocity = -vector / 12;
							d4.velocity -= player.velocity / 8;
							d4.noLight = true;
							d4.noGravity = true;

						}
						else
						{//Charge dust
							Vector2 vector = new Vector2(
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10));

							Dust d4 = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.Clentaminator_Blue, 0, 0, 255,
								new Color(0.8f, 0.4f, 1f), 0.8f)];


							d4.velocity = -vector / 12;
							d4.velocity -= player.velocity / 8;
							d4.noLight = true;
							d4.noGravity = true;
							
							if (player.GetModPlayer<StarsAbovePlayer>().overCharge2 > 0)
                            {
								if(player.GetModPlayer<StarsAbovePlayer>().overCharge2 >= 100)
                                {
									Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.5f);
									Dust d = Main.dust[Dust.NewDust(
									player.MountedCenter + vector, 1, 1,
									DustID.FireworkFountain_Pink, 0, 0, 255,
									new Color(0.8f, 0.4f, 1f), 0.8f)];
									d.velocity = -vector / 12;
									d.velocity -= player.velocity / 8;
									d.noLight = true;
									d.noGravity = true;
								}
								else
                                {
									Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.5f);
									Dust d = Main.dust[Dust.NewDust(
									player.MountedCenter + vector, 1, 1,
									DustID.Clentaminator_Purple, 0, 0, 255,
									new Color(0.8f, 0.4f, 1f), 0.8f)];
									d.velocity = -vector / 12;
									d.velocity -= player.velocity / 8;
									d.noLight = true;
									d.noGravity = true;
								}

							}
							//If charge is above 100, start Overcharging
							player.GetModPlayer<StarsAbovePlayer>().overCharge1 += 1.5f;
							if (player.GetModPlayer<StarsAbovePlayer>().overCharge1 >= 100)
							{
								player.GetModPlayer<StarsAbovePlayer>().overCharge2 += 1.5f;
							}
						}
					}
					else
					{
						Item.useTime = 25;
						Item.useAnimation = 25;
						//SoundEngine.PlaySound(SoundID.Item1, player.position);
						if (player.GetModPlayer<StarsAbovePlayer>().overCharge2 >= 98)//If the weapon is fully overcharged 2
						{
							player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
							player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
							player.GetModPlayer<StarsAbovePlayer>().overCharge1 = 0;
							player.GetModPlayer<StarsAbovePlayer>().overCharge2 = 0;
							//Reset the charge gauge.
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, 
								ProjectileType<GoldenKatanaChargeAttack3>(), player.GetWeaponDamage(Item)*2, 3, player.whoAmI, 0f);
							SoundEngine.PlaySound(SoundID.Item1, player.position);
						}
						else //If the weapon is fully overcharged 1
						{

							if (player.GetModPlayer<StarsAbovePlayer>().overCharge1 >= 98)
							{
								player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
								player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
								player.GetModPlayer<StarsAbovePlayer>().overCharge1 = 0;
								player.GetModPlayer<StarsAbovePlayer>().overCharge2 = 0;
								//Reset the charge gauge.

								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y,
								ProjectileType<GoldenKatanaChargeAttack2>(), player.GetWeaponDamage(Item)*2, 3, player.whoAmI, 0f);
								SoundEngine.PlaySound(SoundID.Item1, player.position);
							}
							else //If the weapon is fully charged...
							{
								if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 98)
								{
									player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
									player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
									player.GetModPlayer<StarsAbovePlayer>().overCharge1 = 0;
									player.GetModPlayer<StarsAbovePlayer>().overCharge2 = 0;
									//Reset the charge gauge.

									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y,
								ProjectileType<GoldenKatanaChargeAttack1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
									SoundEngine.PlaySound(SoundID.Item1, player.position);
								}
								else //Uncharged attack (lower than the threshold.)
								{
									if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && player.GetModPlayer<StarsAbovePlayer>().bowCharge <= 30)
									{
									 //SoundEngine.PlaySound(SoundID.Item11, player.position);

										player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
										player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
										player.GetModPlayer<StarsAbovePlayer>().overCharge1 = 0;
										player.GetModPlayer<StarsAbovePlayer>().overCharge2 = 0;

										if (player.direction == 1)
										{
											//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<GoldenKatanaSwing1>(), 0, 3, player.whoAmI, 0f);


										}
										else
										{
											//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<GoldenKatanaSwing2>(), 0, 3, player.whoAmI, 0f);

										}
										//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlashEffect2>(), 0, 3, player.whoAmI, 0f);
										//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlashEffect1>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

									}
									else //If it was charged partway but prematurely stopped
									{
										player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
										player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
									}
								}
							}
						}
						
					}
				}

			}

		}

		public override bool? UseItem(Player player)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			/*if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<BoilingBloodBuff>()))
			{
				
			}*/


			return base.UseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 130f;
			
			

			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.GoldCoin, 15)
				.AddIngredient(ItemID.SoulofLight, 3)
				.AddIngredient(ItemID.PearlwoodSword, 1)
				.AddIngredient(ItemType<EssenceOfGold>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}
