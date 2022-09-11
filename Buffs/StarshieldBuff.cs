using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class StarshieldBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starshield");
            Description.SetDefault("A stellar barrier protects you, granting 10% of Max HP as defense as well as 20% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += player.statLifeMax2 / 10;
            player.GetDamage(DamageClass.Generic) += 0.2f;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
