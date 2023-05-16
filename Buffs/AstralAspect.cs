using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AstralAspect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Attunement");
            // Description.SetDefault("The Astral Aspect grants 10 extra defense and imbues attacks with the ability to drain Mana from foes at the cost of half your damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10; //
            player.GetDamage(DamageClass.Generic) /= 2;
        }
    }
}
