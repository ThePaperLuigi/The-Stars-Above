using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class ArtificeSirenBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Artifice Siren Active");
            // Description.SetDefault("A hyperpowered mechanical ally is laying waste to your foes");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.statDefense += 30;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
