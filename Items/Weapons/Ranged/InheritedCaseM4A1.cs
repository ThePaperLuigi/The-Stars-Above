using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Ranged.InheritedCaseM4A1;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Projectiles.Other.GoldenKatana;
using StarsAbove.Projectiles.Ranged.InheritedCaseM4A1;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class InheritedCaseM4A1 : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            /*
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                Item.damage = 350;
            }
            else
            {
                Item.damage = 90;
            }*/

            //The damage of your weapon
            Item.DamageType = DamageClass.Ranged;
            Item.width = 68;            //Weapon's texture's width
            Item.height = 68;           //Weapon's texture's height
            Item.useTime = 10;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 10;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
            Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
            Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
            Item.autoReuse = true;
            Item.shoot = ProjectileType<M4A1Round>();
            Item.shootSpeed = 8f;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;

        }

        int currentSwing;
        int slashDuration;
        int comboTimer;
        public override bool AltFunctionUse(Player player)
        {

            return true;
        }
        public override void UpdateInventory(Player player)
        {


            base.UpdateInventory(player);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if(player.GetModPlayer<WeaponPlayer>().bowCharge < 100)
            {
                return false;
            }

            return base.CanConsumeAmmo(ammo, player);
        }
        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                if (!player.HasBuff(BuffType<ComboCooldown>()))
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

            }
            return base.CanUseItem(player);
        }
        bool inheritedCaseActive = false;
        int bulletShootDelay = 0;
        int laserShootDelay = 0;
        public override void HoldItem(Player player)
        {
            var modPlayer = player.GetModPlayer<WeaponPlayer>();
            modPlayer.M4A1Held = true;
            if (modPlayer.ActiveGuns.Count > 0)
            {
                player.AddBuff(BuffType<AuxiliaryArmamentBuff>(), 2);
            }
            bulletShootDelay--;
            laserShootDelay--;
            if(inheritedCaseActive)
            {
                player.AddBuff(BuffType<InheritedCaseActive>(), 10);

            }
            //Weapon Action Key
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed && player.itemTime <= 0)
            {

            }
            if(inheritedCaseActive && player.channel && player.altFunctionUse != 2)
            {
                Item.useTime = 90;
                Item.useAnimation = 90;
                if (!player.HasBuff(BuffType<ComboCooldown>()))
                {
                    float launchSpeed = 20f;
                    Vector2 mousePosition = Main.MouseWorld;
                    Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                    Vector2 arrowVelocity = direction * launchSpeed;

                    Vector2 position = player.Center;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtBlast, player.position);
                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(arrowVelocity.X, arrowVelocity.Y)) * 35f;
                    if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                    {
                        position += muzzleOffset;
                    }
                    //Fire bullets on a timer
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<InheritedCaseGun>(), 0, 0, player.whoAmI, 0f);

                    Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y);
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;

                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<InheritedCaseRocket>(), player.GetWeaponDamage(Item), 1, player.whoAmI);

                    player.AddBuff(BuffType<ComboCooldown>(), 80);
                }
            }
            else
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    float launchSpeed = 10f;
                    Vector2 mousePosition = Main.MouseWorld;
                    Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                    Vector2 arrowVelocity = direction * launchSpeed;


                    if (player.altFunctionUse != 2)
                    {
                        if (player.channel)
                        {
                            Item.useTime = 1;
                            Item.useAnimation = 1;


                            modPlayer.bowChargeActive = true;

                            ChargeSpeed(player);
                            modPlayer.M4A1UseTimer += 3;

                            if (modPlayer.bowCharge == 1)
                            {

                            }
                            else if (modPlayer.bowCharge == 95)//First gauge full
                            {
                                for (int d = 0; d < 15; d++)
                                {
                                    Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                                }

                                SoundEngine.PlaySound(SoundID.Unlock, player.position);
                            }
                            else if (modPlayer.bowCharge >= 100)
                            {
                                if (bulletShootDelay < 0)
                                {
                                    Vector2 position = player.Center;
                                    SoundEngine.PlaySound(SoundID.Item11, player.position);
                                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(arrowVelocity.X, arrowVelocity.Y)) * 35f;
                                    if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                                    {
                                        position += muzzleOffset;
                                    }
                                    //Fire bullets on a timer
                                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<M4A1Gun>(), 0, 0, player.whoAmI, 0f);
                                    Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                                    perturbedSpeed = perturbedSpeed * scale;
                                    player.PickAmmo(Item, out int type, out float speed, out int damage, out float knockback, out int ammoID);

                                    if (ammoID == ItemID.MusketBall || ammoID == ItemID.EndlessMusketPouch)
                                    {
                                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<M4A1Round>(), player.GetWeaponDamage(Item), 1, player.whoAmI);

                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);

                                    }
                                    bulletShootDelay = 4;
                                }

                            }
                            if (modPlayer.overCharge1 == 97.5)//Second gauge full
                            {
                                //Dust
                                for (int d = 0; d < 12; d++)
                                {
                                    Dust.NewDust(player.Center, 0, 0, DustID.PurpleCrystalShard, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
                                }

                                SoundEngine.PlaySound(SoundID.Item117, player.position);
                            }
                            else if (modPlayer.overCharge1 >= 100)
                            {
                                if (laserShootDelay < 0)
                                {
                                    //SoundEngine.PlaySound(SoundID.Item12, player.position);

                                    Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                                    perturbedSpeed = perturbedSpeed * scale;
                                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<M4A1Laser>(), (int)(player.GetWeaponDamage(Item) * 0.7f), 0, player.whoAmI);
                                    laserShootDelay = 15;
                                }
                            }

                            if (modPlayer.overCharge2 >= 100)//Third gauge full
                            {


                                if (modPlayer.AuxiliaryGuns.Count > 0 && player.GetModPlayer<StarsAbovePlayer>().inCombat > 0)
                                {
                                    player.AddBuff(BuffType<AuxiliaryArmamentBuff>(), 10);

                                    Vector2 dustPosition = player.Center;
                                    float dustAmount = 40f;
                                    for (int i = 0; i < dustAmount; i++)
                                    {
                                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                                        Main.dust[dust].scale = 2f;
                                        Main.dust[dust].noGravity = true;
                                        Main.dust[dust].position = dustPosition + spinningpoint5;
                                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                                    }
                                    for (int i = 0; i < dustAmount; i++)
                                    {
                                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + MathHelper.ToRadians(90));
                                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                                        Main.dust[dust].scale = 2f;
                                        Main.dust[dust].noGravity = true;
                                        Main.dust[dust].position = dustPosition + spinningpoint5;
                                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                                    }


                                    //If the user doesn't have the max amount of weapons, spawn a random one and then reset overcharge state.
                                    int randomChoice = Main.rand.Next(modPlayer.AuxiliaryGuns.Count); //Choose a random weapon from the list
                                    int WeaponType = modPlayer.AuxiliaryGuns[randomChoice];//'save' the ProjectileID of that weapon
                                    modPlayer.ActiveGuns.Add(WeaponType);//Add this weapon to the active guns pool
                                                                         //Spawn the weapon
                                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, WeaponType, 0, 0, player.whoAmI, 0f);
                                    //Remove the weapon from the original weapon list
                                    modPlayer.AuxiliaryGuns.RemoveAt(randomChoice);

                                    modPlayer.overCharge2 = 0;
                                    SoundEngine.PlaySound(SoundID.Item105, player.position);
                                }
                                else
                                {
                                    //If the user has the max, this won't reset

                                }

                            }
                            if (modPlayer.bowCharge == 16.5)
                            {
                                SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);

                            }

                            if (modPlayer.bowCharge < 100)
                            {
                                for (int i = 0; i < 30; i++)
                                {//Circle
                                    Vector2 offset = new Vector2();
                                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                    offset.X += (float)(Math.Sin(angle) * (100 - modPlayer.bowCharge));
                                    offset.Y += (float)(Math.Cos(angle) * (100 - modPlayer.bowCharge));

                                    if (modPlayer.overCharge2 > 0)
                                    {
                                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.FireworkFountain_Pink, player.velocity, 200, default(Color), 0.8f);
                                        d2.fadeIn = 0.1f;
                                        d2.noGravity = true;
                                    }
                                    else
                                    {
                                        if (modPlayer.overCharge1 > 0)
                                        {
                                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.Clentaminator_Purple, player.velocity, 200, default(Color), 0.5f);
                                            d2.fadeIn = 0.1f;
                                            d2.noGravity = true;
                                        }
                                        else
                                        {
                                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, DustID.GemTopaz, player.velocity, 200, default(Color), 0.3f);
                                            d2.fadeIn = 0.1f;
                                            d2.noGravity = true;
                                        }
                                    }


                                }
                                //Charge dust
                                Vector2 vector = new Vector2(
                                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                            }
                            else
                            {
                                //Charge dust
                                Vector2 vector = new Vector2(
                                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                                //If charge is above 100, start Overcharging
                                modPlayer.overCharge1 += 0.5f;
                                //Debug
                                // modPlayer.overCharge1 += 4.5f;
                                if (modPlayer.overCharge1 >= 100 && modPlayer.AuxiliaryGuns.Count > 0)
                                {
                                    modPlayer.overCharge2 += 0.5f;
                                    //modPlayer.overCharge2 += 4.5f;
                                }
                            }
                        }
                        else
                        {
                            //When the charge is released
                            Item.useTime = 1;
                            Item.useAnimation = 1;
                            modPlayer.bowChargeActive = false;

                            modPlayer.bowCharge = 0;
                            modPlayer.overCharge1 = 0;
                            modPlayer.overCharge2 = 0;
                        }
                    }

                }
            }
            

        }

        private static void ChargeSpeed(Player player)
        {
            var modPlayer = player.GetModPlayer<WeaponPlayer>();
            modPlayer.bowCharge += 5f;

        }

        public override bool? UseItem(Player player)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
            if (player.altFunctionUse == 2)
            {
                Vector2 dustPosition = player.Center;

                if (!inheritedCaseActive && !player.HasBuff(BuffType<ComboCooldown>()))
                {
                    SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, player.position);

                    float dustAmount = 40f;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 1f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = dustPosition + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + MathHelper.ToRadians(90));
                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 1f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = dustPosition + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                    }
                    inheritedCaseActive = true;
                    player.AddBuff(BuffType<ComboCooldown>(), 10);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, player.position);

                    float dustAmount = 40f;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 1f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = dustPosition + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + MathHelper.ToRadians(90));
                        int dust = Dust.NewDust(dustPosition, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 1f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = dustPosition + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                    }
                    inheritedCaseActive = false;
                    player.AddBuff(BuffType<ComboCooldown>(), 10);
                }
                
			}
            else
            {
                Vector2 dustPosition = player.Center;
               

            }


            return base.UseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 130f;




            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                  .AddIngredient(ItemID.FragmentVortex, 30)
                  .AddIngredient(ItemType<EssenceOfTheRifle>())
                  .AddTile(TileID.Anvils)
                  .Register();

        }
    }
}
