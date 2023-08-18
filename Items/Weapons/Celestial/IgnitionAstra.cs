using Microsoft.Xna.Framework;
 
using StarsAbove.Items.Essences;
using Terraria;using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items.Weapons.Celestial
{
	public class IgnitionAstra : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Wielding this blade will suffuse your surroundings with the power of the Astral or Umbral Aspect" +
				"\nSwitch between aspects with right click (2 second cooldown)" +
				"\nDuring [c/5dc2e3:Astral Aspect], striking foes will restore a portion of mana" +
				"\nDuring [c/5dc2e3:Astral Aspect], Mana will be regenerated passively at an increased rate" +
				"\nDuring [c/5dc2e3:Astral Aspect], Attack will be halved, but defenses will be raised" +
				"\nDuring [c/b03fed:Umbral Aspect], every attack will swing the blade twice in a wide arc and additionally summon a myriad of astral projectiles to descend from the heavens" +
				"\nDuring [c/b03fed:Umbral Aspect], foes struck with the blade will be cursed with Starblight permanently until they are defeated" +
				"\nDuring [c/b03fed:Umbral Aspect], Mana will be continuously drained and Mana regeneration will be disabled, but attack will be increased" +
				"\n'The universe stands at the precipice of the end. At the center of it stands Ignition Astra'" +
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 740;
			}
			else
			{
				Item.damage = 294;
			}
			//The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>(); // Makes our item use our custom damage type.
			Item.damage = 294;
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<Projectiles.ignitionAstraSwing>();
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

		}

			
		int aspect = 0; //Astral Aspect = 0, Umbral Aspect = 1
		int cooldown = 0;
		int manaDrain = 0;
		int manaDrainTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (cooldown < 1)
				{
					if (aspect == 0)//Turning Astral into Umbral
					{
						cooldown = 120;
						aspect = 1;
						SoundEngine.PlaySound(StarsAboveAudio.SFX_Umbral, player.Center);
						for (int d = 0; d < 70; d++)
						{
							Dust.NewDust(player.position, player.width, player.height, 21, 0f, 0f, 150, default(Color), 1.5f);
						}
					}
					else//Turning Umbral into Astral
					{
						cooldown = 120;
						aspect = 0;
						SoundEngine.PlaySound(StarsAboveAudio.SFX_TeleportPrep, player.Center);
						for (int d = 0; d < 70; d++)
						{
							Dust.NewDust(player.position, player.width, player.height, 21, 0f, 0f, 150, default(Color), 1.5f);
						}
					}
				}
				
				
			}
			else 
			{
				
			}
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (aspect == 0)
			{

				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Main.LocalPlayer.Center;
				dust = Main.dust[Terraria.Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 133, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				dust.shader = GameShaders.Armor.GetSecondaryShader(65, Main.LocalPlayer);

				
			}
			else
			{
				if (Main.rand.NextBool(3))
				{
					Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);

				}
			}
		}
		public override void HoldItem(Player player)
		{
			manaDrainTimer++;
			cooldown--;
			if (cooldown < 0)
			{
				cooldown = 0;
			}
			if (aspect == 1) //Umbral Aspect
			{
				
				Item.useTime = 42;          
				Item.useAnimation = 42;
				if (player.statMana > 1)
				{
					Item.noUseGraphic = true;
					Item.noMelee = true;
				}
				else
				{
					Item.noUseGraphic = false;
					Item.noMelee = false;
				}
				player.manaRegenDelay = 2;
				if(manaDrainTimer >= 10)
                {
					player.statMana -= manaDrain;
					manaDrain++;
					manaDrainTimer = 0;
				}
				
				if (player.statMana < 0)
				{
					player.statMana = 0;

				}
				if (player.statMana == 0)
				{
					player.AddBuff(BuffType<Buffs.UmbralAspectDepleted>(), 1);
				}
				if (player.statMana > 1)
				{

					//Main.monolithType = 1;
					
					player.AddBuff(BuffType<Buffs.UmbralAspect>(), 1);
					for (int i = 0; i < 100; i++)
					{
						// Charging dust
						Vector2 vector = new Vector2(
							Main.rand.Next(-348, 348) * (0.003f * 40 - 10),
							Main.rand.Next(-348, 348) * (0.003f * 40 - 10));
						Dust d = Main.dust[Dust.NewDust(
							player.MountedCenter + vector, 1, 1,
							20, 0, 0, 255,
							new Color(0.8f, 0.4f, 1f), 1.5f)];
						d.velocity = -vector / 16;
						d.velocity -= player.velocity / 8;
						d.noLight = true;
						d.noGravity = true;

					}

				}

			}
			else //Astral Aspect
			{
				manaDrain = 0;
				manaDrainTimer = 0;
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.noUseGraphic = false;
				Item.noMelee = false;
				player.statMana += 1 / 2;
				player.AddBuff(BuffType<Buffs.AstralAspect>(), 1);
				//Main.monolithType = 2;
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				if (player.statMana >= 200)
				{
					
				}


			}
			

		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{

			if (aspect == 1)
			{
				for (int d = 0; d < 3; d++)
				{
					Dust.NewDust(target.position, target.width, target.height, 21, 0f, 0f, 150, default(Color), 1.5f);
				}
			}
			else
			{
				player.statMana += 10;
				for (int d = 0; d < 20; d++)
				{
					Dust.NewDust(target.position, target.width, target.height, 113, 0f, 0f, 150, default(Color), 1.5f);
				}
			}
		}

		public override bool OnPickup(Player player)
		{
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, 113, 0f, 0f, 150, default(Color), 1.5f);
			}
			return base.OnPickup(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (aspect == 1)
			{
				
				//yikes
				if (player.statMana > 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI, 0f);
					Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
					float behindTarget = target.Y + 200;
					
					
						position = player.Center + new Vector2((-(float)Main.rand.Next(0, 101) * player.direction), 0f);
						position.Y -= (0);
						Vector2 heading = target - position;
						if (heading.Y < 0f)
						{
							heading.Y *= -1f;
						}
						if (heading.Y < 20f)
						{
							heading.Y = 20f;
						}
						heading.Normalize();
						heading *= new Vector2(velocity.X, velocity.Y).Length();
						velocity.X = heading.X;
						velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
						type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);

					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);
					type = Main.rand.Next(new int[] { type, ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 600, 0 + Main.rand.Next(-20, 20), 0 + Main.rand.Next(1, 40), type, damage / 5, knockback, player.whoAmI, 0f);

					//The mother of all legacy code. Fix later? Who knows



				}
				return false;

			}
			return false;
		}
		public override void AddRecipes()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("CosmiliteBar").Type, 18)
					.AddIngredient(ItemType<EssenceOfTheCosmos>())
					.AddTile(TileID.Anvils)
					.Register();


			}
			else
			{

			
				CreateRecipe(1)
				.AddIngredient(ItemID.LunarBar, 40)
				.AddIngredient(ItemID.FragmentStardust, 20)
				.AddIngredient(ItemID.FragmentSolar, 20)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemID.FragmentNebula, 20)
				.AddIngredient(ItemType<EssenceOfTheCosmos>())
				.AddTile(TileID.Anvils)
				.Register();
			}


		}

	}
}
