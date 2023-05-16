using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Chronoclock
{
    public class TimeBubbleCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Time Bubble Cooldown");
            // Description.SetDefault("The Time Bubble will not shield you from damage if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
