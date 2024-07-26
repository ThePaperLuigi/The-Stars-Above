using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class Lightblessed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lightblessed");
            // Description.SetDefault("The endless light grants you defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 30;

        }

    }
}
