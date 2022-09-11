using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class RedMageCastDelay : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cast Delay");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
