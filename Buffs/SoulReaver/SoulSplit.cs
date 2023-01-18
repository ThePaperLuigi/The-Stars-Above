using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SoulReaver
{
    public class SoulSplit : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Split");
            Description.SetDefault("Corporeal form is recovering, reducing damage and range of Soul Harvest");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}