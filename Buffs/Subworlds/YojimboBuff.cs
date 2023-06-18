using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Subworlds
{
    public class YojimboBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bounty Hunter's Pointers");
            // Description.SetDefault("Yojimbo's casual advice has granted you some extra Luck during Cosmic Voyages");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(SubworldSystem.Current != null)
            {
                player.luck += 0.4f;
            }
            
            
            
           
        }
    }
}
