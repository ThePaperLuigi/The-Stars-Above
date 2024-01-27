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
using Terraria.GameContent.Drawing;
using StarsAbove.Buffs.Gundbits;
using StarsAbove.Projectiles.Magic.Gundbit;

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
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.UseSound = SoundID.Item5;
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TwoCrownBowArrow>();
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon`
			Item.noUseGraphic = false;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
            var modPlayer = player.GetModPlayer<WeaponPlayer>();
			modPlayer.twoCrownBowHeld = true;
            
            if (!player.HasBuff(BuffType<StellarTerminationBuff>())
                && !player.HasBuff(BuffType<StellarTerminationPreBuff>())
                && player.whoAmI == Main.myPlayer            
                && StarsAbove.weaponActionKey.JustPressed
                && player.GetModPlayer<WeaponPlayer>().terminationGauge >= 100)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCharge, player.Center);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<TwoCrownBowLaserPreVFX>(), 0, 4, player.whoAmI, 0f);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<TwoCrownBowLaserPreVFX2>(), 0, 4, player.whoAmI, 0f);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<TwoCrownBowLaserPreVFX3>(), 0, 4, player.whoAmI, 0f);

                player.AddBuff(BuffType<StellarTerminationPreBuff>(), 60);
                //player.GetModPlayer<WeaponPlayer>().terminationGauge = 0;
                
                //SoundEngine.PlaySound(StarsAboveAudio.SFX_GundamLaser, player.Center);

            }
            if(player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ProjectileType<TwoCrownBowLaser>()] < 1 && player.HasBuff(BuffType<StellarTerminationBuff>()))
            {
                for (int d = 0; d < 15; d++)
                {
                    int dustIndex = Dust.NewDust(player.Center, 0, 0, DustID.MinecartSpark, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, default, 1f);
                    Main.dust[dustIndex].noGravity = true;
                }
                int index = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0, 0, ProjectileType<TwoCrownBowLaser>(), player.GetWeaponDamage(Item), 0f, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<TwoCrownBowLaserVFX>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);

                player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
            }
            base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			var modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2)
			{
                return true;
                if (modPlayer.renegadeGauge >= 50)
				{

					
					
					
				}
				else
				{
					return false;
				}

			}
			else
			{
                if(player.HasBuff(BuffType<StellarTerminationBuff>()))
                {
                    return false;
                }


				return true;
			}
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-9, 4);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<TwoCrownBowDepravityCooldown>()))
            {

                player.AddBuff(BuffType<TwoCrownBowDepravityCooldown>(), 20 * 60);
                SoundEngine.PlaySound(SoundID.Item43, player.Center);
                player.AddBuff(BuffType<TwoCrownBowDepravity>(), 180);
				player.AddBuff(BuffID.Swiftness, 180);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y - 500, 0, 0, ProjectileType<TwoCrownBowSkyAttack>(), damage/4, 4, player.whoAmI, 0f);
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.RainbowRodHit,
              new ParticleOrchestraSettings { PositionInWorld = player.Center },
              player.whoAmI);
                for (int d = 0; d < 18; d++)
                {
                    Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default, 1f);

                }

                for (int i3 = 0; i3 < 50; i3++)
                {
                    Vector2 position2 = Vector2.Lerp(new Vector2(player.Center.X, player.Center.Y - 500), player.Center, (float)i3 / 50);
                    Dust d = Dust.NewDustPerfect(position2, DustID.GemAmethyst, null, 240, default, 1f);
                    d.fadeIn = 0.4f;
                    d.noGravity = true;
                }
            }
            else
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

            }
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
