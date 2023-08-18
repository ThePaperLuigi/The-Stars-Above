using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.AshenAmbition;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.AshenAmbition;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items.Weapons.Melee
{
    public class AshenAmbition : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("" +
                "Attacks pierce foes and have no knockback" +
				"\nRight click to disappear, activating [c/926AD9:Ashen Execution] and saving your cursor's position (1 minute cooldown)" +
				"\nAfter a short delay, teleport to your cursor and deal damage around you, instantly executing foes below this weapon's base damage * 5" +
                "\nFoes below the execution threshold will glow" +
				"\nDefeating foes in this manner instantly refreshes the cooldown of [c/926AD9:Ashen Execution] (2 second minimum) and grants one stack of [c/812ABA:Call of the Void]" +
				"\nEach stack of [c/812ABA:Call of the Void] increases movement speed by 5%" +
				"\nAt 5 stacks of [c/812ABA:Call of the Void] all stacks are reset and [c/926AD9:Ashen Execution] has a 10 second cooldown" +
				"\nAdditionally, gain [c/FF7EF3:Ashen Strength] for 2 seconds, drastically restoring HP and increasing damage by 20%" +
				"\nFailing to execute an enemy resets all stacks of [c/812ABA:Call of the Void]" +
                "\n'Put these foolish ambitions to rest'" +
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 22;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = 3;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<AshenAmbitionSpear>();
			Item.shootSpeed = 40f;
			Item.noUseGraphic = true;
			Item.noMelee = true;
		}
		Vector2 vector32;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<AshenAmbitionCooldown>()))
				{
					player.AddBuff(BuffType<AshenAmbitionCooldown>(), 3600);
					if (!player.HasBuff(BuffType<CallOfTheVoid1>()) && !player.HasBuff(BuffType<CallOfTheVoid2>()) && !player.HasBuff(BuffType<CallOfTheVoid3>()) && !player.HasBuff(BuffType<CallOfTheVoid4>()))
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_AshenExecute, player.Center);

					}
					if (player.HasBuff(BuffType<CallOfTheVoid1>()))
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_AshenExecute1, player.Center);
					}
					if (player.HasBuff(BuffType<CallOfTheVoid2>()))
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_AshenExecute2, player.Center);
					}
					if (player.HasBuff(BuffType<CallOfTheVoid3>()))
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_AshenExecute3, player.Center);

					}
					if (player.HasBuff(BuffType<CallOfTheVoid4>()))
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_AshenExecute4, player.Center);

					}
					player.AddBuff(BuffType<AshenAmbitionPrep>(), 30);
					player.AddBuff(BuffType<Invincibility>(), 120);
					player.AddBuff(BuffType<Invisibility>(), 30);

					player.GetModPlayer<WeaponPlayer>().AshenAmbitionOldPosition = player.position;
					for (int g = 0; g < 4; g++)
					{
						int goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					}
					vector32.X = (float)Main.mouseX + Main.screenPosition.X;
					if (player.gravDir == 1f)
					{
						vector32.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
					}
					else
					{
						vector32.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
					}
					vector32.X -= (float)(player.width / 2);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),vector32.X+5, vector32.Y + 40, 0, 0, ProjectileType<AshenAmbitionMarker>(), 0, 0, player.whoAmI);

					return false;
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
			player.GetModPlayer<WeaponPlayer>().AshenAmbitionHeld = true;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, player.Center);


				if (
					npc.active 
					&& npc.Distance(player.Center) < 1000f 
					&& npc.CanBeChasedBy() 
					&& npc.life <= player.GetModPlayer<WeaponPlayer>().AshenAmbitionExecuteThreshold 
					&& !player.HasBuff(BuffType<AshenAmbitionCooldown>())
					)
				{
					for (int i1 = 0; i1 < 12; i1++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * 80);
						offset.Y += (float)(Math.Cos(angle) * 80);

						Dust d = Dust.NewDustPerfect(npc.Center + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
						d.fadeIn = 0.1f;
						d.noGravity = true;
					}
				}




			}
			if(player.HasBuff(BuffType<AshenAmbitionActive>()))
            {
				int index = player.FindBuffIndex(BuffType<AshenAmbitionActive>());
				if (index > -1)
				{
					player.DelBuff(index);
				}
				player.AddBuff(BuffType<AshenAmbitionEnd>(), 20);
				
				
				if (Main.myPlayer == player.whoAmI)
				{

					
						player.Teleport(vector32, 1, 0);
						NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);

					
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, ProjectileType<AshenAmbitionExecute>(), player.GetModPlayer<WeaponPlayer>().AshenAmbitionExecuteThreshold, 0, player.whoAmI);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_LegendarySlash, player.Center);
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(20, 40), 0f + Main.rand.Next(20, 40), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(-40, -20), 0f + Main.rand.Next(-40, -20), 150, default(Color), 1.5f);

					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(-40, -20), 0f + Main.rand.Next(20, 40), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(20, 40), 0f + Main.rand.Next(-40, -20), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 35; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}

				}
				
			}
			


			base.HoldItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		// Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
		// See Source code for Star Wrath projectile to see how it passes through tiles.
		/*	The following changes to SetDefaults */

		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float behindTarget = target.Y + 40;
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 101) * player.direction), 0f);
				position.Y -= (0 * i);
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
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 200, velocity.X, velocity.Y,type, damage * 2, knockback, player.whoAmI, 0f);
			}
			return false;*/
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.GetModPlayer<WeaponPlayer>().AshenAmbitionExecuteThreshold = damage * 5;

			if (player.altFunctionUse == 2)
			{


			}
			else
			{
				
				
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI);

				
				//Main.PlaySound(SoundID.Item1, Main.MouseWorld);
			}
			
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 6)
				.AddIngredient(ItemID.CrimtaneBar, 12)
				.AddIngredient(ItemType<EssenceOfAsh>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 6)
				.AddIngredient(ItemID.DemoniteBar, 12)
				.AddIngredient(ItemType<EssenceOfAsh>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
