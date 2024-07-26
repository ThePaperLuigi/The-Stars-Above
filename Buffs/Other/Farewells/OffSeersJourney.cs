using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Other.Farewells
{
    public class OffSeersJourney : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Off-seer's Journey");
            // Description.SetDefault("Memories of the departed fill you, increasing Luck, max Luck, and enemy spawns");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           
            

        }
    }
}
