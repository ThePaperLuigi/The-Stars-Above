using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Invincibility : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Invincibility");
            // Description.SetDefault("You can not take incoming damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            player.immune = true;
            player.immuneTime = 20;
            
        }

    }
}
