using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SanguineDespair
{
    public class FeralDespair : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Feral Despair");
            // Description.SetDefault("Sanguine energy ebbs and flows in tandem with your life, granting increased damage based on missing HP");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            float missingHealth = player.statLifeMax2 - player.statLife;

            player.GetDamage(DamageClass.Generic) += (0.005f * missingHealth);
        }
    }
}
