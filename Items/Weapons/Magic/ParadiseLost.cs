using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Magic.ParadiseLost;
using StarsAbove.Buffs.Magic.SupremeAuthority;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Magic.EternalStar;
using StarsAbove.Projectiles.Magic.ParadiseLost;
using StarsAbove.Projectiles.Magic.SupremeAuthority;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class ParadiseLost : ModItem
    {
        public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 230;           //The damage of your weapon
            Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
            Item.mana = 0;
            Item.width = 40;            //Weapon's texture's width
            Item.height = 40;           //Weapon's texture's height
            Item.useTime = 40;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 40;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
            Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
            Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            Item.noUseGraphic = true;
            Item.shoot = ProjectileType<EmblazonedCitrine>();
            Item.shootSpeed = 15f;
            Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
        }
        int activateSwordstormTimer;
        Vector2 savedSwordstormPosition;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {


            }
            return base.CanUseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public override void HoldItem(Player player)
        {
            //Test
            player.AddBuff(BuffType<ParadiseLostBuff>(), 2);
            if (player.ownedProjectileCounts[ProjectileType<ParadiseLostProjectile>()] < 1)
            {

                int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ParadiseLostProjectile>(), 0, 4, player.whoAmI, 0f);
                Main.projectile[index].originalDamage = Item.damage;

            }

            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                //Spawn the eye. After a short delay it consumes all marked NPCs and grants Deified. Logic goes there.
                if (!player.HasBuff(BuffType<DeifiedBuff>()))
                {

                    if (player.ownedProjectileCounts[ProjectileType<SupremeAuthorityEye>()] < 1)
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_AbsoluteEye, player.Center);

                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<SupremeAuthorityEye>(), 0, 0, player.whoAmI, 0f);


                    }
                }
                else
                {
                    //If Deified, continue the buff but inflict the debuff
                    player.AddBuff(BuffType<DeifiedBuff>(), 60 * 60);
                    player.AddBuff(BuffType<AtrophiedDeifiedBuff>(), 60 * 60);
                    if (player.statLife / 2 > 1)
                    {
                        player.statLife /= 2;
                    }
                    else
                    {
                        player.statLife = 1;
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_AbsoluteEye, player.Center);
                    for (int d1 = 0; d1 < 15; d1++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-17, 17), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);
                        Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);


                    }
                    player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;

                }

            }
            if (player.HasBuff(BuffType<DeifiedBuff>()))
            {
                Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-3, 3), 150, default(Color), 0.4f);

            }
            if (activateSwordstormTimer == 1)
            {
                int unifiedRandom = (int)MathHelper.ToRadians(Main.rand.Next(0, 364));
                //Swordstorm
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), savedSwordstormPosition.X, savedSwordstormPosition.Y, 0, 0, ProjectileType<AuthoritySwordstorm>(), player.GetWeaponDamage(Item) + (int)MathHelper.Min(200, (player.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs * 5)), 0, player.whoAmI, 0f, unifiedRandom);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), savedSwordstormPosition.X, savedSwordstormPosition.Y, 0, 0, ProjectileType<AuthoritySwordstormVFX>(), 0, 0, player.whoAmI, 0f, unifiedRandom);

                float dustAmount = 28f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), savedSwordstormPosition.X, savedSwordstormPosition.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), ProjectileType<SwordstormEffect>(), 0, 0, player.whoAmI, 0f);

                }

            }
            base.HoldItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.whoAmI == Main.myPlayer)
            {
                if (!player.HasBuff(BuffType<DeifiedBuff>()))
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.friendly && npc.townNPC && npc.Distance(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) < 60)
                        {
                            npc.AddBuff(BuffType<AuthoritySacrificeMark>(), 18000);

                        }
                    }
                    float dustAmount = 24f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 14f);
                        //spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = player.Center + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 15f;
                    }
                    SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, player.Center);

                }
                else
                {
                    if (player.GetModPlayer<WeaponPlayer>().SupremeAuthorityEncroachingStacks >= 2)
                    {
                        activateSwordstormTimer = 20;

                        SoundEngine.PlaySound(StarsAboveAudio.SFX_DisappearPrep, player.Center);
                        savedSwordstormPosition = Main.MouseWorld;
                        player.GetModPlayer<WeaponPlayer>().SupremeAuthorityEncroachingStacks = 0;
                    }
                }

            }
            return base.UseItem(player);
        }

        bool altSwing = false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (player.direction == 1)
                {
                    if (altSwing)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing2>(), damage, knockback, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX2>(), 0, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, player.Center);

                        altSwing = false;
                    }
                    else
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing1>(), damage, knockback, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX1>(), 0, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, player.Center);

                        altSwing = true;
                    }

                }
                else
                {
                    if (altSwing)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing1>(), damage, knockback, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX1>(), 0, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, player.Center);

                        altSwing = false;
                    }
                    else
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing2>(), damage, knockback, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX2>(), 0, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, player.Center);


                        altSwing = true;
                    }
                }

                return false;

            }
            else
            {
                return false;

            }
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.TopazStaff, 1)
                .AddIngredient(ItemID.Silk, 8)
                .AddIngredient(ItemID.NebulaLantern, 1)
                .AddIngredient(ItemID.SoulofMight, 12)
                .AddIngredient(ItemID.LunarBar, 5)
                .AddIngredient(ItemType<EssenceOfAuthority>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }


}
