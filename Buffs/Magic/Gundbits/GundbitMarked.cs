using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Magic.Gundbits
{
    public class GundbitMarked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fleeting Spark");
            // Description.SetDefault("Static electricity arcs from your minions");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}