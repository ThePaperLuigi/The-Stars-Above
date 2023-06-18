using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class CosmicIre : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Ire");
            // Description.SetDefault("Spatial energy imbues your surroundings");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
        }

    }
}
