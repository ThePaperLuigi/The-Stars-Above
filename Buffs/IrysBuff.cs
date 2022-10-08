using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class IrysBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fae Nephilim");
            Description.SetDefault("Corporeal form has changed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(SubworldSystem.Current == null)
            {
                player.wingTime = 10;
            }
          
            
            
        }
    }
}
