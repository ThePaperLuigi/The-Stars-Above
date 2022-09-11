using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CosmicDestroyer
{
    public class MagitonOverheat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magiton Overheat");
            Description.SetDefault("Cosmic Destroyer is imbued with overwhelming strength");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
