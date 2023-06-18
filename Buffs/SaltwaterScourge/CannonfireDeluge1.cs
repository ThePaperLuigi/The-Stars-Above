using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SaltwaterScourge
{
    public class CannonfireDeluge1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cannonfire Deluge (1 Stack)");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

    }
}
