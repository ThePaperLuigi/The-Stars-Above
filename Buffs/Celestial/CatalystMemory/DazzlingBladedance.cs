using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.CatalystMemory
{
    public class DazzlingBladedance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dazzling Aria");
            // Description.SetDefault("The shattered crystal grants 30% increased attack speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.3f;


        }
    }
}
