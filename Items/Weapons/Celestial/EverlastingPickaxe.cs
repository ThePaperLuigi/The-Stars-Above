using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Celestial.EverlastingPickaxe;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Celestial.EverlastingPickaxe;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class EverlastingPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Everlasting Pickaxe");
			/* Tooltip.SetDefault("[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
				"\nRight click to place a dusting of [c/82D1D5:Everlasting Gunpowder] on yourself, which lasts for 6 seconds (20 second cooldown)" +
				"\nSwinging the pickaxe near the [c/82D1D5:Everlasting Gunpowder] causes consecutive explosions towards your cursor, destroying tiles" +
				"\nPress the Weapon Action Key to load [c/82D1D5:Everlasting Gunpowder] into the pickaxe" +
				"\nWhen [c/82D1D5:Everlasting Gunpowder] is loaded, mining is disabled but attacking capabilities are extended and damage is increased by 80%" +
				"\nAttacks swing in an alternating arc, firing a portion of [c/82D1D5:Everlasting Gunpowder] towards the cursor (4 second cooldown)" +
				"\nOnce the [c/82D1D5:Everlasting Gunpowder] makes contact with an enemy, it will detonate multiple times before disappearing" +
				"\nIf the [c/82D1D5:Everlasting Gunpowder] lands on a tile, it will explode once and then disappear" +
				"\nPressing the Weapon Action Key when [c/82D1D5:Everlasting Gunpowder] is loaded will revert to the original state" +
				"\n'At the Netherworld's bottom... I'll be waiting'" +
				$""); */


			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 200;
			}
			else
			{
				Item.damage = 99;
			}
			
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 100;
			Item.height = 100;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			
			Item.pick = 250;
			Item.tileBoost += 5;
			


			

		}
		bool altSwing;
		int currentSwing;
		int slashDuration;
		int comboTimer;

		bool attackModeEnabled;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void UpdateInventory(Player player)
        {
			if (attackModeEnabled)//Disable mining when in attack mode.
			{
				Item.useTime = 25;
				Item.useAnimation = 25;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.shootSpeed = 4f;
				Item.pick = 0;
				Item.noMelee = true;
				Item.noUseGraphic = true;
				Item.useTurn = false;
				Item.shoot = 10;//Doesn't matter, replaced later.
				Item.shootSpeed = 11f;

			}
			else
			{
				Item.useTime = 4;
				Item.useAnimation = 12;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shootSpeed = 4f;
				Item.pick = 250;
				Item.noMelee = false;
				Item.noUseGraphic = false;
				Item.useTurn = true;
				Item.shoot = 0;
				Item.shootSpeed = 11f;
			}

		}
        public override void HoldItem(Player player)
        {
			if (attackModeEnabled)
			{
				player.AddBuff(BuffType<EverlastingGunpowderLoaded>(), 10);

				

			}
			else
			{
				
			}
			//When the Weapon Action Key is pressed...
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<GunpowderSwapCooldown>()))
			{
				//1. Swap stance (This disables mining, increases use time, and hides the weapon when swung.)
				player.AddBuff(BuffType<GunpowderSwapCooldown>(), 60);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtLoad, player.Center);

				if (attackModeEnabled)
				{
					attackModeEnabled = false;
					//Smoke vfx
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
				}
				else
				{
					attackModeEnabled = true;
					//Dust vfx
					for (int d = 0; d < 14; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.8f);
						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.6f);

					}
				}

			}

			


		}



		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}
        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2 && attackModeEnabled)//When the weapon is in attack mode, disable right-click
			{
				return false;
			}

			return true;
        }
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2)//Spawn the Everlasting Gunpowder
			{
				if(!attackModeEnabled)
                {
					if (!player.HasBuff(BuffType<EverlastingGunpowderMiningCooldown>()))
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MiningModeGunpowder>(), 0, 0, player.whoAmI);
						player.AddBuff(BuffType<EverlastingGunpowderMiningCooldown>(), 1200);//20 second CD
					}
				}
				
			}
			else
            {
				if (!attackModeEnabled)
				{
					player.AddBuff(BuffType<EverlastingGunpowderMiningTrigger>(), 20);//20 second CD
					
				}
			}
			

			return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			

			if (attackModeEnabled)
			{
				if (player.altFunctionUse != 2)
				{

					if (player.direction == 1)
					{
						if (altSwing)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing2>(), damage, knockback, player.whoAmI, 0f);

							altSwing = false;
						}
						else
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing1>(), damage, knockback, player.whoAmI, 0f);

							altSwing = true;
						}

					}
					else
					{
						if (altSwing)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing1>(), damage, knockback, player.whoAmI, 0f);

							altSwing = false;
						}
						else
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing2>(), damage, knockback, player.whoAmI, 0f);

							altSwing = true;
						}
					}
					if(!player.HasBuff(BuffType<EverlastingGunpowderAttackCooldown>()))
                    {
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<AttackModeGunpowder>(), damage, knockback, player.whoAmI);

						player.AddBuff(BuffType<EverlastingGunpowderAttackCooldown>(), 240);
                    }


				}

			}
			else
            {
				//Items that can mine also apparently don't work with projectiles attributed to them.
			}
				
			


			return false;
		}

		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("BlossomPickaxe").Type, 1)
					.AddIngredient(ItemID.IronPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("BlossomPickaxe").Type, 1)
					.AddIngredient(ItemID.LeadPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

			}
			else
			{
				CreateRecipe(1)
					.AddIngredient(ItemID.IronPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();

				CreateRecipe(1)
					.AddIngredient(ItemID.LeadPickaxe, 1)
					.AddIngredient(ItemID.ExplosivePowder, 100)
					.AddIngredient(ItemID.LunarBar, 10)
					.AddIngredient(ItemType<EssenceOfTheAbyss>())
					.AddTile(TileID.Anvils)
					.Register();
			}
			

		}
	}
}
