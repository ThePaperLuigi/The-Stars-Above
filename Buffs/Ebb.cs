using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Ebb : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebb");
            Description.SetDefault("The rift between worlds robs you of all mana");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {


            player.statMana = 1;
           

            player.nebulaLevelDamage = -(player.buffTime[buffIndex] / 60) + 8;


        }
    }
}
