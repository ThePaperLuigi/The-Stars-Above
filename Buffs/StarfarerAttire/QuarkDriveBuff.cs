﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StarfarerAttire
{
    public class QuarkDriveBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
