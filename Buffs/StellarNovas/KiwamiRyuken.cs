using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class KiwamiRyuken : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kiwami Ryuken");
            // Description.SetDefault("You are preparing a deadly counterattack");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
