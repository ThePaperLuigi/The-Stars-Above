using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class WrathfulCeruleanFlame : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wrathful Cerulean Flame");
            // Description.SetDefault("Attacks from Shadowless Cerulean pierce armor and crit");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
