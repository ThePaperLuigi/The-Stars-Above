using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StarfarerAttire
{
    public class RebelPathCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
