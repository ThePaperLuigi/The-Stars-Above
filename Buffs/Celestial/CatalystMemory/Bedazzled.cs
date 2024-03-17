using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.CatalystMemory
{
    public class Bedazzled : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bedazzled");
            // Description.SetDefault("Awash in crystalline light, gaining 10% increased attack, 10 defense, and 10% movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.statDefense += 10;


        }
    }
}
