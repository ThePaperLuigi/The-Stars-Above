using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class SovereignDominion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sovereign's Dominion");
            Description.SetDefault("You matter so little!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.moveSpeed *= (float)(1.3);
            player.GetDamage(DamageClass.Generic) *= (float)1.1;
            
        }
    }
}
