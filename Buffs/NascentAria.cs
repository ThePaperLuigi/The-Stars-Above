using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class NascentAria : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nascent Aria");
            Description.SetDefault("Resolve at world's demise is drastically improving maximum health");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 500;
        }
    }
}
