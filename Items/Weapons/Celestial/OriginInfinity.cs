using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.Celestial.OriginInfinity;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class OriginInfinity : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 99;           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 96;            //Weapon's texture's width
			Item.height = 96;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.shoot = 10;
			Item.crit = 26;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().radiance >= 5)
				{
					
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
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when swing the sword
				
					Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 269);
				
				
				
			}
		}
		public override void HoldItem(Player player)
		{
			
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<WeaponPlayer>().radiance++;
			
		}
		public bool altSwing;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (altSwing)
			{
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<OriginInfinitySword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
				altSwing = false;
			}
			else
			{
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<OriginInfinitySword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
				altSwing = true;
			}

			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
