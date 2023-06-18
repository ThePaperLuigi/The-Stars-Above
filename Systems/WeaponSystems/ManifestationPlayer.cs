using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;
using StarsAbove.Items.Consumables;
using StarsAbove.UI.StellarNova;
using SubworldLibrary;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Projectiles.SkyStriker;
using StarsAbove.Buffs.CosmicDestroyer;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.Buffs.AshenAmbition;
using StarsAbove.Biomes;
using StarsAbove.Buffs.TheOnlyThingIKnowForReal;
using StarsAbove.Buffs.VermillionDaemon;
using StarsAbove.Subworlds;
using StarsAbove.Buffs.Ozma;
using StarsAbove.Prefixes;
using StarsAbove.UI.StarfarerMenu;
using StarsAbove.Buffs.StarfarerAttire;
using StarsAbove.Buffs.HunterSymphony;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Buffs.TagDamage;
using StarsAbove.Buffs.BurningDesire;
using StarsAbove.Utilities;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Buffs.BlackSilence;
using StarsAbove.Projectiles.BlackSilence;
using StarsAbove.Items.Armor.BlackSilence;
using StarsAbove.Items.Armor.Manifestation;
using StarsAbove.Buffs.Manifestation;

namespace StarsAbove
{
    public class ManifestationPlayer : ModPlayer
    {
        public bool manifestationHeld;

        public int emotionGauge;
        public int emotionGaugeMax = 100;

        public int emotionGaugeDecayTimer;

        public int timeSpentInEGO;

        public bool EGOManifested;
        public int EGOLossTimer;

        public float gaugeChangeAlpha = 0f;

        public int greaterSplitTimer;//This decays to 0 once a greater split is executed
        public float greaterSplitAlpha = 0f;

        public int greatSplitHorizontalTimer;
        public float greatSplitHorizontalAlpha = 0f;

        public float greatSplitAnimationRotation;
        public float greatSplitAnimationRotationTimer;

        public override void PreUpdate()
        {
            if (Player.HasBuff(BuffType<EGOManifestedBuff>()) && Player.whoAmI == Main.myPlayer)
            {
                //Dust within EGO form
                int dustIndex = Dust.NewDust(new Vector2(Player.position.X, Player.Center.Y + Player.height / 2), Player.width, 0, DustID.LifeDrain, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1.3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 1f;
                //Dust.NewDust(new Vector2(Player.position.X, Player.Center.Y + Player.height / 2), Player.width, 0, DustID.LifeDrain, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);
                //Dust.NewDust(new Vector2(Player.position.X, Player.Center.Y + Player.height / 2), Player.width, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Player.width * 0.6f, Player.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.9f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
                for (int d = 0; d < 6; d++)
                {
                    Dust dust = Dust.NewDustPerfect(Player.Center + direction * distance, DustID.LifeDrain, velocity);
                    dust.scale = 0.5f;
                    dust.fadeIn = 1.1f;
                    dust.noGravity = true;
                    dust.noLight = true;
                    //dust.color = Color.Purple;
                    dust.alpha = 0;
                }
               
            }

            emotionGaugeDecayTimer--;
            greaterSplitTimer--;
            greatSplitHorizontalTimer--;

            if(!manifestationHeld)
            {
                emotionGauge = 0;
            }

            gaugeChangeAlpha -= 0.1f;

            greaterSplitAlpha = Math.Clamp(greaterSplitAlpha, 0f, 1f);
            greaterSplitAlpha -= 0.1f;
            if(greaterSplitTimer > 0)
            {
                greaterSplitAlpha += 0.1f;
            }

            greatSplitHorizontalAlpha = Math.Clamp(greatSplitHorizontalAlpha, 0f, 1f);
            greatSplitHorizontalAlpha -= 0.1f;
            if (greatSplitHorizontalTimer > 0)
            {
                greatSplitHorizontalAlpha += 0.1f;
            }


            if(greatSplitHorizontalTimer > 30)
            {
                greatSplitAnimationRotationTimer += 0.02f;

            }
            else
            {
                greatSplitAnimationRotationTimer += 0.1f;

            }
            greatSplitAnimationRotation = MathHelper.Lerp(20, -20, greatSplitAnimationRotationTimer);
           

            if (Player.HasBuff(BuffType<EGOManifestedBuff>()))
            {
                timeSpentInEGO++;
                if(emotionGaugeDecayTimer < 0)
                {
                    emotionGauge -= 1 ;
                    emotionGaugeDecayTimer = 20 - timeSpentInEGO / 120;
                }
            }
            else
            {
                timeSpentInEGO = 0;
            }
            if(emotionGauge >= emotionGaugeMax && !EGOManifested)
            {
                if(Main.netMode != NetmodeID.Server)
                {
                    SoundEngine.PlaySound(SoundID.Roar, Player.Center);
                    Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 44; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, 0, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 26; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.Firework_Red, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 50; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                }

                Player.AddBuff(BuffType<Invincibility>(), 120);
                EGOManifested = true; //Enter E.G.O.
                //emotionGauge = emotionGaugeMax/2;//Halve gauge.
            }
            if(EGOManifested && emotionGauge < 10)
            {
                EGOLossTimer++;
            }
            else
            {
                EGOLossTimer = 0;
            }
            if(EGOLossTimer > 180)//If time spent below 10% is too long, end EGO.
            {
                EGOManifested = false;
                if (Main.netMode != NetmodeID.Server)
                {
                    SoundEngine.PlaySound(SoundID.Shatter, Player.Center);
                    
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 44; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.Clentaminator_Red, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                    
                }
               
                Player.AddBuff(BuffType<Vulnerable>(), 600);
            }
            
            //if EGO is manifested, apply the buff.
            if(EGOManifested)
            {
                Player.AddBuff(BuffType<EGOManifestedBuff>(), 10);
            }

            emotionGauge = Math.Clamp(emotionGauge, 0, emotionGaugeMax);
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Player.HasBuff(BuffType<EGOManifestedBuff>()))
            {
                Player.maxRunSpeed *= 1.9f;
                Player.accRunSpeed *= 1.9f;
               

            }
        }
        
        public override void OnHurt(Player.HurtInfo info)
        {
            
        }
        
        public override void PreUpdateBuffs()
        {
           
        }
        public override void PostUpdate()
        {
            
        }
        public override void ResetEffects()
        {
            manifestationHeld = false;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (manifestationHeld)
            {
               
            }
            gaugeChangeAlpha = 1f;

            emotionGauge += 2;
        }
        public override void FrameEffects()
        {
            
            if (Player.active && !Player.dead)
            {
                if (Player.HasBuff(BuffType<EGOManifestedBuff>()))
                {

                    Player.head = EquipLoader.GetEquipSlot(Mod, "RedMistHead", EquipType.Head);
                    Player.body = EquipLoader.GetEquipSlot(Mod, "RedMistBody", EquipType.Body);
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "RedMistLegs", EquipType.Legs);


                    //Player.UpdateVisibleAccessories(new Item(ModContent.ItemType<ManifestationCape>()), false);
                    //Player.UpdateVisibleAccessories(new Item(ModContent.ItemType<RedMistGloves>()), false);

                }


            }

            base.FrameEffects();

        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            
            base.ModifyDrawInfo(ref drawInfo);
        }
        
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if(manifestationHeld)
            {
                emotionGauge++;
                gaugeChangeAlpha = 1f;
                if(hit.Crit)
                {
                    if(Player.statLife < 200)
                    {
                        emotionGauge++;
                    }
                    for (int d = 0; d < 22; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-19, 19), Main.rand.NextFloat(-3, 3), 50, default(Color), 1.3f);

                    }
                    for (int d = 0; d < 28; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustID.Blood, Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-2, 2), 0, default(Color), 1.4f);

                    }

                }
                for (int d = 0; d < 18; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 1f);

                }
                for (int d = 0; d < 18; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, DustID.Blood, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4), 0, default(Color), 1f);

                }
                if(!target.active) //On kill
                {
                    for (int d = 0; d < 22; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-13, 13), 50, default(Color), 1.3f);

                    }
                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustID.Blood, Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-9, 9), 0, default(Color), 1.4f);

                    }

                    emotionGauge += 5;
                    if (Player.statLife < 200)
                    {
                        emotionGauge += 5;
                    }
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            if(manifestationHeld)
            {
                modifiers.CritDamage += 1.2f;
            }

        }



    }

};