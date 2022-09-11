using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AstralAspectBow : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Attunement");
            Description.SetDefault("The Astral Aspect grants health on firing, dramatic health regeneration once charged, and empowers Umbral Aspect at the cost of half your damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
             //
            player.GetDamage(DamageClass.Generic) /= 2;
        }
    }
}
