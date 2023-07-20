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
using StarsAbove.NPCs.Vagrant;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.NPCs.Dioskouroi;
using StarsAbove.Projectiles.Bosses;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Buffs.Boss;
using StarsAbove.Utilities;

namespace StarsAbove
{
    public class BossPlayer: ModPlayer
    {
        public bool VagrantBarActive = false;//This changes depending on the boss
        public bool NalhaunBarActive = false;
        public bool TsukiyomiBarActive = false;
        public bool WarriorOfLightBarActive = false;

        public bool CastorBarActive = false;
        public bool PolluxBarActive = false;

        public float temperatureGaugeHot;
        public float temperatureGaugeCold;

        public static bool disableBossAggro = false;
        public int hasBossAggro = 0;

        //These cast values are recycled for every boss. There should be a function that prevents bosses from overlapping.
        public int CastTime = 0;
        public int CastTimeMax = 100;
        public string NextAttack = "";

        //Duo boss Dioskouroi needs more CastTimes.
        public int CastTimeAlt = 0;
        public int CastTimeMaxAlt = 100;
        public string NextAttackAlt = "";

        public int inVagrantFightTimer; //Check to see if you've recently hit the boss.

        public int introCutsceneProgress = 0;

        public int nalhaunCutsceneProgress = 0;
        public int tsukiCutsceneProgress = 0;
        public int tsukiCutscene2Progress = 0;
        public int warriorCutsceneProgress = 0;
        public int warriorCutsceneProgress2 = 0;

        public float WhiteAlpha = 0f;
        public float BlackAlpha = 0f;
        public float VideoAlpha = 0f;
        public int VideoDuration = -1;

        public bool VagrantActive = false; //This can be replaced with a npc check.
        public bool LostToVagrant = false; //This can probably be removed.

        public static bool DisableDamageModifier;

        public bool QTEActive;
        public float QTEProgress = 0;
        public float QTEDifficulty = 3;

        float bossReductionMod;
        float decayRate = 0.8f;
        float stress;
        float damageReductionAmount;

        bool speedrunEasterEgg;
        int bossTimer;

        public override void OnHurt(Player.HurtInfo info)
        {
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<WarriorOfLightBoss>() && Player.Distance(npc.Center) < 2000)
                {
                    if(Main.expertMode)
                    {
                        Player.AddBuff(BuffType<Vulnerable>(), 60);

                    }
                    break;
                }
                

            }

            base.OnHurt(info);
        }
        public override void PreUpdate()
        {
            if(QTEActive)
            {
                if (StarsAbove.novaKey.JustPressed)
                {
                    
                    SoundEngine.PlaySound(SoundID.Item48, Player.Center);

                    for (int d = 0; d < 3; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 0, default(Color), 1.5f);
                    }
                    Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;

                    QTEProgress += 8;
                }

                //Depending on the debuff increase/decrease the severity of the QTE
                if (Player.HasBuff(BuffType<BindingLight>()))
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC npc = Main.npc[k];
                        if (npc.active && npc.type == NPCType<WarriorOfLightBossFinalPhase>())
                        {
                            for (int ir = 0; ir < 20; ir++)
                            {
                                Vector2 positionNew = Vector2.Lerp(Player.Center, new Vector2(npc.Center.X, npc.Center.Y), (float)ir / 20);

                                Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Yellow, null, 240, default(Color), 1f);
                                da.fadeIn = 0.1f;
                                da.noLight = true;
                                da.noGravity = true;

                            }
                            break;
                        }
                        

                    }
                    QTEDifficulty = 0.1f;
                }

                QTEProgress -= QTEDifficulty;
                MathHelper.Clamp(QTEProgress, 0, 100);

                if (QTEProgress >= 100)
                {
                    QTEProgress = 0;

                    //Clear all buffs
                    Player.ClearBuff(BuffType<BindingLight>());
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-6, 6), 0, default(Color), 1.5f);
                    }
                    Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    SoundEngine.PlaySound(SoundID.Shatter, Player.Center);

                }
            }

            //Aggro marker.
            if(Main.netMode != NetmodeID.SinglePlayer && !disableBossAggro)
            {
                for (int i = 0; i <= Main.maxNPCs; i++)
                {
                    if (Main.npc[i].boss && Main.npc[i].active && Main.npc[i].target == Main.myPlayer)
                    {
                        hasBossAggro = 10;
                    }

                }

            }
            
            hasBossAggro--;
            hasBossAggro = Math.Clamp(hasBossAggro, 0, 10);
            if(hasBossAggro > 0 && Player.ownedProjectileCounts[ProjectileType<BossAggroMarker>()] < 1)
            {
                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<BossAggroMarker>(), 0, 0, Player.whoAmI);

            }

            if(NPC.AnyNPCs(NPCType<VagrantBoss>())
                || NPC.AnyNPCs(NPCType<PolluxBoss>())
                || NPC.AnyNPCs(NPCType<CastorBoss>())
                || NPC.AnyNPCs(NPCType<Penthesilea>())
                || NPC.AnyNPCs(NPCType<NalhaunBoss>())
                || NPC.AnyNPCs(NPCType<NalhaunBossPhase2>())
                || NPC.AnyNPCs(NPCType<WarriorOfLightBossFinalPhase>())
                || NPC.AnyNPCs(NPCType<WarriorOfLightBoss>())
                || NPC.AnyNPCs(NPCType<TsukiyomiBoss>()))
            {
                bossTimer++;
            }
            else
            {
                if(bossTimer > 0)
                {
                    if(bossTimer < 600 && !speedrunEasterEgg)
                    {
                        speedrunEasterEgg = true;
                        
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"CombatText.EasterEgg"), 190, 100, 247); }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_SpeedrunEasterEgg, null);

                    }
                }
                bossTimer = 0;
            }
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<VagrantWalls>() && Player.Distance(npc.Center) < 2000)
                {

                    VagrantTeleport(npc);
                    break;
                }
                if (npc.active && npc.type == NPCType<WarriorWallsNPC>() && Player.Distance(npc.Center) < 2000)
                {

                    WarriorTeleport(npc);
                    break;
                }
                if (npc.active && npc.type == NPCType<VagrantWallsHorizontal>() && Player.Distance(npc.Center) < 2000)
                {

                    VagrantTeleportHorizontal(npc);
                    break;
                }
                if (npc.active && npc.type == NPCType<VagrantWallsVertical>() && Player.Distance(npc.Center) < 2000)
                {

                    VagrantTeleportVertical(npc);
                    break;
                }
                if (npc.active && npc.type == NPCType<NPCs.Nalhaun.NalhaunBoss>() && Player.Distance(npc.Center) < 2000)
                {
                    //If nearby the boss.
                    NalhaunWalls(npc);
                    break;
                }
                if (npc.active && npc.type == NPCType<NalhaunPhase2WallsNPC>() && Player.Distance(npc.Center) < 2000)
                {
                    //If nearby the boss.
                    NalhaunPhase2Walls(npc);
                    break;
                }
                if (npc.active && (npc.type == NPCType<NPCs.Tsukiyomi.TsukiyomiBoss>()))
                {
                    //If nearby the boss.
                    TsukiyomiTeleport(npc);
                    break;
                }
                if (npc.active && (npc.type == NPCType<NPCs.Dioskouroi.DioskouroiWallsNPC>()))
                {
                    //If nearby the boss.
                    DioskouroiWalls(npc);
                    break;
                }

            }

            introCutsceneProgress--;
            nalhaunCutsceneProgress--;
            tsukiCutsceneProgress--;
            tsukiCutscene2Progress--;
            warriorCutsceneProgress--;
            warriorCutsceneProgress2--;
            
            VideoDuration--;
            
            BlackAlpha = Math.Clamp(BlackAlpha, 0, 1);
            WhiteAlpha = Math.Clamp(WhiteAlpha, 0, 1);

            VideoAlpha = Math.Clamp(VideoAlpha, 0, 1);

            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && (npc.type == NPCType<NPCs.Dioskouroi.DioskouroiWallsNPC>()))
                {
                    //Closer to Pollux
                    if(Player.position.X > npc.position.X)
                    {
                        if(temperatureGaugeHot <= 0)
                        {
                            temperatureGaugeCold += 0.4f;
                        }
                        else
                        {
                            temperatureGaugeHot -= 0.4f;

                        }
                    }
                    //Closer to Castor
                    if (Player.position.X < npc.position.X)
                    {
                        if (temperatureGaugeCold <= 0)
                        {
                            temperatureGaugeHot += 0.4f;
                        }
                        else
                        {
                            temperatureGaugeCold -= 0.4f;

                        }
                    }
                    break;
                }

            }
            temperatureGaugeCold = Math.Clamp(temperatureGaugeCold,0, 100);
            temperatureGaugeHot = Math.Clamp(temperatureGaugeHot, 0, 100);

            if (!NPC.AnyNPCs(NPCType<NPCs.Dioskouroi.CastorBoss>()) && !NPC.AnyNPCs(NPCType<NPCs.Dioskouroi.PolluxBoss>()))
            {
                temperatureGaugeHot = 0;
                temperatureGaugeCold = 0;
            }
            if(temperatureGaugeHot >= 90)
            {
                Player.AddBuff(BuffID.OnFire, 10);
            }
            if(temperatureGaugeCold >= 90)
            {
                Player.AddBuff(BuffID.Frostburn, 10);
            }

            //Main.NewText(Language.GetTextValue($"H{temperatureGaugeHot} C{temperatureGaugeCold}"), 220, 100, 247);
        }
        public override void PostUpdate()
        {

            
           
        }

        public override void ResetEffects()
        {
            QTEActive = false;

            CastTime = 0;
            CastTimeMax = 100;

            CastTimeAlt = 0;
            CastTimeMaxAlt = 100;

            NalhaunBarActive = false;
            VagrantBarActive = false;
            TsukiyomiBarActive = false;
            WarriorOfLightBarActive = false;
            CastorBarActive = false;
            PolluxBarActive = false;

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            BossDamageModifier(target, ref modifiers);
        }
        private void BossDamageModifier(NPC npc, ref NPC.HitModifiers modifiers)
        {
            //(lower means weaker weapons do more avg. damage)
            if (!DisableDamageModifier)
            {
                bossReductionMod = 0;
                if (npc.type == ModContent.NPCType<DummyEnemy>())
                {
                    bossReductionMod = 500;
                }
                if (npc.type == ModContent.NPCType<VagrantBoss>())
                {
                    bossReductionMod = 700;

                }
                if (npc.type == ModContent.NPCType<CastorBoss>() || npc.type == ModContent.NPCType<PolluxBoss>())
                {
                    bossReductionMod = 1000;

                }
                if (npc.type == ModContent.NPCType<Penthesilea>())
                {
                    bossReductionMod = 1100;

                }
                if (npc.type == ModContent.NPCType<NalhaunBoss>() || npc.type == ModContent.NPCType<NalhaunBossPhase2>())
                {
                    bossReductionMod = 1400;

                }
                if (npc.type == ModContent.NPCType<WarriorOfLightBoss>() || npc.type == ModContent.NPCType<WarriorOfLightBossFinalPhase>())
                {
                    bossReductionMod = 1000;

                }
                if (npc.type == ModContent.NPCType<Arbitration>())
                {
                    bossReductionMod = 500;

                }
                if (npc.type == ModContent.NPCType<TsukiyomiBoss>())
                {
                    bossReductionMod = 2000;

                }
                if (bossReductionMod > 0)
                {

                    modifiers.FinalDamage *= (1 - damageReductionAmount);
                    

                   
                }

                
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (bossReductionMod > 0)
            {
                decayRate = 0.8f;
                //Main.NewText(Language.GetTextValue($"Damage Pre-Modifier: {hit.SourceDamage}"), 60, 170, 247);
                //Main.NewText(Language.GetTextValue($"Damage: {damageDone}"), 120, 100, 147);
                //Main.NewText(Language.GetTextValue($"Stress: {stress}"), 150, 150, 247);
                //Main.NewText(Language.GetTextValue($"Damage Reduction: {damageReductionAmount}"), 220, 100, 247);

                stress += damageDone;
                stress *= decayRate;
                damageReductionAmount = (float)Math.Tanh(stress / bossReductionMod);

            }
        }
        private void WarriorTeleport(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = WarriorWallsNPC.arenaWidth / 2;
                int halfHeight = WarriorWallsNPC.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)//Left wall
                {
                    newPosition.X = npc.Center.X - halfWidth - Player.width - 1;
                    Player.velocity = new Vector2(20, Player.velocity.Y);
                    // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("1"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;

                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)//Right Wall
                {
                    newPosition.X = npc.Center.X + halfWidth + 1;
                    Player.velocity = new Vector2(-20, Player.velocity.Y);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("2"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;

                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)//Top
                {
                    newPosition.Y = npc.Center.Y - halfHeight - Player.height - 1;
                    Player.velocity = new Vector2(Player.velocity.X, 20);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("3"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;

                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)//Bottom
                {
                    newPosition.Y = npc.Center.Y + halfHeight + 1;
                    Player.velocity = new Vector2(Player.velocity.X, -20);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("4"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {

                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    //player.Teleport(newPosition, 1, 0);
                    //NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);


                }
            }

        }
        private void VagrantTeleport(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = VagrantWalls.arenaWidth / 2;
                int halfHeight = VagrantWalls.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X + halfWidth - Player.width - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;
                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X - halfWidth + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;
                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y + halfHeight - Player.height - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;
                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y - halfHeight + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
        private void VagrantTeleportVertical(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = VagrantWallsVertical.arenaWidth / 2;
                int halfHeight = VagrantWallsVertical.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X + halfWidth - Player.width - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;
                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X - halfWidth + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;
                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y + halfHeight - Player.height - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;
                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y - halfHeight + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
        private void VagrantTeleportHorizontal(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = VagrantWallsHorizontal.arenaWidth / 2;
                int halfHeight = VagrantWallsHorizontal.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X + halfWidth - Player.width - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;
                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X - halfWidth + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;
                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y + halfHeight - Player.height - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;
                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y - halfHeight + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }

        private void NalhaunWalls(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = NPCs.Nalhaun.NalhaunBoss.arenaWidth / 2;
                int halfHeight = NPCs.Nalhaun.NalhaunBoss.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)//Left wall
                {
                    newPosition.X = npc.Center.X - halfWidth - Player.width - 1;
                    Player.velocity = new Vector2(8, Player.velocity.Y);
                    // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("1"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 8f;

                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)//Right Wall
                {
                    newPosition.X = npc.Center.X + halfWidth + 1;
                    Player.velocity = new Vector2(-8, Player.velocity.Y);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("2"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 8f;

                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)//Top
                {
                    newPosition.Y = npc.Center.Y - halfHeight - Player.height - 1;
                    Player.velocity = new Vector2(Player.velocity.X, 8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("3"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 8f;

                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)//Bottom
                {
                    newPosition.Y = npc.Center.Y + halfHeight + 1;
                    Player.velocity = new Vector2(Player.velocity.X, -8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("4"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {

                        newPosition.Y += 8f;
                    }
                }
            }
        }
        private void NalhaunPhase2Walls(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = NalhaunPhase2WallsNPC.arenaWidth / 2;
                int halfHeight = NalhaunPhase2WallsNPC.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)//Left wall
                {
                    newPosition.X = npc.Center.X - halfWidth - Player.width - 1;
                    Player.velocity = new Vector2(8, Player.velocity.Y);
                    // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("1"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 8f;

                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)//Right Wall
                {
                    newPosition.X = npc.Center.X + halfWidth + 1;
                    Player.velocity = new Vector2(-8, Player.velocity.Y);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("2"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 8f;

                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)//Top
                {
                    newPosition.Y = npc.Center.Y - halfHeight - Player.height - 1;
                    Player.velocity = new Vector2(Player.velocity.X, 8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("3"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 8f;

                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)//Bottom
                {
                    newPosition.Y = npc.Center.Y + halfHeight + 1;
                    Player.velocity = new Vector2(Player.velocity.X, -8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("4"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {

                        newPosition.Y += 8f;
                    }
                }
            }
        }
        private void DioskouroiWalls(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = DioskouroiWallsNPC.arenaWidth / 2;
                int halfHeight = DioskouroiWallsNPC.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)//Left wall
                {
                    newPosition.X = npc.Center.X - halfWidth - Player.width - 1;
                    Player.velocity = new Vector2(8, Player.velocity.Y);
                    // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("1"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 8f;

                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)//Right Wall
                {
                    newPosition.X = npc.Center.X + halfWidth + 1;
                    Player.velocity = new Vector2(-8, Player.velocity.Y);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("2"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 8f;

                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)//Top
                {
                    newPosition.Y = npc.Center.Y - halfHeight - Player.height - 1;
                    Player.velocity = new Vector2(Player.velocity.X, 8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("3"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 8f;

                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)//Bottom
                {
                    newPosition.Y = npc.Center.Y + halfHeight + 1;
                    Player.velocity = new Vector2(Player.velocity.X, -8);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("4"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {

                        newPosition.Y += 8f;
                    }
                }
            }
        }
        private void TsukiyomiTeleport(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = 900;
                int halfHeight = 1800;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X;

                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X;

                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y;

                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y;

                }
                if (newPosition != Player.position)
                {
                    Player.AddBuff(BuffType<Invincibility>(), 10);
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
    }

};