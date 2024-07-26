using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.DragaliaFound
{
    public class BondforgedBladeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
            player.GetModPlayer<WeaponPlayer>().DragonshiftGauge += 0.05f;
            player.GetModPlayer<WeaponPlayer>().DragonshiftGauge = MathHelper.Clamp(player.GetModPlayer<WeaponPlayer>().DragonshiftGauge, 0, 100);
        }
    }
}
