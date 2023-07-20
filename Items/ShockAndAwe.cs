using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Projectiles.ShockAndAwe;
using System;

namespace StarsAbove.Items
{
    public class ShockAndAwe : ModItem
	{
		public override void SetStaticDefaults() {
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 98;
			Item.height = 38;
			Item.useTime = 48;
			Item.useAnimation = 48;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 14;
			Item.rare = ItemRarityID.Orange;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<HuckleberryRound>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
        public override bool AltFunctionUse(Player player)
        {
			return true;
        }
        public override void HoldItem(Player player)
		{

			
			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-50, 5);
		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 55f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			if (player.altFunctionUse == 2)
			{
				SoundEngine.PlaySound(SoundID.Item1, player.Center);

				if (player.direction == 1)
				{
					Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<GardenerSlash1>(), (int)(damage), 0, player.whoAmI, 0f);

				}
				else
				{
					Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ProjectileType<GardenerSlash2>(), (int)(damage), 0, player.whoAmI, 0f);

				}
			}
			else
			{
				float rotation = (float)Math.Atan2(position.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), position.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));
				SoundEngine.PlaySound(StarsAboveAudio.SFX_ShockAndAweRocket, player.Center);
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ShockAndAweRocketLauncher>(), 0, 0, player.whoAmI, 0f);

				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<ShockAndAweRocket>(), (int)(damage), 0, player.whoAmI, 0f);

				for (int d = 0; d < 25; d++)
				{
					float Speed2 = Main.rand.NextFloat(4, 38);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.Flare, perturbedSpeed.X, perturbedSpeed.Y, 0, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 25; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
				position -= Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 105f;
				for (int d = 0; d < 45; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * 1), (float)((Math.Sin(rotation) * Speed3) * 1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
				
			}

			return false;
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Bomb, 5)
				.AddIngredient(ItemID.TungstenBar, 18)
				.AddIngredient(ItemID.Dynamite, 1)
				.AddIngredient(ItemType<EssenceOfTheSoldier>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Bomb, 5)
				.AddIngredient(ItemID.SilverBar, 18)
				.AddIngredient(ItemID.Dynamite, 1)
				.AddIngredient(ItemType<EssenceOfTheSoldier>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
