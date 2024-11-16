using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Magic.RadGun;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Magic
{
    public class RadGun : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("Right click to load 12 bullets into the magazine with 40 mana" +
				"\nTiming the reload perfectly will boost damage up to 8 times and refund 20 mana" +
				"\nFailing to time the reload perfectly will reset the bonus" +
				"\n'Are you a bad enough dude?'" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 16;
            Item.DamageType = ModContent.GetInstance<Systems.MysticDamageClass>();
            Item.mana = 0;
			Item.width = 44;
			Item.height = 40;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10;
			Item.rare = 6;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<NotRadRound>();
			Item.shootSpeed = 11f;
			Item.UseSound = SoundID.Item11;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

		}
		int combo = 0;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (player.statMana >= 40 && player.GetModPlayer<WeaponPlayer>().RadTimerEnabled == false && player.GetModPlayer<WeaponPlayer>().RadBullets <= 0)
				{
					player.GetModPlayer<WeaponPlayer>().RadReload = false;
					player.statMana -= 40;
					player.manaRegenDelay = 220;
					player.GetModPlayer<WeaponPlayer>().RadTimer = 0;
					player.GetModPlayer<WeaponPlayer>().RadTimerEnabled = true;
					if (combo < 8)
					{
						combo++;
					}
				}
				else
				{
					if (player.GetModPlayer<WeaponPlayer>().RadTimer > 40 && player.GetModPlayer<WeaponPlayer>().RadTimer < 60 && player.GetModPlayer<WeaponPlayer>().RadTimerEnabled == true && player.GetModPlayer<WeaponPlayer>().RadBullets <= 0)//Success!
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_RadGunSuccess, player.Center);
						player.statMana += 20;
						player.GetModPlayer<WeaponPlayer>().RadTimerEnabled = false;
						player.GetModPlayer<WeaponPlayer>().RadBullets = 12;
						player.GetModPlayer<WeaponPlayer>().RadReload = true;
						return true;
					}
					else
					{
						if (player.GetModPlayer<WeaponPlayer>().RadTimer > 20 && player.GetModPlayer<WeaponPlayer>().RadTimer < 40 || player.GetModPlayer<WeaponPlayer>().RadTimer > 60 && player.GetModPlayer<WeaponPlayer>().RadTimerEnabled == true && player.GetModPlayer<WeaponPlayer>().RadBullets <= 0)//Failure
						{
							player.GetModPlayer<WeaponPlayer>().RadTimerEnabled = false;
							SoundEngine.PlaySound(StarsAboveAudio.SFX_RadGunFail, player.Center);
							player.GetModPlayer<WeaponPlayer>().RadBullets = 12;
							player.GetModPlayer<WeaponPlayer>().RadTimer = 0;
							combo = 0;
							return true;
						}
						else
						{
							return false;
						}
					}
				}
			}
			if (player.GetModPlayer<WeaponPlayer>().RadBullets > 0)
			{
				player.GetModPlayer<WeaponPlayer>().RadBullets--;
				return true;
			}
			else
			{
				return false;
			}
			
		}

		public override void HoldItem(Player player)
		{
			
			if (player.GetModPlayer<WeaponPlayer>().RadTimer >= 100 && player.GetModPlayer<WeaponPlayer>().RadTimerEnabled)//fail
			{
				player.GetModPlayer<WeaponPlayer>().RadTimerEnabled = false;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_RadGunFail, player.Center);
				player.GetModPlayer<WeaponPlayer>().RadBullets = 12;
				player.GetModPlayer<WeaponPlayer>().RadTimer = 0;
				combo = 0;
				
			}
			if (player.GetModPlayer<WeaponPlayer>().RadTimerEnabled)
			{
				player.GetModPlayer<WeaponPlayer>().RadTimer+= 2 + (combo / 70);
			}
			
			base.HoldItem(player);
		}


		

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{


			if (player.altFunctionUse == 2)
			{

			}
			else
			{
				if (player.GetModPlayer<WeaponPlayer>().RadReload)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<RadRound>(), damage + (6 * combo), knockback, player.whoAmI, 0f);


				}
				else
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI, 0f);

				}
			}

			return false;
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
				.AddIngredient(ItemID.Wood, 12)
				.AddIngredient(ItemID.Revolver, 1)
				.AddIngredient(ItemID.Lens, 2)
				.AddIngredient(ItemType<EssenceOfStyle>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
