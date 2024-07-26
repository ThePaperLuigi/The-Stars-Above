using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.Youmu
{
    public class YoumuCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantom Slash Cooldown");
            // Description.SetDefault("When this buff expires, you can use Phantom Slash again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetDamage(DamageClass.Generic) += 0.3f;

        }
    }
}
