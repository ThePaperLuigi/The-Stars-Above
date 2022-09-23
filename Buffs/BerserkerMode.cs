using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BerserkerMode : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Mode");
            Description.SetDefault("All attacks do more damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 1;
            player.electrified = true;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
