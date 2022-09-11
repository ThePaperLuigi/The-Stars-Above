using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.HunterSymphony
{
    public class InfernalMelody3 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Melody (3/3)");
            Description.SetDefault("Right click with the Hunter's Symphony to activate Infernal Melody");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
