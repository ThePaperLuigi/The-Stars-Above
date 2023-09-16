using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.DragaliaFound
{
    public class DragonshiftActiveBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(ModContent.BuffType<DragonshiftActiveBuff>(), 10);
            player.GetModPlayer<WeaponPlayer>().DragonshiftGauge -= 0.1f;
            if(player.GetModPlayer<WeaponPlayer>().DragonshiftGauge <= 0)
            {
                player.DelBuff(buffIndex);
            }

        }
    }
}
