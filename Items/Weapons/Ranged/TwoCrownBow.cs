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
using StarsAbove.Projectiles.Ranged.TwoCrownBow;
using StarsAbove.Projectiles.Ranged.DevotedHavoc;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarsAbove.Buffs.TwoCrownBow;
using SubworldLibrary;
using Terraria.Map;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class TwoCrownBow : ModItem
	{
		public override void SetStaticDefaults() {
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 110;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.UseSound = SoundID.Item5;
			Item.rare = ItemRarityID.Lime;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TwoCrownBowArrow>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = false;
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
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-9, 4);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            position += muzzleOffset;
            
            for (int d = 0; d < 40; d++)
            {
                Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(60));
                float scale = 1f - (Main.rand.NextFloat() * .9f);
                perturbedSpeed1 = perturbedSpeed1 * scale;
                int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemDiamond, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 0.6f);
                Main.dust[dustIndex].noGravity = true;

            }

            

            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(2));
            velocity.X = perturbedSpeed.X;
            velocity.Y = perturbedSpeed.Y;
            if (player.ownedProjectileCounts[ProjectileType<TwoCrownVFX>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<TwoCrownVFX>(), 0, 4, player.whoAmI, 0f);


            }
            player.AddBuff(BuffType<TwoCrownBowFiring>(), 10);

            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<TwoCrownBowArrow>(), damage, knockback, player.whoAmI);

            return false;
		}

		

		public override void AddRecipes()
		{
			/*CreateRecipe(1)
					.AddIngredient(ItemID.LaserRifle)
					.AddIngredient(ItemID.Minishark)
					.AddIngredient(ItemID.MeteoriteBar, 4)
					.AddIngredient(ItemID.HallowedBar, 5)
					.AddIngredient(ItemType<EssenceOfTheRenegade>())
					.AddTile(TileID.Anvils)
					.Register();*/
		}
	}

}
