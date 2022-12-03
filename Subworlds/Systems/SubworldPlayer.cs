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
using SubworldLibrary;
using StarsAbove.Biomes;
using StarsAbove.Subworlds;

namespace StarsAbove
{
    public class SubworldPlayer : ModPlayer
    {

        public float gravityMod;


        public override void PreUpdate()
        {

        }
        public override void PostUpdate()
        {



        }

        public override void PostUpdateBuffs()
        {
            if (Player.InModBiome(ModContent.GetInstance<SeaOfStarsBiome>()))
            {
                //Make sure the player can't do what's not allowed:
                Player.AddBuff(BuffType<Superimposed>(), 2);
                Player.noBuilding = true;

                //Space gravity!
                Player.gravity -= 0.3f;
               
                //Clear the background.
                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                //Fall too far into the void, and you'll be launched back up while taking heavy DoT.
                if ((int)(Player.Center.Y / 16) > 400)
                {
                    Player.AddBuff(BuffType<SpatialBurn>(), 120);

                    Player.velocity = new Vector2(Player.velocity.X, -17);
                }
            }

            //Observatory.
            if (SubworldSystem.IsActive<Observatory>())
            {
                Player.AddBuff(BuffType<Superimposed>(), 2);
                Player.noBuilding = true;
                Main.cloudBGActive = 1f;

                if ((int)(Player.Center.Y / 16) > 415)
                {
                    Player.velocity = new Vector2(Player.velocity.X, -21);

                }

                Player.gravity -= gravityMod;
            }

            //Final boss arena.
            if (SubworldSystem.IsActive<EternalConfluence>())
            {
                Player.AddBuff(BuffType<Superimposed>(), 2);
                Player.noBuilding = true;
                Main.cloudBGActive = 1f;

                if ((int)(Player.Center.Y / 16) < 385)
                {
                    //player.AddBuff(BuffType<SpatialBurn>(), 120);

                    Player.velocity = new Vector2(Player.velocity.X, 6);

                }
            }

            base.PostUpdateBuffs();
        }

        public override void ResetEffects()
        {


        }



    }

};