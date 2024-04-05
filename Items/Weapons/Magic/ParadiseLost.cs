using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Boss;
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
            Item.DamageType = ModContent.GetInstance<Systems.PsychomentDamageClass>();
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
            if(player.GetModPlayer<WeaponPlayer>().paradiseLostAnimationTimer > 0)
            {
                return false;
            }
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
            if(!player.GetModPlayer<WeaponPlayer>().paradiseLostActive)
            {
                player.AddBuff(BuffType<PlagueProphetBuff>(), 2);
            }
            

            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                if (!player.HasBuff(BuffType<ParadiseLostBuff>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, player.Center);

                    player.GetModPlayer<WeaponPlayer>().paradiseLostActive = true;
                    player.AddBuff(BuffType<ParadiseLostBuff>(), 2);
                    player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                    if (player.ownedProjectileCounts[ProjectileType<ParadiseLostProjectile>()] < 1)
                    {

                        int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ParadiseLostProjectile>(), 0, 4, player.whoAmI, 0f);
                        Main.projectile[index].originalDamage = Item.damage;

                    }
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.friendly && npc.townNPC && npc.HasBuff(BuffType<BaptisedBuff>()))
                        {
                            npc.AddBuff(BuffType<ApostleBuff>(), 18000);
                            npc.position = player.position;

                        }
                    }
                }
                else
                {
                    player.AddBuff(BuffType<Vulnerable>(), 60*60);

                    player.GetModPlayer<WeaponPlayer>().paradiseLostActive = false;
                    player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                    //End Paradise Lost
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.friendly && npc.townNPC && npc.HasBuff(BuffType<ApostleBuff>()))
                        {
                            npc.DelBuff(npc.FindBuffIndex(BuffType<ApostleBuff>()));

                        }
                    }
                }

            }

            base.HoldItem(player);
        }

        public override bool? UseItem(Player player)
        {
            
            if (player.altFunctionUse == 2)
            {
                if (player.HasBuff(BuffType<ParadiseLostBuff>()))
                {
                    player.GetModPlayer<WeaponPlayer>().paradiseLostAnimationTimer = 1f;
                    //Advent

                }
                else
                {
                    if (player.whoAmI == Main.myPlayer)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && npc.friendly && npc.townNPC && npc.Distance(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) < 60)
                            {
                                npc.AddBuff(BuffType<BaptisedBuff>(), 18000);

                            }
                        }
                        
                    }
                    float dustAmount = 24f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 14f);
                        //spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = player.Center + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 15f;
                    }
                    SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, player.Center);
                }

            }
            else
            {
                if (player.HasBuff(BuffType<ParadiseLostBuff>()))
                {
                    if (player.whoAmI == Main.myPlayer)
                    {
                        int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<ParadiseLostAttack>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

                    }
                    //generic attack

                }
                else
                {

                    //nothing
                }
            }
            return base.UseItem(player);
        }

        bool altSwing = false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                

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
                
                .AddTile(TileID.Anvils)
                .Register();
        }
    }


}
