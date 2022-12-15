using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.BloodBlade;
using StarsAbove.Projectiles.BloodBlade;
using System;
using Terraria.Audio;
using StarsAbove.Projectiles.SoulReaver;
using StarsAbove.Buffs.SoulReaver;
using StarsAbove.Projectiles;

namespace StarsAbove.Items
{
    public class Manifestation : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Manifestation");
			Tooltip.SetDefault("" +
				"Attacks with this weapon sweep in a huge arc; critical strikes inflict Bleed for 8 seconds" +//Charge attack sentence
				"\nTaking or dealing damage charges the [Emotion Gauge]" +
                "\nBelow 200 HP, taking or dealing damage will charge the [Emotion Gauge] at doubled efficiency" +
				"\nOnce the [Emotion Gauge] is full, automatically gain the buff [Manifestation of E.G.O.] and drain the Emotion Gauge by half" +
				"\n[Manifestation of E.G.O.] grants 50% increased damage and 20% increased movement speed, but reduces defense by 30" +
				"\nIf Manifestation of E.G.O. is active, half of the Emotion Gauge can be expended to perform unique attacks" +
                "\nExecute [Great Split: Vertical] with right click or [Great Split: Horizontal] with the Weapon Action Key (Both attacks share a 1 minute cooldown)" +
				"\n[Great Split: Vertical] will teleport to the nearest foe and deal 5x guaranteed critical damage after a short delay" +
                "\nIf the foe is defeated with this attack, immediately execute the attack again on the next nearest foe while granting the buff [Onrush], becoming Invincible for 1 second" +
				"\n[Great Split: Horizontal] will deal guaranteed critical damage to all foes in a colossal radius around you after a short delay" +
                "\nBoth [Greater Split] attacks will execute any non-boss enemy below 30% HP" +
                "\nIf [Manifestation of E.G.O.] is active, the [Emotion Gauge] will passively decrease over time" +
                "\nIf the [Emotion Gauge] is below 10% for 5 seconds, [Manifestation of E.G.O.] will end and you will be inflicted with Vulnerability for 10 seconds" +
				"\n'Let's start this for real- I'll crush you all...'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 211;           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 25f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.crit = 20;
		}
		int currentSwing;
		int slashDuration;

		int soulHarvestRange = 700;
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			
			float launchSpeed = 110f;
			float launchSpeed2 = 10f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * 26f;
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (player.altFunctionUse == 2)
			{
				
				
			}
			else
            {
				
            }
			
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			
		}
        public override bool? UseItem(Player player)
        {


			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<StarsAbovePlayer>().SoulReaverHeld = true;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];



				if (npc.active && npc.CanBeChasedBy() && npc.Distance(Main.MouseWorld) < 30)
				{
					npc.AddBuff(BuffID.ShadowFlame, 10);

					for (int r = 0; r < 10; r++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * (20));
						offset.Y += (float)(Math.Cos(angle) * (20));

						Dust d2 = Dust.NewDustPerfect(Main.MouseWorld + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.4f);
						d2.fadeIn = 0.1f;
						d2.noGravity = true;
					}
				}

				



			}
			//If player has soul split, range of Soul Harvest is decreased drastically
			if (player.HasBuff(BuffType<SoulSplit>()))
            {
				soulHarvestRange = 400;
            }
			else
            {
				soulHarvestRange = 700;
            }
			if(player.GetModPlayer<StarsAbovePlayer>().SoulReaverSouls > 0)
            {
				player.AddBuff(BuffType<DarkSoul>(), 10);
            }
			
			if (Main.myPlayer == player.whoAmI)
			{
				float launchSpeed = 64f;
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;

				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * (soulHarvestRange));
					offset.Y += (float)(Math.Cos(angle) * (soulHarvestRange));

					Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.5f);
					d2.fadeIn = 0.1f;
					d2.noGravity = true;
				}

				if (player.altFunctionUse != 2)
				{
					if (player.channel)
					{
						Item.useTime = 10;
						Item.useAnimation = 10;
						player.AddBuff(BuffType<SoulReaverCharging>(), 10);
						player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = true;
						player.GetModPlayer<StarsAbovePlayer>().bowCharge += 2;
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 1)
						{
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 98)
						{
							for (int d = 0; d < 32; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
							}



						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 16)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverVFX1>(), 0, 3, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverVFX2>(), 0, 3, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverVFX3>(), 0, 3, player.whoAmI, 0f);

							SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);
						}
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge < 100)
						{
							for (int i = 0; i < 30; i++)
							{//Circle
								Vector2 offset = new Vector2();
								double angle = Main.rand.NextDouble() * 2d * Math.PI;
								offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
								offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));

								Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.5f);
								d2.fadeIn = 0.1f;
								d2.noGravity = true;
							}
							//Charge dust
							Vector2 vector = new Vector2(
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
								Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
							Dust d = Main.dust[Dust.NewDust(
								player.MountedCenter + vector, 1, 1,
								DustID.Clentaminator_Purple, 0, 0, 255,
								new Color(0.8f, 0.4f, 1f), 0.8f)];
							d.velocity = -vector / 12;
							d.velocity -= player.velocity / 8;
							d.noLight = true;
							d.noGravity = true;

						}
						else
						{
							Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
						}
					}
					else
					{
						Item.useTime = 25;
						Item.useAnimation = 25;

						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 98)//If the weapon is fully charged...
						{

							player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
							player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
							//Reset the charge gauge.





							//If you're in range
							if (Vector2.Distance(Main.MouseWorld, player.Center) <= soulHarvestRange)
							{
								SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, player.position);
								//Grant Invincibility, because you'll be most likely teleporting into the enemy's hitbox.
								player.AddBuff(BuffType<Invincibility>(), 10);
								player.AddBuff(BuffType<Vulnerable>(), 180);


								//Play the weapon swing sound.
								SoundEngine.PlaySound(SoundID.Item1, player.position);
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverPortal>(), 0, 3, player.whoAmI, 0f);

								for (int i = 0; i < 100; i++)
								{
									Vector2 position = Vector2.Lerp(player.Center, Main.MouseWorld, (float)i / 100);
									Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Pink, null, 240, default(Color), 0.4f);
									d.fadeIn = 0.3f;
									d.noLight = true;
									d.noGravity = true;

								}
								//Teleport the player to the teleport position (this would be the mouse)
								player.Teleport(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 10), 1, 0);
								//Tell the server you're teleporting
								NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y - 10, 1, 0, 0);
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulReaverPortal>(), 0, 3, player.whoAmI, 0f);
								//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<fastRadiate>(), 0, 3, player.whoAmI, 0f);
								if (player.HasBuff(BuffType<SoulSplit>()))
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulHarvest>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

								}
								else
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<SoulHarvest>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

								}
								player.AddBuff(BuffType<SoulSplit>(), 600);
								//Screen shake
								player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
							}

							//Use a different projectile/deal less or more damage depending on the debuff

							/*if (player.HasBuff(BuffType<ImpactRecoil>()))
							{
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSpin>(), (player.GetWeaponDamage(Item) / 2) / 2, 3, player.whoAmI, 0f);

							}
							else
							{
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSpin>(), (player.GetWeaponDamage(Item) / 2), 3, player.whoAmI, 0f);

							}
							*/



							//Add a cooldown debuff
							//player.AddBuff(BuffType<ImpactRecoil>(), 720);

						}
						else
						{
							if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && player.GetModPlayer<StarsAbovePlayer>().bowCharge <= 30)
							{//Uncharged attack (lower than the threshold.)
							 //SoundEngine.PlaySound(SoundID.Item11, player.position);

								player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
								player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
								SoundEngine.PlaySound(SoundID.Item1, player.position);

								if (player.direction == 1)
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlash1>(), 0, 3, player.whoAmI, 0f);


								}
								else
								{
									Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlash2>(), 0, 3, player.whoAmI, 0f);

								}
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlashEffect2>(), 0, 3, player.whoAmI, 0f);
								Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SoulReaverSlashEffect1>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

							}
							else
							{
								player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
								player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
							}
						}
					}
				}

			}

			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.direction == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y, ProjectileType<SoulReaverSlash1>(), 0, 3, player.whoAmI, 0f);


				}
				else
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y, ProjectileType<SoulReaverSlash2>(), 0, 3, player.whoAmI, 0f);

				}
				Projectile.NewProjectile(source, player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y, ProjectileType<SoulReaverRangedSlash>(), damage/2, knockback, player.whoAmI, 0f); ;
				SoundEngine.PlaySound(SoundID.Item1, player.position);

			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ectoplasm, 8)
				.AddIngredient(ItemID.Bone, 12)
				.AddIngredient(ItemID.SoulofMight, 8)
				.AddIngredient(ItemID.DeathSickle, 1)
				.AddIngredient(ItemID.SoulofNight, 20)
				.AddIngredient(ItemType<EssenceOfSouls>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
