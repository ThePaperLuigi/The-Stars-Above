using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.HunterSymphony
{
    public class VitalitySong : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitality Song");
            Description.SetDefault("Powerful melodies grant 15% increased movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
