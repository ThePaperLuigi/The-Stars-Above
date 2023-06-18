using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CosmicDestroyer
{
    public class Overheated : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Overheated");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
