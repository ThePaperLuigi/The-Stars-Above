using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BrilliantSpectrum
{
    public class BrilliantSpectrumBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var modPlayer = player.GetModPlayer<WeaponPlayer>();
            player.GetAttackSpeed(DamageClass.Generic) += (modPlayer.refractionGauge / modPlayer.refractionGaugeMax);
            player.GetDamage(DamageClass.Generic) += modPlayer.refractionGauge / modPlayer.refractionGaugeMax;
        }
    }
}
