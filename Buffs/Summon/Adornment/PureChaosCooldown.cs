using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.Adornment
{
    public class PureChaosCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pure Chaos Cooldown");
            // Description.SetDefault("Pure Chaos will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
