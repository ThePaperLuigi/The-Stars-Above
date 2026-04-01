using StarsAbove.Mounts.Forklift;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Mounts
{
    public class AsphodeneForkliftMountBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<AsphodeneForklift>(), player);
            player.AddBuff(ModContent.BuffType<AsphodeneForkliftMountBuff>(), 10);
            
        }
    }
}
