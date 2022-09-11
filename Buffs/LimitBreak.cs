using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class LimitBreak : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Limit Break");
            Description.SetDefault("All limits are being surpassed, directly turning health into mana and granting a drastic increase to the strength of magical attacks");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.1f;
            player.GetDamage(DamageClass.Magic) *= 2;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
