using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class DebugInfiniteNova : ModBuff
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

            player.GetModPlayer<StarsAbovePlayer>().novaGauge += 10;
        }

        public override bool RightClick(int buffIndex)
        {

            return true;
        }
    }
}
