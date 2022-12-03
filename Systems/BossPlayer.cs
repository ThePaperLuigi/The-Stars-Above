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

namespace StarsAbove
{
    public class BossPlayer: ModPlayer
    {
        public bool VagrantBarActive = false;//This changes depending on the boss
        //public bool NalhaunBarActive = false; //etc.

        //These cast values are recycled for every boss. There should be a function that prevents bosses from overlapping.
        public int CastTime = 0;
        public int CastTimeMax = 100;
        public string NextAttack = "";

        public int inVagrantFightTimer; //Check to see if you've recently hit the boss.



        public bool VagrantActive = false; //This can be replaced with a npc check.
        public bool LostToVagrant = false; //This can probably be removed.
        public int vagrantTimeLeft; //This can probably be removed.
        


        public override void PreUpdate()
        {
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<VagrantBoss>())
                {

                    VagrantTeleport(npc);
                    break;
                }
            }
        }
        public override void PostUpdate()
        {

            NextAttack = "";
           
        }



        public override void ResetEffects()
        {
            CastTime = 0;
            CastTimeMax = 100;
            
            VagrantBarActive = false;
            
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

    }

};