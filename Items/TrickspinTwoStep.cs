using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.TrickspinTwoStep;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
	public class TrickspinTwoStep : ModItem
	{
		public override void SetStaticDefaults()
		{
			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 24; // The width of the item's hitbox.
			Item.height = 24; // The height of the item's hitbox.

			Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
			Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
			Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
			Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
			Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
			Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

			Item.damage = 40; // The amount of damage the item does to an enemy or player.
			Item.DamageType = DamageClass.Summon; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
			Item.knockBack = 2.5f; // The amount of knockback the item inflicts.
			Item.crit = 8; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
			Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
			Item.rare = ItemRarityID.LightPurple; // The item's rarity. This changes the color of the item's name.
			Item.value = Item.buyPrice(gold: 1); // The amount of money that the item is can be bought for.

			Item.shoot = ModContent.ProjectileType<TrickspinTwoStepProjectile>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
			Item.shootSpeed = 16f; // The velocity of the shot projectile.			
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{

				if (player.GetModPlayer<WeaponPlayer>().TrickspinReady)
				{
					float launchSpeed = 18f;
					Vector2 pos = player.GetModPlayer<WeaponPlayer>().TrickspinCenter;
					Vector2 direction = Vector2.Normalize(pos - player.Center);
					Vector2 arrowVelocity = direction * launchSpeed;
					player.velocity += new Vector2(arrowVelocity.X, arrowVelocity.Y);

					//Dust or something
				}
			}

			return base.UseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.altFunctionUse == 2)
			{
				//If player does NOT have a yoyo
				if (player.ownedProjectileCounts[ProjectileType<TrickspinSuspendYoyo>()] < 1)
                {
					Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<TrickspinSuspendYoyo>(), (int)(damage), 0, player.whoAmI, 0f);

				}
				else
                {
					
                }

				return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        

		public override void AddRecipes()
		{
			
		}
	}
}