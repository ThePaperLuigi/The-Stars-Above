using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class SeabornWrath : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seaborn Wrath");
            Description.SetDefault("The ocean currents grant your minions extra damage and your weapon is strengthened");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Summon) += 1f;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
