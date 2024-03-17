﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.ArachnidNeedlepoint
{
    public class RemoveStationarySpiders : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Remove Stationary Spiders!");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
        public override void Update(NPC npc, ref int buffIndex)
        {



        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {

            return base.ReApply(npc, time, buffIndex);
        }
    }
}