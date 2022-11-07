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

namespace StarsAbove.Items
{
    public class SoulReaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Reaver");
			Tooltip.SetDefault("" +
				"[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +//Charge attack sentence
				"Holding this weapon enchants your cursor, causing nearby foes to become inflicted with Shadowflame for half a second" +
                "\nAttacks with this weapon swing in a wide arc and will strike multiple times" +
				"\nCharging this weapon will execute [Soul Harvest], teleporting you to your cursor and dealing potent damage to foes around you" +
                "\n[Soul Harvest] will deal 50% increased damage to foes inflicted with Shadowflame" +
                "\nGain Invincibility for 1 second after use of [Soul Harvest], but become inflicted with Vulnerability for 3 seconds as well, halving defenses" +
                "\nAfter [Soul Harvest] you will be inflicted with [Soul Split] for 10 seconds, reducing the damage of [Soul Harvest] by 2/3" +
                "\nCritical strikes with [Soul Harvest] will instead reduce the duration of [Soul Split] to 2 seconds" +
				"\nDefeating foes will grant a stack of [Dark Soul], granting 2% increased damage per stack (capping at 10 stacks)" +
                "\nLose all stacks of [Dark Soul] after leaving combat" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 65;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		
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
				if(!player.HasBuff(BuffType<Buffs.BloodBlade.BladeArtDragonPrepBuff>()) && !player.HasBuff(BuffType<Buffs.BloodBlade.BladeArtDragonCooldown>()) && player.statLife > (int)(player.statLifeMax*0.2) + 1)
                {
					player.AddBuff(BuffType<BladeArtDragonCooldown>(), 1200);
					player.AddBuff(BuffType<Invincibility>(), 50);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<BloodArtPrep>(), player.GetWeaponDamage(Item)*4, 3, player.whoAmI, 0f);
					player.statLife -= (int)(player.statLifeMax * 0.2);

					
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

			if (Main.myPlayer == player.whoAmI)
			{
				float launchSpeed = 4f + (int)Math.Round(player.GetModPlayer<StarsAbovePlayer>().bowCharge / 30);
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
							Dust.NewDust(player.Center, 0, 0, 235, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}
						
						
						SoundEngine.PlaySound(SoundID.Item113, player.position);
					}
					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 16)
					{
						//Weapon animation.
						//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TheOnlyThingIKnowForRealSheathe>(), 0, 3, player.whoAmI, 0f);

						SoundEngine.PlaySound(SoundID.Item1, player.position);
					}
					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge < 100)
					{
						for (int i = 0; i < 30; i++)
						{//Circle
							Vector2 offset = new Vector2();
							double angle = Main.rand.NextDouble() * 2d * Math.PI;
							offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
							offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));

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

					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 98)//If the weapon is fully charged...
					{
						
						player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
						player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
						//Reset the charge gauge.

						//Grant Invincibility, because you'll be most likely teleporting into the enemy's hitbox.
						player.AddBuff(BuffType<Invincibility>(), 60);

						//Play the weapon swing sound.
						SoundEngine.PlaySound(SoundID.Item1, player.position);

						//Draw fireworks from the player's position to the teleport position.
						if (Vector2.Distance(Main.MouseWorld, player.Center) <= 600f)
						{
							for (int i = 0; i < 100; i++)
							{
								Vector2 position = Vector2.Lerp(player.Center, Main.MouseWorld, (float)i / 100);
								Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Pink, null, 240, default(Color), 0.9f);
								d.fadeIn = 0.3f;
								d.noLight = true;
								d.noGravity = true;

							}
							//Teleport the player to the teleport position (this would be the mouse)
							player.Teleport(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 10), 1, 0);
							//Tell the server you're teleporting
							NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y - 10, 1, 0, 0);
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

						//Screen shake
						player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

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

							

						}
						else
						{
							player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
							player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
						}
					}
				}


			}

			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			if(player.altFunctionUse != 2)
            {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<SoulReaverSlashEffect2>(), 0, 3, player.whoAmI, 0f);;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<SoulReaverSlashEffect1>(), damage, 3, player.whoAmI, 0f);
			}
			
			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
