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

namespace StarsAbove
{
    public class BossPlayer: ModPlayer
    {
        public bool VagrantBarActive = false;//This changes depending on the boss
        public bool NalhaunBarActive = false;
        public bool TsukiyomiBarActive = false;

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

        public int nalhaunCutsceneProgress = 0;
        public int tsukiCutsceneProgress = 0;
        public int tsukiCutscene2Progress = 0;

        public float WhiteAlpha = 0f;
        public float BlackAlpha = 0f;
        public float VideoAlpha = 0f;
        public int VideoDuration = -1;

        public bool VagrantActive = false; //This can be replaced with a npc check.
        public bool LostToVagrant = false; //This can probably be removed.

        


        public override void PreUpdate()
        {
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


            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<VagrantWalls>() && Player.Distance(npc.Center) < 2000)
                {

                    VagrantTeleport(npc);
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

            nalhaunCutsceneProgress--;
            tsukiCutsceneProgress--;
            tsukiCutscene2Progress--;
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
                            temperatureGaugeCold += 0.3f;
                        }
                        else
                        {
                            temperatureGaugeHot -= 0.3f;

                        }
                    }
                    //Closer to Castor
                    if (Player.position.X < npc.position.X)
                    {
                        if (temperatureGaugeCold <= 0)
                        {
                            temperatureGaugeHot += 0.3f;
                        }
                        else
                        {
                            temperatureGaugeCold -= 0.3f;

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
            CastTime = 0;
            CastTimeMax = 100;

            CastTimeAlt = 0;
            CastTimeMaxAlt = 100;

            NalhaunBarActive = false;
            VagrantBarActive = false;
            TsukiyomiBarActive = false;

            CastorBarActive = false;
            PolluxBarActive = false;

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
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

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
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

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
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

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
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
    }

};