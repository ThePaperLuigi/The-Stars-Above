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
        
       
        

    }

};