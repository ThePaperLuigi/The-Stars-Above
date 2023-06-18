using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SkyStrikerBuffs
{
    public class StrikerMeleeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aerial Forme: Close Combat");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
