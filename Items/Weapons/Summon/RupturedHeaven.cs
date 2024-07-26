using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.Summon.RupturedHeaven;
using StarsAbove.Projectiles.Summon.MorningStar;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Summon
{
    public class RupturedHeaven : ModItem
	{
		// The texture doesn't have the same name as the item, so this property points to it.
		//public override string Texture => "ExampleMod/Content/Items/Weapons/ExampleWhip";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruptured Heaven");
			/* Tooltip.SetDefault("WORK IN PROGRESS WEAPON" +
				"Attacks with this weapon swing in an arc, granting Rage on hit for 2 seconds" +
                "\nRight click to whip the weapon, dealing 20% less damage but granting [Tactician's Eye] on critical strikes" +
                "\n[Tactician's Eye] grants Dangersense, Hunter, and Heartreach to yourself and nearby allies for 5 seconds" +
				"\nPress the Weapon Action Key to cycle between [Instructor's Lectures], granting buffs to nearby allies as long as the weapon is held" +
				"\n[c/8EE7E4:Lion's Lecture] increases Melee Damage by 18% and attack speed by 10%" +
				"\n[c/B4E93A:Eagle's Lecture] increases Magic Damage by 18% and grants Mana Regeneration" +
				"\n[c/F3E63E:Deer's Lecture] increases Ranged Damage by 18% and critical strike chance by 8%" +
				"\n'Let the lesson begin!'" +
				$""); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Call this method to quickly set some of the properties below.
			//Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.damage = 25;
			Item.knockBack = 7;
			Item.rare = ItemRarityID.LightRed;

			Item.shoot = ModContent.ProjectileType<MorningStarWhip>();
			Item.shootSpeed = 4;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item152;
			Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int randomCast;
		bool altSwing;
		int lecture;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void HoldItem(Player player)
        {
			//player.AddBuff(BuffType<MorningStarHeld>(), 10);
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();

			if(lecture == 0)//
            {
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Player p = Main.player[i];
					if (p.active && p.Distance(player.Center) < 1000 && p != player)
					{
						//p.AddBuff(BuffID.Heartreach, 300);  // Lion's Lecture
						
						
					}



				}
			}
			if(lecture == 1)
            {

            }
			if(lecture == 2)
            {

            }
			
		}

        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				

			}
			else
			{
				
			}

			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<RupturedSwing2>(),damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<RupturedSwing1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<RupturedSwing1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<RupturedSwing2>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			else
            {
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<RupturedHeavenWhip>(), damage, knockback, player.whoAmI, 0f);

			}

			return false;

			
			

			//return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			
		}
	}
}