using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Mercy
{
    public class EdgeOfAnguishCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Edge of Anguish Cooldown");
            // Description.SetDefault("When this debuff ends, you can use Edge of Anguish again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
