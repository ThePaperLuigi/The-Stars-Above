using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.ForceOfNature;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class ForceOfNature : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Force-of-Nature");

			/* Tooltip.SetDefault("Fires a high amounts of bullets in a wide spread" +
				"\nLanding hits on foes grants Swiftness for 4 seconds" +
				"\nAttacks cause self-knockback" +
				"\nThe weapon must reload after two attacks" +
				"\nFire the weapon without bullets to reload" +
				"\nRight click to fire a [c/EE582F:Blasting Charge] to launch yourself far in the opposite direction (6 second cooldown)" +
				"\nFiring the [c/EE582F:Blasting Charge] reloads the weapon without cooldown" +
				$""); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 47;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<ForceOfNatureRound>();
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item11;
			Item.noUseGraphic = true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{




		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<BlastingChargeCooldownBuff>()))
				{
					player.GetModPlayer<WeaponPlayer>().forceBullets = 2;
					return true;
					
				}
				return false;

			}
			if (player.GetModPlayer<WeaponPlayer>().forceBullets <= 0)
			{
				
				
					player.GetModPlayer<WeaponPlayer>().forceBullets = 2;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, Vector2.Zero.X, Vector2.Zero.Y, ProjectileType<ForceOfNatureReload>(), 0, 0, player.whoAmI);
					player.AddBuff(BuffType<ForceOfNatureReloadBuff>(), 40);
					return false;
				
			}
			else
            {
				if(!player.HasBuff(BuffType<ForceOfNatureReloadBuff>()))
                {
					return true;
				}
				return false;
			}


			
		}

		

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?

			
		public override bool? UseItem(Player player)
		{
			

			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse == 2)
			{

				Vector2 Lunge = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * -16f;
				player.velocity = Lunge;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<ForceOfNatureGun>(), 0, knockback, player.whoAmI);

				player.AddBuff(BuffType<BlastingChargeCooldownBuff>(), 360);
				//player.GetModPlayer<WeaponPlayer>().forceBullets--;
				for (int d = 0; d < 27; d++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(55));
					float scale = 3f - (Main.rand.NextFloat() * .3f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;

				}

				return false;


			}
			if (player.GetModPlayer<WeaponPlayer>().forceBullets > 0)
            {
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<ForceOfNatureGun>(), 0, knockback, player.whoAmI);


				int numberProjectiles = 12 + Main.rand.Next(2); //random shots
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(45)); // 30 degree spread.
																														// If you want to randomize the speed to stagger the projectiles
						float scale = 1f - (Main.rand.NextFloat() * .3f);
						perturbedSpeed = perturbedSpeed * scale;
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
					}
					Vector2 Lunge = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * -8f;
					player.velocity = Lunge;
					player.GetModPlayer<WeaponPlayer>().forceBullets--;


					for (int d = 0; d < 21; d++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(47));
						float scale = 2f - (Main.rand.NextFloat() * .9f);
						perturbedSpeed = perturbedSpeed * scale;
						int dustIndex = Dust.NewDust(position, 0, 0, 127, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
						Main.dust[dustIndex].noGravity = true;
						
					}
					for (int d = 0; d < 16; d++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(47));
						float scale = 2f - (Main.rand.NextFloat() * .9f);
						perturbedSpeed = perturbedSpeed * scale;
						int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;
					}


				
			}
			else
            {
				
				
				return false;
                
			}
			
			return true;
		}

		// How can I get a "Clockwork Assault Rifle" effect?
		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
		/*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override void OnConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

		// How can I shoot 2 different projectiles at the same time?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

		// How can I choose between several projectiles randomly?
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/


		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.CobaltBar, 8)
				.AddIngredient(ItemType<EssenceOfBlasting>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.PalladiumBar, 8)
				.AddIngredient(ItemType<EssenceOfBlasting>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
