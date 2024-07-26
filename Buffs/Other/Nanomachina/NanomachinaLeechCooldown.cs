using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.Nanomachina
{
    public class NanomachinaLeechCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nanomachina Leech Cooldown");
            // Description.SetDefault("Nanomachines will not leech HP and Mana if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
