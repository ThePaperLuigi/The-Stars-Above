using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Pets;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Other.ArchitectLuminance;
using StarsAbove.Buffs.Other.ArchitectsLuminance;
using StarsAbove.Projectiles.Other.DreadmotherDarkIdol;
using StarsAbove.Projectiles.Melee.SoulReaver;
using StarsAbove.Buffs.Summon.DragaliaFound;
using StarsAbove.Buffs;
using StarsAbove.Projectiles.Summon.DragaliaFound;
using System;

namespace StarsAbove.Items.Weapons.Other
{
    public class DreadmotherDarkIdol : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 112;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 158;            //Weapon's texture's width
			Item.height = 158;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item15;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ProjectileType<Projectiles.Other.ArchitectLuminance.ArchitectShoot>();
			Item.shootSpeed = 38;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (player.HasBuff(BuffType<ComboCooldown>()))
            {
                return false;
            }
            if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<ArtificeSirenBuff>()) && !player.HasBuff(BuffType<ArtificeSirenCooldown>()))
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
			player.GetModPlayer<WeaponPlayer>().dreadmotherHeld = true;
			Item.scale = 2f;
			//player.AddBuff(BuffType<Buffs.Other.ArchitectsLuminance.ArchitectLuminanceBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Other.ArchitectLuminance.Armament>()] < 1)
			{
				//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Other.ArchitectLuminance.Armament>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


			}
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
                attackComboCooldown--;
                if (attackComboCooldown <= 0)
                {
                    attackType = 0;
                }

                Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 10;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 10;
				Item.UseSound = SoundID.Item1;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				Item.useStyle = 5;
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
				Item.useStyle = 5;
				Item.useTime = 12;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 12;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{
				Item.useStyle = ItemUseStyleID.HiddenAnimation;
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item15;
			}

				base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            // 
            // 60 frames = 1 second
            //player.GetModPlayer<WeaponPlayer>().radiance++;

        }
        int attackType;
        int attackComboCooldown;

        private bool altSwing;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {

				

			}
			else if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{

				float numberProjectiles = 6;
				float rotation = MathHelper.ToRadians(15);
				position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.LunarFlare, damage/3 , knockback, player.whoAmI);
				}
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y, ProjectileType<Projectiles.Other.ArchitectLuminance.ArchitectShoot>(), 0, 3, player.whoAmI, 0f);


			}
            else if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
                attackComboCooldown = 60;

                switch (attackType)
                {
                    case 0:
                        Projectile.NewProjectile(source, player.Center, velocity, ProjectileType<DreadmotherClawAttackAlt>(), damage, knockback, player.whoAmI, 0, 0, player.direction);

                        attackType++;
                        return false;
                    case 1:
                        Projectile.NewProjectile(source, player.Center, velocity, ProjectileType<DreadmotherClawAttackSideAlt>(), damage, knockback, player.whoAmI, 0, 0, player.direction);

                        attackType++;
                        return false;
                    case 2:
                        Projectile.NewProjectile(source, player.Center, velocity, ProjectileType<DreadmotherClawAttackSide>(), damage, knockback, player.whoAmI, 0, 0, player.direction);

                        attackType++;
                        return false;
                    case 3:
                        Projectile.NewProjectile(source, player.Center, velocity, ProjectileType<DreadmotherClawAttack>(), damage, knockback, player.whoAmI, 0, 0, player.direction);

                        attackType++;
                        return false;
                    case 4:
						player.AddBuff(BuffType<ComboCooldown>(), 20);
                        Projectile.NewProjectile(source, player.Center, velocity*2, ProjectileType<DreadmotherClawAttackFinish>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                        attackType = 0;
                        return false;
                }
                if (attackType > 4)
                {
                    attackType = 0;
                }

            }
            else if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {

               
            }
            return false;
		}

		public override void AddRecipes()
		{
			
		}
	}
}
