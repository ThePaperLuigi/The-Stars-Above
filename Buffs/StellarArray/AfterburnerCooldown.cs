using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class AfterburnerCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Afterburner Cooldown");
            // Description.SetDefault("Afterburner will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
