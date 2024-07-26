using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.Ozma
{
    public class FinaleDescendsCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Finale Descends Cooldown");
            // Description.SetDefault("Finale Descends will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
