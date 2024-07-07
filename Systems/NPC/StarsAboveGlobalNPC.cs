
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using StarsAbove.NPCs.OffworldNPCs;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using SubworldLibrary;
using StarsAbove.Subworlds;
using StarsAbove.Items.Prisms;
using StarsAbove.Items.Consumables;
using StarsAbove.Items.Accessories;
using System;
using StarsAbove.Biomes;
using StarsAbove.Items.Materials;
using StarsAbove.NPCs;
using StarsAbove.Buffs.Boss;
using StarsAbove.NPCs.Vagrant;
using Microsoft.CodeAnalysis;
using StarsAbove.Items.Memories;
using StarsAbove.Projectiles.Summon.StarphoenixFunnel;
using Terraria.GameContent.Drawing;
using Terraria.Audio;
using StarsAbove.Buffs.Magic.CloakOfAnArbiter;
using StarsAbove.Buffs.Ranged.StringOfCurses;
using StarsAbove.Buffs.Magic.IrminsulDream;
using StarsAbove.Buffs.Other.Farewells;
using StarsAbove.Systems;
using StarsAbove.Buffs.Magic.ParadiseLost;
using Terraria.GameContent;
using StarsAbove.NPCs.OffworldNPCs.Caelum;
using StarsAbove.Subworlds.ThirdRegion;
using StarsAbove.NPCs.NeonVeil;
using StarsAbove.Projectiles.Melee.Umbra;
using StarsAbove.Projectiles.Ranged.QuisUtDeus;
using StarsAbove.Buffs.Other.Phasmasaber;

namespace StarsAbove.Systems
{
    public class StarsAboveGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool NanitePlague;
        public bool OceanCulling;
        public bool Petrified;
        public bool Riptide;
        public bool Starblight;
        public bool RyukenStun;
        public bool voidAtrophy1;
        public bool voidAtrophy2;
        public bool ruination;
        public bool MortalWounds;
        public bool Glitterglue;
        public bool InfernalBleed;
        public bool Hyperburn;
        public bool KarmicRetribution;
        public bool VerdantEmbrace;
        public bool AuthoritySacrificeMark;
        public int NanitePlagueLevel = 0;
        public int JudgementStacks = 0;
        public int spectralNailStacks = 0;
        public int elementalSurgeStacks = 0;
        public int quisUtDeusStacks = 0;

        public int completeCombustionStacks = 0;

        int dustTimer = 0;
        public override void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
        {
            if(npc.HasBuff(BuffType<ApostleBuff>()))
            {
                damage += 50;
            }
            base.TownNPCAttackStrength(npc, ref damage, ref knockback);
        }
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome<NeonVeilBiome>())
            {
                pool.Clear();
                pool.Add(NPCType<NPCs.PrismLoot>(), 0.01f);
                pool.Add(NPCType<NPCs.OffworldNPCs.AmethystHeadpiercer>(), 0.02f);
                pool.Add(NPCID.BlackSlime, 0.5f);
                pool.Add(NPCType<NPCs.OffworldNPCs.AmethystSwordsinner>(), 0.02f);
                pool.Add(NPCType<NPCs.OffworldNPCs.AsteroidWormHead>(), 0.01f);

                pool.Add(NPCType<SemaphoreEnemy>(), 0.7f);
                pool.Add(NPCType<LogicVirusEnemy>(), 0.7f);


                if (!NPC.AnyNPCs(NPCType<NPCs.TownNPCs.Garridine>()))
                {
                    pool.Add(NPCType<NPCs.TownNPCs.Garridine>(), 0.1f);

                }

            }
            
           
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.HasBuff(BuffType<BossEnemySpawnMod>()))
            {
                spawnRate *= 5;
                maxSpawns = (int)(maxSpawns * 0.001f);
            }
            if (player.HasBuff(BuffType<OffSeersPurpose>()))
            {
                spawnRate = (int)(spawnRate * 0.6);
                maxSpawns = (int)(maxSpawns * 2.5f);
            }
            if (player.HasBuff(BuffType<Conversationalist>()))
            {
                spawnRate = (int)(spawnRate * 3);
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (NanitePlagueLevel > 20)
            {
                NanitePlagueLevel = 20;
            }
            if (NanitePlague)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 16;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
            if (Starblight)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;

                damage = 30;

            }
            if (VerdantEmbrace)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 4;

                damage = 4;

                if (npc.HasBuff(BuffID.OnFire))
                {

                    npc.DelBuff(npc.FindBuffIndex(BuffID.OnFire));
                    npc.DelBuff(npc.FindBuffIndex(BuffType<VerdantEmbrace>()));

                    if (npc.life > Math.Min((int)(npc.lifeMax * 0.1), 120))
                    {
                        npc.life -= Math.Min((int)(npc.lifeMax * 0.1), 120);
                    }
                    else
                    {
                        npc.life = 1;
                    }

                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(230, 164, 164, 240), $"{Math.Min((int)(npc.lifeMax * 0.1), 120)}", false, false);
                    return;
                }
                if (npc.HasBuff(BuffID.Frostburn))
                {
                    npc.DelBuff(npc.FindBuffIndex(BuffID.Frostburn));
                    npc.DelBuff(npc.FindBuffIndex(BuffType<VerdantEmbrace>()));

                    if (npc.life > Math.Min((int)(npc.lifeMax * 0.1), 120))
                    {
                        npc.life -= Math.Min((int)(npc.lifeMax * 0.1), 120);
                    }
                    else
                    {
                        npc.life = 1;
                    }

                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(164, 220, 230, 240), $"{Math.Min((int)(npc.lifeMax * 0.03), 120)}", false, false);
                    return;
                }
                if (npc.HasBuff(BuffID.CursedInferno))
                {
                    npc.DelBuff(npc.FindBuffIndex(BuffType<VerdantEmbrace>()));

                    npc.DelBuff(npc.FindBuffIndex(BuffID.CursedInferno));
                    if (npc.life > Math.Min((int)(npc.lifeMax * 0.1), 120))
                    {
                        npc.life -= Math.Min((int)(npc.lifeMax * 0.1), 120);
                    }
                    else
                    {
                        npc.life = 1;
                    }
                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(193, 230, 164, 240), $"{Math.Min((int)(npc.lifeMax * 0.03), 120)}", false, false);
                    return;
                }
                if (npc.HasBuff(BuffID.ShadowFlame))
                {
                    npc.DelBuff(npc.FindBuffIndex(BuffType<VerdantEmbrace>()));

                    npc.DelBuff(npc.FindBuffIndex(BuffID.ShadowFlame));
                    if (npc.life > Math.Min((int)(npc.lifeMax * 0.1), 120))
                    {
                        npc.life -= Math.Min((int)(npc.lifeMax * 0.1), 120);
                    }
                    else
                    {
                        npc.life = 1;
                    }

                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(211, 164, 230, 240), $"{Math.Min((int)(npc.lifeMax * 0.03), 120)}", false, false);
                    return;
                }
            }
            if (Hyperburn)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;

                damage = 30;

            }

            if (npc.HasBuff(BuffType<SpiritflameDebuff>()))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;

                damage = 30;

            }
            if (KarmicRetribution)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 10;

                damage = 1;

            }
            if (InfernalBleed)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 60;

                damage = 60;

            }
            if (ruination)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 2;

                damage = 2;

            }
            if (voidAtrophy2)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 100;

                damage = 400;

            }
            if (voidAtrophy1)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 100;

                damage = 200;

            }

        }
        public override void PostAI(NPC npc)
        {
            if((npc.position.Y/16 + npc.height) > (Main.maxTilesY - 86))
            {
                npc.velocity.Y -= 10f;
            }
            base.PostAI(npc);
        }
        public override void ResetEffects(NPC npc)
        {
            OceanCulling = false;
            MortalWounds = false;
            NanitePlague = false;
            Petrified = false;
            Riptide = false;
            Starblight = false;
            voidAtrophy1 = false;
            voidAtrophy2 = false;
            RyukenStun = false;
            Glitterglue = false;
            InfernalBleed = false;
            VerdantEmbrace = false;
            AuthoritySacrificeMark = false;
            Hyperburn = false;
            KarmicRetribution = false;
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if(npc.HasBuff(BuffType<ApostleBuff>()))
            {
                // This is where we specify which way to flip the sprite. If the npc is moving to the left, then flip it vertically.
                SpriteEffects spriteEffects = npc.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                // Getting texture of npc
                Rectangle sourceRectangle;
                Vector2 origin;
                sourceRectangle = new Rectangle((int)npc.Center.X, (int)npc.Center.Y, 260, 260);
                origin = sourceRectangle.Size() / 2f;

                Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
                Vector2 drawOrigin = new Vector2(npc.width * 0.5f, npc.height * 0.5f);
                int r1 = (int)color1.R;
                //drawOrigin.Y += 34f;
                //drawOrigin.Y += 8f;
                --drawOrigin.X;
                Vector2 position1 = npc.Center - Main.screenPosition;
                Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostVFX2");
                Texture2D VFX1 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Effects/ParadiseLostVFX");

                float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 1.0 / 1.0);
                float num12 = num11;
                if ((double)num12 > 0.5)
                    num12 = 1f - num11;
                if ((double)num12 < 0.0)
                    num12 = 0.0f;
                float num13 = (float)(((double)num11 + 0.5) % 1.0);
                float num14 = num13;
                if ((double)num14 > 0.5)
                    num14 = 1f - num13;
                if ((double)num14 < 0.0)
                    num14 = 0.0f;
                Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
                drawOrigin = r2.Size() / 2f;
                Vector2 position3 = position1 + new Vector2(0.0f, -10f);
                Microsoft.Xna.Framework.Color color3 = new Color(255, 0, 0, 100);
                float magicFade = 1f + num11 * 0.25f;

                Main.spriteBatch.Draw(VFX1, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, 0, drawOrigin, 1f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

                float num15 = 1f + num11 * 0.45f;
                Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, 0, drawOrigin, 0.5f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

                float original16 = 1f + num13 * 0.45f;

                float num16 = -1f + num13 * -0.15f;
                Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, 0, drawOrigin, 0.5f * original16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);


            }

            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (SubworldSystem.Current == null)
            {
                if (EverlastingLightEvent.isEverlastingLightActive && !npc.boss && npc.damage > 0 && !npc.friendly)
                {


                    npc.color = Color.LightGoldenrodYellow;
                }
            }
            if (Hyperburn)
            {
                npc.color = Color.Pink;
            }
            if (VerdantEmbrace)
            {
                npc.color = Color.Green;
            }
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {


            return base.GetAlpha(npc, drawColor);
        }

        public override void AI(NPC npc)
        {
            if (EverlastingLightEvent.isEverlastingLightActive && !npc.boss && npc.damage > 0 && !npc.friendly && SubworldSystem.Current == null)
            {

                if (Main.rand.Next(2000) < 2)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        // Random upward vector.
                        Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-1, -4));
                        if (Main.netMode != NetmodeID.MultiplayerClient) { Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, vel, ProjectileID.GreekFire3, 40, 0, 0, 0, 1); }
                    }


                    npc.life += npc.lifeMax / 10;
                    if (npc.life > npc.lifeMax)
                    {
                        npc.life = npc.lifeMax;
                    }
                }


                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 158, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                }
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 91, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.32f;



                }

                Lighting.AddLight(npc.position, 0.1f, 0.1f, 0.1f);

            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (SubworldSystem.Current == null)
            {
                if (EverlastingLightEvent.isEverlastingLightActive && !npc.boss && npc.damage > 0 && !npc.friendly)
                {



                    drawColor = drawColor.MultiplyRGB(Color.Yellow);
                }
            }
            if (npc.HasBuff(BuffType<ApostleBuff>()))
            {
                //drawColor = drawColor.MultiplyRGBA;

            }
            if (npc.HasBuff(BuffType<Necrosis>()))
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Bone, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (npc.HasBuff(BuffType<FairyTagDamage>()))
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Enchanted_Gold, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (npc.HasBuff(BuffType<Ensnared>()))
            {
                for (int i3 = 0; i3 < 10; i3++)
                {

                    Dust d = Main.dust[Dust.NewDust(new Vector2(npc.Center.X - npc.width, npc.Center.Y - npc.height / 3), npc.width * 2, 0, DustID.GoldFlame, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
                for (int i3 = 0; i3 < 10; i3++)
                {

                    Dust d = Main.dust[Dust.NewDust(new Vector2(npc.Center.X - npc.width, npc.Center.Y - npc.height / 4), npc.width * 2, 0, DustID.GoldFlame, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
                for (int i3 = 0; i3 < 10; i3++)
                {

                    Dust d = Main.dust[Dust.NewDust(new Vector2(npc.Center.X - npc.width, npc.Center.Y - npc.height / 2), npc.width * 2, 0, DustID.GoldFlame, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
                for (int i3 = 0; i3 < 10; i3++)
                {

                    Dust d = Main.dust[Dust.NewDust(new Vector2(npc.Center.X - npc.width, npc.Center.Y - npc.height / 1.5f), npc.width * 2, 0, DustID.GoldFlame, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
            }
            if (NanitePlague)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 235, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (npc.HasBuff(BuffType<ApostleBuff>()))
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 235, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (Hyperburn)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.FireworkFountain_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;

                }
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Firework_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.1f);

                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {

                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (npc.HasBuff(BuffType<SpiritflameDebuff>()))
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.FireworkFountain_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;

                }
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Firework_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.1f);

                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {

                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (KarmicRetribution)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.FireworkFountain_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;

                }
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Firework_Pink, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.1f);

                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {

                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (VerdantEmbrace)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.FireworkFountain_Green, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.7f);
                    Main.dust[dust2].noGravity = true;

                }
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.FireworkFountain_Green, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.5f);

                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {

                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }


            if (AuthoritySacrificeMark)
            {
                dustTimer++;
                if (dustTimer > 10)
                {
                    float dustAmount = 6f;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(npc.velocity.ToRotation());
                        int dust = Dust.NewDust(npc.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = npc.Center + spinningpoint5;
                        Main.dust[dust].velocity = npc.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
                    }
                    dustTimer = 0;
                }


            }
            if (npc.HasBuff(BuffType<BaptisedBuff>()) && !npc.HasBuff(BuffType<ApostleBuff>()))
            {
                dustTimer++;
                if (dustTimer > 60)
                {
                    float dustAmount = 10f;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(npc.velocity.ToRotation());
                        int dust = Dust.NewDust(npc.Center, 0, 0, DustID.LifeDrain);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = npc.Center + spinningpoint5;
                        Main.dust[dust].velocity = npc.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
                    }
                    dustTimer = 0;
                }


            }
            if (MortalWounds)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 105, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                //Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (InfernalBleed)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 105, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 0.8f;
                    Main.dust[dust2].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust2].noGravity = false;
                        Main.dust[dust2].scale *= 0.2f;
                    }
                }

                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);

                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                if (Main.rand.NextBool(4))
                {

                    Main.dust[dust].scale *= 0.5f;
                }

                //Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
            if (Petrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 0, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 2f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 1.5f;
                    }
                }
            }
            if (Riptide)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 15, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.8f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }
                }
                if (Main.rand.NextBool(3))
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.bubble>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.8f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }
                }
            }
            if (OceanCulling)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.WaterShine>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.4f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 1f;
                    }
                }

                int dust1 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<Dusts.WaterShine>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 1.8f;
                Main.dust[dust1].velocity.Y -= 0.5f;
                Main.dust[dust1].scale = 0.4f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust1].noGravity = false;
                    Main.dust[dust1].scale = 1f;
                }

            }
            if (ruination)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.8f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }
                }
                if (Main.rand.Next(12) < 2)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 109, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.8f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }
                }
            }
            if (Starblight)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.3f;
                    if (Main.rand.NextBool(6))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 1f;
                    }


                }
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1.8f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].scale = 0.2f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust2].noGravity = false;
                    Main.dust[dust2].scale = 0.5f;
                }

            }
            if (Glitterglue)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 86, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.3f;
                    if (Main.rand.NextBool(6))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }


                }
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 97, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1.8f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].scale = 0.2f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust2].noGravity = false;
                    Main.dust[dust2].scale = 1f;
                }

            }
            if (voidAtrophy1 || voidAtrophy2)
            {
                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.3f;
                    if (Main.rand.NextBool(6))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }


                }
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1.8f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].scale = 0.2f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust2].noGravity = false;
                    Main.dust[dust2].scale = 1f;
                }

            }
            if (RyukenStun)
            {

                if (Main.rand.Next(12) < 5)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 205, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].scale = 0.3f;
                    if (Main.rand.NextBool(6))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 2f;
                    }


                }
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 223, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 20, default(Color), 0.8f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1.8f;
                Main.dust[dust2].velocity.Y -= 0.5f;
                Main.dust[dust2].scale = 0.2f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust2].noGravity = false;
                    Main.dust[dust2].scale = 1f;
                }

            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {

        }
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {


        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<StarsAbovePlayer>().spectralNail == 2)
            {
                spectralNailStacks++;
                if (spectralNailStacks >= 5)
                {
                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(255, 62, 247, 240), $"{(int)(damageDone * 0.2)}", false, false);
                    npc.SimpleStrikeNPC((int)(damageDone * 0.2), 0, false, 0, DamageClass.Default, false, 0);

                    spectralNailStacks = 0;
                }
            }

        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<NalhaunSword>())
            {
                modifiers.FinalDamage += 1f;
            }



        }



        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<NalhaunSword>())
            {
                modifiers.SourceDamage += 1f;
            }



        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileType<QuisUtDeusRound>())
            {
                quisUtDeusStacks++;
                if (quisUtDeusStacks > 5)
                {

                    quisUtDeusStacks = 0;

                    Vector2 position = npc.Center + new Vector2(Main.rand.Next(-200, 201), Main.rand.Next(-100, 101));
                    position.Y -= 500;
                    Vector2 heading = npc.Center - position;
                    heading.Normalize();
                    heading *= 16f;

                    Vector2 velocity = new Vector2(0, 0);
                    velocity.X = heading.X;
                    velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
                    if (projectile.owner == Main.LocalPlayer.whoAmI)
                    {
                        Projectile.NewProjectile(Main.LocalPlayer.GetSource_OnHit(npc), position.X, position.Y, velocity.X, velocity.Y, ProjectileID.StarWrath, damageDone, 0, Main.LocalPlayer.whoAmI, 0f);

                    }
                }

            }
            if (projectile.type == ProjectileType<StarphoenixRound>())
            {
                elementalSurgeStacks++;
                if (elementalSurgeStacks > 6)
                {
                    SoundEngine.PlaySound(SoundID.Item27, npc.Center);
                    elementalSurgeStacks = 0;
                    npc.SimpleStrikeNPC((int)(damageDone * 1.5), 0, false, 0, DamageClass.Default, false, 0);
                    Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks++;
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(npc.Hitbox) },
            projectile.owner);
                    if (Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks > 5)
                    {
                        Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks = 5;
                    }
                }

            }
            if (projectile.type == ProjectileType<CatalystKey>())
            {
                elementalSurgeStacks++;
                if (elementalSurgeStacks > 6)
                {
                    SoundEngine.PlaySound(SoundID.Item27, npc.Center);
                    elementalSurgeStacks = 0;
                    npc.SimpleStrikeNPC((int)(damageDone * 1.5), 0, false, 0, DamageClass.Default, false, 0);
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(npc.Hitbox) },
            projectile.owner);
                    if (Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks > 5)
                    {
                        Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks = 5;
                    }
                }

            }
            if (projectile.type == ProjectileType<StarphoenixMinionBullet>())
            {
                elementalSurgeStacks++;
                if (elementalSurgeStacks > 6)
                {
                    SoundEngine.PlaySound(SoundID.Item27, npc.Center);
                    elementalSurgeStacks = 0;
                    npc.SimpleStrikeNPC((int)(damageDone * 1.5), 0, false, 0, DamageClass.Default, false, 0);
                    Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks++;
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(npc.Hitbox) },
            projectile.owner);
                    if (Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks > 5)
                    {
                        Main.player[projectile.owner].GetModPlayer<WeaponPlayer>().alignmentStacks = 5;
                    }
                }

            }
            if (Main.player[projectile.owner].GetModPlayer<StarsAbovePlayer>().spectralNail == 2)
            {
                spectralNailStacks++;
                if (spectralNailStacks >= 5)
                {
                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(255, 62, 247, 240), $"{(int)(damageDone * 0.2)}", false, false);
                    npc.SimpleStrikeNPC((int)(damageDone * 0.2), 0, false, 0, DamageClass.Default, false, 0);

                    spectralNailStacks = 0;
                }
            }
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
        }




        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.QueenSlimeBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<YoumuHilt>(), 4));

            }
            if (npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer || npc.type == NPCID.TheDestroyer || npc.type == NPCID.SkeletronPrime)
            {
                //npcLoot.Add(ItemDropRule.Common(ItemType<MechanicalPrism>(), 4));
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<Pawn>(), 10));

            }
            if (npc.type == NPCID.Zombie)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<PrimeCut>(), 50));

            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<RedSpiderLily>(), 4));
                if (Main.expertMode)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemType<MindflayerWorm>(), 4));

                }
            }
            if (npc.type == NPCID.Harpy)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<CapedFeather>(), 50));

            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<OvergrownPrism>(), 4));
                npcLoot.Add(ItemDropRule.Common(ItemType<DekuNut>(), 4));

            }
            if (npc.type == NPCID.MartianWalker)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<ChronalDeccelerator>(), 10));

            }
            if (npc.type == NPCID.Pumpking)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<GuppyHead>(), 10));

            }
            if (npc.type == NPCID.Deerclops)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<MonsterTooth>(), 10));

            }
            if (npc.type == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<LihzahrdPrism>(), 4));
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<Rageblade>(), 4));
            }
            if (npc.type == NPCID.HallowBoss)
            {
                //npcLoot.Add(ItemDropRule.Common(ItemType<EmpressPrism>(), 4));
            }
            if (npc.type == NPCID.DukeFishron)
            {
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<ResonanceGem>(), 4));

            }
            if (npc.type == NPCID.MoonLordCore)
            {
                //npcLoot.Add(ItemDropRule.Common(ItemType<LuminitePrism>(), 4));

            }
            if (npc.type == NPCID.MoonLordCore || npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<PearlescentOrb>(), 4));

            }

            /*
            VagrantDrops VagrantDropCondition = new VagrantDrops();
            IItemDropRule conditionalRule = new LeadingConditionRule(VagrantDropCondition);

            IItemDropRule rule = ItemDropRule.Common(ItemType<PrismaticCore>(), chanceDenominator: 100);
            conditionalRule.OnSuccess(rule);
            npcLoot.Add(conditionalRule);

            IItemDropRule conditionalRule1 = new LeadingConditionRule(VagrantDropCondition);
            IItemDropRule rule1 = ItemDropRule.Common(ItemType<Starlight>(), chanceDenominator: 25);
            conditionalRule.OnSuccess(rule1);
            npcLoot.Add(conditionalRule1);
            */
            //IItemDropRule conditionalRule2 = new LeadingConditionRule(VagrantDropCondition);
            //IItemDropRule rule2 = ItemDropRule.Common(ItemType<PerfectlyGenericAccessory>(), chanceDenominator: 10000);
            //conditionalRule.OnSuccess(rule2);
            //npcLoot.Add(conditionalRule2);


        }


        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {

            
			VagrantDrops VagrantDropCondition = new VagrantDrops();
			IItemDropRule conditionalRule = new LeadingConditionRule(VagrantDropCondition);
			IItemDropRule rule = ItemDropRule.Common(ModContent.ItemType<PrismaticCore>(), chanceDenominator: 100);
			conditionalRule.OnSuccess(rule);
			globalLoot.Add(conditionalRule);

			IItemDropRule conditionalRule1 = new LeadingConditionRule(VagrantDropCondition);
			IItemDropRule rule1 = ItemDropRule.Common(ModContent.ItemType<Starlight>(), chanceDenominator: 25);
			conditionalRule.OnSuccess(rule1);
			globalLoot.Add(conditionalRule1);

			

        }



    }
}
