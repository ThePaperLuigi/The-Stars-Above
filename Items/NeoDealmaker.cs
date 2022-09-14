using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using StarsAbove.Projectiles;
using System;
using Terraria.Localization;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.NeoDealmaker;
using Terraria.Audio;

namespace StarsAbove.Items
{
	public class NeoDealmaker : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/F7D76A:Hold left click to charge the weapon; the longer the weapon is charged, the stronger the resulting attack will become]" +
				"\nAttacks with this weapon fire small pellets, inflicting Midas for 20 seconds on critical strikes" +
				"\nCharged attacks will instead fire a [c/FBD63E:BIG SHOT], dealing 2x base damage" +
				"\nThe [c/FBD63E:BIG SHOT] pierces up to 8 foes and ignores terrain" +
				"\nAdditionally, enemies inflicted with Midas take 15% more damage from the [c/FBD63E:BIG SHOT]" +
				"\nCritical strikes increase this modifier to 25%" +
				"\nDefeating foes with the [c/FBD63E:BIG SHOT] is guaranteed to drop 5 silver" +
				"\n'Now's your chance to be a...'" +
				$"");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 32;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;

			Item.rare = ItemRarityID.Orange;
			//item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.channel = true;//Important for all "bows"
			Item.shoot = ProjectileType<BigShot>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

		}


		bool meleeStance = false;
		int meleeStanceTimer = 0;

		public override bool AltFunctionUse(Player player)
		{
			return false;
		}
		public override bool CanUseItem(Player player)
		{

			if (Main.myPlayer == player.whoAmI)
			{



			}
			return true;

		}
		public override void HoldItem(Player player)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				float launchSpeed = 10f + (int)Math.Round(player.GetModPlayer<StarsAbovePlayer>().bowCharge / 30);
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity = direction * launchSpeed;




				if (player.channel)
				{
					Item.useTime = 2;
					Item.useAnimation = 2;
					player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = true;
					player.GetModPlayer<StarsAbovePlayer>().bowCharge += 3;
					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 1)
					{
						//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
					}
					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 99)
					{
						for (int d = 0; d < 22; d++)
						{
							Dust.NewDust(player.Center, 0, 0, 159, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
						}

						SoundEngine.PlaySound(SoundID.CoinPickup, player.position);
					}
					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge < 100)
					{
						for (int i = 0; i < 30; i++)
						{//Circle
							Vector2 offset = new Vector2();
							double angle = Main.rand.NextDouble() * 2d * Math.PI;
							offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
							offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));

							Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 159, player.velocity, 200, default(Color), 0.5f);
							d2.fadeIn = 0.1f;
							d2.noGravity = true;
						}
						//Charge dust
						Vector2 vector = new Vector2(
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
							Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
						Dust d = Main.dust[Dust.NewDust(
							player.MountedCenter + vector, 1, 1,
							15, 0, 0, 255,
							new Color(0.8f, 0.4f, 1f), 0.8f)];
						d.velocity = -vector / 12;
						d.velocity -= player.velocity / 8;
						d.noLight = true;
						d.noGravity = true;

					}
					else
					{
						Dust.NewDust(player.Center, 0, 0, 159, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
					}
				}
				else
				{
					Item.useTime = 15;
					Item.useAnimation = 15;

					if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100)
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_DealmakerCharged, player.Center);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<BigShot>(), player.GetWeaponDamage(Item) * 2, 5, player.whoAmI, 0f);
						player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
						player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;


					}
					else
					{
						if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0)
						{//

							SoundEngine.PlaySound(StarsAboveAudio.SFX_DealmakerShoot, player.Center);

							player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
							player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;

							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SmallShot>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
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
				.AddIngredient(ItemID.GoldCoin, 3)
				.AddIngredient(ItemType<EssenceOfTheAnomaly>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
