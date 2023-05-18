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

namespace StarsAbove.Items
{
    public class BrilliantSpectrum : ModItem
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
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();          //Is your weapon a melee weapon?
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
			//player.GetModPlayer<WeaponPlayer>().AshenAmbitionHeld = true;
			
			

        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.GetModPlayer<WeaponPlayer>().AshenAmbitionExecuteThreshold = damage * 5;

			if (player.altFunctionUse == 2)
			{


			}
			else
			{
				
				
				//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI);

				
				
			}
			
			
			return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
