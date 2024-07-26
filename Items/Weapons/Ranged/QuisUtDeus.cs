using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.CrimsonOutbreak;
using StarsAbove.Projectiles.Ranged.QuisUtDeus;
using StarsAbove.Projectiles.Ranged.IzanagisEdge;
using System;
using StarsAbove.Dusts;
using Terraria.Graphics.Shaders;
using StarsAbove.Buffs.Ranged.QuisUtDeus;
using StarsAbove.Projectiles.StellarNovas;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class QuisUtDeus : ModItem
	{
		public override void SetStaticDefaults() {
			
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 9;
			Item.useTime = 9;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<QuisUtDeusRound>();
			Item.shootSpeed = 20f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				if(player.HasBuff(BuffType<CallOfTheStarsBuff>()) || player.HasBuff(BuffType<CallOfTheStarsCooldown>()))
				{
					return false;
				}
			}


			return base.CanUseItem(player);
		}

		public override void HoldItem(Player player)
		{
            if(player.HasBuff(BuffType<CallOfTheStarsBuff>()))
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.2f ;
            }
			if(player.GetModPlayer<StarsAbovePlayer>().timeAfterGettingHit >= 60 * 10)
			{
				player.AddBuff(BuffType<BenedictioBuff>(), 2);

				if(player.velocity.Y == 0)
				{
                    for (int i3 = 0; i3 < 50; i3++)
                    {

                        Dust d = Main.dust[Dust.NewDust(new Vector2(player.Center.X - player.width, player.Center.Y + player.height / 2), player.width * 2 - 3, 0, DustID.GemAmethyst, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                        d.fadeIn = 0.3f;
                        d.noGravity = true;
                    }
                }
				else
				{
                    for (int i2 = 0; i2 < 5; i2++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 30f);
                        offset.Y += (float)(Math.Cos(angle) * 30f);

                        Dust d2 = Dust.NewDustPerfect(player.Center + offset, DustID.GemAmethyst, player.velocity, 200, default, 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }

                }
                
            }
        }
        

        public override bool? UseItem(Player player)
		{
            if (player.altFunctionUse == 2)
            {

                player.AddBuff(BuffType<CallOfTheStarsBuff>(), 60 * 8);
                SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive);

                Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ProjectileType<CallOfTheStarsBackground>(), 0, 0, player.whoAmI, 0, 8 * 60);

            }

            return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse == 2)
            {

                return false;
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<QuisUtDeusRound>(), damage, knockback, player.whoAmI);
            
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<QuisUtDeusGun>(), 0, knockback, player.whoAmI);
            
			SoundEngine.PlaySound(SoundID.Item11, player.Center);
            position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 40f;

            int dustAmount = 40;
            for (int i = 0; (float)i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                spinningpoint5 = spinningpoint5.RotatedBy(velocity.ToRotation());
                int dust = Dust.NewDust(position, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = position + spinningpoint5;
                Main.dust[dust].velocity = velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
            }
           


            
            return false;
		}

		public override void AddRecipes()
		{
            CreateRecipe(1)
                  .AddIngredient(ItemID.LunarBar, 12)
                  .AddIngredient(ItemID.SoulofLight, 8)
                  .AddIngredient(ItemID.FallenStar, 8)
                  .AddIngredient(ItemID.StarWrath, 1)
                  .AddIngredient(ItemType<EssenceOfTheStars>())
                  .AddTile(TileID.Anvils)
                  .Register();
        }
	}
}
