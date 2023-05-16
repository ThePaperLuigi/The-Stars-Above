using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CatalystMemory
{
    public class DazzlingAria : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dazzling Aria");
            // Description.SetDefault("The shattered crystal grants 50 defense and powerful life regeneration");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 30;
            player.statDefense += 50;

            
            
        }
    }
}
