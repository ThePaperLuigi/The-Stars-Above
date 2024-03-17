using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.LightUnrelenting
{
    public class LimitBreakCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Limit Break Cooldown");
            // Description.SetDefault("When this debuff ends, you will be able to use Limit Break again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
