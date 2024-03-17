using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.HunterSymphony
{
    public class InfernalMelody : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infernal Melody");
            // Description.SetDefault("Powerful melodies grant 20% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.2f;
        }
    }
}
