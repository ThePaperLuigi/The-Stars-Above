using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Buffs.Summon.Adornment
{
    public class AdornmentCritBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pure Chaos (Critical Strikes)");
            // Description.SetDefault("Chaotic energy has granted a boon");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {




        }
    }
}
