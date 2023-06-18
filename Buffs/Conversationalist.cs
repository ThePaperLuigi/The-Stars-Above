using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class Conversationalist : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Conversationalist");
            // Description.SetDefault("Enemy spawns have been reduced");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
                        
           
        }
    }
}
