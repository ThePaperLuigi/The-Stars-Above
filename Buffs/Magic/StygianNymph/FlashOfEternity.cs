using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.StygianNymph
{
    public class FlashOfEternity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flash Of Eternity");
            // Description.SetDefault("The next instance of damage is negated");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
