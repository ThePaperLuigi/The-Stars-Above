using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Flow : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flow");
            // Description.SetDefault("The rift between worlds grants infinite mana, 40% increased damage, and 20% faster attack speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.4f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
            player.statMana+=5;

            player.nebulaLevelDamage = (player.buffTime[buffIndex] / 60);


        }
    }
}
