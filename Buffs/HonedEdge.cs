using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class HonedEdge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honed Edge");
            Description.SetDefault("When reloading with Izanagi's Burden, the next shot will become extraordinarily powerful");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           
        }
    }
}
