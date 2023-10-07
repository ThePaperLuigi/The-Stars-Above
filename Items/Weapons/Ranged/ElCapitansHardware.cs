using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.ElCapitansHardware;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class ElCapitansHardware : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("El Capitan's Hardware");
			/* Tooltip.SetDefault("" +
                "Attacks with this weapon pierce foes and bounce off walls up to 4 times" +
				"\nStriking foes charges the [c/96BB7F:Renegade Gauge] (Critical strikes grant more charge)" +
				"\nStriking bosses grants bonus charge to the [c/96BB7F:Renegade Gauge]" +
				"\nOnce the [c/96BB7F:Renegade Gauge] is above 50%, right-click to consume the [c/96BB7F:Renegade Gauge], swapping weapons and firing a [c/F9CF49:Gyro Disk] that lasts for 4 seconds" +
				"\nThe [c/F9CF49:Gyro Disk] automatically targets and attacks nearby foes" +
				"\nIf the [c/96BB7F:Renegade Gauge] is higher than 50% on activation, the [c/F9CF49:Gyro Disk] gains bonus damage proportional to the extra amount consumed" +
				"\n'GAME RIGHTS?!'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 48;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Lime;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<PierceGunShot>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
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
			var modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2)
			{
				if (modPlayer.renegadeGauge >= 50)
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



				return true;
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float launchSpeed = 120f;
			float launchSpeed2 = 20f;
			float launchSpeed3 = 5f;

			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 Gun = direction * launchSpeed2;
			Vector2 Disk = direction * launchSpeed3;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 80f;
			position = new Vector2(position.X, position.Y);
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, Gun.X, Gun.Y, ProjectileType<DroneGun>(), 0, knockback, player.whoAmI);
				for (int d = 0; d < 27; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
					float scale = 2f - (Main.rand.NextFloat() * 1f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 27; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(125));
					float scale = 1f - (Main.rand.NextFloat() * .6f);
					perturbedSpeed = -(perturbedSpeed * scale);
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
			}
			else
			{


				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, Gun.X, Gun.Y, ProjectileType<PierceGun>(), 0, knockback, player.whoAmI);
				for (int d = 0; d < 27; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
					float scale = 2f - (Main.rand.NextFloat() * 1f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 27; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(125));
					float scale = 1f - (Main.rand.NextFloat() * .6f);
					perturbedSpeed = -(perturbedSpeed * scale);
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
			}
			
			
			SoundEngine.PlaySound(StarsAboveAudio.SFX_HuckleberryShoot, player.Center);
			if (player.altFunctionUse == 2)
			{
				damage += (player.GetModPlayer<WeaponPlayer>().renegadeGauge - 50);
				Projectile.NewProjectile(source, position.X, position.Y, Disk.X, Disk.Y, ProjectileType<GyroDisk>(), damage, knockback, player.whoAmI);
				player.GetModPlayer<WeaponPlayer>().renegadeGauge = 0;
			}
			else
			{


				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
				
			}
			
			return false;
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
					.AddIngredient(ItemID.LaserRifle)
					.AddIngredient(ItemID.Minishark)
					.AddIngredient(ItemID.MeteoriteBar, 4)
					.AddIngredient(ItemID.HallowedBar, 5)
					.AddIngredient(ItemType<EssenceOfTheRenegade>())
					.AddTile(TileID.Anvils)
					.Register();
		}
	}
}
