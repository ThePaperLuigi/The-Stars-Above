using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.DragaliaFound;
using StarsAbove.Items.Essences;
using StarsAbove.Mounts.DragaliaFound;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Projectiles.Summon.DragaliaFound;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class DragaliaFound : ModItem
    {
        public override void SetStaticDefaults()
        {


            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 90;           //The damage of your weapon
            Item.DamageType = DamageClass.SummonMeleeSpeed;          //Is your weapon a melee weapon?
            Item.width = 38;            //Weapon's texture's width
            Item.height = 132;           //Weapon's texture's height
            Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
            Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
            Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
            Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileType<Projectiles.Summon.PhantomInTheMirror.PhantomInTheMirrorProjectile>();
            Item.shootSpeed = 3f;
            Item.value = Item.buyPrice(gold: 1);
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
            if (!player.HasBuff(BuffType<DragonshiftActiveBuff>()) && player.altFunctionUse == 2)
            {
                return false;
            }
            return base.CanUseItem(player);
        }


        internal static void Teleport(Player player)
        {

        }
        public override void HoldItem(Player player)
        {
            if (NPC.downedSlimeKing)
            {
                Item.damage = 15;
            }
            if (NPC.downedBoss1)
            {
                Item.damage = 16;
            }
            if (NPC.downedBoss2)
            {
                Item.damage = 17;
            }
            if (NPC.downedQueenBee)
            {
                Item.damage = 18;
            }
            if (NPC.downedBoss3)
            {
                Item.damage = 29;
            }
            if (Main.hardMode)
            {
                Item.damage = 32;
            }
            if (NPC.downedMechBossAny)
            {
                Item.damage = 40;
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                Item.damage = 50;
            }
            if (NPC.downedPlantBoss)
            {
                Item.damage = 70;
            }
            if (NPC.downedGolemBoss)
            {
                Item.damage = 80;
            }
            if (NPC.downedFishron)
            {
                Item.damage = 90;
            }
            if (NPC.downedAncientCultist)
            {
                Item.damage = 111;
            }
            if (NPC.downedMoonlord)
            {
                Item.damage = 151;
            }

            player.GetModPlayer<WeaponPlayer>().DragaliaFoundHeld = true;
            player.AddBuff(BuffType<TempestDragonlightBuff>(), 10);
            attackComboCooldown--;
            if (attackComboCooldown <= 0)
            {
                attackType = 0;
            }
            if (attackType == 3)
            {
                //The stab is faster
                player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
            }
            if (Main.myPlayer == player.whoAmI)
            {
                if (StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<DragonshiftActiveBuff>()) && player.GetModPlayer<WeaponPlayer>().DragonshiftGauge >= 50)
                {
                    player.GetModPlayer<StarsAbovePlayer>().WhiteFade = 20;
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<DragonArm>(), 0, 0, player.whoAmI);

                    player.mount.SetMount(MountType<DragonshiftMount>(), player);
                    player.AddBuff(BuffType<DragonshiftActiveBuff>(), 240);
                    player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;
                    //Boom
                    SoundEngine.PlaySound(SoundID.Roar, player.Center);
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.MountedCenter, Vector2.Zero, ProjectileType<radiate>(), 0, 0, player.whoAmI);

                    for (int d = 0; d < 50; d++)
                    {
                        Dust.NewDust(player.MountedCenter, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 54; d++)
                    {
                        Dust.NewDust(player.MountedCenter, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 54; d++)
                    {
                        Dust.NewDust(player.MountedCenter, 0, 0, DustID.GreenFairy, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 2.5f);
                    }
                    float dustAmount = 120f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(player.MountedCenter, 0, 0, DustID.GemEmerald);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = player.MountedCenter + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 30f;
                    }
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(player.MountedCenter, 0, 0, DustID.GemEmerald);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = player.MountedCenter + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 40f;
                    }
                }

            }
            base.HoldItem(player);
        }


        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        int attackType;
        int attackComboCooldown;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            attackComboCooldown = 30;
            if (player.altFunctionUse == 2)
            {
                if (!player.HasBuff(BuffType<DragonshiftSpecialAttackCooldownBuff>()))
                {
                    player.AddBuff(BuffType<DragonshiftSpecialAttackCooldownBuff>(), 60 * 6);

                    player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                    //Boom
                    SoundEngine.PlaySound(SoundID.Roar, player.Center);
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.MountedCenter, Vector2.Zero, ProjectileType<fastRadiate>(), 0, 0, player.whoAmI);
                    for (int ir = 0; ir < Main.maxNPCs; ir++)
                    {
                        NPC npc = Main.npc[ir];
                        if (npc.active && npc.CanBeChasedBy() && !npc.HasBuff(BuffType<DragonshiftSpecialAttackCooldownBuff>()) && npc.Distance(player.Center) < 600)
                        {
                            npc.AddBuff(BuffType<DragonshiftSpecialAttackCooldownBuff>(), 180);
                            Projectile.NewProjectile(player.GetSource_FromThis(), npc.Center, Vector2.Zero, ProjectileType<DragonTornado>(), damage / 2, 0, player.whoAmI, 0);

                            for (int d = 0; d < 20; d++)
                            {
                                Dust.NewDust(npc.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                            }
                            for (int d = 0; d < 24; d++)
                            {
                                Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1f);
                            }
                            for (int d = 0; d < 24; d++)
                            {
                                Dust.NewDust(npc.Center, 0, 0, DustID.GreenFairy, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1f);
                            }
                            float dustAmount = 40f;
                            for (int i = 0; (float)i < dustAmount; i++)
                            {
                                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                                spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                                int dust = Dust.NewDust(npc.Center, 0, 0, DustID.GemEmerald);
                                Main.dust[dust].scale = 2f;
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].position = npc.Center + spinningpoint5;
                                Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                            }
                            for (int i = 0; (float)i < dustAmount; i++)
                            {
                                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                                spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                                int dust = Dust.NewDust(npc.Center, 0, 0, DustID.GemEmerald);
                                Main.dust[dust].scale = 2f;
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].position = npc.Center + spinningpoint5;
                                Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 20f;
                            }
                        }
                    }



                }
            }
            else
            {
                if (player.HasBuff(BuffType<DragonshiftActiveBuff>()))
                {
                    switch (attackType)
                    {
                        case 0: //Swing downwards
                            Projectile.NewProjectile(source, player.Center, velocity * 2, ProjectileType<DragaliaFoundDragonAttack>(), damage * 2, knockback * 3, player.whoAmI, -1, player.itemTimeMax, 1.5f);
                            attackType++;
                            player.velocity = velocity * 2f;
                            return false;
                        case 1: //Swing upwards
                            Projectile.NewProjectile(source, player.Center, velocity * 2, ProjectileType<DragaliaFoundDragonAttack>(), damage * 2, knockback * 3, player.whoAmI, 1, player.itemTimeMax, 1.5f);
                            attackType = 0;
                            player.velocity = velocity * 2f;


                            return false;
                    }
                }
                else
                {
                    switch (attackType)
                    {
                        case 0: //Swing downwards
                            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                            attackType++;
                            return false;
                        case 1: //Swing upwards
                            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
                            attackType++;
                            player.velocity.Y -= 4;
                            return false;
                        case 2: //Swing down again but faster + prep stab
                            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSwordRecoil>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                            attackType++;
                            return false;
                        case 3: //Stab					
                            Projectile.NewProjectile(source, player.Center, Vector2.Normalize(velocity), ProjectileType<DragaliaFoundStab>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                            player.velocity += velocity * 3f;
                            float num10 = 16f;
                            for (int num11 = 0; (float)num11 < num10; num11++)
                            {
                                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num11 * ((float)Math.PI * 2f / num10)) * new Vector2(1f, 4f);
                                spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                                int num13 = Dust.NewDust(player.Center, 0, 0, DustID.TintableDustLighted);
                                Main.dust[num13].color = Color.SkyBlue;
                                Main.dust[num13].scale = 2f;
                                Main.dust[num13].noGravity = true;
                                Main.dust[num13].position = player.Center + spinningpoint5;
                                Main.dust[num13].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
                            }
                            player.AddBuff(BuffType<Invincibility>(), 30);
                            attackType++;
                            return false;
                        case 4: //Spin
                            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragaliaFoundSwordSpin>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                            player.AddBuff(BuffType<BondforgedBladeBuff>(), 180);
                            attackType = 0;
                            return false;
                    }
                }

                if (attackType > 4)
                {
                    attackType = 0;
                }
            }

            /**/
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.Obsidian, 12)
                .AddIngredient(ItemID.Feather, 8)
                .AddIngredient(ItemID.BladeofGrass, 1)
                .AddIngredient(ItemType<EssenceOfTheDragon>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
