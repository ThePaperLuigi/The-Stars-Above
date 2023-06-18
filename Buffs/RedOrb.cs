using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class RedOrb : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Red Orb");
            // Description.SetDefault("Preparing 'Flickering Strike'");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            

        }
    }
}
