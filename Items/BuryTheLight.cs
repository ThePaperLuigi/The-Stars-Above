using Microsoft.Xna.Framework;

using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Projectiles;
using System;
using System.Collections.Generic;
using StarsAbove.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class BuryTheLight : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 2355;
			}
			else
			{
				Item.damage = 320;
			}
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>(); // Makes our item use our custom damage type.

			

			Item.width = 28;          
			Item.height = 28;        
			Item.useTime = 14;        
			Item.useAnimation = 14;        
			Item.useStyle = ItemUseStyleID.Swing;         
			Item.knockBack = 5;         
			Item.value = Item.buyPrice(gold: 1);           
			Item.rare = ItemRarityID.Purple;
			Item.expert = true;
			Item.expertOnly = true;
			Item.autoReuse = true;         
			Item.crit = 0;
			Item.shoot = ProjectileType<BuryTheLightSlash>();
			Item.shootSpeed = 0.1f;
			Item.noMelee = true; 
			Item.noUseGraphic = true; 
			Item.autoReuse = true;

		}
		int judgementSlashCharge = 0;

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			int tooltip = tooltips.FindLastIndex(x => x.Mod.Equals("Terraria") && x.Name == "Tooltip11");
			if (ModLoader.TryGetMod("CalamityMod", out _))
			{
				if (tooltip != -1)
				{
					tooltips.Insert(++tooltip, new TooltipLine(Mod, $"{Mod.Name}:Tooltip12", LangHelper.GetTextValue($"ItemTooltip.{Name}.Calamity")));
				}
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<StarsAbovePlayer>().judgementGauge >= 100)
				{
					judgementSlashCharge = 100;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_BuryTheLightPrep, player.Center);

					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				if (judgementSlashCharge > 0 || player.GetModPlayer<StarsAbovePlayer>().judgementCutTimer > 0)
				{
					return false;
				}
			}
			return true;
		}

		public override bool? UseItem(Player player)
		{
			



			return true;
		}

		public override void HoldItem(Player player)
		{
			if (player.inventory[58].IsAir)
			{
				player.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility++;
				if (player.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility >= 8)
				{
					Vector2 vector = new Vector2(
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
					Dust d = Main.dust[Dust.NewDust(
						player.MountedCenter + vector, 1, 1,
						20, 0, 0, 255,
						new Color(0.8f, 0.4f, 1f), 0.8f)];
					d.velocity = -vector / 12;
					d.velocity -= player.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
					player.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility = 8;
				}

				if (judgementSlashCharge > 1)
				{
					player.GetModPlayer<StarsAbovePlayer>().judgementGauge -= 2;
					if (player.GetModPlayer<StarsAbovePlayer>().judgementGauge < 0)
					{
						player.GetModPlayer<StarsAbovePlayer>().judgementGauge = 0;
					}



					for (int i = 0; i < 80; i++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * (0 + judgementSlashCharge * 8));
						offset.Y += (float)(Math.Cos(angle) * (0 + judgementSlashCharge * 8));

						Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
						d2.fadeIn = 0.1f;
						d2.noGravity = true;
					}
					//Charge dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
					Dust d = Main.dust[Dust.NewDust(
						player.MountedCenter + vector, 1, 1,
						21, 0, 0, 255,
						new Color(0.8f, 0.4f, 1f), 0.8f)];
					d.velocity = -vector / 12;
					d.velocity -= player.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}
				judgementSlashCharge--;
				if (judgementSlashCharge == 1)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_iceCracking, player.Center);

					player.GetModPlayer<StarsAbovePlayer>().judgementCutTimer = 100;
					float launchSpeed = 12f;
					Vector2 mousePosition = Main.MouseWorld;
					Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
					Vector2 playerV = direction * launchSpeed;
					player.velocity = playerV;
					for (int d = 0; d < 40; d++)
					{
					}
				}
				if (player.GetModPlayer<StarsAbovePlayer>().judgementCutTimer > 60)
				{
					SoundEngine.PlaySound(SoundID.Item1, player.Center);
					player.immune = true;
					player.immuneTime = 120;
					Projectile.NewProjectile(null, player.Center.X + Main.rand.Next(-50, 50), player.Center.Y + Main.rand.Next(-50, 50), 0, 0, ProjectileType<BuryTheLightSlash2Pre>(), 0, 0, player.whoAmI);

				}
				if (player.GetModPlayer<StarsAbovePlayer>().judgementCutTimer == 1)
				{

					//player.GetModPlayer<StarsAbovePlayer>().activateShockwaveEffect = true;
					player.immune = false;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_LegendarySlash, player.Center);
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 35; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}

				}
				//
				player.GetArmorPenetration(DamageClass.Generic) += 100;
			}

			
			base.HoldItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 20);

			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				
				
			}
			else
			{
				Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
				SoundEngine.PlaySound(SoundID.Item1, Main.MouseWorld);
			}

			


			return false;
		}
		//[JITWhenModsEnabled("CalamityMod")]
		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("AuricBar").Type, 4)
					.AddIngredient(ItemType<TotemOfLightEmpowered>())
					.AddIngredient(ItemType<EssenceOfTheBeginningAndEnd>())
					.AddTile(TileID.Anvils)
					.Register();
			}
			else
			{
				CreateRecipe(1)
					.AddIngredient(ItemType<TotemOfLightEmpowered>())
					.AddIngredient(ItemType<EssenceOfTheBeginningAndEnd>())
					.AddTile(TileID.Anvils)
					.Register();
				
			}
			

		}
	}
}
