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
using StarsAbove.Projectiles.Other.Hawkmoon;
using StarsAbove.Buffs.Boss;
using StarsAbove.Buffs.Melee.SoulReaver;
using StarsAbove.Buffs.Other.DreadmotherDarkIdol;
using StarsAbove.Buffs.Other.SoliloquyOfSovereignSeas;
using StarsAbove.Projectiles.Other.SoliloquyOfSovereignSeas;

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
			Item.damage = 222;           //The damage of your weapon
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
            if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect != 2
                && player.GetModPlayer<StarsAbovePlayer>().MagicAspect != 2
                && player.GetModPlayer<StarsAbovePlayer>().RangedAspect != 2
                && player.GetModPlayer<StarsAbovePlayer>().SummonAspect != 2)
            {

                return false;
            }
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
                Item.channel = false;
            }
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				Item.useStyle = 5;
				Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 25;
				Item.UseSound = null;
                Item.channel = true;

                float launchSpeed = 10f;
                Vector2 mousePosition = Main.MouseWorld;
                Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                Vector2 arrowVelocity = direction * launchSpeed;

                if (player.channel)
                {
                    Item.useTime = 10;
                    Item.useAnimation = 10;
                    player.GetModPlayer<WeaponPlayer>().bowChargeActive = true;
                    player.GetModPlayer<WeaponPlayer>().bowCharge += 2;
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge == 1)
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
                    }
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge == 98)
                    {
                        for (int d = 0; d < 32; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
                        }
                    }
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge < 100)
                    {
                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
                            offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.5f);
                            d2.fadeIn = 0.1f;
                            d2.noGravity = true;
                        }
                        //Charge dust
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                            Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                        Dust d = Main.dust[Dust.NewDust(
                            player.MountedCenter + vector, 1, 1,
                            DustID.Clentaminator_Purple, 0, 0, 255,
                            new Color(0.8f, 0.4f, 1f), 0.8f)];
                        d.velocity = -vector / 12;
                        d.velocity -= player.velocity / 8;
                        d.noLight = true;
                        d.noGravity = true;

                    }
                    else
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.Clentaminator_Purple, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                    }
                }
                else
                {
                    Item.useTime = 45;
                    Item.useAnimation = 45;

                    if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 98)//If the weapon is fully charged...
                    {

                        player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                        player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                        //Reset the charge gauge.
                        if(player.HasBuff(BuffType<DreadmotherOrbitalBuff>()))
                        {
                            player.ClearBuff(BuffType<DreadmotherOrbitalBuff>());
                        }
                        SoundEngine.PlaySound(SoundID.Item15, player.position);
                        float dustAmount = 60f;
                        for (int i = 0; (float)i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                            int dust = Dust.NewDust(player.Center, 0, 0, DustID.Shadowflame);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = player.Center + spinningpoint5;
                            Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 20f;
                        }
                        player.AddBuff(BuffType<DreadmotherOrbitalBuff>(), 6 * 60);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<DreadmotherMagicOrbitals>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 5f, 0);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<DreadmotherMagicOrbitals>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 5f, 180);
                    }
                    else
                    {
                        if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0 && player.GetModPlayer<WeaponPlayer>().bowCharge <= 30)
                        {//Uncharged attack (lower than the threshold.)
                            SoundEngine.PlaySound(SoundID.Item125, player.position);

                            player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                            player.GetModPlayer<WeaponPlayer>().bowCharge = 0;

                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X - 70, player.MountedCenter.Y, -5, Main.rand.Next(-2, 3), ProjectileType<DreadmotherMagicSpheres>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 5f, 0);
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + 70, player.MountedCenter.Y, 5, Main.rand.Next(-2, 3), ProjectileType<DreadmotherMagicSpheres>(), player.GetWeaponDamage(Item) , 0, player.whoAmI, 5f , 90);
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y + 70, Main.rand.Next(-2,3), 5, ProjectileType<DreadmotherMagicSpheres>(), player.GetWeaponDamage(Item) , 0, player.whoAmI, 5f, 180);
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y - 70, Main.rand.Next(-2, 3), -5, ProjectileType<DreadmotherMagicSpheres>(), player.GetWeaponDamage(Item) , 0, player.whoAmI, 5f, 270);


                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<DreadmotherStaff>(), 0, 3, player.whoAmI, 0f);

                        }
                        else
                        {
                            player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                            player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                        }
                    }
                }
            }
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
                Item.channel = false;
                Item.useStyle = 5;
				Item.useTime = 32;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 32;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{
                Item.channel = false;
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
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 55f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<DreadmotherGun>(), 0, knockback, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X/8, velocity.Y/8, ProjectileType<DreadmotherEnergyBall>(), damage/3, 3, player.whoAmI, 0f);



            }
            else if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
                /*
				float numberProjectiles = 6;
				float rotation = MathHelper.ToRadians(15);
				position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.LunarFlare, damage/3 , knockback, player.whoAmI);
				}
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, velocity.X, velocity.Y, ProjectileType<DreadmotherStaff>(), 0, 3, player.whoAmI, 0f);
                */

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
                int dustEffect = DustID.Shadowflame;
                for (int d = 0; d < 20; d++)
                {
                    Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                }
                float dustAmount = 120f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                    int dust = Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Main.MouseWorld + spinningpoint5;
                    Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 20f;
                }
                player.AddBuff(BuffType<DreadmotherMinionBuff>(), 2);
                if (player.ownedProjectileCounts[ProjectileType<DreadmotherFlyingMinion>()] <= 0)
                {
                    player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<DreadmotherFlyingMinion>(), damage, knockback);

                    player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<DreadmotherMeleeSummon>(), damage, knockback);
                }

            }
            return false;
		}

		public override void AddRecipes()
		{
            CreateRecipe(1)
                  .AddIngredient(ItemID.SoulofFright, 12)
                  .AddIngredient(ItemID.SpookyWood, 40)
                  .AddIngredient(ItemType<EssenceOfTheDarkMaker>())
                  .AddTile(TileID.Anvils)
                  .Register();
        }
	}
}
