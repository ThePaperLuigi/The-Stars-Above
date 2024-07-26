using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.DreamersInkwell
{
    public class InkwellWindDelay : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Afterburner");
            // Description.SetDefault("All attacks will crit");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
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
