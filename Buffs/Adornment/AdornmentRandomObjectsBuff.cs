using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Buffs.Adornment
{
    public class AdornmentRandomObjectsBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pure Chaos (Random Objects)");
            Description.SetDefault("Chaotic energy has granted a bane");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            


        }
    }
}
